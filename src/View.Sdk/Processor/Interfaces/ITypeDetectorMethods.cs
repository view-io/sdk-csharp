namespace View.Sdk.Processor.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk;

    /// <summary>
    /// Type detector methods interface.
    /// </summary>
    public interface ITypeDetectorMethods
    {
        /// <summary>
        /// Detect the type of a document.
        /// </summary>
        /// <param name="jsonContent">JSON content to analyze.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Type detection response.</returns>
        Task<TypeResult> DetectType(
            string jsonContent,
            CancellationToken token = default);

    }
}
