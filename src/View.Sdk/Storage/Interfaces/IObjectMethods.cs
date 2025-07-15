namespace View.Sdk.Storage.Interfaces
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Storage;

    /// <summary>
    /// Interface for object methods.
    /// </summary>
    public interface IObjectMethods
    {
        #region Basic-Object-Operations
        
        /// <summary>
        /// Check if an object exists in a bucket.
        /// </summary>
        /// <param name="bucketGuid">Bucket GUID.</param>
        /// <param name="objectKey">Object key.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if the object exists.</returns>
        public Task<bool> Exists(string bucketGuid, string objectKey, CancellationToken token = default);

        /// <summary>
        /// Retrieve an object from a bucket.
        /// </summary>
        /// <param name="bucketGuid">Bucket GUID.</param>
        /// <param name="objectKey">Object key.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Raw object data as string.</returns>
        public Task<string> Retrieve(string bucketGuid, string objectKey, CancellationToken token = default);

        /// <summary>
        /// Retrieve a byte range from an object in a bucket.
        /// </summary>
        /// <param name="bucketGuid">Bucket GUID.</param>
        /// <param name="objectKey">Object key.</param>
        /// <param name="startByte">Start byte position (inclusive).</param>
        /// <param name="endByte">End byte position (inclusive).</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Raw object data as string for the specified range.</returns>
        public Task<string> RetrieveRange(string bucketGuid, string objectKey, int startByte, int endByte, CancellationToken token = default);

        /// <summary>
        /// Retrieve object metadata from a bucket.
        /// </summary>
        /// <param name="bucketGuid">Bucket GUID.</param>
        /// <param name="objectKey">Object key.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Object metadata.</returns>
        public Task<ObjectMetadata> RetrieveMetadata(string bucketGuid, string objectKey, CancellationToken token = default);

        /// <summary>
        /// Delete an object from a bucket.
        /// </summary>
        /// <param name="bucketGuid">Bucket GUID.</param>
        /// <param name="objectKey">Object key.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(string bucketGuid, string objectKey, CancellationToken token = default);

        /// <summary>
        /// Create a non-chunked object in a bucket.
        /// </summary>
        /// <param name="bucketGuid">Bucket GUID.</param>
        /// <param name="objectKey">Object key.</param>
        /// <param name="data">Raw object data as string.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Object metadata of the created object.</returns>
        public Task<ObjectMetadata> CreateNonChunked(string bucketGuid, string objectKey, string data, CancellationToken token = default);

        /// <summary>
        /// Create a chunked object in a bucket.
        /// </summary>
        /// <param name="bucketGuid">Bucket GUID.</param>
        /// <param name="objectKey">Object key.</param>
        /// <param name="data">Raw chunked object data as string.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Object metadata of the created object.</returns>
        public Task<ObjectMetadata> CreateChunked(string bucketGuid, string objectKey, string data, CancellationToken token = default);

        /// <summary>
        /// Create an object with expiration in a bucket.
        /// </summary>
        /// <param name="bucketGuid">Bucket GUID.</param>
        /// <param name="objectKey">Object key.</param>
        /// <param name="expiration">Expiration object with expiration date.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Return object metadata.</returns>
        public Task<ObjectMetadata> CreateExpiration(string bucketGuid, string objectKey, Expiration expiration, CancellationToken token = default);
        
        #endregion
        
        #region ACL-Operations

        /// <summary>
        /// Retrieve object ACL from a bucket.
        /// </summary>
        /// <param name="bucketGuid">Bucket GUID.</param>
        /// <param name="objectKey">Object key.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Object ACL.</returns>
        public Task<BucketAcl> RetrieveACL(string bucketGuid, string objectKey, CancellationToken token = default);

        /// <summary>
        /// Create object ACL in a bucket.
        /// </summary>
        /// <param name="bucketGuid">Bucket GUID.</param>
        /// <param name="objectKey">Object key.</param>
        /// <param name="acl">Object ACL.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Created object ACL.</returns>
        public Task<List<BucketAclEntry>> CreateACL(string bucketGuid, string objectKey, BucketAcl acl, CancellationToken token = default);

        /// <summary>
        /// Delete object ACL from a bucket.
        /// </summary>
        /// <param name="bucketGuid">Bucket GUID.</param>
        /// <param name="objectKey">Object key.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> DeleteACL(string bucketGuid, string objectKey, CancellationToken token = default);
        
        #endregion
        
        #region Tag-Operations

        /// <summary>
        /// Create tags for an object in a bucket.
        /// </summary>
        /// <param name="bucketGuid">Bucket GUID.</param>
        /// <param name="objectKey">Object key.</param>
        /// <param name="tags">List of object tags.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of created object tags.</returns>
        public Task<List<BucketTag>> CreateTags(string bucketGuid, string objectKey, List<BucketTag> tags, CancellationToken token = default);

        /// <summary>
        /// Retrieve tags for an object from a bucket.
        /// </summary>
        /// <param name="bucketGuid">Bucket GUID.</param>
        /// <param name="objectKey">Object key.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of object tags.</returns>
        public Task<List<BucketTag>> RetrieveTags(string bucketGuid, string objectKey, CancellationToken token = default);

        /// <summary>
        /// Delete tags for an object from a bucket.
        /// </summary>
        /// <param name="bucketGuid">Bucket GUID.</param>
        /// <param name="objectKey">Object key.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> DeleteTags(string bucketGuid, string objectKey, CancellationToken token = default);

        #endregion

        #region Reprocess-Operations

        /// <summary>
        /// Trigger reprocessing of an object in a bucket.
        /// </summary>
        /// <param name="bucketGuid">Bucket GUID.</param>
        /// <param name="objectKey">Object key.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Reprocess(string bucketGuid, string objectKey, CancellationToken token = default);
        
        #endregion
    }
}