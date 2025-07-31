namespace View.Sdk.Configuration.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using RestWrapper;
    using View.Sdk.Configuration.Interfaces;

    /// <summary>
    /// Tenant methods
    /// </summary>
    /// 
    public class TenantMethods : ITenantMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Tenant methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public TenantMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<TenantMetadata> Retrieve(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID;
            return await _Sdk.Retrieve<TenantMetadata>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<TenantMetadata> Update(TenantMetadata tenant, CancellationToken token = default)
        {
            if (tenant == null) throw new ArgumentNullException(nameof(tenant));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID;
            return await _Sdk.Update<TenantMetadata>(url, tenant, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<EnumerationResult<TenantMetadata>> Enumerate(int maxKeys = 5, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v2.0/tenants?max-keys=" + maxKeys + "&token=" + _Sdk.TenantGUID;
            return await _Sdk.Enumerate<TenantMetadata>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<TenantMetadata>> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/";
            return await _Sdk.RetrieveMany<TenantMetadata>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<TenantMetadata> Create(TenantMetadata tenant, CancellationToken token = default)
        {
            if (tenant == null) throw new ArgumentNullException(nameof(tenant));
            string url = _Sdk.Endpoint + "v1.0/tenants/";
            return await _Sdk.Create<TenantMetadata>(url, tenant, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + guid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Exists(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + guid;
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }
        #endregion

        #region Private-Methods

        #endregion
    }
}
