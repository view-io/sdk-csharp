namespace View.Sdk.Processor
{

    /// <summary>
    /// Semantic cell metadata rule.
    /// </summary>
    public class SemanticCellMetadataRule
    {
        #region Public-Members
        
        /// <summary>
        /// Endpoint for the semantic cell extractor service.
        /// </summary>
        public string SemanticCellEndpoint { get; set; } = null;

        /// <summary>
        /// Minimum chunk content length.
        /// </summary>
        public int MinChunkContentLength { get; set; }

        /// <summary>
        /// Maximum chunk content length.
        /// </summary>
        public int MaxChunkContentLength { get; set; }

        /// <summary>
        /// Shift size for chunking.
        /// </summary>
        public int ShiftSize { get; set; }

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public SemanticCellMetadataRule() { }

        #endregion
    }
}
