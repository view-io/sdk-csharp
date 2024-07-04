namespace View.Sdk
{
    using System;
    using System.Security.Cryptography;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Bucket metadata.
    /// </summary>
    public class BucketMetadata
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
        public string GUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public string TenantGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Pool GUID.
        /// </summary>
        public string PoolGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Owner GUID.
        /// </summary>
        public string OwnerGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Bucket category.
        /// </summary>
        public BucketCategoryEnum Category { get; set; } = BucketCategoryEnum.Data;

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Region string.
        /// </summary>
        public string RegionString { get; set; } = "us-west-1";

        /// <summary>
        /// Enable or disable versioning.
        /// </summary>
        public bool Versioning { get; set; } = true;

        /// <summary>
        /// Retention, in minutes.
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
        /// Maximum upload size.
        /// </summary>
        public long? MaxUploadSize { get; set; } = null;

        /// <summary>
        /// Maximum multipart upload seconds.
        /// </summary>
        public int MaxMultipartUploadSeconds { get; set; } = (60 * 60 * 24 * 7); // seven days

        /// <summary>
        /// Last access, UTC time.
        /// </summary>
        public DateTime LastAccessUtc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Created, UTC time.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Owner.
        /// </summary>
        public UserMaster Owner { get; set; } = null;

        #endregion

        #region Private-Members

        private int _Id = 0;
        private int? _RetentionMinutes = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public BucketMetadata()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}