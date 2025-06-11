namespace View.Sdk.Storage.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Storage.Interfaces;

    /// <summary>
    /// Bucket methods implementation.
    /// </summary>
    public class BucketMethods : IBucketMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Bucket methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public BucketMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        #region Bucket-Operations

        /// <inheritdoc />
        public async Task<BucketMetadata> Create(BucketMetadata bucket, CancellationToken token = default)
        {
            if (bucket == null) throw new ArgumentNullException(nameof(bucket));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets";
            return await _Sdk.Create(url, bucket, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<BucketMetadata> RetrieveMetadata(string bucketGuid, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid + "?md";
            return await _Sdk.Retrieve<BucketMetadata>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<BucketStatistics> RetrieveStatistics(string bucketGuid, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid;
            return await _Sdk.Retrieve<BucketStatistics>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<BucketMetadata>> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets";
            return await _Sdk.RetrieveMany<BucketMetadata>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<BucketMetadata> Update(BucketMetadata bucket, CancellationToken token = default)
        {
            if (bucket == null) throw new ArgumentNullException(nameof(bucket));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucket.GUID;
            return await _Sdk.Update(url, bucket, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(string bucketGuid, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<BucketEnumerationResult> Enumerate(string bucketGuid, int maxKeys = 5, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid + "?max-keys=" + maxKeys;
            return await _Sdk.Retrieve<BucketEnumerationResult>(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Tag-Operations

        /// <inheritdoc />
        public async Task<List<BucketTag>> RetrieveTags(string bucketGuid, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid + "?tags";
            return await _Sdk.RetrieveMany<BucketTag>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<BucketTag>> CreateTag(Guid buckedGuid, List<BucketTag> tag, CancellationToken token = default)
        {
            if (tag == null) throw new ArgumentNullException(nameof(tag));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + buckedGuid + "?tags";
            return await _Sdk.Create(url, tag, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteTag(string bucketGuid, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid + "?tags";
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region ACL-Operations

        /// <inheritdoc />
        public async Task<BucketAcl> RetrieveACL(string bucketGuid, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid + "?acl";
            return await _Sdk.Retrieve<BucketAcl>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<BucketAcl> CreateACL(string bucketGuid, BucketAcl acl, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));
            if (acl == null) throw new ArgumentNullException(nameof(acl));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid + "?acl";
            return await _Sdk.Create(url, acl, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteACL(string bucketGuid, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid + "?acl";
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #endregion

        #region Private-Methods

        #endregion
    }
}