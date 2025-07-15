namespace View.Sdk.Configuration.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Configuration.Interfaces;

    /// <summary>
    /// Permission methods
    /// </summary>
    public class PermissionMethods : IPermissionMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Permission methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public PermissionMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<Permission> Create(Permission permission, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/permissions";
            return await _Sdk.Create<Permission>(url, permission, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Exists(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/permissions/" + guid;
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Permission> Retrieve(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/permissions/" + guid;
            return await _Sdk.Retrieve<Permission>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<Permission>> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/permissions";
            return await _Sdk.RetrieveMany<Permission>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Permission> Update(Permission permission, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/permissions/" + permission.GUID;
            return await _Sdk.Update<Permission>(url, permission, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/permissions/" + guid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<EnumerationResult<Permission>> Enumerate(int maxKeys = 5, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v2.0/tenants/" + _Sdk.TenantGUID + "/permissions?max-keys=" + maxKeys;
            return await _Sdk.Enumerate<Permission>(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}