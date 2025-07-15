namespace View.Sdk.EnterpriseDesktop.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.EnterpriseDesktop.Interfaces;

    /// <summary>
    /// Desktop user methods implementation.
    /// </summary>
    public class DesktopUserMethods : IDesktopUserMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Desktop user methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public DesktopUserMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<List<DesktopUser>> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/enterprisedesktop/users";
            return await _Sdk.RetrieveMany<DesktopUser>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<DesktopUser> Retrieve(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/enterprisedesktop/users/" + guid.ToString();
            return await _Sdk.Retrieve<DesktopUser>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Exists(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/enterprisedesktop/users/" + guid.ToString();
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<DesktopUser> Create(DesktopUser user, CancellationToken token = default)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/enterprisedesktop/users";
            return await _Sdk.Post<DesktopUser>(url, user, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<DesktopUser> Update(DesktopUser user, CancellationToken token = default)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/enterprisedesktop/users/" + user.GUID;
            return await _Sdk.Update<DesktopUser>(url, user, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/enterprisedesktop/users/" + guid.ToString();
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}