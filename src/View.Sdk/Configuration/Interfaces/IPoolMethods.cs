namespace View.Sdk.Configuration.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for pool methods.
    /// </summary>
    public interface IPoolMethods
    {
        /// <summary>
        /// Create a pool.
        /// </summary>
        /// <param name="pool">Pool.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Pool.</returns>
        public Task<StoragePool> Create(StoragePool pool, CancellationToken token = default);

        /// <summary>
        /// Check if a pool exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read a pool.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Pool.</returns>
        public Task<StoragePool> Retrieve(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read pools.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Pools.</returns>
        public Task<List<StoragePool>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Update a pool.
        /// </summary>
        /// <param name="pool">Pool.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Pool.</returns>
        public Task<StoragePool> Update(StoragePool pool, CancellationToken token = default);

        /// <summary>
        /// Delete a pool.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(Guid guid, CancellationToken token = default);
    }
}