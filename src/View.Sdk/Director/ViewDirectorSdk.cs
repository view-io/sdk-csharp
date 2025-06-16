namespace View.Sdk.Director
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// View Director SDK for interacting with the View Director REST API.
    /// </summary>
    public class ViewDirectorSdk : ViewSdkBase
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private string _Header = "[ViewDirectorSdk] ";

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate the View Director SDK.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        public ViewDirectorSdk(string endpoint) : base(endpoint)
        {
            Header = _Header;
        }

        /// <summary>
        /// Instantiate the View Director SDK.
        /// </summary>
        /// <param name="tenantGuid">Tenant GUID.</param>
        /// <param name="accessKey">Access key.</param>
        /// <param name="endpoint">Endpoint.</param>
        public ViewDirectorSdk(Guid tenantGuid, string accessKey, string endpoint) : base(tenantGuid, accessKey, endpoint)
        {
            Header = _Header;
        }

        #endregion

        #region Public-Methods

        #region Connection-Methods

        /// <summary>
        /// Retrieve a list of connections from the Director API.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Connection list.</returns>
        public async Task<object> ConnectionList(CancellationToken token = default)
        {
            string url = Endpoint + "/v1.0/connections";
            return await Retrieve<object>(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Embeddings-Methods

        /// <summary>
        /// Generate embeddings for the specified content using the Director API.
        /// </summary>
        /// <param name="request">Embeddings request.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Embeddings result.</returns>
        public async Task<DirectorEmbeddingsResult> GenerateEmbeddings(DirectorEmbeddingsRequest request, CancellationToken token = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            string url = Endpoint + "/v1.0/tenants/" + TenantGUID + "/embeddings/";
            return await Post<DirectorEmbeddingsRequest, DirectorEmbeddingsResult>(url, request, token).ConfigureAwait(false);

        }

        #endregion

        #endregion

        #region Private-Methods

        #endregion
    }
}
