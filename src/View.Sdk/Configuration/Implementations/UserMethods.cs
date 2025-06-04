namespace View.Sdk.Configuration.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Configuration.Interfaces;

    /// <summary>
    /// User methods
    /// </summary>
    /// 
    public class UserMethods : IUserMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// User methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public UserMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<UserMaster> Create(UserMaster user, CancellationToken token = default)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/users";
            return await _Sdk.Create<UserMaster>(url, user, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Exists(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/users/" + guid;
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<UserMaster> Retrieve(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/users/" + guid;
            return await _Sdk.Retrieve<UserMaster>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<UserMaster>> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/users";
            return await _Sdk.RetrieveMany<UserMaster>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<UserMaster> Update(UserMaster user, CancellationToken token = default)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/users/" + user.GUID;
            return await _Sdk.Update<UserMaster>(url, user, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<EnumerationResult<UserMaster>> Enumerate(int maxKeys = 5, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v2.0/tenants/" + _Sdk.TenantGUID + "/users?max-keys=" + maxKeys + "&token=" + _Sdk.TenantGUID;
            return await _Sdk.Enumerate<UserMaster>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/users/" + guid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}
