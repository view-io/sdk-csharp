namespace View.Sdk.Configuration.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for bucket methods.
    /// </summary>
    public interface IBucketMethods
    {
        /// <summary>
        /// Create a bucket.
        /// </summary>
        /// <param name="bucket">Bucket.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Bucket.</returns>
        public Task<BucketMetadata> Create(BucketMetadata bucket, CancellationToken token = default);

        /// <summary>
        /// Check if a bucket exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read a bucket.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Bucket.</returns>
        public Task<BucketMetadata> Retrieve(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read buckets.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Buckets.</returns>
        public Task<List<BucketMetadata>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Update a bucket.
        /// </summary>
        /// <param name="bucket">Bucket.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Bucket.</returns>
        public Task<BucketMetadata> Update(BucketMetadata bucket, CancellationToken token = default);

        /// <summary>
        /// Delete a bucket.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(Guid guid, CancellationToken token = default);
    }
}