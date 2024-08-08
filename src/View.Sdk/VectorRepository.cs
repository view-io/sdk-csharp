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
        public string GUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public string TenantGUID { get; set; } = Guid.NewGuid().ToString();

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
        /// Prefix to prepend to language model prompts.
        /// </summary>
        public string PromptPrefix { get; set; } = 
            "Use the following pieces of context to answer the question at the end.  " +
            "Documents are sorted by similarity to the question.  " +
            "If the context is not enough for you to answer the question, " +
            "politely explain that you don't have relevant context.  " +
            "Do not try to make up an answer.";

        /// <summary>
        /// Suffix to append to language model prompts.
        /// </summary>
        public string PromptSuffix { get; set; } = null;

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
