namespace View.Sdk.Processor.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk;

    /// <summary>
    /// Lexi embeddings methods interface.
    /// </summary>
    public interface ILexiEmbeddingsMethods
    {

        /// <summary>
        /// Process a document.
        /// </summary>
        /// <param name="results">Search results.</param>
        /// <param name="embedRule">Embeddings rule.</param>
        /// <param name="vectorRepo">Vector repository.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        Task<LexiEmbeddingsResult> Process(
            SearchResult results,
            EmbeddingsRule embedRule,
            VectorRepository vectorRepo,
            CancellationToken token = default);

    }
}
