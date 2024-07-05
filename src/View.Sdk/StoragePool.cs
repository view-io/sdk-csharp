namespace View.Sdk
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Storage pool.
    /// </summary>
    public class StoragePool
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
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(Id));
                _Id = value;
            }
        }

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
        /// Encryption key.
        /// </summary>
        public string EncryptionKeyGUID { get; set; } = null;

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; } = null;

        /// <summary>
        /// Provider.
        /// </summary>
        public string Provider { get; set; } = "Disk";

        /// <summary>
        /// Object key write mode.
        /// </summary>
        public ObjectWriteModeEnum WriteMode { get; set; } = ObjectWriteModeEnum.GUID;

        /// <summary>
        /// Enable or disable SSL.
        /// </summary>
        public bool UseSsl { get; set; } = false;

        /// <summary>
        /// Endpoint URL for the storage pool provider.
        /// This value should be of the form [protocol]://[hostname]:[port]/ where [protocol] is either http or https.
        /// This value is required for both Azure and any AWS S3 compatible storage systems (such as Minio).
        /// </summary> 
        public string Endpoint { get; set; } = null;

        /// <summary>
        /// Access key.
        /// </summary>
        public string AccessKey { get; set; } = null;

        /// <summary>
        /// Secret key.
        /// </summary>
        public string SecretKey { get; set; } = null;

        /// <summary>
        /// AWS region.
        /// </summary>
        public string AwsRegion { get; set; } = null;

        /// <summary>
        /// AWS bucket.
        /// </summary>
        public string AwsBucket { get; set; } = null;

        /// <summary>
        /// Base URL for AWS S3 compatible storage platforms.  
        /// This value should be of the form '.hostname.com' to identify 'bucket' as the bucket in 'bucket.hostname.com'.
        /// </summary>
        public string AwsBaseDomain { get; set; } = null;

        /// <summary>
        /// Base URL to use for objects, i.e. https://[bucketname].s3.[regionname].amazonaws.com/.
        /// For non-S3 endpoints, use {bucket} and {key} to indicate where these values should be inserted, i.e. http://{bucket}.[hostname]:[port]/{key} or https://[hostname]:[port]/{bucket}/key.
        /// </summary>
        public string AwsBaseUrl { get; set; } = null;

        /// <summary>
        /// Disk directory.
        /// </summary>
        public string DiskDirectory { get; set; } = null;

        /// <summary>
        /// Azure account.
        /// </summary>
        public string AzureAccount { get; set; } = null;

        /// <summary>
        /// Azure container.
        /// </summary>
        public string AzureContainer { get; set; } = null;

        /// <summary>
        /// Compression type.
        /// </summary>
        public CompressionTypeEnum Compress { get; set; } = CompressionTypeEnum.None;

        /// <summary>
        /// Flag to enable or disable read caching.
        /// </summary>
        public bool EnableReadCaching { get; set; } = false;

        /// <summary>
        /// Creation timestamp, in UTC.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        #endregion

        #region Private-Members

        private int _Id = 0;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate the object.
        /// </summary>
        public StoragePool()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Members

        #endregion
    }
}