namespace View.Sdk.Crawler.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for crawl filter methods.
    /// </summary>
    public interface ICrawlFilterMethods
    {
        /// <summary>
        /// Create a crawl filter.
        /// </summary>
        /// <param name="filter">Crawl filter.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Crawl filter.</returns>
        public Task<CrawlFilter> Create(CrawlFilter filter, CancellationToken token = default);

        /// <summary>
        /// Check if a crawl filter exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Retrieve a crawl filter.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Crawl filter.</returns>
        public Task<CrawlFilter> Retrieve(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Retrieve crawl filters.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of crawl filters.</returns>
        public Task<List<CrawlFilter>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Update a crawl filter.
        /// </summary>
        /// <param name="filter">Crawl filter.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Crawl filter.</returns>
        public Task<CrawlFilter> Update(CrawlFilter filter, CancellationToken token = default);

        /// <summary>
        /// Delete a crawl filter.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Enumerate crawl filters.
        /// </summary>
        /// <param name="maxKeys">Maximum number of keys to return.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumeration result.</returns>
        public Task<EnumerationResult<CrawlFilter>> Enumerate(int maxKeys = 5, CancellationToken token = default);
    }
}