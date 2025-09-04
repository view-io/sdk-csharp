namespace View.Sdk.Processor.Interfaces
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk;

    /// <summary>
    /// Cleanup methods interface.
    /// </summary>
    public interface ICleanupMethods
    {
        /// <summary>
        /// Cleanup a document.  This variant is used by the storage server.
        /// </summary>
        /// <param name="tenant">Tenant metadata.</param>
        /// <param name="collection">Collection metadata.</param>
        /// <param name="bucket">Bucket metadata.</param>
        /// <param name="pool">Storage pool metadata.</param>
        /// <param name="obj">Object metadata.</param>
        /// <param name="mdRule">Metadata rule.</param>
        /// <param name="embedRule">Embeddings rule.</param>
        /// <param name="vectorRepo">Vector repository.</param>
        /// <param name="graphRepo">Graph repository.</param>
        /// <param name="async">Boolean indicating if the task should be performed asynchronously.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Cleanup response.</returns>
        Task<CleanupResult> Process(
            TenantMetadata tenant,
            Collection collection,
            StoragePool pool,
            BucketMetadata bucket,
            ObjectMetadata obj,
            MetadataRule mdRule,
            EmbeddingsRule embedRule,
            VectorRepository vectorRepo,
            GraphRepository graphRepo,
            bool async = false,
            CancellationToken token = default);
    }
}
