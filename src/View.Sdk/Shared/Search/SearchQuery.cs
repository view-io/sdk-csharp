namespace View.Sdk.Shared.Search
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Object used to search a collection.
    /// </summary>
    public class SearchQuery
    {
        #region Public-Members

        /// <summary>
        /// The GUID of the search operation.
        /// </summary>
        [JsonPropertyOrder(1)]
        public string GUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        [JsonPropertyOrder(2)]
        public string TenantGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Collection GUID.
        /// </summary>
        [JsonPropertyOrder(3)]
        public string CollectionGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Maximum number of results to retrieve.
        /// </summary>
        [JsonPropertyOrder(4)]
        public int MaxResults
        {
            get
            {
                return _MaxResults;
            }
            set
            {
                if (value < 1) throw new ArgumentException("MaxResults must be greater than zero.");
                if (value > 100) throw new ArgumentException("MaxResults must be one hundred or less.");
                _MaxResults = value;
            }
        }

        /// <summary>
        /// Continuation token.
        /// </summary>
        [JsonPropertyOrder(5)]
        public string ContinuationToken { get; set; } = null;

        /// <summary>
        /// Required terms and search filter that must be satisfied to include a document in the results.
        /// </summary>
        [JsonPropertyOrder(6)]
        public QueryFilter Required
        {
            get
            {
                return _Required;
            }
            set
            {
                if (value == null) _Required = new QueryFilter();
                else _Required = value;
            }
        }

        /// <summary>
        /// Optional terms and search filter that may match on documents but are not required.
        /// </summary>
        [JsonPropertyOrder(7)]
        public QueryFilter Optional
        {
            get
            {
                return _Optional;
            }
            set
            {
                if (value == null) _Optional = new QueryFilter();
                else _Optional = value;
            }
        }

        /// <summary>
        /// Terms and search filter that must be excluded from the results.
        /// </summary>
        [JsonPropertyOrder(8)]
        public QueryFilter Exclude
        {
            get
            {
                return _Exclude;
            }
            set
            {
                if (value == null) _Exclude = new QueryFilter();
                else _Exclude = value;
            }
        }

        /// <summary>
        /// Order by.
        /// </summary>
        [JsonPropertyOrder(9)]
        public OrderByEnum OrderBy { get; set; } = OrderByEnum.CreatedDescending;

        #endregion

        #region Private-Members

        private int _MaxResults = 10;

        private QueryFilter _Required = new QueryFilter();
        private QueryFilter _Optional = new QueryFilter();
        private QueryFilter _Exclude = new QueryFilter();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public SearchQuery()
        {
            GUID = Guid.NewGuid().ToString();
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
