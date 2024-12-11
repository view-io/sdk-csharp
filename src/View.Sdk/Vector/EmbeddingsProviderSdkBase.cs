namespace View.Sdk.Vector
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Semantic;
    using View.Sdk.Serialization;

    /// <summary>
    /// View embeddings generator SDK base class.
    /// </summary>
    public abstract class EmbeddingsProviderSdkBase : IDisposable
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
        private string _BaseUrl = "http://localhost:8000/";
        private int _TimeoutMs = 300000;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="tenantGuid">Tenant GUID.</param>
        /// <param name="generator">Embeddings generator.</param>
        /// <param name="endpoint">Endpoint URL.</param>
        /// <param name="apiKey">API key.</param>
        /// <param name="logger">Logger method.</param>
        public EmbeddingsProviderSdkBase(
            string tenantGuid,
            EmbeddingsGeneratorEnum generator,
            string endpoint,
            string apiKey,
            Action<SeverityEnum, string> logger = null)
        {
            if (String.IsNullOrEmpty(tenantGuid)) throw new ArgumentNullException(nameof(tenantGuid));

            if (!String.IsNullOrEmpty(endpoint) && !endpoint.EndsWith("/")) endpoint += "/";

            TenantGUID = tenantGuid;
            Generator = generator;
            BaseUrl = endpoint;
            ApiKey = apiKey;
            Logger = logger;
        }

        #endregion

        #region Public-Methods

        #region General

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
        public abstract Task<bool> ValidateConnectivity(CancellationToken token = default);

        #endregion

        #region Model-Management

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

        #endregion

        #region Embeddings

        /// <summary>
        /// Generate embeddings.
        /// </summary>
        /// <param name="embedRequest">Embeddings request.</param>
        /// <param name="timeoutMs">Timeout in milliseconds.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns></returns>
        public abstract Task<EmbeddingsResult> GenerateEmbeddings(
            EmbeddingsRequest embedRequest, 
            int timeoutMs = 30000, 
            CancellationToken token = default); 

        #endregion

        #endregion

        #region Private-Methods
         
        private IEnumerable<SemanticChunk> IterateSemanticChunks(List<SemanticCell> cells)
        {
            List<SemanticChunk> chunks = new List<SemanticChunk>();
            if (cells == null || cells.Count < 1) yield break;

            foreach (SemanticCell cell in cells)
            {
                if (cell.Children != null) IterateSemanticChunks(cell.Children);
                if (cell.Chunks != null)
                {
                    foreach (SemanticChunk chunk in cell.Chunks)
                        yield return chunk;
                }
            }

            yield break;
        }

        #endregion
    }
}
