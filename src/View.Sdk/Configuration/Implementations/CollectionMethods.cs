namespace View.Sdk.Configuration.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Configuration.Interfaces;

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
        public async Task<Collection> Create(Collection collection, CancellationToken token = default)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/collections";
            return await _Sdk.Create<Collection>(url, collection, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Collection> Retrieve(string collectionGuid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(collectionGuid)) throw new ArgumentNullException(nameof(collectionGuid));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/collections/" + collectionGuid;
            return await _Sdk.Retrieve<Collection>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<Collection>> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/collections";
            return await _Sdk.RetrieveMany<Collection>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CollectionStatistics> RetrieveStatistics(string collectionGuid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(collectionGuid)) throw new ArgumentNullException(nameof(collectionGuid));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/collections/" + collectionGuid + "?stats";
            return await _Sdk.Retrieve<CollectionStatistics>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(string collectionGuid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(collectionGuid)) throw new ArgumentNullException(nameof(collectionGuid));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/collections/" + collectionGuid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<EnumerationResult<Collection>> Enumerate(int maxKeys = 5, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v2.0/tenants/" + _Sdk.TenantGUID + "/collections?max-keys=" + maxKeys + "&token=" + _Sdk.TenantGUID;
            return await _Sdk.Enumerate<Collection>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Exists(string collectionGuid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(collectionGuid)) throw new ArgumentNullException(nameof(collectionGuid));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/collections/" + collectionGuid;
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}