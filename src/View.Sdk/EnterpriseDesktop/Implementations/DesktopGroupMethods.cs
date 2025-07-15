namespace View.Sdk.EnterpriseDesktop.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.EnterpriseDesktop.Interfaces;

    /// <summary>
    /// Desktop group methods implementation.
    /// </summary>
    public class DesktopGroupMethods : IDesktopGroupMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Desktop group methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public DesktopGroupMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<List<DesktopGroup>> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/enterprisedesktop/groups";
            return await _Sdk.RetrieveMany<DesktopGroup>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<DesktopGroup> Retrieve(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/enterprisedesktop/groups/" + guid.ToString();
            return await _Sdk.Retrieve<DesktopGroup>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Exists(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/enterprisedesktop/groups/" + guid.ToString();
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<DesktopGroup> Create(DesktopGroup group, CancellationToken token = default)
        {
            if (group == null) throw new ArgumentNullException(nameof(group));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/enterprisedesktop/groups";
            return await _Sdk.Post<DesktopGroup>(url, group, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<DesktopGroup> Update(DesktopGroup group, CancellationToken token = default)
        {
            if (group == null) throw new ArgumentNullException(nameof(group));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/enterprisedesktop/groups/" + group.GUID;
            return await _Sdk.Update<DesktopGroup>(url, group, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/enterprisedesktop/groups/" + guid.ToString();
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}