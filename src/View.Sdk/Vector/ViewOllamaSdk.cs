namespace View.Sdk.Vector
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using RestWrapper;
    using View.Sdk.Serialization;
    using View.Sdk;
    using System.Runtime.InteropServices;
    using static System.Net.Mime.MediaTypeNames;
    using System.Linq;

    /// <summary>
    /// View Ollama SDK.
    /// </summary>
    public class ViewOllamaSdk : EmbeddingsSdkBase
    {
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

        #region Public-Members

        #endregion

        #region Private-Members

        private Serializer _Serializer = new Serializer();
        private string _DefaultModel = "llama3";

        #endregion

        #region Constructors-and-Factories

        /// <inheritdoc />
        public ViewOllamaSdk(
            string endpoint = "http://localhost:7869",
            string apiKey = null,
            int batchSize = 8,
            int maxParallelTasks = 16,
            int maxRetries = 3,
            int maxFailures = 3,
            Action<SeverityEnum, string> logger = null) : base(
                EmbeddingsGeneratorEnum.Ollama,
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
            try
            {
                using (RestRequest req = new RestRequest(Endpoint, HttpMethod.Head))
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
        public override async Task<bool> LoadModels(List<string> models, CancellationToken token = default)
        {
            throw new InvalidOperationException("This API is not implemented for this embeddings generator.");
        }

        /// <inheritdoc />
        public override async Task<bool> LoadModel(string model, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(model)) return false;

            string url = Endpoint + "api/pull";

            try
            {
                using (RestRequest req = new RestRequest(url, HttpMethod.Post))
                {
                    req.ContentType = "application/json";

                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    dict.Add("model", model);

                    string json = _Serializer.SerializeJson(dict, true);

                    using (RestResponse resp = await req.SendAsync(json, token).ConfigureAwait(false))
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
        public override async Task<List<ModelInformation>> ListModels(CancellationToken token = default)
        {
            string url = Endpoint + "api/tags";

            try
            {
                using (RestRequest req = new RestRequest(url, HttpMethod.Get))
                {
                    using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                    {
                        if (resp == null)
                        {
                            Logger?.Invoke(SeverityEnum.Warn, "no response from " + url);
                            return null;
                        }
                        else
                        {
                            if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                            {
                                if (!String.IsNullOrEmpty(resp.DataAsString))
                                {
                                    OllamaModelResult result = _Serializer.DeserializeJson<OllamaModelResult>(resp.DataAsString);
                                    return ModelInformation.FromOllamaResponse(result);
                                }
                                else
                                {
                                    Logger?.Invoke(SeverityEnum.Warn, "no data from " + url);
                                    return null;
                                }
                            }
                            else
                            {
                                Logger?.Invoke(SeverityEnum.Warn, "status " + resp.StatusCode + " received from " + url + ": " + Environment.NewLine + resp.DataAsString);
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger?.Invoke(SeverityEnum.Warn, "exception while retrieving models: " + Environment.NewLine + e.ToString());
                return null;
            }
        }

        /// <inheritdoc />
        public override async Task<bool> DeleteModel(string name, CancellationToken token = default)
        {
            string url = Endpoint + "api/delete";

            try
            {
                using (RestRequest req = new RestRequest(url, HttpMethod.Delete))
                {
                    req.ContentType = "application/json";

                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    dict.Add("name", name);

                    string json = _Serializer.SerializeJson(dict, true);

                    using (RestResponse resp = await req.SendAsync(json, token).ConfigureAwait(false))
                    {
                        if (resp == null)
                        {
                            Logger?.Invoke(SeverityEnum.Warn, "no response from " + url);
                            return false;
                        }
                        else
                        {
                            if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                            {
                                return true;
                            }
                            else
                            {
                                Logger?.Invoke(SeverityEnum.Warn, "status " + resp.StatusCode + " received from " + url + ": " + Environment.NewLine + resp.DataAsString);
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger?.Invoke(SeverityEnum.Warn, "exception while deleting models: " + Environment.NewLine + e.ToString());
                return false;
            }
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
            string url = Endpoint + "api/embed";
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
                                        OllamaEmbeddingsResult ollamaResult = Serializer.DeserializeJson<OllamaEmbeddingsResult>(resp.DataAsString);
                                        result.Success = true;
                                        result.Model = model;
                                        result.Url = url;
                                        result.StatusCode = resp.StatusCode;
                                        result.Result = OllamaEmbeddingsResult.ToEmbeddingsMaps(content, ollamaResult);

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

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    }
}
