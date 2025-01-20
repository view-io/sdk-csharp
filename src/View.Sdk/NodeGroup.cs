namespace View.Models
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Node group.
    /// </summary>
    public class NodeGroup
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
        /// Tenant GUID.
        /// </summary>
        public Guid TenantGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Node GUID.
        /// </summary>
        public Guid NodeGUID { get; set; } = Guid.NewGuid();

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
        public NodeGroup()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
