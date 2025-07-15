namespace View.Sdk.Assistant.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for assistant configuration methods.
    /// </summary>
    public interface IConfigMethods
    {
        /// <summary>
        /// Retrieve assistant configurations.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of assistant configurations.</returns>
        public Task<AssistantConfigurationResponse> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Retrieve an assistant configuration.
        /// </summary>
        /// <param name="configGuid">Configuration GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Assistant configuration.</returns>
        public Task<AssistantConfig> Retrieve(Guid configGuid, CancellationToken token = default);

        /// <summary>
        /// Check if an assistant configuration exists.
        /// </summary>
        /// <param name="configGuid">Configuration GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid configGuid, CancellationToken token = default);

        /// <summary>
        /// Create a RAG assistant configuration.
        /// </summary>
        /// <param name="config">Assistant configuration.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Created assistant configuration.</returns>
        public Task<AssistantConfiguration> CreateRag(AssistantConfig config, CancellationToken token = default);

        /// <summary>
        /// Create a chat-only assistant configuration.
        /// </summary>
        /// <param name="config">Assistant configuration.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Created assistant configuration.</returns>
        public Task<AssistantConfiguration> CreateChat(AssistantConfig config, CancellationToken token = default);

        /// <summary>
        /// Update an assistant configuration.
        /// </summary>
        /// <param name="config">Assistant configuration.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Updated assistant configuration.</returns>
        public Task<AssistantConfiguration> Update(AssistantConfig config, CancellationToken token = default);

        /// <summary>
        /// Delete an assistant configuration.
        /// </summary>
        /// <param name="configGuid">Configuration GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if deleted successfully.</returns>
        public Task<bool> Delete(Guid configGuid, CancellationToken token = default);
    }
}