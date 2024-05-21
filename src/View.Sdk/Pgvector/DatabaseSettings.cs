namespace View.Sdk.Pgvector
{
    using System;
    using View.Sdk.Shared.Embeddings;

    /// <summary>
    /// Database settings.
    /// </summary>
    public class DatabaseSettings
    {
        #region Public-Members

        /// <summary>
        /// Model.
        /// </summary>
        public string Model { get; set; } = "all-MiniLM-L6-v2";

        /// <summary>
        /// Dimensionality.
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
        /// Vector repository type.
        /// </summary>
        public RepositoryTypeEnum VectorRepositoryType { get; set; } = RepositoryTypeEnum.Pgvector;

        /// <summary>
        /// Vector database hostname.
        /// </summary>
        public string VectorDatabaseHostname { get; set; } = "localhost";

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
        /// Vector database name.
        /// </summary>
        public string VectorDatabaseName { get; set; } = "vectordb";

        /// <summary>
        /// Vector database table.
        /// </summary>
        public string VectorDatabaseTable { get; set; } = "vectors";

        /// <summary>
        /// Vector database user.
        /// </summary>
        public string VectorDatabaseUser { get; set; } = "postgres";

        /// <summary>
        /// Vector database password.
        /// </summary>
        public string VectorDatabasePassword { get; set; } = "password";
         
        #endregion

        #region Private-Members

        private int _Dimensionality = 384;
        private int _VectorDatabasePort = 5432;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public DatabaseSettings()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
