namespace View.Sdk
{
    using System;
    using System.Security.Cryptography;
    using System.Text.Json.Serialization;
    using View.Sdk;

    /// <summary>
    /// Vector repository.
    /// </summary>
    public class VectorRepository
    {
        #region Public-Members

        /// <summary>
        /// GUID.
        /// </summary>
        public Guid GUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public Guid TenantGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; } = "My vector repository";

        /// <summary>
        /// Type of vector repository.
        /// </summary>
        public VectorRepositoryTypeEnum RepositoryType { get; set; } = VectorRepositoryTypeEnum.Pgvector;

        /// <summary>
        /// Endpoint URL.
        /// </summary>
        public string EndpointUrl { get; set; } = null;

        /// <summary>
        /// API key.
        /// </summary>
        public string ApiKey { get; set; } = null;

        /// <summary>
        /// Embeddings model.
        /// </summary>
        public string Model { get; set; } = "all-MiniLM-L6-v2";

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
        /// Database hostname.
        /// </summary>
        public string DatabaseHostname { get; set; } = null;

        /// <summary>
        /// Database name.
        /// </summary>
        public string DatabaseName { get; set; } = null;

        /// <summary>
        /// Schema name.
        /// </summary>
        public string SchemaName { get; set; } = "public";

        /// <summary>
        /// Database table name.
        /// </summary>
        public string DatabaseTable { get; set; } = null;

        /// <summary>
        /// Database port.
        /// </summary>
        public int DatabasePort
        {
            get
            {
                return _DatabasePort;
            }
            set
            {
                if (value < 0 || value > 65535) throw new ArgumentOutOfRangeException(nameof(DatabasePort));
                _DatabasePort = value;
            }
        }

        /// <summary>
        /// Database user.
        /// </summary>
        public string DatabaseUser { get; set; } = null;

        /// <summary>
        /// Database password.
        /// </summary>
        public string DatabasePassword { get; set; } = null;

        /// <summary>
        /// Creation timestamp, in UTC time.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        #endregion

        #region Private-Members

        private int _Dimensionality = 384;
        private int _DatabasePort = 0;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public VectorRepository()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
