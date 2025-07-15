namespace View.Sdk.Lexi.Interfaces
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for search methods.
    /// </summary>
    public interface ISearchMethods
    {
        /// <summary>
        /// Search a collection.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="query">Query.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Search result.</returns>
        public Task<SearchResult> Search(Guid collectionGuid, CollectionSearchRequest query, CancellationToken token = default);

        /// <summary>
        /// Search a collection and include document data in the results.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="query">Query.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Search result with document data included.</returns>
        public Task<SearchResult> SearchIncludeData(Guid collectionGuid, CollectionSearchRequest query, CancellationToken token = default);

        /// <summary>
        /// Search a collection and include top terms in the results.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="query">Query.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Search result with top terms.</returns>
        public Task<SearchResult> SearchIncludeTopTerms(Guid collectionGuid, CollectionSearchRequest query, CancellationToken token = default);

        /// <summary>
        /// Search a collection asynchronously.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="query">Query.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Search result.</returns>
        public Task<SearchResult> SearchAsync(Guid collectionGuid, CollectionSearchRequest query, CancellationToken token = default);

        /// <summary>
        /// Enumerate documents in a collection.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="query">Query.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumeration result containing source documents.</returns>
        public Task<EnumerationResult<SourceDocument>> Enumerate(Guid collectionGuid, CollectionSearchRequest query, CancellationToken token = default);
    }
}