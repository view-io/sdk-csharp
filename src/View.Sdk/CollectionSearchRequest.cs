﻿namespace View.Sdk
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Object used to search a collection.
    /// </summary>
    public class CollectionSearchRequest
    {
        #region Public-Members

        /// <summary>
        /// The GUID of the search operation.
        /// </summary>
        [JsonPropertyOrder(1)]
        public Guid? GUID { get; set; } = null;

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        [JsonPropertyOrder(2)]
        public Guid? TenantGUID { get; set; } = null;

        /// <summary>
        /// Collection GUID.
        /// </summary>
        [JsonPropertyOrder(3)]
        public Guid? CollectionGUID { get; set; } = null;

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
        /// Number of results to skip.
        /// </summary>
        public int Skip
        {
            get
            {
                return _Skip;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(Skip));
                _Skip = value;
            }
        }

        /// <summary>
        /// Continuation token.
        /// </summary>
        public string ContinuationToken { get; set; } = null;

        /// <summary>
        /// Ordering.
        /// </summary>
        public EnumerationOrderEnum Ordering { get; set; } = EnumerationOrderEnum.CreatedDescending;

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
        private int _Skip = 0;
        private QueryFilter _Filter = new QueryFilter();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public CollectionSearchRequest()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
