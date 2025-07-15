namespace View.Sdk.Configuration.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for collection methods.
    /// </summary>
    public interface ICollectionMethods
    {
        /// <summary>
        /// Create a collection.
        /// </summary>
        /// <param name="collection">Collection.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Collection.</returns>
        public Task<Collection> Create(Collection collection, CancellationToken token = default);

        /// <summary>
        /// Retrieve a collection.
        /// </summary>
        /// <param name="collectionGuid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Collection.</returns>
        public Task<Collection> Retrieve(string collectionGuid, CancellationToken token = default);

        /// <summary>
        /// Retrieve collections.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of collection.</returns>
        public Task<List<Collection>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Retrieve collection statistics.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Collection statistics.</returns>
        public Task<CollectionStatistics> RetrieveStatistics(string collectionGuid, CancellationToken token = default);

        /// <summary>
        /// Delete collection.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(string collectionGuid, CancellationToken token = default);

        /// <summary>
        /// Enumerate collections.
        /// </summary>
        /// <param name="maxKeys">Maximum number of keys to return.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>EnumerationResult containing collections.</returns>
        public Task<EnumerationResult<Collection>> Enumerate(int maxKeys = 5, CancellationToken token = default);

        /// <summary>
        /// Check if a collection exists.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if the collection exists.</returns>
        public Task<bool> Exists(string collectionGuid, CancellationToken token = default);
    }
}