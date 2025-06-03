namespace View.Sdk.Configuration.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for webhook event methods.
    /// </summary>
    public interface IWebhookEventMethods
    {

        /// <summary>
        /// Check if a webhook event exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read a webhook event.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Embeddings rule.</returns>
        public Task<WebhookEvent> Retrieve(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read webhook events.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Webhook events.</returns>
        public Task<List<WebhookEvent>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Enumerate webhook events with pagination support.
        /// </summary>
        /// <param name="maxKeys">Maximum number of keys to return in a single operation.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumeration result containing webhook events.</returns>
        public Task<EnumerationResult<WebhookEvent>> Enumerate(int maxKeys = 5, CancellationToken token = default);
    }
}