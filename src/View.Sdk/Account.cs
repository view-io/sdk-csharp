namespace View.Sdk
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Account.
    /// </summary>
    public class Account
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
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(Id));
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
        /// Additional data.
        /// </summary>
        public string AdditionalData { get; set; } = null;

        /// <summary>
        /// Creation timestamp.
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
        public Account()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
