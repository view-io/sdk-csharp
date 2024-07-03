namespace View.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
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
        /// Timestamps.
        /// </summary>
        public Timestamp Timestamp { get; set; } = new Timestamp();

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public string TenantGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// The GUID of the collection that was queried.
        /// </summary>
        public string CollectionGUID { get; set; } = null;

        /// <summary>
        /// Data flow request GUID.
        /// </summary>
        public string DataFlowRequestGUID { get; set; } = null;

        /// <summary>
        /// The search query performed.
        /// </summary>
        public SearchQuery Query { get; set; } = null;

        /// <summary>
        /// Boolean indicating end of results.
        /// </summary>
        public bool EndOfResults { get; set; } = true;

        /// <summary>
        /// Continuation token to use when continuing the search.
        /// </summary>
        public string ContinuationToken { get; set; } = null;

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

        /// <summary>
        /// Embeddings documents generated from matched documents.
        /// </summary>
        public List<EmbeddingsDocument> Embeddings { get; set; } = null;

        #endregion

        #region Private-Members

        private long _RecordsRemaining = 0;
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
