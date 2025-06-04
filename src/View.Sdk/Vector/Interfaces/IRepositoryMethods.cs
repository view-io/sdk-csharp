namespace View.Sdk.Vector.Interfaces
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Embeddings;

    /// <summary>
    /// Interface for repository methods.
    /// </summary>
    public interface IRepositoryMethods
    {

        /// <summary>
        /// Enumerate documents.
        /// </summary>
        /// <param name="query">Enumeration query.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumeration result.</returns>
        Task<EnumerationResult<EmbeddingsDocument>> EnumerateMany(
            EnumerationQuery query,
            CancellationToken token = default);

        /// <summary>
        /// Truncate repository.
        /// </summary>
        /// <param name="repoGuid">Vector repository GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if deleted.</returns>
        Task<bool> Truncate(
            Guid repoGuid,
            CancellationToken token = default);

        /// <summary>
        /// Get statistics for a repository.
        /// </summary>
        /// <param name="repoGuid">Vector repository GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Statistics.</returns>
        Task<VectorRepositoryStatistics> GetStatistics(
            Guid repoGuid,
            CancellationToken token = default);
    }
}