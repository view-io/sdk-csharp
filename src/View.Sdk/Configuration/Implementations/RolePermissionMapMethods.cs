namespace View.Sdk.Configuration.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Configuration.Interfaces;

    /// <summary>
    /// Role permission map methods
    /// </summary>
    public class RolePermissionMapMethods : IRolePermissionMapMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Role permission map methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public RolePermissionMapMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<RolePermissionMap> Create(RolePermissionMap rolePermissionMap, CancellationToken token = default)
        {
            if (rolePermissionMap == null) throw new ArgumentNullException(nameof(rolePermissionMap));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/rolepermissionmaps";
            return await _Sdk.Create<RolePermissionMap>(url, rolePermissionMap, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Exists(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/rolepermissionmaps/" + guid;
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<RolePermissionMap> Retrieve(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/rolepermissionmaps/" + guid;
            return await _Sdk.Retrieve<RolePermissionMap>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<RolePermissionMap>> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/rolepermissionmaps";
            return await _Sdk.RetrieveMany<RolePermissionMap>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<RolePermissionMap> Update(RolePermissionMap rolePermissionMap, CancellationToken token = default)
        {
            if (rolePermissionMap == null) throw new ArgumentNullException(nameof(rolePermissionMap));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/rolepermissionmaps/" + rolePermissionMap.GUID;
            return await _Sdk.Update<RolePermissionMap>(url, rolePermissionMap, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/rolepermissionmaps/" + guid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<EnumerationResult<RolePermissionMap>> Enumerate(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v2.0/tenants/" + _Sdk.TenantGUID + "/rolepermissionmaps";
            return await _Sdk.Enumerate<RolePermissionMap>(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}