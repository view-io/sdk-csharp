namespace View.Sdk.Configuration.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for graph repository methods.
    /// </summary>
    public interface IGraphRepositoryMethods
    {
        /// <summary>
        /// Create a graph repository.
        /// </summary>
        /// <param name="repo">Graph repository.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph repository.</returns>
        public Task<GraphRepository> Create(GraphRepository repo, CancellationToken token = default);

        /// <summary>
        /// Check if a graph repository exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read a graph repository.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph repository.</returns>
        public Task<GraphRepository> Retrieve(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read graph repositories.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph repositories.</returns>
        public Task<List<GraphRepository>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Update a graph repository.
        /// </summary>
        /// <param name="repo">graph repository.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph repository.</returns>
        public Task<GraphRepository> Update(GraphRepository repo, CancellationToken token = default);

        /// <summary>
        /// Delete a graph repository.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Enumerate graph repositories.
        /// </summary>
        /// <param name="maxKeys">Maximum number of keys to return.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumeration result.</returns>
        public Task<EnumerationResult<GraphRepository>> Enumerate(int maxKeys = 5, CancellationToken token = default);
    }
}