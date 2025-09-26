namespace View.Sdk.Processor.Interfaces
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk;

    /// <summary>
    /// Processor methods interface.
    /// </summary>
    public interface IProcessorMethods
    {
        /// <summary>
        /// Process a document.  This variant is used by the storage server.
        /// </summary>
        /// <param name="mdRuleGuid">Metadata rule GUID.</param>
        /// <param name="embedRuleGuid">Embeddings rule GUID.</param>
        /// <param name="obj">Object metadata.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Processor response.</returns>
        Task<ProcessorResult> Process(
            Guid mdRuleGuid,
            Guid embedRuleGuid,
            ObjectMetadata obj,
            CancellationToken token = default);

        /// <summary>
        /// Enumerate processor tasks.
        /// </summary>
        /// <param name="maxKeys">Maximum number of keys to return.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumeration result containing processor tasks.</returns>
        Task<EnumerationResult<ProcessorTask>> Enumerate(
            int maxKeys = 5,
            CancellationToken token = default);

        /// <summary>
        /// Retrieve a processor task by GUID.
        /// </summary>
        /// <param name="guid">Processor task GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Processor task.</returns>
        Task<ProcessorTask> Retrieve(
            Guid guid,
            CancellationToken token = default);
    }
}
