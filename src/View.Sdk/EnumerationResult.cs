namespace View.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using Timestamps;

    /// <summary>
    /// Object returned as the result of an enumeration against a table.
    /// </summary>
    public class EnumerationResult
    {
        #region Public-Members

        /// <summary>
        /// Indicates if the statistics operation was successful.
        /// </summary>
        [JsonPropertyOrder(1)]
        public bool Success { get; set; } = false;

        /// <summary>
        /// Exception.
        /// </summary>
        [JsonPropertyOrder(2)]
        public Exception Exception { get; set; } = null;

        /// <summary>
        /// Start and end timestamps.
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
        /// The enumeration query performed.
        /// </summary>
        [JsonPropertyOrder(6)]
        public EnumerationQuery Query { get; set; } = null;

        /// <summary>
        /// Source documents that matched the query.
        /// </summary>
        [JsonPropertyOrder(7)]
        public List<SourceDocument> SourceDocuments
        {
            get
            {
                return _SourceDocuments;
            }
            set
            {
                if (value == null) _SourceDocuments = new List<SourceDocument>();
                else _SourceDocuments = value;
            }
        }

        /// <summary>
        /// Source documents that matched the query.
        /// </summary>
        [JsonPropertyOrder(8)]
        public List<EmbeddingsDocument> EmbeddingsDocuments
        {
            get
            {
                return _EmbeddingsDocuments;
            }
            set
            {
                if (value == null) _EmbeddingsDocuments = new List<EmbeddingsDocument>();
                else _EmbeddingsDocuments = value;
            }
        }

        /// <summary>
        /// Boolean indicating end of results.
        /// </summary>
        [JsonPropertyOrder(9)]
        public bool EndOfResults { get; set; } = true;

        /// <summary>
        /// Continuation token to use when continuing the search.
        /// </summary>
        [JsonPropertyOrder(10)]
        public string ContinuationToken { get; set; } = null;

        /// <summary>
        /// Number of candidate records remaining in the enumeration.
        /// </summary>
        [JsonPropertyOrder(11)]
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

        #endregion

        #region Private-Members

        private List<SourceDocument> _SourceDocuments = new List<SourceDocument>();
        private List<EmbeddingsDocument> _EmbeddingsDocuments = new List<EmbeddingsDocument>();
        private long _RecordsRemaining = 0;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiates the object.
        /// </summary>
        public EnumerationResult()
        {
        }

        /// <summary>
        /// Instantiates the object.
        /// </summary>
        /// <param name="query">Enumeration query.</param>
        public EnumerationResult(EnumerationQuery query)
        {
            Query = query;
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion 
    }
}
