namespace View.Sdk.Lexi.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for ingest queue methods.
    /// </summary>
    public interface IIngestQueueMethods
    {
        /// <summary>
        /// Check if an ingest queue entry exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read an ingest queue entry.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="includeStats">Include statistics in the query.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Ingest queue entry.</returns>
        public Task<IngestionQueueEntry> Retrieve(Guid guid, bool includeStats = false, CancellationToken token = default);

        /// <summary>
        /// Read ingest queue entries.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Ingest queue entries.</returns>
        public Task<List<IngestionQueueEntry>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Delete an ingest queue entry.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Retrieve statistics for the ingest queue.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Ingest queue statistics.</returns>
        public Task<IngestQueueStatistics> RetrieveStatistics(CancellationToken token = default);
    }
}