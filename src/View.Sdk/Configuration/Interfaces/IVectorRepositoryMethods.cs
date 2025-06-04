namespace View.Sdk.Configuration.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for vector repository methods.
    /// </summary>
    public interface IVectorRepositoryMethods
    {
        /// <summary>
        /// Create a vector repository.
        /// </summary>
        /// <param name="repo">Vector repository.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Vector repository.</returns>
        public Task<VectorRepository> Create(VectorRepository repo, CancellationToken token = default);

        /// <summary>
        /// Check if a vector repository exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read a vector repository.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>vector repository.</returns>
        public Task<VectorRepository> Retrieve(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read vector repositories.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Vector repositories.</returns>
        public Task<List<VectorRepository>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Update a vector repository.
        /// </summary>
        /// <param name="repo">Vector repository.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Vector repository.</returns>
        public Task<VectorRepository> Update(VectorRepository repo, CancellationToken token = default);

        /// <summary>
        /// Delete a vector repository.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Enumerate vector repositories with pagination support.
        /// </summary>
        /// <param name="maxKeys">Maximum number of keys to return in a single operation.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumeration result containing vector repositories.</returns>
        public Task<EnumerationResult<VectorRepository>> Enumerate(int maxKeys = 5, CancellationToken token = default);
    }
}