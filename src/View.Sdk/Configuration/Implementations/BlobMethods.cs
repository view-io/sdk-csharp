namespace View.Sdk.Configuration.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Configuration.Interfaces;

    /// <summary>
    /// Blob methods
    /// </summary>
    public class BlobMethods : IBlobMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Blob methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public BlobMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<Blob> Create(Blob blob, CancellationToken token = default)
        {
            if (blob == null) throw new ArgumentNullException(nameof(blob));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/blobs";
            return await _Sdk.Create<Blob>(url, blob, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Blob> Retrieve(string blobGuid, bool inclData = false, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(blobGuid)) throw new ArgumentNullException(nameof(blobGuid));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/blobs/" + blobGuid;
            
            if (inclData)
            {
                url += "?incldata=true";
            }
            
            return await _Sdk.Retrieve<Blob>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Blob> Read(string blobGuid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(blobGuid)) throw new ArgumentNullException(nameof(blobGuid));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/blobs/public/" + blobGuid;
            return await _Sdk.Retrieve<Blob>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<Blob>> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/blobs";
            return await _Sdk.RetrieveMany<Blob>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Blob> Update(Blob blob, CancellationToken token = default)
        {
            if (blob == null) throw new ArgumentNullException(nameof(blob));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/blobs/" + blob.GUID;
            return await _Sdk.Update<Blob>(url, blob, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(string blobGuid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(blobGuid)) throw new ArgumentNullException(nameof(blobGuid));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/blobs/" + blobGuid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<EnumerationResult<Blob>> Enumerate(int maxKeys = 5, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v2.0/tenants/" + _Sdk.TenantGUID + "/blobs?max-keys=" + maxKeys + "&token=" + _Sdk.TenantGUID;
            return await _Sdk.Enumerate<Blob>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Exists(Guid guid  ,CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/blobs/" + guid;
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}