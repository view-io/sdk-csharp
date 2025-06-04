namespace View.Sdk.Lexi.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Lexi.Interfaces;

    /// <summary>
    /// Source document methods
    /// </summary>
    public class SourceDocumentMethods : ISourceDocumentMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Source document methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public SourceDocumentMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<List<SourceDocument>> RetrieveMany(Guid collectionGuid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/collections/" + collectionGuid + "/documents";
            return await _Sdk.RetrieveMany<SourceDocument>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<SourceDocument> Retrieve(Guid collectionGuid, Guid documentGuid, bool includeData = false, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/collections/" + collectionGuid + "/documents/" + documentGuid;
            if (includeData) url += "?incldata";
            return await _Sdk.Retrieve<SourceDocument>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<SourceDocumentStatistics> RetrieveStatistics(Guid collectionGuid, Guid documentGuid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/collections/" + collectionGuid + "/documents/" + documentGuid + "?stats";
            return await _Sdk.Retrieve<SourceDocumentStatistics>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<SourceDocument> Upload(SourceDocument document, CancellationToken token = default)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/collections/" + document.CollectionGUID + "/documents";
            return await _Sdk.Create<SourceDocument>(url, document, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid collectionGuid, Guid documentGuid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/collections/" + collectionGuid + "/documents/" + documentGuid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid collectionGuid, string key, string version, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
            if (String.IsNullOrEmpty(version)) throw new ArgumentNullException(nameof(version));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/collections/" + collectionGuid + "/documents?key=" + key + "&versionId=" + version;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}