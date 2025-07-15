namespace View.Sdk.Configuration.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for webhook rule methods.
    /// </summary>
    public interface IWebhookRuleMethods
    {
        /// <summary>
        /// Create a webhook rule.
        /// </summary>
        /// <param name="rule">Webhook rule.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Webhook rule.</returns>
        public Task<WebhookRule> Create(WebhookRule rule, CancellationToken token = default);

        /// <summary>
        /// Check if a webhook rule exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read a webhook rule.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Webhook rule.</returns>
        public Task<WebhookRule> Retrieve(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read webhook rules.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Webhook rules.</returns>
        public Task<List<WebhookRule>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Update a webhook rule.
        /// </summary>
        /// <param name="rule">Webhook rule.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Webhook rule.</returns>
        public Task<WebhookRule> Update(WebhookRule rule, CancellationToken token = default);

        /// <summary>
        /// Delete a webhook rule.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Enumerate webhook rules with pagination support.
        /// </summary>
        /// <param name="maxKeys">Maximum number of keys to return in a single operation.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumeration result containing webhook rules.</returns>
        public Task<EnumerationResult<WebhookRule>> Enumerate(int maxKeys = 5, CancellationToken token = default);
    }
}