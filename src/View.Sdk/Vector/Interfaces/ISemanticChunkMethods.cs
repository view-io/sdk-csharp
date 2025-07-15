namespace View.Sdk.Vector.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Semantic;

    /// <summary>
    /// Interface for semantic chunk methods.
    /// </summary>
    public interface ISemanticChunkMethods
    {
        /// <summary>
        /// Read semantic chunks for a given document.
        /// </summary>
        /// <param name="repoGuid">Vector repository GUID.</param>
        /// <param name="docGuid">Document GUID.</param>
        /// <param name="cellGuid">Cell GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Semantic chunks.</returns>
        Task<List<SemanticChunk>> ReadMany(
            Guid repoGuid,
            Guid docGuid,
            Guid cellGuid,
            CancellationToken token = default);

        /// <summary>
        /// Read a specific semantic chunk for a given document.
        /// </summary>
        /// <param name="repoGuid">Vector repository GUID.</param>
        /// <param name="docGuid">Document GUID.</param>
        /// <param name="cellGuid">Cell GUID.</param>
        /// <param name="chunkGuid">Chunk GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Semantic chunk.</returns>
        Task<SemanticChunk> Read(
            Guid repoGuid,
            Guid docGuid,
            Guid cellGuid,
            Guid chunkGuid,
            CancellationToken token = default);

        /// <summary>
        /// Check if a specific semantic chunk exists for a given document.
        /// </summary>
        /// <param name="repoGuid">Vector repository GUID.</param>
        /// <param name="docGuid">Document GUID.</param>
        /// <param name="cellGuid">Cell GUID.</param>
        /// <param name="chunkGuid">Chunk GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if the semantic chunk exists.</returns>
        Task<bool> Exists(
            Guid repoGuid,
            Guid docGuid,
            Guid cellGuid,
            Guid chunkGuid,
            CancellationToken token = default);
    }
}