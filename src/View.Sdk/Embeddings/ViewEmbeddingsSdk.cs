namespace View.Sdk.Embeddings
{
    using RestWrapper;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Embeddings.Providers;
    using View.Sdk.Embeddings.Providers.Langchain;
    using View.Sdk.Embeddings.Providers.Ollama;
    using View.Sdk.Embeddings.Providers.OpenAI;
    using View.Sdk.Embeddings.Providers.VoyageAI;
    using View.Sdk.Semantic;
    using View.Sdk.Serialization;

    /// <summary>
    /// View embeddings generator SDK.
    /// </summary>
    public class ViewEmbeddingsSdk : IDisposable
    {
        #region Public-Members

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public Guid TenantGUID { get; set; } = Guid.NewGuid();

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
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(BaseUrl));
                Uri uri = new Uri(value);
                _BaseUrl = value;
            }
        }

        /// <summary>
        /// Maximum number of chunks to include in an individual processing batch.  Default is 16.
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
        /// Maximum number of batches to process in parallel.  Default is 16.
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
        /// Maximum number of retries to perform on any given batch.
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
        /// Maximum number of failed batches before failing the operation.
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
        /// Timeout in milliseconds for each batch.
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

        private string _Header = "[EmbeddingsSdk] ";
        private Serializer _Serializer = new Serializer();
        private string _BaseUrl = "http://localhost:8000/";

        private int _BatchSize = 16;
        private int _MaxParallelTasks = 16;
        private int _MaxRetries = 5;
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
        /// <param name="generator">Embeddings generator type.</param>
        /// <param name="baseUrl">Base URL, i.e. http://localhost:8000/.  Do not include URL paths, only protocol, hostname, and port.</param>
        /// <param name="apiKey">API key.</param>
        /// <param name="batchSize">Batch size, that is, the maximum number of distinct content entries to process at once..</param>
        /// <param name="maxParallelTasks">Maximum number of parallel tasks, that is, the maximum number of parallel batches to process concurrently.</param>
        /// <param name="maxRetries">Maximum number of retries to perform on any given batch before failing.</param>
        /// <param name="maxFailures">Maximum number of failures to support before failing an entire operation.</param>
        /// <param name="timeoutMs">Timeout for each operation, in milliseconds.</param>
        public ViewEmbeddingsSdk(
            Guid tenantGuid,
            EmbeddingsGeneratorEnum generator,
            string baseUrl,
            string apiKey,
            int batchSize = 16,
            int maxParallelTasks = 16,
            int maxRetries = 3,
            int maxFailures = 3,
            int timeoutMs = 300000)
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

            _Semaphore = new SemaphoreSlim(_MaxParallelTasks, _MaxParallelTasks);

            switch (Generator)
            {
                case EmbeddingsGeneratorEnum.LCProxy:
                    _SdkBase = new ViewLangchainSdk(TenantGUID, BaseUrl, ApiKey);
                    _Header = "[EmbeddingsSdk-LCProxy] ";
                    break;
                case EmbeddingsGeneratorEnum.Ollama:
                    _SdkBase = new ViewOllamaSdk(TenantGUID, BaseUrl, ApiKey);
                    _Header = "[EmbeddingsSdk-Ollama] ";
                    break;
                case EmbeddingsGeneratorEnum.OpenAI:
                    _SdkBase = new ViewOpenAiSdk(TenantGUID, BaseUrl, ApiKey);
                    _Header = "[EmbeddingsSdk-OpenAI] ";
                    break;
                case EmbeddingsGeneratorEnum.VoyageAI:
                    _SdkBase = new ViewVoyageAiSdk(TenantGUID, BaseUrl, ApiKey);
                    _Header = "[EmbeddingsSdk-VoyageAI] ";
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
            _SdkBase.Dispose();
            _Semaphore = null;
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

            #region Populate-Contents

            if (embedRequest.SemanticCells.Count > 0)
            {
                List<SemanticChunk> chunks = SemanticCell.AllChunks(embedRequest.SemanticCells).ToList();
                Log(SeverityEnum.Debug, "merging contents from " + embedRequest.SemanticCells.Count + " semantic cells and " + chunks.Count + " semantic chunks");

                foreach (SemanticChunk chunk in chunks)
                {
                    if (String.IsNullOrEmpty(chunk.Content)) continue;
                    if (!embedRequest.Contents.Any(c => !String.IsNullOrEmpty(c) && c.Equals(chunk.Content)))
                    {
                        embedRequest.Contents.Add(chunk.Content);
                    }
                }
            }

            embedRequest.Contents.RemoveAll(i => String.IsNullOrWhiteSpace(i));

            if (embedRequest.Contents.Count < 1)
            {
                return new EmbeddingsResult
                {
                    Success = false,
                    Error = new ApiErrorResponse(ApiErrorEnum.RequiredPropertiesMissing, null, "No contents were found for embeddings processing."),
                    StatusCode = 0
                };
            }
            else
            {
                Log(SeverityEnum.Debug, "processing " + embedRequest.Contents.Count + " discrete content entries");
            }

            #endregion

            #region Build-Batches

            var batches = embedRequest.Contents
                .Select((item, index) => new { Item = item, Index = index })
                .GroupBy(x => x.Index / _BatchSize)
                .Select(g => g.Select(x => x.Item).ToList())
                .ToList();

            if (batches == null || batches.Count < 1)
            {
                Log(SeverityEnum.Debug, "no batches found in supplied input");
                return new EmbeddingsResult
                {
                    Success = true,
                    Error = null,
                    StatusCode = 200,
                    BatchCount = 0
                };
            }
            else
            {
                Log(SeverityEnum.Debug, "split " + embedRequest.Contents.Count + " content entries into " + batches.Count + " discrete batches (maximum batch size " + _BatchSize + ")");
            }

            #endregion

            #region Build-Result

            object batchLock = new object();

            EmbeddingsResult embedResult = new EmbeddingsResult
            {
                Success = true,
                Error = null,
                StatusCode = 200,
                BatchCount = batches.Count,
                SemanticCells = embedRequest.SemanticCells,
                ContentEmbeddings = new List<ContentEmbedding>()
            };

            foreach (string content in embedRequest.Contents)
            {
                embedResult.ContentEmbeddings.Add(new ContentEmbedding
                {
                    Content = content,
                    Embeddings = new List<float>()
                });
            }

            #endregion

            #region Process-Batches

            int totalFailures = 0;

            Log(SeverityEnum.Debug, "processing " + batches.Count + " discrete batches with maximum parallel task count of " + _MaxParallelTasks);

            await Parallel.ForEachAsync(
                batches,
                new ParallelOptions { MaxDegreeOfParallelism = _MaxParallelTasks },
                async (batch, token) =>
                {
                    bool success = await ProcessBatch(embedRequest, embedResult, batch, batchLock);
                    if (!success)
                    {
                        totalFailures += 1;
                        if (totalFailures >= _MaxFailures)
                        {
                            embedResult.Success = false;
                        }
                    }
                });

            #endregion

            return embedResult;
        }

        #endregion

        #region Private-Methods

        private void Log(SeverityEnum sev, string msg)
        {
            if (Logger != null && !String.IsNullOrEmpty(msg))
            {
                Logger(sev, _Header + msg);
            }
        }

        private async Task<bool> ProcessBatch(
            EmbeddingsRequest req,
            EmbeddingsResult result,
            List<string> batch,
            object resultLock,
            CancellationToken token = default)
        {
            #region Variables

            int failureCount = 0;

            EmbeddingsRequest batchRequest = new EmbeddingsRequest();
            batchRequest.EmbeddingsRule = req.EmbeddingsRule;
            batchRequest.Model = req.Model;
            batchRequest.ApiKey = ApiKey;
            batchRequest.Contents = batch;

            EmbeddingsResult batchResult = new EmbeddingsResult();
            batchResult.Success = false;

            #endregion

            #region Process-Batch

            while (failureCount < MaxRetries)
            {
                try
                {
                    batchResult = await _SdkBase.GenerateEmbeddings(batchRequest, _TimeoutMs, token).ConfigureAwait(false);
                    if (batchResult == null)
                    {
                        Log(SeverityEnum.Warn, "no response from embeddings generator " + Generator.ToString());
                        failureCount++;
                    }
                    else
                    {
                        if (batchResult.Success) break;
                        else
                        {
                            Log(SeverityEnum.Warn, "failure reported by embeddings generator " + Generator.ToString());
                            failureCount++;
                        }
                    }
                }
                catch (Exception e)
                {
                    Log(SeverityEnum.Warn, "exception while generating embeddings: " + Environment.NewLine + e.ToString());
                    failureCount++;
                }
            }

            #endregion

            #region Merge

            if (batchResult != null)
            {
                if (!batchResult.Success)
                {
                    Log(SeverityEnum.Warn, _Header + "batch failed");
                }
                else if (batchResult.ContentEmbeddings != null && batchResult.ContentEmbeddings.Count > 0)
                {
                    lock (resultLock)
                    {
                        foreach (ContentEmbedding ce in result.ContentEmbeddings)
                        {
                            if (String.IsNullOrEmpty(ce.Content)) continue;
                            if (batchResult.ContentEmbeddings.Any(b => !String.IsNullOrEmpty(b.Content) && b.Content.Equals(ce.Content)))
                            {
                                ce.Embeddings = batchResult.ContentEmbeddings.First(b => b.Content.Equals(ce.Content)).Embeddings;
                            }
                        }

                        foreach (SemanticChunk chunk in SemanticCell.AllChunks(result.SemanticCells))
                        {
                            if (String.IsNullOrEmpty(chunk.Content)) continue;
                            if (batchResult.ContentEmbeddings.Any(b => !String.IsNullOrEmpty(b.Content) && b.Content.Equals(chunk.Content)))
                            {
                                chunk.Embeddings = batchResult.ContentEmbeddings.First(b => b.Content.Equals(chunk.Content)).Embeddings;
                            }
                        }
                    }
                }
            }
            else
            {
                Log(SeverityEnum.Warn, _Header + "no batch result retrieved");
            }

            #endregion

            return batchResult.Success;
        }

        #endregion
    }
}
