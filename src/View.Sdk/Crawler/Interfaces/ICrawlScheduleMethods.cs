namespace View.Sdk.Crawler.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for crawl schedule methods.
    /// </summary>
    public interface ICrawlScheduleMethods
    {
        /// <summary>
        /// Create a crawl schedule.
        /// </summary>
        /// <param name="schedule">Crawl schedule.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Crawl schedule.</returns>
        public Task<CrawlSchedule> Create(CrawlSchedule schedule, CancellationToken token = default);

        /// <summary>
        /// Check if a crawl schedule exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Retrieve a crawl schedule.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Crawl schedule.</returns>
        public Task<CrawlSchedule> Retrieve(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Retrieve crawl schedules.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of crawl schedules.</returns>
        public Task<List<CrawlSchedule>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Update a crawl schedule.
        /// </summary>
        /// <param name="schedule">Crawl schedule.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Crawl schedule.</returns>
        public Task<CrawlSchedule> Update(CrawlSchedule schedule, CancellationToken token = default);

        /// <summary>
        /// Delete a crawl schedule.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Enumerate crawl schedules.
        /// </summary>
        /// <param name="maxKeys">Maximum number of keys to return.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumeration result.</returns>
        public Task<EnumerationResult<CrawlSchedule>> Enumerate(int maxKeys = 5, CancellationToken token = default);
    }
}