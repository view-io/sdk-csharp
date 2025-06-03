namespace View.Sdk.Configuration.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for embeddings rule methods.
    /// </summary>
    public interface IEmbeddingsRuleMethods
    {
        /// <summary>
        /// Create a embeddings rule.
        /// </summary>
        /// <param name="rule">Embeddings rule.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Embeddings rule.</returns>
        public Task<EmbeddingsRule> Create(EmbeddingsRule rule, CancellationToken token = default);

        /// <summary>
        /// Check if a embeddings rule exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read a embeddings rule.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Embeddings rule.</returns>
        public Task<EmbeddingsRule> Retrieve(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read embeddings rules.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Embeddings rules.</returns>
        public Task<List<EmbeddingsRule>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Update a embeddings rule.
        /// </summary>
        /// <param name="rule">Embeddings rule.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Embeddings rule.</returns>
        public Task<EmbeddingsRule> Update(EmbeddingsRule rule, CancellationToken token = default);

        /// <summary>
        /// Delete a embeddings rule.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Enumerate embeddings rules with pagination support.
        /// </summary>
        /// <param name="maxKeys">Maximum number of keys to return.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumeration result containing embeddings rules.</returns>
        public Task<EnumerationResult<EmbeddingsRule>> Enumerate(int maxKeys = 5, CancellationToken token = default);
    }
}