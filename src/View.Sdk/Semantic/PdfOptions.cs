namespace View.Sdk.Semantic
{
    using System;

    /// <summary>
    /// PDF options.
    /// </summary>
    public class PdfOptions
    {
        #region Public-Members

        /// <summary>
        /// PDF processing mode.
        /// </summary>
        public PdfModeEnum Mode { get; set; } = PdfModeEnum.BoundingBoxExtraction;

        /// <summary>
        /// True to indicate that the marked-up PDF including bounding boxes should be returned.
        /// Only applicable when Mode is set to BoundingBoxExtraction.
        /// </summary>
        public bool ReturnMarkup { get; set; } = false;

        /// <summary>
        /// True to indicate that the marked-up PDF file should be preserved in the temporary directory.
        /// Only applicable when Mode is set to BoundingBoxExtraction.
        /// </summary>
        public bool RetainArtifact { get; set; } = false;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public PdfOptions()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
