namespace View.Sdk.Configuration.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Configuration.Interfaces;

    /// <summary>
    /// Role methods
    /// </summary>
    public class RoleMethods : IRoleMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Role methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public RoleMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<Role> Create(Role role, CancellationToken token = default)
        {
            if (role == null) throw new ArgumentNullException(nameof(role));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/roles";
            return await _Sdk.Create<Role>(url, role, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Role> Retrieve(string roleGuid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(roleGuid)) throw new ArgumentNullException(nameof(roleGuid));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/roles/" + roleGuid;            
            return await _Sdk.Retrieve<Role>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<Role>> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/roles";
            return await _Sdk.RetrieveMany<Role>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Role> Update(Role role, CancellationToken token = default)
        {
            if (role == null) throw new ArgumentNullException(nameof(role));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/roles/" + role.GUID;
            return await _Sdk.Update<Role>(url, role, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(string roleGuid,CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(roleGuid)) throw new ArgumentNullException(nameof(roleGuid));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/roles/" + roleGuid;
            return await _Sdk.Delete(url ,token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Exists(string roleGuid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(roleGuid)) throw new ArgumentNullException(nameof(roleGuid));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/roles/" + roleGuid;
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<EnumerationResult<Role>> Enumerate(int maxKeys = 5, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v2.0/tenants/" + _Sdk.TenantGUID + "/roles?max-keys=" + maxKeys;
            return await _Sdk.Enumerate<Role>(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}