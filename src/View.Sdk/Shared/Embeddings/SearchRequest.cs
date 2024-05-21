namespace View.Sdk.Shared.Embeddings
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Search request.
    /// </summary>
    public class SearchRequest
    {
        #region Public-Members

        /// <summary>
        /// Search type.
        /// </summary>
        public SearchTypeEnum SearchType { get; set; } = SearchTypeEnum.Cosine;

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

        /// <summary>
        /// Dimensionality of embeddings.
        /// </summary>
        public int Dimensionality
        {
            get
            {
                return _Dimensionality;
            }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(Dimensionality));
                _Dimensionality = value;
            }
        }

        /// <summary>
        /// Starting index.
        /// </summary>
        public int StartIndex
        {
            get
            {
                return _StartIndex; 
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(StartIndex));
                _StartIndex = value;
            }
        }

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
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(MaxResults));
                _MaxResults = value;
            }
        }

        /// <summary>
        /// Embeddings.
        /// </summary>
        public List<decimal> Embeddings
        {
            get
            {
                return _Embeddings;
            }
            set
            {
                if (value == null) _Embeddings = new List<decimal>();
                else _Embeddings = value;
            }
        }

        #endregion

        #region Private-Members

        private int _VectorDatabasePort = 5432;
        private int _StartIndex = 0;
        private int _MaxResults = 100;
        private int _Dimensionality = 384;
        private List<decimal> _Embeddings = new List<decimal>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public SearchRequest()
        {
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
