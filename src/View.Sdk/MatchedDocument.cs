namespace View.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text.Json.Serialization;
    using Timestamps;

    /// <summary>
    /// Matched document.
    /// </summary>
    public class MatchedDocument
    {
        #region Public-Members

        /// <summary>
        /// The score of the document, between 0 and 1, over both terms and filters.  Only relevant when optional terms or filters are supplied in the search.
        /// </summary>
        public decimal? Score { get; set; } = null;

        /// <summary>
        /// The terms score of the document, between 0 and 1, when optional terms are supplied.
        /// </summary>
        public decimal? TermsScore { get; set; } = null;

        /// <summary>
        /// The filters score of the document, between 0 and 1, when optional filters are supplied.
        /// </summary>
        public decimal? FiltersScore { get; set; } = null;

        /// <summary>
        /// Source document metadata, if requested.
        /// </summary>
        public SourceDocument SourceDocument { get; set; } = null;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
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
