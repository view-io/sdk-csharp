namespace View.Sdk.Configuration.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Configuration.Interfaces;

    /// <summary>
    /// Encryptionkey methods
    /// </summary>
    public class EncryptionKeyMethods : IEncryptionKeyMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// EncryptionKey methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public EncryptionKeyMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<EncryptionKey> Create(EncryptionKey key, CancellationToken token = default)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/encryptionkeys";
            return await _Sdk.Create<EncryptionKey>(url, key, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Exists(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/encryptionkeys/" + guid;
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<EncryptionKey> Retrieve(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/encryptionkeys/" + guid;
            return await _Sdk.Retrieve<EncryptionKey>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<EncryptionKey>> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/encryptionkeys";
            return await _Sdk.RetrieveMany<EncryptionKey>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<EncryptionKey> Update(EncryptionKey key, CancellationToken token = default)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/encryptionkeys/" + key.GUID;
            return await _Sdk.Update<EncryptionKey>(url, key, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/encryptionkeys/" + guid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<EnumerationResult<EncryptionKey>> Enumerate(int maxKeys = 5, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v2.0/tenants/" + _Sdk.TenantGUID + "/encryptionkeys?max-keys=" + maxKeys + "&token=" + _Sdk.TenantGUID;
            return await _Sdk.Enumerate<EncryptionKey>(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}
