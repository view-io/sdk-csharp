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
