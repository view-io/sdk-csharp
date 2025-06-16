namespace View.Sdk.Assistant.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for assistant chat thread methods.
    /// </summary>
    public interface IChatThreadMethods
    {
        /// <summary>
        /// Retrieve chat threads.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of chat threads.</returns>
        public Task<ChatThreadsResponse> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Retrieve a chat thread.
        /// </summary>
        /// <param name="threadGuid">Thread GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Chat thread.</returns>
        public Task<ChatThread> Retrieve(Guid threadGuid, CancellationToken token = default);

        /// <summary>
        /// Check if a chat thread exists.
        /// </summary>
        /// <param name="threadGuid">Thread GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid threadGuid, CancellationToken token = default);

        /// <summary>
        /// Create a chat thread.
        /// </summary>
        /// <param name="chatThread">Chat thread to create.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Created chat thread.</returns>
        public Task<ChatThread> Create(ChatThread chatThread, CancellationToken token = default);

        /// <summary>
        /// Append a message to a chat thread.
        /// </summary>
        /// <param name="threadGuid">Thread GUID.</param>
        /// <param name="message">Message to append.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Updated chat thread.</returns>
        public Task<ChatThread> Append(Guid threadGuid, ChatMessage message, CancellationToken token = default);

        /// <summary>
        /// Delete a chat thread.
        /// </summary>
        /// <param name="threadGuid">Thread GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if deleted.</returns>
        public Task<bool> Delete(Guid threadGuid, CancellationToken token = default);
    }
}