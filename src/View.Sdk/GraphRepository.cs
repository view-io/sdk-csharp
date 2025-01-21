namespace View.Sdk
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Graph repository.
    /// </summary>
    public class GraphRepository
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
        public string Name { get; set; } = "My graph repository";

        /// <summary>
        /// Type of graph repository.
        /// </summary>
        public GraphRepositoryTypeEnum RepositoryType { get; set; } = GraphRepositoryTypeEnum.LiteGraph;

        /// <summary>
        /// Endpoint URL.
        /// </summary>
        public string EndpointUrl { get; set; } = null;

        /// <summary>
        /// API key.
        /// </summary>
        public string ApiKey { get; set; } = null;

        /// <summary>
        /// Username.
        /// </summary>
        public string Username { get; set; } = null;

        /// <summary>
        /// Password.
        /// </summary>
        public string Password { get; set; } = null;

        /// <summary>
        /// Hostname.
        /// </summary>
        public string Hostname { get; set; } = null;

        /// <summary>
        /// Port.
        /// </summary>
        public int Port
        {
            get
            {
                return _Port;
            }
            set
            {
                if (value < 0 || value > 65535) throw new ArgumentOutOfRangeException(nameof(Port));
                _Port = value;
            }
        }

        /// <summary>
        /// Enable or disable SSL.
        /// </summary>
        public bool Ssl { get; set; } = false;

        /// <summary>
        /// Graph identifier.
        /// </summary>
        public string GraphIdentifier { get; set; } = null;

        /// <summary>
        /// Creation timestamp, in UTC time.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        #endregion

        #region Private-Members

        private int _Port = 0;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public GraphRepository()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
