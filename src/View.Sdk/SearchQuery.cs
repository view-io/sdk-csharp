namespace View.Sdk
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
        public string ContinuationToken { get; set; } = null;

        /// <summary>
        /// Order by.
        /// </summary>
        public OrderByEnum OrderBy { get; set; } = OrderByEnum.CreatedDescending;

        /// <summary>
        /// Required terms and search filter that must be satisfied to include a document in the results.
        /// </summary>
        public QueryFilter Filter
        {
            get
            {
                return _Filter;
            }
            set
            {
                if (value == null) _Filter = new QueryFilter();
                else _Filter = value;
            }
        }

        /// <summary>
        /// Embeddings rule.
        /// </summary>
        public EmbeddingsRule EmbeddingsRule { get; set; } = null;

        #endregion

        #region Private-Members

        private int _MaxResults = 10;

        private QueryFilter _Filter = new QueryFilter();

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
