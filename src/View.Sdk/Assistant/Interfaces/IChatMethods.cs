namespace View.Sdk.Assistant.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for assistant chat methods.
    /// </summary>
    public interface IChatMethods
    {
        /// <summary>
        /// Process a chat request with a specific assistant configuration.
        /// </summary>
        /// <param name="configGuid">Configuration GUID.</param>
        /// <param name="request">Chat request.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumerable of tokens.</returns>
        public IAsyncEnumerable<string> ProcessConfigChat(Guid configGuid, AssistantRequest request, CancellationToken token = default);

        /// <summary>
        /// Process a RAG question request.
        /// </summary>
        /// <param name="request">RAG question request.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumerable of tokens.</returns>
        public IAsyncEnumerable<string> ProcessRagQuestion(AssistantRequest request, CancellationToken token = default);

        /// <summary>
        /// Process a RAG message request.
        /// </summary>
        /// <param name="request">RAG message request.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumerable of tokens.</returns>
        public IAsyncEnumerable<string> ProcessRagMessage(AssistantRequest request, CancellationToken token = default);


        /// <summary>
        /// Process a chat-only question request.
        /// </summary>
        /// <param name="request">Chat-only question request.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumerable of tokens.</returns>
        public IAsyncEnumerable<string> ProcessChatOnlyQuestion(AssistantRequest request, CancellationToken token = default);

        /// <summary>
        /// Process a chat-only message request.
        /// </summary>
        /// <param name="request">Chat-only message request.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumerable of tokens.</returns>
        public IAsyncEnumerable<string> ProcessChatOnlyMessage(AssistantRequest request, CancellationToken token = default);

        /// <summary>
        /// Process a chat-only message request with OpenAI.
        /// </summary>
        /// <param name="request">Chat-only message request for OpenAI.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumerable of tokens.</returns>
        public IAsyncEnumerable<string> ProcessChatOnlyMessageOpenAI(AssistantRequest request, CancellationToken token = default);
    }
}