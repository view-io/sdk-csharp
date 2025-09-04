namespace View.Sdk.Processor.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk;

    /// <summary>
    /// UDR generator methods interface.
    /// </summary>
    public interface IUdrGeneratorMethods
    {
        /// <summary>
        /// Process document.
        /// </summary>
        /// <param name="doc">Document request.</param>
        /// <param name="filename">Filename containing data.  Setting this value will overwrite the 'Data' property in the document request.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Document response.</returns>
        Task<UdrDocument> GenerateUdr(UdrDocumentRequest doc, string filename = null, CancellationToken token = default);

    }
}
