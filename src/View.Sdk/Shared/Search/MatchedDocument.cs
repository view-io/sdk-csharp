namespace View.Sdk.Shared.Search
{
    using System.Text.Json.Serialization;
    using View.Sdk.Shared.Udr;

    /// <summary>
    /// A document matching a search query.
    /// </summary>
    public class MatchedDocument
    {
        #region Public-Members

        /// <summary>
        /// Globally-unique identifier for the source document.
        /// </summary>
        [JsonPropertyOrder(1)]
        public string GUID { get; set; } = null;

        /// <summary>
        /// The type of document.
        /// </summary>
        [JsonPropertyOrder(2)]
        public DocumentTypeEnum DocumentType { get; set; } = DocumentTypeEnum.Unknown;

        /// <summary>
        /// The score of the document, between 0 and 1, over both terms and filters.  Only relevant when optional terms or filters are supplied in the search.
        /// </summary>
        [JsonPropertyOrder(3)]
        public decimal? Score { get; set; } = null;

        /// <summary>
        /// The terms score of the document, between 0 and 1, when optional terms are supplied.
        /// </summary>
        [JsonPropertyOrder(4)]
        public decimal? TermsScore { get; set; } = null;

        /// <summary>
        /// The filters score of the document, between 0 and 1, when optional filters are supplied.
        /// </summary>
        [JsonPropertyOrder(5)]
        public decimal? FiltersScore { get; set; } = null;

        /// <summary>
        /// Source document metadata, if requested.
        /// </summary>
        [JsonPropertyOrder(6)]
        public SourceDocument Metadata { get; set; } = null;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiates the object.
        /// </summary>
        public MatchedDocument()
        {
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
