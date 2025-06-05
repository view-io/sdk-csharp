namespace View.Sdk.Crawler.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for crawl plan methods.
    /// </summary>
    public interface ICrawlPlanMethods
    {
        /// <summary>
        /// Create a crawl plan.
        /// </summary>
        /// <param name="plan">Crawl plan.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Crawl plan.</returns>
        public Task<CrawlPlan> Create(CrawlPlan plan, CancellationToken token = default);

        /// <summary>
        /// Check if a crawl plan exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Retrieve a crawl plan.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Crawl plan.</returns>
        public Task<CrawlPlan> Retrieve(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Retrieve crawl plans.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of crawl plans.</returns>
        public Task<List<CrawlPlan>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Update a crawl plan.
        /// </summary>
        /// <param name="plan">Crawl plan.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Crawl plan.</returns>
        public Task<CrawlPlan> Update(CrawlPlan plan, CancellationToken token = default);

        /// <summary>
        /// Delete a crawl plan.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Enumerate crawl plans.
        /// </summary>
        /// <param name="maxKeys">Maximum number of keys to return.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumeration result.</returns>
        public Task<EnumerationResult<CrawlPlan>> Enumerate(int maxKeys = 5, CancellationToken token = default);
    }
}