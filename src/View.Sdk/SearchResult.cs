namespace View.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using Timestamps;
    using View.Sdk.Embeddings;

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
        /// Timestamps.
        /// </summary>
        public Timestamp Timestamp { get; set; } = new Timestamp();

        /// <summary>
        /// Maximum number of results to retrieve.
        /// </summary>
        public int MaxResults
        {
            get
            {
                return _MaxResults;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(MaxResults));
                _MaxResults = value;
            }
        }

        /// <summary>
        /// Data flow request GUID.
        /// </summary>
        public Guid? DataFlowRequestGUID { get; set; } = null;

        /// <summary>
        /// Boolean indicating end of results.
        /// </summary>
        public bool EndOfResults { get; set; } = true;

        /// <summary>
        /// Continuation token to use when continuing the search.
        /// </summary>
        public Guid? ContinuationToken { get; set; } = null;

        /// <summary>
        /// Number of candidate records remaining in the search.
        /// </summary>
        public long RecordsRemaining
        {
            get
            {
                return _RecordsRemaining;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(RecordsRemaining));
                _RecordsRemaining = value;
            }
        }

        /// <summary>
        /// Documents that matched the query.
        /// </summary>
        public List<SourceDocument> Documents { get; set; } = null;

        /// <summary>
        /// Embeddings documents generated from matched documents.
        /// </summary>
        public List<EmbeddingsDocument> Embeddings { get; set; } = null;

        #endregion

        #region Private-Members

        private long _RecordsRemaining = 0;
        private int _MaxResults = 0;
        private List<SourceDocument> _Documents = new List<SourceDocument>();

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
