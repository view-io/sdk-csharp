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

        /// <summary>
        /// Vector repository type.
        /// </summary>
        public RepositoryTypeEnum VectorRepositoryType { get; set; } = RepositoryTypeEnum.Pgvector;

        /// <summary>
        /// Vector database hostname.
        /// </summary>
        public string VectorDatabaseHostname { get; set; } = null;

        /// <summary>
        /// Vector database name.
        /// </summary>
        public string VectorDatabaseName { get; set; } = null;

        /// <summary>
        /// Vector database table name.
        /// </summary>
        public string VectorDatabaseTable { get; set; } = null;

        /// <summary>
        /// Vector database port.
        /// </summary>
        public int VectorDatabasePort
        {
            get
            {
                return _VectorDatabasePort;
            }
            set
            {
                if (value < 0 || value > 65535) throw new ArgumentOutOfRangeException(nameof(VectorDatabasePort));
                _VectorDatabasePort = value;
            }
        }

        /// <summary>
        /// Vector database user.
        /// </summary>
        public string VectorDatabaseUser { get; set; } = null;

        /// <summary>
        /// Vector database password.
        /// </summary>
        public string VectorDatabasePassword { get; set; } = null;

        #endregion

        #region Private-Members

        private int _MaxResults = 1000;
        private int _VectorDatabasePort = 5432;

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
