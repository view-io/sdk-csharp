namespace View.Sdk.Shared.Embeddings
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization; 

    /// <summary>
    /// Object used to enumerate a table.
    /// </summary>
    public class EnumerationQuery
    {
        #region Public-Members

        /// <summary>
        /// The GUID of the enumeration operation.
        /// </summary>
        [JsonPropertyOrder(1)]
        public string GUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        [JsonPropertyOrder(2)]
        public string TenantGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// The GUID of the collection to query.
        /// </summary>
        [JsonPropertyOrder(3)]
        public string CollectionGUID { get; set; } = null;

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
                if (value > 1000) throw new ArgumentException("MaxResults must be one thousand or less.");
                _MaxResults = value;
            }
        }

        /// <summary>
        /// Continuation token.
        /// </summary>
        [JsonPropertyOrder(5)]
        public string ContinuationToken { get; set; } = null;

        /// <summary>
        /// Order by.
        /// </summary>
        [JsonPropertyOrder(7)]
        public OrderByEnum OrderBy { get; set; } = OrderByEnum.CreatedDescending;

        #endregion

        #region Private-Members

        private int _MaxResults = 1000;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public EnumerationQuery()
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
