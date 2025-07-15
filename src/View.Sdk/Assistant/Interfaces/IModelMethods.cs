namespace View.Sdk.Assistant.Interfaces
{
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for assistant model methods.
    /// </summary>
    public interface IModelMethods
    {
        /// <summary>
        /// Retrieve available models.
        /// </summary>
        /// <param name="request">Model request with Ollama hostname and port.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of available models.</returns>
        public Task<List<Model>> RetrieveMany(ModelRequest request, CancellationToken token = default);

        /// <summary>
        /// Pulls (retrieves) a specific model from the Ollama host and streams progress updates.
        /// </summary>
        /// <param name="request">The model request containing the model name, Ollama hostname, and port information.</param>
        /// <param name="token">A cancellation token to cancel the operation.</param>
        /// <returns>
        /// An asynchronous stream of progress messages from the model pulling operation. 
        /// Each item represents a status update from the server, such as download progress.
        /// </returns>
        public IAsyncEnumerable<string> Retrieve(ModelRequest request, CancellationToken token = default);

        /// <summary>
        /// Delete a specific model.
        /// </summary>
        /// <param name="request">Model request with model name, Ollama hostname and port.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Model operation response.</returns>
        public Task<ModelResponse> Delete(ModelRequest request, CancellationToken token = default);

        /// <summary>
        /// Preload a specific model.
        /// </summary>
        /// <param name="request">Model request with model name, Ollama hostname and port.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Model operation response.</returns>
        public Task<ModelResponse> PreLoad(ModelRequest request, CancellationToken token = default);

        /// <summary>
        /// Unload a specific model.
        /// </summary>
        /// <param name="request">Model request with model name, Ollama hostname and port.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Model operation response.</returns>
        public Task<ModelResponse> Unload(ModelRequest request, CancellationToken token = default);
    }
}