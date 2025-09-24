namespace View.Sdk.Processor
{
    using View.Sdk;

    /// <summary>
    /// Semantic cell extraction request (processing pipeline).
    /// </summary>
    public class SemanticCellExtractionRequest
    {
        #region Public-Members

        /// <summary>
        /// Document type to parse.
        /// </summary>
        public DocumentTypeEnum DocumentType { get; set; } = DocumentTypeEnum.Unknown;

        /// <summary>
        /// Metadata rule with extractor configuration.
        /// </summary>
        public SemanticCellMetadataRule MetadataRule { get; set; } = new SemanticCellMetadataRule();

        /// <summary>
        /// Base64-encoded document data.
        /// </summary>
        public string Data { get; set; } = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public SemanticCellExtractionRequest() { }

        #endregion
   
    }
}
