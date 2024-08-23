namespace View.Sdk.Vector
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Serialization;

    /// <summary>
    /// Embeddings generator SDK base class.
    /// </summary>
    public abstract class EmbeddingsSdkBase
    {
        #region Public-Members

        /// <summary>
        /// Embeddings generator.  Default is LCProxy.
        /// </summary>
        public EmbeddingsGeneratorEnum Generator { get; private set; } = EmbeddingsGeneratorEnum.LCProxy;

        /// <summary>
        /// Serializer.
        /// </summary>
        public Serializer Serializer
        {
            get
            {
                return _Serializer;
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(Serializer));
                _Serializer = value;
            }
        }

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
        /// Maximum number of chunks to include in an individual processing tasks.  Default is 4.
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
        /// Logger.
        /// </summary>
        public Action<SeverityEnum, string> Logger { get; set; } = null;

        #endregion

        #region Private-Members

        private Serializer _Serializer = new Serializer();
        private string _Endpoint = "http://localhost:8301/";
        private int _BatchSize = 4;
        private int _MaxParallelTasks = 16;
        private int _MaxRetries = 3;
        private int _MaxFailures = 3;

        private SemaphoreSlim _Semaphore = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="generator">Embeddings generator.</param>
        /// <param name="endpoint">Endpoint URL.</param>
        /// <param name="apiKey">API key.</param>
        /// <param name="batchSize">Maximum number of chunks to submit in an individual processing request.</param>
        /// <param name="maxParallelTasks">Maximum number of parallel tasks.</param>
        /// <param name="maxRetries">Maximum number of retries to perform on any given task.</param>
        /// <param name="maxFailures">Maximum number of failures to support before failing the operation.</param>
        /// <param name="logger">Logger method.</param>
        public EmbeddingsSdkBase(
            EmbeddingsGeneratorEnum generator,
            string endpoint,
            string apiKey,
            int batchSize = 8,
            int maxParallelTasks = 16,
            int maxRetries = 3,
            int maxFailures = 3,
            Action<SeverityEnum, string> logger = null)
        {
            Generator = generator;
            Endpoint = endpoint;
            ApiKey = apiKey;
            BatchSize = batchSize;
            MaxParallelTasks = maxParallelTasks;
            MaxRetries = maxRetries;
            MaxFailures = maxFailures;
            Logger = logger;

            _Semaphore = new SemaphoreSlim(_MaxParallelTasks);
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// Validate connectivity.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if connected.</returns>
        public abstract Task<bool> ValidateConnectivity(CancellationToken token = default);

        /// <summary>
        /// Process semantic cells and generate embeddings.
        /// </summary>
        /// <param name="cells">Semantic cells.</param>
        /// <param name="model">Model.</param>
        /// <param name="timeoutMs">Timeout, in milliseconds.  Default is 60,000 milliseconds (1 minute).</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Semantic cells, where chunks have embeddings.</returns>
        public abstract Task<List<SemanticCell>> ProcessSemanticCells(
            List<SemanticCell> cells,
            string model,
            int timeoutMs = (60 * 1000),
            CancellationToken token = default);

        /// <summary>
        /// List models.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of model names.</returns>
        public abstract Task<List<ModelInformation>> ListModels(CancellationToken token = default);

        /// <summary>
        /// Load a model.
        /// </summary>
        /// <param name="model">Model.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public abstract Task<bool> LoadModel(string model, CancellationToken token = default);

        /// <summary>
        /// Load models.
        /// </summary>
        /// <param name="models">Models.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public abstract Task<bool> LoadModels(List<string> models, CancellationToken token = default);

        /// <summary>
        /// Delete a model.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public abstract Task<bool> DeleteModel(string name, CancellationToken token = default);

        /// <summary>
        /// Build semantic chunk list from a hierarchy of semantic cells.
        /// </summary>
        /// <param name="cells">Semantic cells.</param>
        /// <returns>List of semantic chunks.</returns>
        public List<SemanticChunk> BuildSemanticChunkList(List<SemanticCell> cells)
        {
            List<SemanticChunk> chunks = new List<SemanticChunk>();
            if (cells == null || cells.Count < 1) return chunks;

            foreach (SemanticCell cell in cells)
            {
                List<SemanticChunk> child = ExtractChunks(cell);
                if (child != null && child.Count > 0) chunks.AddRange(child);
            }

            return chunks;
        }

        /// <summary>
        /// Merge embeddings data from maps into semantic chunks.
        /// </summary>
        /// <param name="chunks">Chunks.</param>
        /// <param name="maps">Maps.</param>
        public void MergeEmbeddingsMaps(List<SemanticChunk> chunks, List<EmbeddingsMap> maps)
        {
            foreach (EmbeddingsMap map in maps)
            {
                if (chunks.Any(c => c.Content.Equals(map.Content)))
                {
                    List<SemanticChunk> update = chunks.Where(c => c.Content.Equals(map.Content)).ToList();

                    foreach (SemanticChunk chunk in update)
                    {
                        chunk.Embeddings = map.Embeddings;
                    }
                }
            }
        }

        #endregion

        #region Private-Methods

        private List<SemanticChunk> ExtractChunks(SemanticCell cell)
        {
            List<SemanticChunk> chunks = new List<SemanticChunk>();
            if (cell == null) return chunks;

            if (cell.Children != null && cell.Children.Count > 0)
            {
                foreach (SemanticCell child in cell.Children)
                {
                    List<SemanticChunk> childChunks = ExtractChunks(child);
                    if (childChunks != null && childChunks.Count > 0) chunks.AddRange(childChunks);
                }
            }

            if (cell.Chunks != null && cell.Chunks.Count > 0)
                chunks.AddRange(cell.Chunks);

            return chunks;
        }

        #endregion
    }
}
