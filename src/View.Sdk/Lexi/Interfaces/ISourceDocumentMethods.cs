namespace View.Sdk.Lexi.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for source document methods.
    /// </summary>
    public interface ISourceDocumentMethods
    {
        /// <summary>
        /// Retrieve documents.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of source documents.</returns>
        public Task<List<SourceDocument>> RetrieveMany(Guid collectionGuid, CancellationToken token = default);

        /// <summary>
        /// Retrieve document.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="documentGuid">Document GUID.</param>
        /// <param name="includeData">Flag to indicate whether or not source document data should be included.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Source document.</returns>
        public Task<SourceDocument> Retrieve(Guid collectionGuid, Guid documentGuid, bool includeData = false, CancellationToken token = default);

        /// <summary>
        /// Retrieve document.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="key">Key.</param>
        /// <param name="version">Version.</param>
        /// <param name="includeData">Flag to indicate whether or not source document data should be included.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Source document.</returns>
        public Task<SourceDocument> Retrieve(Guid collectionGuid, string key, string version, bool includeData = false, CancellationToken token = default);

        /// <summary>
        /// Retrieve top terms for a document.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="documentGuid">Document GUID.</param>
        /// <param name="maxKeys">Maximum number of top terms to return. Default is 10.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Document top terms.</returns>
        public Task<CollectionTopTerms> RetrieveTopTerms(Guid collectionGuid, Guid documentGuid, int maxKeys = 5, CancellationToken token = default);

        /// <summary>
        /// Retrieve document statistics.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="documentGuid">Document GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Source document statistics.</returns>
        public Task<SourceDocumentStatistics> RetrieveStatistics(Guid collectionGuid, Guid documentGuid, CancellationToken token = default);

        /// <summary>
        /// Upload a document.
        /// </summary>
        /// <param name="document">Source document.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Source document.</returns>
        public Task<SourceDocument> Upload(SourceDocument document, CancellationToken token = default);

        /// <summary>
        /// Delete a document.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="documentGuid">Document GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(Guid collectionGuid, Guid documentGuid, CancellationToken token = default);

        /// <summary>
        /// Delete a document.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="key">Document key.</param>
        /// <param name="version">Document version.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(Guid collectionGuid, string key, string version, CancellationToken token = default);

        /// <summary>
        /// Check if a document exists.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="documentGuid">Document GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid collectionGuid, Guid documentGuid, CancellationToken token = default);
    }
}