namespace View.Sdk.Configuration.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Configuration.Interfaces;

    /// <summary>
    /// User role map methods
    /// </summary>
    public class UserRoleMapMethods : IUserRoleMapMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// User role map methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public UserRoleMapMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<UserRoleMap> Create(UserRoleMap userRoleMap, CancellationToken token = default)
        {
            if (userRoleMap == null) throw new ArgumentNullException(nameof(userRoleMap));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/userrolemaps";
            return await _Sdk.Create<UserRoleMap>(url, userRoleMap, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Exists(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/userrolemaps/" + guid;
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<UserRoleMap> Retrieve(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/userrolemaps/" + guid;
            return await _Sdk.Retrieve<UserRoleMap>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<UserRoleMap>> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/userrolemaps";
            return await _Sdk.RetrieveMany<UserRoleMap>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<UserRoleMap> Update(UserRoleMap userRoleMap, CancellationToken token = default)
        {
            if (userRoleMap == null) throw new ArgumentNullException(nameof(userRoleMap));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/userrolemaps/" + userRoleMap.GUID;
            return await _Sdk.Update<UserRoleMap>(url, userRoleMap, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/userrolemaps/" + guid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<EnumerationResult<UserRoleMap>> Enumerate(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v2.0/tenants/" + _Sdk.TenantGUID + "/userrolemaps";
            return await _Sdk.Enumerate<UserRoleMap>(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}