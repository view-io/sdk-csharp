namespace View.Sdk.Semantic
{
    using System;

    /// <summary>
    /// Semantic cell request.
    /// </summary>
    public class SemanticCellRequest
    {
        #region Public-Members

        /// <summary>
        /// Document type.
        /// </summary>
        public DocumentTypeEnum DocumentType { get; set; } = DocumentTypeEnum.Unknown;

        /// <summary>
        /// Gets or sets the minimum length of chunk content.
        /// </summary>
        public int MinChunkContentLength { get; set; }

        /// <summary>
        /// Gets or sets the maximum length of chunk content.
        /// </summary>
        public int MaxChunkContentLength { get; set; }

        /// <summary>
        /// Gets or sets the size of the shift used when creating chunks.
        /// </summary>
        public int ShiftSize { get; set; }

        /// <summary>
        /// Data.
        /// When serializing, convert to a base64-encoded string.
        /// </summary>
        public byte[] Data { get; set; } = null;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public SemanticCellRequest()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
