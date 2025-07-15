namespace View.Sdk.Vector.Interfaces
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Embeddings;

    /// <summary>
    /// Interface for document methods.
    /// </summary>
    public interface IDocumentMethods
    {
        /// <summary>
        /// Check if a document exists.
        /// </summary>
        /// <param name="repoGuid">Vector repository GUID.</param>
        /// <param name="docGuid">Document GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if deleted.</returns>
        Task<bool> Exists(
            Guid repoGuid,
            Guid docGuid,
            CancellationToken token = default);

        /// <summary>
        /// Write a document.
        /// </summary>
        /// <param name="document">Embeddings document.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        Task<EmbeddingsDocument> Write(
            EmbeddingsDocument document,
            CancellationToken token = default);

        /// <summary>
        /// Delete a document.
        /// </summary>
        /// <param name="repoGuid">Vector repository GUID.</param>
        /// <param name="docGuid">Document GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if deleted.</returns>
        Task<bool> Delete(
            Guid repoGuid,
            Guid docGuid,
            CancellationToken token = default);

        /// <summary>
        /// Delete documents by filter.
        /// </summary>
        /// <param name="repoGuid">Repository GUID.</param>
        /// <param name="deleteRequest">Delete request.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        Task<bool> DeleteByFilter(
            Guid repoGuid,
            VectorDeleteRequest deleteRequest,
            CancellationToken token = default);

        /// <summary>
        /// Retrieve a document.
        /// </summary>
        /// <param name="repoGuid">Vector repository GUID.</param>
        /// <param name="docGuid">Document GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Embeddings document.</returns>
        Task<EmbeddingsDocument> Retrieve(
            Guid repoGuid,
            Guid docGuid,
            CancellationToken token = default);
    }
}