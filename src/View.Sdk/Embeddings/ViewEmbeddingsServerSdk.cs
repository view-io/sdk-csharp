namespace View.Sdk.Embeddings
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Serialization;

    /// <summary>
    /// View embeddings server SDK.
    /// </summary>
    public class ViewEmbeddingsServerSdk : ViewSdkBase, IDisposable
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private string _Header = "[EmbeddingsServerSdk] ";
        private Serializer _Serializer = new Serializer();
        private bool _Disposed = false;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// View embeddings server SDK.
        /// </summary>
        /// <param name="tenantGuid">Tenant GUID.</param>
        /// <param name="endpoint">Base URL, i.e. http://localhost:8000/.  Do not include URL paths, only protocol, hostname, and port.</param>
        /// <param name="accessKey">API key.</param>
        public ViewEmbeddingsServerSdk(
            Guid tenantGuid,
            string endpoint,
            string accessKey) : base(tenantGuid, accessKey, endpoint)
        {
            Log(SeverityEnum.Debug, _Header + "initialized using endpoint " + endpoint);
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// Dispose.
        /// </summary>
        /// <param name="disposing">Disposing.</param>
        protected new void Dispose(bool disposing)
        {
            if (!_Disposed)
            {
                if (disposing)
                {
                    _Serializer = null;
                }

                base.Dispose(disposing);
                _Disposed = true;
            }
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public new void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Generate embeddings.
        /// </summary>
        /// <param name="embedRequest">Embeddings request.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Embeddings response.</returns>
        public async Task<GenerateEmbeddingsResult> GenerateEmbeddings(
            GenerateEmbeddingsRequest embedRequest,
            CancellationToken token = default)
        {
            if (embedRequest == null) throw new ArgumentNullException(nameof(embedRequest));
            if (embedRequest.EmbeddingsRule == null) throw new ArgumentNullException(nameof(EmbeddingsRule));
            if (String.IsNullOrEmpty(embedRequest.EmbeddingsRule.EmbeddingsGeneratorUrl)) throw new ArgumentNullException(nameof(EmbeddingsRule.EmbeddingsGeneratorUrl));

            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/embeddings";
            return await Post<GenerateEmbeddingsRequest, GenerateEmbeddingsResult>(url, embedRequest, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Find embeddings by SHA256 hash.
        /// </summary>
        /// <param name="request">Embeddings request.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Find embeddings result containing existing and missing embeddings.</returns>
        public async Task<FindEmbeddingsResult> FindByHash(
            FindEmbeddingsRequest request,
            CancellationToken token = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(EmbeddingsRule));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/vectorrepositories/" + request.VectorRepositoryGUID + "/find";
            return await Post<FindEmbeddingsRequest, FindEmbeddingsResult>(url, request, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}
