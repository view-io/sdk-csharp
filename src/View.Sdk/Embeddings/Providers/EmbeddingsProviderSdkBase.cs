namespace View.Sdk.Embeddings.Providers
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection.PortableExecutable;
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
        /// Enable or disable logging of request bodies.
        /// </summary>
        public bool LogRequests { get; set; } = false;

        /// <summary>
        /// Enable or disable logging of response bodies.
        /// </summary>
        public bool LogResponses { get; set; } = false;

        /// <summary>
        /// Method to invoke to send log messages.
        /// </summary>
        public Action<SeverityEnum, string> Logger { get; set; } = null;

        /// <summary>
        /// Header to prepend to log messages.
        /// </summary>
        public string Header
        {
            get
            {
                return _Header;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _Header = value;
                }
                else
                {
                    if (!value.EndsWith(" ")) value += " ";
                    _Header = value;
                }
            }
        }

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public Guid TenantGUID { get; set; } = Guid.NewGuid();

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
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(BaseUrl));
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

        #endregion

        #region Private-Members

        private string _Header = "[EmbeddingsSdk] ";
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
        public EmbeddingsProviderSdkBase(
            Guid tenantGuid,
            EmbeddingsGeneratorEnum generator,
            string endpoint,
            string apiKey)
        {
            if (!string.IsNullOrEmpty(endpoint) && !endpoint.EndsWith("/")) endpoint += "/";

            TenantGUID = tenantGuid;
            Generator = generator;
            BaseUrl = endpoint;
            ApiKey = apiKey;
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
        /// Emit a log message.
        /// </summary>
        /// <param name="sev">Severity.</param>
        /// <param name="msg">Message.</param>
        public void Log(SeverityEnum sev, string msg)
        {
            if (string.IsNullOrEmpty(msg)) return;
            Logger?.Invoke(sev, _Header + msg);
        }

        /// <summary>
        /// Validate connectivity.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if connected.</returns>
        public abstract Task<bool> ValidateConnectivity(CancellationToken token = default);

        /// <summary>
        /// Generate embeddings.
        /// </summary>
        /// <param name="embedRequest">Embeddings request.</param>
        /// <param name="timeoutMs">Timeout in milliseconds.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns></returns>
        public abstract Task<GenerateEmbeddingsResult> GenerateEmbeddings(
            GenerateEmbeddingsRequest embedRequest,
            int timeoutMs = 30000,
            CancellationToken token = default);

        /// <summary>
        /// Read response data from RestResponse, handling both chunked and non-chunked responses.
        /// </summary>
        /// <param name="resp">RestResponse object.</param>
        /// <param name="url">URL for logging purposes.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Response data as string.</returns>
        public async Task<string> ReadResponseDataAsync(RestWrapper.RestResponse resp, string url, CancellationToken token = default)
        {
            if (resp == null) return null;

            string responseData = null;

            // Handle chunked transfer encoding
            if (resp.ChunkedTransferEncoding)
            {
                Log(SeverityEnum.Debug, "reading chunked response from " + url);
                var chunks = new List<string>();
                RestWrapper.ChunkData chunk;
                while ((chunk = await resp.ReadChunkAsync(token).ConfigureAwait(false)) != null)
                {
                    if (chunk.Data != null && chunk.Data.Length > 0)
                    {
                        chunks.Add(System.Text.Encoding.UTF8.GetString(chunk.Data));
                    }
                    if (chunk.IsFinal) break;
                }
                responseData = string.Join("", chunks);
            }
            else
            {
                responseData = resp.DataAsString;
            }

            return responseData;
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}
