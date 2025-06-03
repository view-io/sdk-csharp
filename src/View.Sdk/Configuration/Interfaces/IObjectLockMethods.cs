namespace View.Sdk.Configuration.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for object lock methods.
    /// </summary>
    public interface IObjectLockMethods
    {
        /// <summary>
        /// Create an object lock.
        /// </summary>
        /// <param name="endpoint">Object lock.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Object lock.</returns>
        public Task<ObjectLock> Create(ObjectLock endpoint, CancellationToken token = default);

        /// <summary>
        /// Check if an object lock exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read an object lock.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Object lock.</returns>
        public Task<ObjectLock> Retrieve(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read Object locks.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Object locks.</returns>
        public Task<List<ObjectLock>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Update an object lock.
        /// </summary>
        /// <param name="endpoint">Object lock.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Object lock.</returns>
        public Task<ObjectLock> Update(ObjectLock endpoint, CancellationToken token = default);

        /// <summary>
        /// Delete an object lock.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Enumerate object locks with pagination support.
        /// </summary>
        /// <param name="maxKeys">Maximum number of keys to return in a single operation.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumeration result containing object locks.</returns>
        public Task<EnumerationResult<ObjectLock>> Enumerate(int maxKeys = 5, CancellationToken token = default);
    }
}