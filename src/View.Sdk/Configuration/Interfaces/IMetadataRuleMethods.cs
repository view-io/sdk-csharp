namespace View.Sdk.Configuration.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for metadata rule methods.
    /// </summary>
    public interface IMetadataRuleMethods
    {
        /// <summary>
        /// Create a metadata rule.
        /// </summary>
        /// <param name="rule">Metadata rule.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Metadata rule.</returns>
        public Task<MetadataRule> Create(MetadataRule rule, CancellationToken token = default);

        /// <summary>
        /// Check if a metadata rule exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read a metadata rule.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Metadata rule.</returns>
        public Task<MetadataRule> Retrieve(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read metadata rules.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Metadata rules.</returns>
        public Task<List<MetadataRule>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Update a metadata rule.
        /// </summary>
        /// <param name="rule">Metadata rule.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Metadata rule.</returns>
        public Task<MetadataRule> Update(MetadataRule rule, CancellationToken token = default);

        /// <summary>
        /// Delete a metadata rule.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Enumerate metadata rules.
        /// </summary>
        /// <param name="maxKeys">Maximum number of rules to return.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumeration result containing metadata rules.</returns>
        public Task<EnumerationResult<MetadataRule>> Enumerate(int maxKeys = 5, CancellationToken token = default);
    }
}