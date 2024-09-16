namespace View.Sdk.Vector
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net.Http;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.VisualBasic;
    using RestWrapper;
    using View.Sdk;
    using View.Sdk.Serialization;
    using View.Sdk.Semantic;

    /// <summary>
    /// VoyageAI embeddings generator.
    /// </summary>
    public class ViewVoyageAiSdk : EmbeddingsSdkBase, IDisposable
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private string _DefaultModel = "voyage-large-2-instruct";

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="endpoint">Base URL.  Default is https://api.openai.com/v1/.</param>
        /// <param name="apiKey">API key.</param>
        /// <param name="batchSize">Maximum number of chunks to submit in an individual processing request.</param>
        /// <param name="maxParallelTasks">Maximum number of parallel tasks.</param>
        /// <param name="maxRetries">Maximum number of retries to perform on any given task.</param>
        /// <param name="maxFailures">Maximum number of failures to support before failing the operation.</param>
        /// <param name="logger">Logger.</param>
        public ViewVoyageAiSdk(
            string endpoint,
            string apiKey = null,
            int batchSize = 8,
            int maxParallelTasks = 16,
            int maxRetries = 3,
            int maxFailures = 3,
            Action<SeverityEnum, string> logger = null) : base(
                EmbeddingsGeneratorEnum.VoyageAI, 
                endpoint, 
                apiKey, 
                batchSize, 
                maxParallelTasks, 
                maxRetries, 
                maxFailures, 
                logger)
        {
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public override async Task<bool> ValidateConnectivity(CancellationToken token = default)
        {
            string url = Endpoint + "healthz";

            try
            {
                using (RestRequest req = new RestRequest(Endpoint, HttpMethod.Get))
                {
                    using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                    {
                        if (resp != null && resp.StatusCode >= 200 && resp.StatusCode <= 299) return true;
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <inheritdoc />
        public override async Task<List<SemanticCell>> ProcessSemanticCells(
            List<SemanticCell> cells,
            string model,
            int timeoutMs = 300000,
            CancellationToken token = default)
        {
            if (cells == null || cells.Count < 1) return cells;
            if (string.IsNullOrEmpty(model)) model = _DefaultModel;

            List<SemanticChunk> chunks = BuildSemanticChunkList(cells);
            if (chunks != null && chunks.Count > 0)
                await ProcessSemanticChunks(model, chunks, timeoutMs, token).ConfigureAwait(false);

            return cells;
        }

        /// <inheritdoc />
        public override Task<List<ModelInformation>> ListModels(CancellationToken token = default)
        {
            throw new InvalidOperationException("This API is not implemented for this embeddings generator.");
        }

        /// <inheritdoc />
        public override Task<bool> LoadModels(List<string> models, CancellationToken token = default)
        {
            throw new InvalidOperationException("This API is not implemented for this embeddings generator.");
        }

        /// <inheritdoc />
        public override Task<bool> DeleteModel(string name, CancellationToken token = default)
        {
            throw new InvalidOperationException("This API is not implemented for this embeddings generator.");
        }

        /// <inheritdoc />
        public override Task<bool> LoadModel(string model, CancellationToken token = default)
        {
            throw new InvalidOperationException("This API is not implemented for this embeddings generator.");
        }

        #endregion

        #region Private-Methods

        private async Task ProcessSemanticChunks(
            string model,
            List<SemanticChunk> chunks,
            int timeoutMs = 300000,
            CancellationToken token = default)
        {
            var batches = chunks.Select((chunk, index) => new { chunk, index })
                                .GroupBy(x => x.index / BatchSize)
                                .Select(g => g.Select(x => x.chunk).ToList())
                                .ToList();

            using (SemaphoreSlim semaphore = new SemaphoreSlim(MaxParallelTasks, MaxParallelTasks))
            {
                var tasks = batches.Select(async batch =>
                {
                    await semaphore.WaitAsync();
                    try
                    {
                        await Task.Run(() => ProcessBatch(model, batch, timeoutMs, token), token);
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                });

                await Task.WhenAll(tasks);
            }
        }

        private async Task ProcessBatch(
            string model,
            List<SemanticChunk> chunks,
            int timeoutMs = 300000,
            CancellationToken token = default)
        {
            string url = Endpoint + "embeddings";
            int failureCount = 0;

            List<string> content = new List<string>();
            foreach (SemanticChunk chunk in chunks)
                if (!String.IsNullOrEmpty(chunk.Content)) content.Add(chunk.Content);

            EmbeddingsResult result = new EmbeddingsResult();
            result.Success = false;

            while (failureCount < MaxRetries)
            {
                try
                {
                    using (RestRequest req = new RestRequest(url, HttpMethod.Post))
                    {
                        req.ContentType = "application/json";
                        req.TimeoutMilliseconds = timeoutMs;
                        req.Authorization.BearerToken = ApiKey;

                        Dictionary<string, object> dict = new Dictionary<string, object>();
                        dict.Add("model", model);
                        dict.Add("input", content);

                        string json = Serializer.SerializeJson(dict, true);

                        using (RestResponse resp = await req.SendAsync(json, token).ConfigureAwait(false))
                        {
                            if (resp == null)
                            {
                                Logger?.Invoke(SeverityEnum.Warn, "no response from " + url);
                                Interlocked.Increment(ref failureCount);
                            }
                            else
                            {
                                if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                                {
                                    if (!String.IsNullOrEmpty(resp.DataAsString))
                                    {
                                        VoyageAiEmbeddingsResult openAiResult = Serializer.DeserializeJson<VoyageAiEmbeddingsResult>(resp.DataAsString);
                                        result.Success = true;
                                        result.Model = model;
                                        result.Url = url;
                                        result.StatusCode = resp.StatusCode;
                                        result.Result = VoyageAiEmbeddingsResult.ToEmbeddingsMaps(content, openAiResult);

                                        MergeEmbeddingsMaps(chunks, result.Result);

                                        break;
                                    }
                                    else
                                    {
                                        Logger?.Invoke(SeverityEnum.Warn, "no data received from " + url);
                                        result = new EmbeddingsResult
                                        {
                                            Success = false,
                                            Model = model,
                                            Url = url,
                                            StatusCode = resp.StatusCode
                                        };
                                        break;
                                    }
                                }
                                else
                                {
                                    Logger?.Invoke(SeverityEnum.Warn, "status " + resp.StatusCode + " received from " + url + ": " + Environment.NewLine + resp.DataAsString);
                                    Interlocked.Increment(ref failureCount);
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger?.Invoke(SeverityEnum.Warn, "exception while generating embeddings: " + Environment.NewLine + e.ToString());
                    Interlocked.Increment(ref failureCount);
                }
            }

            if (result.Success)
            {
                foreach (EmbeddingsMap map in result.Result)
                {
                    foreach (SemanticChunk chunk in chunks)
                    {
                        if (map.Content.Equals(chunk.Content)) chunk.Embeddings = map.Embeddings;
                    }
                }
            }
        }

        #endregion
    }
}
