namespace View.Sdk.Vector.Implementations
{
    using RestWrapper;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk;
    using View.Sdk.Embeddings;
    using View.Sdk.Vector.Interfaces;

    /// <summary>
    /// Document methods
    /// </summary>
    public class DocumentMethods : IDocumentMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Document methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public DocumentMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<bool> Exists(
            Guid repoGuid,
            Guid docGuid,
            CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/vectorrepositories/" + repoGuid + "/documents/" + docGuid;
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<EmbeddingsDocument> Write(
            EmbeddingsDocument document,
            CancellationToken token = default)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/vectorrepositories/" + document.VectorRepositoryGUID + "/documents";
            return await _Sdk.Post<EmbeddingsDocument>(url, document, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(
            Guid repoGuid,
            Guid docGuid,
            CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/vectorrepositories/" + repoGuid + "/documents/" + docGuid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteByFilter(
            Guid repoGuid,
            VectorDeleteRequest deleteRequest,
            CancellationToken token = default)
        {
            if (deleteRequest == null) throw new ArgumentNullException(nameof(deleteRequest));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/vectorrepositories/" + repoGuid + "/documents";
            return await _Sdk.Delete<VectorDeleteRequest>(url, deleteRequest, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<EmbeddingsDocument> Retrieve(
            Guid repoGuid,
            Guid docGuid,
            CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/vectorrepositories/" + repoGuid + "/documents/" + docGuid;
            return await _Sdk.Retrieve<EmbeddingsDocument>(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}