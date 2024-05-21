namespace View.Sdk.Shared.Embeddings
{
    using System;
    using System.Collections.Generic;
    using Timestamps;

    /// <summary>
    /// Search result.
    /// </summary>
    public class SearchResult
    {
        #region Public-Members

        /// <summary>
        /// Indicates if the parser was successful.
        /// </summary>
        public bool Success { get; set; } = false;

        /// <summary>
        /// Exception, if any.
        /// </summary>
        public Exception Exception { get; set; } = null;

        /// <summary>
        /// Timestamps.
        /// </summary>
        public Timestamp Timestamp { get; set; } = new Timestamp();

        /// <summary>
        /// Matched documents.
        /// </summary>
        public List<MatchedDocument> Documents
        {
            get
            {
                return _Documents;
            }
            set
            {
                if (value == null) _Documents = new List<MatchedDocument>();
                else _Documents = value;
            }
        }

        #endregion

        #region Private-Members

        private List<MatchedDocument> _Documents = new List<MatchedDocument>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public SearchResult()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
