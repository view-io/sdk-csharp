namespace View.Sdk.Processor
{
    using System;
    using View.Sdk;
    using View.Sdk.Processor.Implementations;
    using View.Sdk.Processor.Interfaces;

    /// <summary>
    /// View Processor SDK.
    /// </summary>
    public class ViewProcessorSdk : ViewSdkBase, IDisposable
    {
        #region Public-Members

        /// <summary>
        /// Processor methods.
        /// </summary>
        public IProcessorMethods Processor { get; set; }

        /// <summary>
        /// Cleanup methods.
        /// </summary>
        public ICleanupMethods Cleanup { get; set; }

        /// <summary>
        /// Lexi embeddings methods.
        /// </summary>
        public ILexiEmbeddingsMethods LexiEmbeddings { get; set; }

        /// <summary>
        /// Type detector methods.
        /// </summary>
        public ITypeDetectorMethods TypeDetector { get; set; }

        /// <summary>
        /// UDR generator methods.
        /// </summary>
        public IUdrGeneratorMethods UdrGenerator { get; set; }

        /// <summary>
        /// Semantic cell extraction methods.
        /// </summary>
        public ISemanticCellExtractionMethods SemanticCellExtraction { get; set; }

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="tenantGuid">Tenant GUID.</param>
        /// <param name="accessKey">Access key.</param>
        /// <param name="endpoint">Endpoint URL, i.e. http://localhost:8000/.</param>
        /// <param name="xToken">
        /// Optional token to be included as the <c>x-token</c> header in API requests that require additional authorization.
        /// This is used in cases where certain endpoints require an extra level of authentication.
        /// </param>
        public ViewProcessorSdk(Guid tenantGuid, string accessKey, string endpoint = "http://localhost:8000/", string xToken = null) : base(tenantGuid, accessKey, endpoint)
        {
            Header = "[ViewProcessorSdk] ";
            XToken = xToken;
            Processor = new ProcessorMethods(this);
            Cleanup = new CleanupMethods(this);
            LexiEmbeddings = new LexiEmbeddingsMethods(this);
            TypeDetector = new TypeDetectorMethods(this);
            UdrGenerator = new UdrGeneratorMethods(this);
            SemanticCellExtraction = new SemanticCellExtractionMethods(this);
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
