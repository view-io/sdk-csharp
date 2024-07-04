namespace View.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using Timestamps;
    using View.Sdk;

    /// <summary>
    /// Object returned as the result of an enumeration against a table.
    /// </summary>
    public class EnumerationResult
    {
        #region Public-Members

        /// <summary>
        /// Indicates if the statistics operation was successful.
        /// </summary>
        public bool Success { get; set; } = false;

        /// <summary>
        /// Start and end timestamps.
        /// </summary>
        public Timestamp Timestamp { get; set; } = new Timestamp();

        /// <summary>
        /// Tenant metadata.
        /// </summary>
        public TenantMetadata Tenant { get; set; } = null;

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public string TenantGUID { get; set; } = null;

        /// <summary>
        /// Collection.
        /// </summary>
        public Collection Collection { get; set; } = null;

        /// <summary>
        /// The GUID of the collection that was queried.
        /// </summary>
        public string CollectionGUID { get; set; } = null;

        /// <summary>
        /// Bucket metadata.
        /// </summary>
        public BucketMetadata Bucket { get; set; } = null;

        /// <summary>
        /// Bucket GUID.
        /// </summary>
        public string BucketGUID { get; set; } = null;

        /// <summary>
        /// The enumeration query performed.
        /// </summary>
        public EnumerationQuery Query { get; set; } = null;

        /// <summary>
        /// Maximum number of keys to retrieve.
        /// </summary>
        public int MaxKeys
        {
            get
            {
                return _MaxKeys;
            }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(MaxKeys));
                _MaxKeys = value;
            }
        }

        /// <summary>
        /// Iterations required.
        /// </summary>
        public int IterationsRequired
        {
            get
            {
                return _IterationsRequired;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(IterationsRequired));
                _IterationsRequired = value;
            }
        }

        /// <summary>
        /// Object statistics.
        /// </summary>
        public ObjectStatistics Statistics { get; set; } = null;

        /// <summary>
        /// Prefix.
        /// </summary>
        public string Prefix { get; set; } = string.Empty;

        /// <summary>
        /// Marker.
        /// </summary>
        public string Marker { get; set; } = string.Empty;

        /// <summary>
        /// Delimiter.
        /// </summary>
        public string Delimiter { get; set; } = string.Empty;

        /// <summary>
        /// Token.
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Next token.
        /// </summary>
        public string NextToken { get; set; } = string.Empty;

        /// <summary>
        /// Shared prefixes.
        /// </summary>
        public List<string> SharedPrefixes { get; set; } = null;

        /// <summary>
        /// Object metadata.
        /// </summary>
        public List<ObjectMetadata> Objects { get; set; } = null;

        /// <summary>
        /// Delete markers.
        /// </summary>
        public List<ObjectMetadata> DeleteMarkers { get; set; } = null;

        /// <summary>
        /// Source documents that matched the query.
        /// </summary>
        public List<SourceDocument> SourceDocuments { get; set; } = null;

        /// <summary>
        /// Source documents that matched the query.
        /// </summary>
        public List<EmbeddingsDocument> EmbeddingsDocuments { get; set; } = null;

        /// <summary>
        /// Boolean indicating end of results.
        /// </summary>
        public bool EndOfResults { get; set; } = true;

        /// <summary>
        /// Continuation token to use when continuing the search.
        /// </summary>
        public string ContinuationToken { get; set; } = null;

        /// <summary>
        /// Number of candidate records remaining in the enumeration.
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

        #endregion

        #region Private-Members

        private int _MaxKeys = 1000;
        private int _IterationsRequired = 0;
        private long _RecordsRemaining = 0;
        private ObjectStatistics _Statistics = new ObjectStatistics();
        private List<ObjectMetadata> _Objects = new List<ObjectMetadata>();
        private List<ObjectMetadata> _DeleteMarkers = new List<ObjectMetadata>();
        private List<string> _SharedPrefixes = new List<string>();

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
