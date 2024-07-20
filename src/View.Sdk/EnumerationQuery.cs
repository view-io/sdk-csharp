namespace View.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using Timestamps;

    /// <summary>
    /// Object used to enumerate a table.
    /// </summary>
    public class EnumerationQuery
    {
        #region Public-Members

        /// <summary>
        /// Timestamp.
        /// </summary>
        public Timestamp Timestamp { get; set; } = new Timestamp();

        /// <summary>
        /// Tenant.
        /// </summary>
        public TenantMetadata Tenant { get; set; } = null;

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public string TenantGUID { get; set; } = null;

        /// <summary>
        /// Bucket.
        /// </summary>
        public BucketMetadata Bucket { get; set; } = null;

        /// <summary>
        /// Bucket GUID.
        /// </summary>
        public string BucketGUID { get; set; } = null;

        /// <summary>
        /// Collection.
        /// </summary>
        public Collection Collection { get; set; } = null;

        /// <summary>
        /// Collection GUID.
        /// </summary>
        public string CollectionGUID { get; set; } = null;

        /// <summary>
        /// Source document.
        /// </summary>
        public SourceDocument SourceDocument { get; set; } = null;
        
        /// <summary>
        /// Source document GUID.
        /// </summary>
        public string SourceDocumentGUID { get; set; } = null;

        /// <summary>
        /// Vector repository.
        /// </summary>
        public VectorRepository VectorRepository { get; set; } = null;

        /// <summary>
        /// Vector repository GUID.
        /// </summary>
        public string VectorRepositoryGUID { get; set; } = null;

        /// <summary>
        /// Graph repository.
        /// </summary>
        public GraphRepository GraphRepository { get; set; } = null;

        /// <summary>
        /// Graph repository GUID.
        /// </summary>
        public string GraphRepositoryGUID { get; set; } = null;

        /// <summary>
        /// Graph node identifier.
        /// </summary>
        public string GraphNodeIdentifier { get; set; } = null;

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
                if (value < 1) throw new ArgumentException("MaxResults must be greater than zero.");
                if (value > 1000) throw new ArgumentException("MaxResults must be one thousand or less.");
                _MaxResults = value;
            }
        }

        /// <summary>
        /// Continuation token.
        /// </summary>
        public string ContinuationToken { get; set; } = null;

        /// <summary>
        /// Prefix.
        /// </summary>
        public string Prefix { get; set; } = null;

        /// <summary>
        /// Suffix.
        /// </summary>
        public string Suffix { get; set; } = null;

        /// <summary>
        /// Marker.
        /// </summary>
        public string Marker { get; set; } = null;

        /// <summary>
        /// Delimiter.
        /// </summary>
        public string Delimiter { get; set; } = string.Empty;

        /// <summary>
        /// Token.
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Include owners.
        /// </summary>
        public bool IncludeOwners { get; set; } = true; // S3 compatibility

        /// <summary>
        /// Search filters to apply to enumeration.
        /// </summary>
        public List<SearchFilter> Filters
        {
            get
            {
                return _Filters;
            }
            set
            {
                if (value == null)
                {
                    _Filters = new List<SearchFilter>();
                }
                else
                {
                    _Filters = value;
                }
            }
        }

        /// <summary>
        /// Order by.
        /// </summary>
        public EnumerationOrderEnum Ordering { get; set; } = EnumerationOrderEnum.CreatedDescending;

        #endregion

        #region Private-Members

        private int _MaxResults = 1000;
        private List<SearchFilter> _Filters = new List<SearchFilter>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public EnumerationQuery()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion 
    }
}
