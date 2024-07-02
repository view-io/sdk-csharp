namespace View.Sdk
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Embeddings rule.
    /// </summary>
    public class EmbeddingsRule
    {
        #region Public-Members

        /// <summary>
        /// GUID.
        /// </summary>
        [JsonPropertyOrder(-1)]
        public string GUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public string TenantGUID { get; set; } = null;

        /// <summary>
        /// Bucket GUID.
        /// </summary>
        public string BucketGUID { get; set; } = null;

        /// <summary>
        /// Owner GUID.
        /// </summary>
        public string OwnerGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; } = null;

        /// <summary>
        /// Content-type.
        /// </summary>
        public string ContentType { get; set; } = "text/plain";

        /// <summary>
        /// Prefix.
        /// </summary>
        public string Prefix { get; set; } = null;

        /// <summary>
        /// Suffix.
        /// </summary>
        public string Suffix { get; set; } = null;

        /// <summary>
        /// Target bucket GUID.
        /// </summary>
        public string TargetBucketGUID { get; set; } = null;

        /// <summary>
        /// Embeddings generator.
        /// </summary>
        public EmbeddingsGeneratorEnum EmbeddingsGenerator { get; set; } = EmbeddingsGeneratorEnum.LCProxy;

        /// <summary>
        /// Embeddings generator URL.
        /// </summary>
        public string GeneratorUrl { get; set; } = "http://localhost:8301/";

        /// <summary>
        /// Embeddings provider API key.
        /// </summary>
        public string GeneratorApiKey { get; set; } = null;

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
        /// Data flow endpoint.
        /// </summary>
        public string DataFlowEndpoint { get; set; } = "http://localhost:8501/processor";

        /// <summary>
        /// Vector repository type.
        /// </summary>
        public RepositoryTypeEnum VectorRepositoryType { get; set; } = RepositoryTypeEnum.Pgvector;

        /// <summary>
        /// Vector store URL.
        /// </summary>
        public string VectorStoreUrl { get; set; } = "http://localhost:8311/";

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
        /// Maximum content length.
        /// </summary>
        public int MaxContentLength
        {
            get
            {
                return _MaxContentLength;
            }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(MaxContentLength));
                _MaxContentLength = value;
            }
        }

        /// <summary>
        /// The number of minutes to retain the generated document.
        /// </summary>
        public int? RetentionMinutes
        {
            get
            {
                return _RetentionMinutes;
            }
            set
            {
                if (value != null && value.Value < 1) throw new ArgumentOutOfRangeException(nameof(RetentionMinutes));
                _RetentionMinutes = value;
            }
        }

        /// <summary>
        /// Creation timestamp.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        #endregion

        #region Private-Members

        private int? _RetentionMinutes = null;
        private int _Dimensionality = 384;
        private int _VectorDatabasePort = 5432;
        private int _MaxContentLength = 16 * 1024 * 1024;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate the object.
        /// </summary>
        public EmbeddingsRule()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Members

        #endregion
    }
}