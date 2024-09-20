namespace View.Sdk
{
    using System;
    using System.Text.Json.Serialization;
 
    /// <summary>
    /// Binary large object (BLOB).
    /// </summary>
    public class Blob
    {
        #region Public-Members

        /// <summary>
        /// Database row ID.
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
        /// Globally-unique identifier.
        /// </summary>
        [JsonPropertyOrder(-1)]
        public string GUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public string TenantGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Content type.
        /// </summary>
        public string ContentType { get; set; } = null;

        /// <summary>
        /// Name of the BLOB.
        /// </summary>
        public string Name { get; set; } = null;

        /// <summary>
        /// Description.
        /// </summary>
        public string Description { get; set; } = null;

        /// <summary>
        /// URL to access the BLOB directly.
        /// </summary>
        public string Url { get; set; } = null;

        /// <summary>
        /// Content length of the BLOB.
        /// </summary>
        public long Length { get; set; } = 0;

        /// <summary>
        /// Object type to which this BLOB refers.
        /// </summary>
        public string RefObjType { get; set; } = null;

        /// <summary>
        /// Globally-unique identifier the object to which this BLOB refers.
        /// </summary>
        public string RefObjGUID { get; set; } = null;

        /// <summary>
        /// MD5.
        /// </summary>
        public string MD5Hash { get; set; } = string.Empty;

        /// <summary>
        /// SHA1.
        /// </summary>
        public string SHA1Hash { get; set; } = null;

        /// <summary>
        /// SHA256.
        /// </summary>
        public string SHA256Hash { get; set; } = null;

        /// <summary>
        /// Timestamp from creation in UTC time.
        /// </summary>
        [JsonPropertyOrder(990)]
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// BLOB data.
        /// </summary>
        [JsonPropertyOrder(991)]
        public byte[] Data { get; set; } = null;

        #endregion

        #region Private-Members

        private int _Id = 0;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate the object.
        /// </summary>
        public Blob()
        {
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}