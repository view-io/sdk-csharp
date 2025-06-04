namespace View.Sdk.Configuration.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for webhook target methods.
    /// </summary>
    public interface IWebhookTargetMethods
    {
        /// <summary>
        /// Create a webhook target.
        /// </summary>
        /// <param name="target">Webhook target.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Webhook target.</returns>
        public Task<WebhookTarget> Create(WebhookTarget target, CancellationToken token = default);

        /// <summary>
        /// Check if a webhook target exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read a webhook target.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Webhook target.</returns>
        public Task<WebhookTarget> Retrieve(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read webhook targets.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Webhook targets.</returns>
        public Task<List<WebhookTarget>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Update a webhook target.
        /// </summary>
        /// <param name="target">Webhook target.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Webhook target.</returns>
        public Task<WebhookTarget> Update(WebhookTarget target, CancellationToken token = default);

        /// <summary>
        /// Delete a webhook target.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Enumerate webhook targets with pagination support.
        /// </summary>
        /// <param name="maxKeys">Maximum number of keys to return in a single operation.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumeration result containing webhook targets.</returns>
        public Task<EnumerationResult<WebhookTarget>> Enumerate(int maxKeys = 5, CancellationToken token = default);
    }
}