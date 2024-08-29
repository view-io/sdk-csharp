namespace View.Sdk.Vector
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Serialization;

    /// <summary>
    /// View embeddings generator SDK.
    /// </summary>
    public class ViewEmbeddingsSdk : IDisposable
    {
        #region Public-Members

        /// <summary>
        /// Embeddings generator.  Default is LCProxy.
        /// </summary>
        public EmbeddingsGeneratorEnum Generator { get; private set; } = EmbeddingsGeneratorEnum.LCProxy;

        /// <summary>
        /// API key.
        /// </summary>
        public string ApiKey { get; private set; } = null;

        /// <summary>
        /// Endpoint URL.  Default is http://localhost:8301/.
        /// </summary>
        public string Endpoint
        {
            get
            {
                return _Endpoint;
            }
            private set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(Endpoint));
                Uri uri = new Uri(value);
                _Endpoint = value;
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
        /// Logger.
        /// </summary>
        public Action<SeverityEnum, string> Logger { get; set; } = null;

        #endregion

        #region Private-Members

        private Serializer _Serializer = new Serializer();
        private string _Endpoint = "http://localhost:8301/";

        private int _BatchSize = 16;
        private int _MaxParallelTasks = 16;
        private int _MaxRetries = 3;
        private int _MaxFailures = 3;
        private int _TimeoutMs = 300000;

        private ViewLcproxySdk _LcProxy = null;
        private ViewOllamaSdk _Ollama = null;
        private ViewOpenAiSdk _OpenAI = null;
        private ViewVoyageAiSdk _VoyageAI = null;

        private SemaphoreSlim _Semaphore = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="generator">Embeddings generator.</param>
        /// <param name="endpoint">Endpoint URL.</param>
        /// <param name="apiKey">API key.</param>
        /// <param name="batchSize">Batch size.</param>
        /// <param name="maxParallelTasks">Maximum number of parallel tasks.</param>
        /// <param name="maxRetries">Maximum number of retries to perform on any given task.</param>
        /// <param name="maxFailures">Maximum number of failures to support before failing the operation.</param>
        /// <param name="timeoutMs">Timeout, in milliseconds.</param>
        /// <param name="logger">Logger method.</param>
        public ViewEmbeddingsSdk(
            EmbeddingsGeneratorEnum generator,
            string endpoint,
            string apiKey,
            int batchSize = 16,
            int maxParallelTasks = 16,
            int maxRetries = 3,
            int maxFailures = 3,
            int timeoutMs = 300000,
            Action<SeverityEnum, string> logger = null)
        {
            Generator = generator;
            ApiKey = apiKey;
            Endpoint = endpoint;
            BatchSize = batchSize;
            MaxParallelTasks = maxParallelTasks;
            MaxRetries = maxRetries;
            MaxFailures = maxFailures;
            TimeoutMs = timeoutMs;
            Logger = logger;

            _Semaphore = new SemaphoreSlim(_MaxParallelTasks);

            InitializeGenerator();
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
            if (Generator == EmbeddingsGeneratorEnum.LCProxy)
            {
                return await _LcProxy.ValidateConnectivity(token).ConfigureAwait(false);
            }
            else if (Generator == EmbeddingsGeneratorEnum.Ollama)
            {
                return await _Ollama.ValidateConnectivity(token).ConfigureAwait(false);
            }
            else if (Generator == EmbeddingsGeneratorEnum.OpenAI)
            {
                return await _OpenAI.ValidateConnectivity(token).ConfigureAwait(false);
            }
            else if (Generator == EmbeddingsGeneratorEnum.VoyageAI)
            {
                return await _VoyageAI.ValidateConnectivity(token).ConfigureAwait(false);
            }
            else
                throw new ArgumentException("Unknown embeddings generator '" + Generator.ToString() + "'.");
        }

        /// <summary>
        /// Process semantic cells and generate embeddings.
        /// </summary>
        /// <param name="cells">Semantic cells.</param>
        /// <param name="model">Model.</param>
        /// <param name="timeoutMs">Timeout, in milliseconds.  Default is 60,000 milliseconds (1 minute).</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Semantic cells, where chunks have embeddings.</returns>
        public async Task<List<SemanticCell>> ProcessSemanticCells(
            List<SemanticCell> cells, 
            string model,
            int timeoutMs = 300000,
            CancellationToken token = default)
        {
            List<SemanticCell> ret = new List<SemanticCell>();
            if (cells == null || cells.Count < 1) return ret;
            if (String.IsNullOrEmpty(model)) throw new ArgumentNullException(nameof(model));
            if (timeoutMs < 1) throw new ArgumentOutOfRangeException(nameof(timeoutMs));

            if (Generator == EmbeddingsGeneratorEnum.LCProxy)
            {
                return await _LcProxy.ProcessSemanticCells(cells, model, timeoutMs, token).ConfigureAwait(false);
            }
            else if (Generator == EmbeddingsGeneratorEnum.Ollama)
            {
                return await _Ollama.ProcessSemanticCells(cells, model, timeoutMs, token).ConfigureAwait(false);
            }
            else if (Generator == EmbeddingsGeneratorEnum.OpenAI)
            {
                return await _OpenAI.ProcessSemanticCells(cells, model, timeoutMs, token).ConfigureAwait(false);
            }
            else if (Generator == EmbeddingsGeneratorEnum.VoyageAI)
            {
                return await _VoyageAI.ProcessSemanticCells(cells, model, timeoutMs, token).ConfigureAwait(false);
            }
            else
                throw new ArgumentException("Unknown embeddings generator '" + Generator.ToString() + "'.");
        }

        #endregion

        #region Private-Methods

        private void InitializeGenerator()
        {
            switch (Generator)
            {
                case EmbeddingsGeneratorEnum.LCProxy:
                    _LcProxy = new ViewLcproxySdk(Endpoint, ApiKey, BatchSize, MaxParallelTasks, MaxRetries, MaxFailures, Logger);
                    break;
                case EmbeddingsGeneratorEnum.Ollama:
                    _Ollama = new ViewOllamaSdk(Endpoint, ApiKey, BatchSize, MaxParallelTasks, MaxRetries, MaxFailures, Logger);
                    break;
                case EmbeddingsGeneratorEnum.OpenAI:
                    _OpenAI = new ViewOpenAiSdk(Endpoint, ApiKey, BatchSize, MaxParallelTasks, MaxRetries, MaxFailures, Logger);
                    break;
                case EmbeddingsGeneratorEnum.VoyageAI:
                    _VoyageAI = new ViewVoyageAiSdk(Endpoint, ApiKey, BatchSize, MaxParallelTasks, MaxRetries, MaxFailures, Logger);
                    break;
                default:
                    throw new ArgumentException("Unknown embeddings generator '" + Generator.ToString() + "'.");
            }
        }

        #endregion
    }
}
