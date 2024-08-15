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
    public class ViewEmbeddingsSdk
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

        #endregion

        #region Private-Members

        private Serializer _Serializer = new Serializer();
        private string _Endpoint = "http://localhost:8301/";
        private int _MaxParallelTasks = 16;

        private ViewLcproxySdk _LcProxy = null;
        private ViewOpenAiSdk _OpenAI = null;

        private SemaphoreSlim _Semaphore = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="generator">Embeddings generator.</param>
        /// <param name="endpoint">Endpoint URL.</param>
        /// <param name="apiKey">API key.</param>
        /// <param name="maxParallelTasks">Maximum number of parallel tasks.</param>
        public ViewEmbeddingsSdk(
            EmbeddingsGeneratorEnum generator,
            string endpoint,
            string apiKey,
            int maxParallelTasks = 16)
        {
            Generator = generator;
            ApiKey = apiKey;
            Endpoint = endpoint;
            MaxParallelTasks = maxParallelTasks;

            _Semaphore = new SemaphoreSlim(_MaxParallelTasks);

            InitializeGenerator();
        }

        #endregion

        #region Public-Methods

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
            else if (Generator == EmbeddingsGeneratorEnum.OpenAI)
            {
                return await _OpenAI.ValidateConnectivity(token).ConfigureAwait(false);
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
        public async Task<List<SemanticCell>> Process(
            List<SemanticCell> cells, 
            string model,
            int timeoutMs = (60 * 1000),
            CancellationToken token = default)
        {
            List<SemanticCell> ret = new List<SemanticCell>();
            if (cells == null || cells.Count < 1) return ret;
            if (String.IsNullOrEmpty(model)) throw new ArgumentNullException(nameof(model));
            if (timeoutMs < 1) throw new ArgumentOutOfRangeException(nameof(timeoutMs));

            await ProcessSemanticCellsAsync(cells, model, timeoutMs, token).ConfigureAwait(false);
            return cells;
        }

        #endregion

        #region Private-Methods

        private void InitializeGenerator()
        {
            switch (Generator)
            {
                case EmbeddingsGeneratorEnum.LCProxy:
                    _LcProxy = new ViewLcproxySdk(Endpoint, ApiKey);
                    break;
                case EmbeddingsGeneratorEnum.OpenAI:
                    _OpenAI = new ViewOpenAiSdk(Endpoint, ApiKey);
                    break;
                default:
                    throw new ArgumentException("Unknown embeddings generator '" + Generator.ToString() + "'.");
            }
        }

        private async Task ProcessSemanticCellsAsync(
            List<SemanticCell> cells,
            string model,
            int timeoutMs,
            CancellationToken token = default)
        {
            Queue<SemanticCell> queue = new Queue<SemanticCell>(cells);
            List<Task> tasks = new List<Task>();

            while (queue.Count > 0 || tasks.Count > 0)
            {
                while (queue.Count > 0 && tasks.Count < _MaxParallelTasks)
                {
                    SemanticCell cell = queue.Dequeue();
                    tasks.Add(ProcessSemanticCell(cell, model, timeoutMs, queue, token));
                }

                if (tasks.Count > 0)
                {
                    Task completed = await Task.WhenAny(tasks);
                    tasks.Remove(completed);
                    await completed;
                }
            }
        }

        private async Task ProcessSemanticCell(
            SemanticCell cell, 
            string model,
            int timeoutMs,
            Queue<SemanticCell> queue,
            CancellationToken token)
        {
            await _Semaphore.WaitAsync(token).ConfigureAwait(false);

            try
            {
                if (cell.Chunks != null)
                {
                    foreach (SemanticChunk chunk in cell.Chunks)
                    {
                        EmbeddingsResult result = await Generate(
                            chunk.Content,
                            model,
                            timeoutMs,
                            token).ConfigureAwait(false);

                        if (result.Success)
                        {
                            chunk.Embeddings = result.Embeddings;
                        }
                        else
                        {
                            throw new ExternalException("Embeddings generation failed for chunk with SHA-256 " + chunk.SHA256Hash);
                        }
                    }
                }
            }
            finally
            {
                _Semaphore.Release();
            }

            // Enqueue children immediately after processing, outside the semaphore
            if (cell.Children != null && cell.Children.Count > 0)
            {
                foreach (SemanticCell child in cell.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        private async Task<EmbeddingsResult> Generate(
            string content, 
            string model,
            int timeoutMs,
            CancellationToken token = default)
        {
            switch (Generator)
            {
                case EmbeddingsGeneratorEnum.LCProxy:
                    return await _LcProxy.Generate(model, content, timeoutMs, token).ConfigureAwait(false);
                case EmbeddingsGeneratorEnum.OpenAI:
                    return await _OpenAI.Generate(model, content, timeoutMs, token).ConfigureAwait(false);
                default:
                    throw new ArgumentException("Unknown embeddings generator '" + Generator.ToString() + "'.");
            }
        }

        #endregion
    }
}
