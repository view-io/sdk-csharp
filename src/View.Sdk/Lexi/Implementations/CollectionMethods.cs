namespace View.Sdk.Lexi.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Lexi.Interfaces;

    /// <summary>
    /// Collection methods
    /// </summary>
    public class CollectionMethods : ICollectionMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Collection methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public CollectionMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<List<Collection>> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/collections";
            return await _Sdk.RetrieveMany<Collection>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Collection> Retrieve(Guid collectionGuid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/collections/" + collectionGuid;
            return await _Sdk.Retrieve<Collection>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CollectionStatistics> RetrieveStatistics(Guid collectionGuid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/collections/" + collectionGuid + "?stats";
            return await _Sdk.Retrieve<CollectionStatistics>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Collection> Create(Collection collection, CancellationToken token = default)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/collections";
            return await _Sdk.Create<Collection>(url, collection, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid collectionGuid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/collections/" + collectionGuid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}
