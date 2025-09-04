namespace View.Sdk.Processor.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Processor;
    using View.Sdk.Semantic;

    /// <summary>
    /// Semantic cell extraction methods interface (processing pipeline).
    /// </summary>
    public interface ISemanticCellExtractionMethods
    {
        /// <summary>
        /// Extract semantic cells from a document.
        /// </summary>
        /// <param name="request">Semantic cell extraction request.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Semantic cell extraction result.</returns>
        Task<SemanticCellResult> Process(SemanticCellExtractionRequest request, CancellationToken token = default);
    }
}
