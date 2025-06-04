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
    }
}