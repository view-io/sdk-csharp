namespace View.Sdk.Shared.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json.Serialization;
    using Timestamps;

    /// <summary>
    /// Object returned as the result of a search against a collection.
    /// </summary>
    public class SearchResult
    {
        #region Public-Members

        /// <summary>
        /// Indicates if the statistics operation was successful.
        /// </summary>
        [JsonPropertyOrder(1)]
        public bool Success { get; set; } = true;

        /// <summary>
        /// Exception.
        /// </summary>
        [JsonPropertyOrder(2)]
        public Exception Exception { get; set; } = null;

        /// <summary>
        /// Timestamps.
        /// </summary>
        [JsonPropertyOrder(3)]
        public Timestamp Timestamp { get; set; } = new Timestamp();

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        [JsonPropertyOrder(4)]
        public string TenantGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// The GUID of the collection that was queried.
        /// </summary>
        [JsonPropertyOrder(5)]
        public string CollectionGUID { get; set; } = null;
         
        /// <summary>
        /// The search query performed.
        /// </summary>
        [JsonPropertyOrder(6)]
        public SearchQuery Query { get; set; } = null;

        /// <summary>
        /// Boolean indicating end of results.
        /// </summary>
        [JsonPropertyOrder(7)]
        public bool EndOfResults { get; set; } = true;

        /// <summary>
        /// Continuation token to use when continuing the search.
        /// </summary>
        [JsonPropertyOrder(8)]
        public string ContinuationToken { get; set; } = null;

        /// <summary>
        /// Number of candidate records remaining in the search.
        /// </summary>
        [JsonPropertyOrder(9)]
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
        /// Required query terms and search filters that are not able to be satisfied.
        /// </summary>
        [JsonPropertyOrder(10)]
        public QueryFilter NotSatisfiable { get; set; } = null;

        /// <summary>
        /// Documents that matched the query.
        /// </summary>
        [JsonPropertyOrder(999)]
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

        private List<string> _TermsNotFound = new List<string>();
        private List<MatchedDocument> _Documents = new List<MatchedDocument>();
        private long _RecordsRemaining = 0;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiates the object.
        /// </summary>
        public SearchResult()
        {
        }

        /// <summary>
        /// Instantiates the object.
        /// </summary>
        /// <param name="query">Search query.</param>
        public SearchResult(SearchQuery query)
        {
            Query = query;
        }

        #endregion

        #region Public-Methods
         
        /// <summary>
        /// Sort matches by score.
        /// </summary>
        public void SortMatchesByScore()
        {
            if (Documents == null || Documents.Count < 1) return;
            Documents = Documents.OrderByDescending(d => d.Score).ToList();
        }

        #endregion

        #region Private-Methods

        #endregion 
    }
}
