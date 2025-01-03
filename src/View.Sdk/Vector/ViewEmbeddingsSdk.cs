namespace View.Sdk.Vector
{
    using RestWrapper;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Semantic;
    using View.Sdk.Serialization;
    using View.Sdk.Vector.Langchain;
    using View.Sdk.Vector.Ollama;
    using View.Sdk.Vector.OpenAI;
    using View.Sdk.Vector.VoyageAI;

    /// <summary>
    /// View embeddings generator SDK.
    /// </summary>
    public class ViewEmbeddingsSdk : IDisposable
    {
        #region Public-Members

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public string TenantGUID { get; set; } = null;

        /// <summary>
        /// Embeddings generator.  Default is LCProxy.
        /// </summary>
        public EmbeddingsGeneratorEnum Generator { get; private set; } = EmbeddingsGeneratorEnum.LCProxy;

        /// <summary>
        /// API key.
        /// </summary>
        public string ApiKey { get; private set; } = null;

        /// <summary>
        /// Base URL.  Default is http://localhost:8000/.
        /// </summary>
        public string BaseUrl
        {
            get
            {
                return _BaseUrl;
            }
            private set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(BaseUrl));
                Uri uri = new Uri(value);
                _BaseUrl = value;
            }
        }

        /// <summary>
        /// Maximum number of chunks to include in an individual processing tasks.  Default is 16.
        /// </summary>
        public int BatchSize
        {
            get
            {
                return _BatchSize;
            }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(BatchSize));
                _BatchSize = value;
            }
        }

        /// <summary>
        /// Maximum number of parallel tasks.  Default is 16.
        /// </summary>
        public int MaxParallelTasks
        {
            get
            {
                return _MaxParallelTasks;
            }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(MaxParallelTasks));
                _MaxParallelTasks = value;
            }
        }

        /// <summary>
        /// Maximum number of retries to perform on any given task.
        /// </summary>
        public int MaxRetries
        {
            get
            {
                return _MaxRetries;
            }
            private set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(MaxRetries));
                _MaxRetries = value;
            }
        }

        /// <summary>
        /// Maximum number of failures to support before failing the operation.
        /// </summary>
        public int MaxFailures
        {
            get
            {
                return _MaxFailures;
            }
            private set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(MaxFailures));
                _MaxFailures = value;
            }
        }

        /// <summary>
        /// Timeout in milliseconds.
        /// </summary>
        public int TimeoutMs
        {
            get
            {
                return _TimeoutMs;
            }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(TimeoutMs));
                _TimeoutMs = value;
            }
        }

        /// <summary>
        /// Enable or disable logging of request bodies.
        /// </summary>
        public bool LogRequests
        {
            get
            {
                return _SdkBase.LogRequests;
            }
            set
            {
                _SdkBase.LogRequests = value;
            }
        }

        /// <summary>
        /// Enable or disable logging of response bodies.
        /// </summary>
        public bool LogResponses
        {
            get
            {
                return _SdkBase.LogResponses;
            }
            set
            {
                _SdkBase.LogResponses = value;
            }
        }

        /// <summary>
        /// Method to invoke to send log messages.
        /// </summary>
        public Action<SeverityEnum, string> Logger
        {
            get
            {
                return _SdkBase.Logger;
            }
            set
            {
                _SdkBase.Logger = value;
            }
        }

        #endregion

        #region Private-Members

        private Serializer _Serializer = new Serializer();
        private string _BaseUrl = "http://localhost:8000/";

        private int _BatchSize = 16;
        private int _MaxParallelTasks = 16;
        private int _MaxRetries = 3;
        private int _MaxFailures = 3;
        private int _TimeoutMs = 300000;

        private EmbeddingsProviderSdkBase _SdkBase = null;

        private SemaphoreSlim _Semaphore = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="tenantGuid">Tenant GUID.</param>
        /// <param name="generator">Embeddings generator.</param>
        /// <param name="baseUrl">Base URL, i.e. http://localhost:8000/.</param>
        /// <param name="apiKey">API key.</param>
        /// <param name="batchSize">Batch size.</param>
        /// <param name="maxParallelTasks">Maximum number of parallel tasks.</param>
        /// <param name="maxRetries">Maximum number of retries to perform on any given task.</param>
        /// <param name="maxFailures">Maximum number of failures to support before failing the operation.</param>
        /// <param name="timeoutMs">Timeout, in milliseconds.</param>
        /// <param name="logger">Logger method.</param>
        public ViewEmbeddingsSdk(
            string tenantGuid,
            EmbeddingsGeneratorEnum generator,
            string baseUrl,
            string apiKey,
            int batchSize = 16,
            int maxParallelTasks = 16,
            int maxRetries = 3,
            int maxFailures = 3,
            int timeoutMs = 300000,
            Action<SeverityEnum, string> logger = null)
        {
            TenantGUID = tenantGuid;
            Generator = generator;
            ApiKey = apiKey;
            BaseUrl = baseUrl;
            BatchSize = batchSize;
            MaxParallelTasks = maxParallelTasks;
            MaxRetries = maxRetries;
            MaxFailures = maxFailures;
            TimeoutMs = timeoutMs;
            Logger = logger;

            _Semaphore = new SemaphoreSlim(_MaxParallelTasks, _MaxParallelTasks);

            switch (Generator)
            {
                case EmbeddingsGeneratorEnum.LCProxy:
                    _SdkBase = new ViewLangchainSdk(TenantGUID, BaseUrl, ApiKey);
                    break;
                case EmbeddingsGeneratorEnum.Ollama:
                    _SdkBase = new ViewOllamaSdk(TenantGUID, BaseUrl, ApiKey);
                    break;
                case EmbeddingsGeneratorEnum.OpenAI:
                    _SdkBase = new ViewOpenAiSdk(TenantGUID, BaseUrl, ApiKey);
                    break;
                case EmbeddingsGeneratorEnum.VoyageAI:
                    _SdkBase = new ViewVoyageAiSdk(TenantGUID, BaseUrl, ApiKey);
                    break;
                default:
                    throw new ArgumentException("Unknown embeddings generator '" + Generator.ToString() + "'.");
            }
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            _Serializer = null;
        }

        /// <summary>
        /// Validate connectivity.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if connected.</returns>
        public async Task<bool> ValidateConnectivity(CancellationToken token = default)
        {
            return await _SdkBase.ValidateConnectivity(token).ConfigureAwait(false);
        }

        /// <summary>
        /// Generate embeddings.
        /// </summary>
        /// <param name="embedRequest">Embeddings request.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Embeddings response.</returns>
        public async Task<EmbeddingsResult> GenerateEmbeddings(EmbeddingsRequest embedRequest, CancellationToken token = default)
        {
            if (embedRequest == null) throw new ArgumentNullException(nameof(embedRequest));
            return await _SdkBase.GenerateEmbeddings(embedRequest, _TimeoutMs, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Process semantic cells and generate embeddings.
        /// </summary>
        /// <param name="cells">Semantic cells.</param>
        /// <param name="model">Model.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Semantic cells, where chunks have embeddings.</returns>
        public async Task<List<SemanticCell>> ProcessSemanticCells(
            List<SemanticCell> cells, 
            string model,
            CancellationToken token = default)
        {
            if (cells == null || cells.Count < 1) return new List<SemanticCell>();
            if (String.IsNullOrEmpty(model)) throw new ArgumentNullException(nameof(model));

            int failureCount = 0;
            object failureLock = new object();

            // Create queue of batches
            List<List<SemanticChunk>> batches = GetChunks(cells)
                .Select((chunk, index) => new { chunk, index })
                .GroupBy(x => x.index / BatchSize)
                .Select(g => g.Select(x => x.chunk).ToList())
                .ToList();

            Queue<List<SemanticChunk>> queue = new Queue<List<SemanticChunk>>(batches);
            List<Task> runningTasks = new List<Task>();
            object processingLock = new object();

            async Task ProcessNextBatch()
            {
                while (true)
                {
                    List<SemanticChunk> batch;
                    lock (processingLock)
                    {
                        if (queue.Count == 0) return;
                        batch = queue.Dequeue();
                    }

                    try
                    {
                        await _Semaphore.WaitAsync(token);
                        try
                        {
                            bool success = await ProcessBatch(model, batch, token);
                            if (!success)
                            {
                                bool shouldThrow = false;
                                lock (failureLock)
                                {
                                    failureCount++;
                                    if (failureCount >= MaxFailures)
                                    {
                                        shouldThrow = true;
                                    }
                                }
                                if (shouldThrow)
                                {
                                    throw new InvalidOperationException(
                                        $"Too many failures encountered while generating embeddings using {Generator}. " +
                                        $"Failure count: {failureCount}");
                                }
                            }
                        }
                        finally
                        {
                            _Semaphore.Release();
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }

            try
            {
                for (int i = 0; i < Math.Min(MaxParallelTasks, batches.Count); i++)
                {
                    runningTasks.Add(ProcessNextBatch());
                }

                await Task.WhenAll(runningTasks);
            }
            catch (Exception)
            {
                token.ThrowIfCancellationRequested();
                throw;
            }

            return cells;
        }

        #endregion

        #region Private-Methods

        private async Task<bool> ProcessBatch(
            string model,
            List<SemanticChunk> chunks,
            CancellationToken token = default)
        {
            int failureCount = 0;

            List<string> contents = new List<string>();
            foreach (SemanticChunk chunk in chunks)
                if (!String.IsNullOrEmpty(chunk.Content)) contents.Add(chunk.Content);

            EmbeddingsRequest request = new EmbeddingsRequest();
            request.Model = model;
            request.ApiKey = ApiKey;
            request.Contents = contents;

            EmbeddingsResult result = new EmbeddingsResult();
            result.Success = false;

            while (failureCount < MaxRetries)
            {
                try
                {
                    result = await _SdkBase.GenerateEmbeddings(request, _TimeoutMs, token).ConfigureAwait(false);
                    if (result == null)
                    {
                        Logger?.Invoke(SeverityEnum.Warn, "no response from embeddings generator " + Generator.ToString());
                        failureCount++;
                    }
                    else
                    {
                        if (result.Success) break;
                        else
                        {
                            Logger?.Invoke(SeverityEnum.Warn, "failure reported by embeddings generator " + Generator.ToString());
                            failureCount++;
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger?.Invoke(SeverityEnum.Warn, "exception while generating embeddings: " + Environment.NewLine + e.ToString());
                    failureCount++;
                }
            }

            if (result.Success)
            {
                UpdateChunkEmbeddings(chunks, result.Result);
            }

            return result.Success;
        }

        private IEnumerable<SemanticChunk> GetChunks(List<SemanticCell> cells)
        {
            if (cells == null || cells.Count < 1) yield break;

            foreach (SemanticCell cell in cells)
            {
                if (cell.Children != null && cell.Children.Count > 0)
                {
                    foreach (var chunk in GetChunks(cell.Children))
                    {
                        yield return chunk;
                    }
                }

                if (cell.Chunks != null && cell.Chunks.Count > 0)
                {
                    foreach (SemanticChunk chunk in cell.Chunks)
                    {
                        yield return chunk;
                    }
                }
            }

            yield break;
        }
        
        private void UpdateChunkEmbeddings(List<SemanticChunk> chunks, List<EmbeddingsMap> embeddingsMap)
        {
            if (chunks == null || embeddingsMap == null || chunks.Count == 0 || embeddingsMap.Count == 0)
                return;

            foreach (SemanticChunk chunk in chunks)
            {
                if (!String.IsNullOrEmpty(chunk.Content))
                {
                    EmbeddingsMap matchingEmbedding = embeddingsMap.FirstOrDefault(e => e.Content == chunk.Content);
                    if (matchingEmbedding != null)
                    {
                        chunk.Embeddings = matchingEmbedding.Embeddings;
                    }
                }
            }
        }

        #endregion
    }
}
