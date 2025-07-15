namespace View.Sdk.Configuration.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Configuration.Interfaces;

    /// <summary>
    /// Pool methods
    /// </summary>
    public class PoolMethods : IPoolMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Pool methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public PoolMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<StoragePool> Create(StoragePool pool, CancellationToken token = default)
        {
            if (pool == null) throw new ArgumentNullException(nameof(pool));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/pools";
            return await _Sdk.Create<StoragePool>(url, pool, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Exists(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/pools/" + guid;
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<StoragePool> Retrieve(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/pools/" + guid;
            return await _Sdk.Retrieve<StoragePool>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<StoragePool>> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/pools";
            return await _Sdk.RetrieveMany<StoragePool>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<StoragePool> Update(StoragePool pool, CancellationToken token = default)
        {
            if (pool == null) throw new ArgumentNullException(nameof(pool));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/pools/" + pool.GUID;
            return await _Sdk.Update<StoragePool>(url, pool, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/pools/" + guid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        #endregion
    }
}