namespace View.Sdk.Configuration.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Configuration.Interfaces;

    /// <summary>
    /// Object lock methods
    /// </summary>
    public class ObjectLockMethods : IObjectLockMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Object lock methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public ObjectLockMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<ObjectLock> Create(ObjectLock endpoint, CancellationToken token = default)
        {
            if (endpoint == null) throw new ArgumentNullException(nameof(endpoint));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/objectlocks";
            return await _Sdk.Create<ObjectLock>(url, endpoint, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Exists(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/objectlocks/" + guid;
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<ObjectLock> Retrieve(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/objectlocks/" + guid;
            return await _Sdk.Retrieve<ObjectLock>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<ObjectLock>> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/objectlocks";
            return await _Sdk.RetrieveMany<ObjectLock>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<ObjectLock> Update(ObjectLock endpoint, CancellationToken token = default)
        {
            if (endpoint == null) throw new ArgumentNullException(nameof(endpoint));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/objectlocks/" + endpoint.GUID;
            return await _Sdk.Update<ObjectLock>(url, endpoint, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/objectlocks/" + guid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<EnumerationResult<ObjectLock>> Enumerate(int maxKeys = 5, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v2.0/tenants/" + _Sdk.TenantGUID + "/objectlocks?max-keys=" + maxKeys + "&token=" + _Sdk.TenantGUID;
            return await _Sdk.Enumerate<ObjectLock>(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}