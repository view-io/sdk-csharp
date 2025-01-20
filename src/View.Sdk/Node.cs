namespace View.Sdk
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Node.
    /// </summary>
    public class Node
    {
        #region Public-Members

        /// <summary>
        /// ID.
        /// </summary>
        [JsonIgnore]
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(Id));
                _Id = value;
            }
        }

        /// <summary>
        /// GUID.
        /// </summary>
        public Guid GUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; } = null;

        /// <summary>
        /// Hostname.
        /// </summary>
        public string Hostname { get; set; } = "localhost";

        /// <summary>
        /// Software instance type.
        /// </summary>
        public SoftwareInstanceTypeEnum InstanceType { get; set; } = SoftwareInstanceTypeEnum.StorageServer;

        /// <summary>
        /// Last start timestamp, in UTC time.
        /// </summary>
        public DateTime LastStartUtc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Creation timestamp, in UTC time.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        #endregion

        #region Private-Members

        private int _Id = 0;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public Node()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
