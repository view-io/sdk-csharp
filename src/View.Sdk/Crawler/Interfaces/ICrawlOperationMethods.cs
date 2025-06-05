namespace View.Sdk.Crawler.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Crawler;

    /// <summary>
    /// Interface for crawl operation methods.
    /// </summary>
    public interface ICrawlOperationMethods
    {
        /// <summary>
        /// Enumerate crawl operations.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of crawl operations.</returns>
        public Task<EnumerationResult<CrawlOperation>> Enumerate(CancellationToken token = default);

        /// <summary>
        /// Retrieve all crawl operations.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of crawl operations.</returns>
        public Task<List<CrawlOperation>> RetrieveAll(CancellationToken token = default);

        /// <summary>
        /// Retrieve a crawl operation.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Crawl operation.</returns>
        public Task<CrawlOperation> Retrieve(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Retrieve enumeration for a crawl operation.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumeration result.</returns>
        public Task<CrawlEnumeration> RetrieveEnumeration(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Start a crawl operation.
        /// </summary>
        /// <param name="request">Crawl operation request containing the operation GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Crawl operation.</returns>
        public Task<CrawlOperation> Start(CrawlOperationRequest request, CancellationToken token = default);

        /// <summary>
        /// Stop a crawl operation.
        /// </summary>
        /// <param name="request">Crawl operation request containing the operation GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Crawl operation.</returns>
        public Task<CrawlOperation> Stop(CrawlOperationRequest request, CancellationToken token = default);

        /// <summary>
        /// Delete a crawl operation.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Check if a crawl operation exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid guid, CancellationToken token = default);
    }
}