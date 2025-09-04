namespace View.Sdk.Storage.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Storage;

    /// <summary>
    /// Interface for bucket methods.
    /// </summary>
    public interface IBucketMethods
    {
        #region Bucket-Operations
        /// <summary>
        /// Create a bucket.
        /// </summary>
        /// <param name="bucket">Bucket metadata.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Bucket metadata.</returns>
        public Task<BucketMetadata> Create(BucketMetadata bucket, CancellationToken token = default);

        /// <summary>
        /// Retrieve bucket metadata.
        /// </summary>
        /// <param name="bucketGuid">Bucket GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Bucket metadata.</returns>
        public Task<BucketMetadata> RetrieveMetadata(string bucketGuid, CancellationToken token = default);

        /// <summary>
        /// Retrieve bucket statistics.
        /// </summary>
        /// <param name="bucketGuid">Bucket GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Bucket statistics.</returns>
        public Task<BucketStatistics> RetrieveStatistics(string bucketGuid, CancellationToken token = default);

        /// <summary>
        /// Object list.
        /// </summary>
        /// <param name="bucketGuid">Bucket GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of bucket metadata.</returns>
        public Task<List<BucketMetadata>> ListObjects(string bucketGuid, CancellationToken token = default);

        /// <summary>
        /// Update bucket.
        /// </summary>
        /// <param name="bucket">Bucket metadata.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Bucket metadata.</returns>
        public Task<BucketMetadata> Update(BucketMetadata bucket, CancellationToken token = default);

        /// <summary>
        /// Delete bucket.
        /// </summary>
        /// <param name="bucketGuid">Bucket GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(string bucketGuid, CancellationToken token = default);

        /// <summary>
        /// Enumerate bucket contents with pagination support.
        /// </summary>
        /// <param name="bucketGuid">Bucket GUID.</param>
        /// <param name="maxKeys">Maximum number of keys to return in the response. Default is 5.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>BucketEnumerationResult containing objects in the bucket.</returns>
        public Task<BucketEnumerationResult> Enumerate(string bucketGuid, int maxKeys = 5, CancellationToken token = default);
        
        #endregion
        
        #region Tag-Operations

        /// <summary>
        /// Retrieve bucket tags.
        /// </summary>
        /// <param name="bucketGuid">Bucket GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of bucket tags.</returns>
        public Task<List<BucketTag>> RetrieveTags(string bucketGuid, CancellationToken token = default);

        /// <summary>
        /// Create a bucket tag.
        /// </summary>
        /// <param name="buckedGuid">Bucket GUID.</param>
        /// <param name="tag">Bucket tag list.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Created bucket tag.</returns>
        public Task<List<BucketTag>> CreateTag(Guid buckedGuid, List<BucketTag> tag, CancellationToken token = default);

        /// <summary>
        /// Delete a bucket tag.
        /// </summary>
        /// <param name="bucketGuid">Bucket GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> DeleteTag(string bucketGuid, CancellationToken token = default);
        
        #endregion
        
        #region ACL-Operations

        /// <summary>
        /// Retrieve bucket acl.
        /// </summary>
        /// <param name="bucketGuid">Bucket GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Bucket acl.</returns>
        public Task<BucketAcl> RetrieveACL(string bucketGuid, CancellationToken token = default);

        /// <summary>
        /// Create bucket acl.
        /// </summary>
        /// <param name="bucketGuid">Bucket GUID.</param>
        /// <param name="acl">Bucket acl.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Created bucket acl.</returns>
        public Task<List<BucketAclEntry>> CreateACL(string bucketGuid, BucketAcl acl, CancellationToken token = default);

        /// <summary>
        /// Delete bucket acl.
        /// </summary>
        /// <param name="bucketGuid">Bucket GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> DeleteACL(string bucketGuid, CancellationToken token = default);
        
        #endregion
    }
}
