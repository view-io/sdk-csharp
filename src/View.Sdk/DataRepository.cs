namespace View.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Data repository.
    /// </summary>
    public class DataRepository
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
        /// Owner GUID.
        /// </summary>
        public Guid OwnerGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; } = "My file repository";

        /// <summary>
        /// Repository type.
        /// </summary>
        public DataRepositoryTypeEnum RepositoryType { get; set; } = DataRepositoryTypeEnum.File;

        /// <summary>
        /// Boolean flag to enable or disable SSL.
        /// </summary>
        public bool? UseSsl { get; set; } = null;

        /// <summary>
        /// Boolean to indicate whether or not subdirectories should be included.
        /// Only applicable to CIFS and NFS crawlers.
        /// </summary>
        public bool IncludeSubdirectories { get; set; } = true;

        #region Disk

        /// <summary>
        /// Disk directory.
        /// </summary>
        public string DiskDirectory
        {
            get
            {
                return _DiskDirectory;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    value = value.Replace("\\", "/");
                    if (!value.EndsWith("/")) value += "/";
                }
                _DiskDirectory = value;
            }
        }

        #endregion

        #region S3

        /// <summary>
        /// S3 endpoint URL, e.g. http://localhost:8000/
        /// </summary>
        public string S3EndpointUrl
        {
            get
            {
                return _S3EndpointUrl;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    value = value.Replace("\\", "/");
                    if (!value.EndsWith("/")) value += "/";
                }
                _S3EndpointUrl = value;
            }
        }

        /// <summary>
        /// S3 base URL.
        /// </summary>
        public string S3BaseUrl
        {
            get
            {
                return _S3BaseUrl;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    value = value.Replace("\\", "/");
                    if (!value.EndsWith("/")) value += "/";
                }
                _S3BaseUrl = value;
            }
        }

        /// <summary>
        /// S3 access key.
        /// </summary>
        public string S3AccessKey { get; set; } = null;

        /// <summary>
        /// S3 secret key.
        /// </summary>
        public string S3SecretKey { get; set; } = null;

        /// <summary>
        /// Bucket name.
        /// </summary>
        public string S3BucketName { get; set; } = null;

        /// <summary>
        /// S3 region.
        /// </summary>
        public string S3Region { get; set; } = null;

        #endregion

        #region Azure

        /// <summary>
        /// Azure endpoint URL, e.g. http://localhost:8000/
        /// </summary>
        public string AzureEndpointUrl
        {
            get
            {
                return _AzureEndpointUrl;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    value = value.Replace("\\", "/");
                    if (!value.EndsWith("/")) value += "/";
                }
                _AzureEndpointUrl = value;
            }
        }

        /// <summary>
        /// Azure account name.
        /// </summary>
        public string AzureAccountName { get; set; } = null;

        /// <summary>
        /// Azure container name.
        /// </summary>
        public string AzureContainerName { get; set; } = null;

        /// <summary>
        /// Azure access key.
        /// </summary>
        public string AzureAccessKey { get; set; } = null;

        #endregion

        #region CIFS

        /// <summary>
        /// CIFS hostname.
        /// </summary>
        public string CifsHostname { get; set; } = null;

        /// <summary>
        /// CIFS username.
        /// </summary>
        public string CifsUsername { get; set; } = null;

        /// <summary>
        /// CIFS password.
        /// </summary>
        public string CifsPassword { get; set; } = null;

        /// <summary>
        /// CIFS share name.
        /// </summary>
        public string CifsShareName { get; set; } = null;

        #endregion

        #region NFS

        /// <summary>
        /// NFS hostname.
        /// </summary>
        public string NfsHostname { get; set; } = null;

        /// <summary>
        /// NFS user ID.
        /// </summary>
        public int? NfsUserId
        {
            get
            {
                return _NfsUserId;
            }
            set
            {
                if (value != null && value.Value < 0) throw new ArgumentOutOfRangeException(nameof(NfsUserId));
                _NfsUserId = value;
            }
        }

        /// <summary>
        /// NFS group ID.
        /// </summary>
        public int? NfsGroupId
        {
            get
            {
                return _NfsGroupId;
            }
            set
            {
                if (value != null && value.Value < 0) throw new ArgumentOutOfRangeException(nameof(NfsGroupId));
                _NfsGroupId = value;
            }
        }

        /// <summary>
        /// NFS share name.
        /// </summary>
        public string NfsShareName { get; set; } = null;

        /// <summary>
        /// NFS version.
        /// </summary>
        public string NfsVersion
        {
            get
            {
                return _NfsVersion;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    if (!_ValidNfsVersions.Contains(value)) throw new ArgumentException("Invalid NFS version, use V2, V3, or V4.");
                }

                _NfsVersion = value;
            }
        }

        #endregion

        #region Web

        /// <summary>
        /// Web authentication type.
        /// </summary>
        WebAuthenticationTypeEnum WebAuthentication { get; set; } = WebAuthenticationTypeEnum.None;

        /// <summary>
        /// Username for web basic authentication.
        /// </summary>
        public string WebUsername { get; set; } = null;

        /// <summary>
        /// Password for web basic authentication.
        /// </summary>
        public string WebPassword { get; set; } = null;

        /// <summary>
        /// Header to use for attaching an API key to the web request.
        /// </summary>
        public string WebApiKeyHeader { get; set; } = null;

        /// <summary>
        /// Web API key to attach.
        /// </summary>
        public string WebApiKey { get; set; } = null;

        /// <summary>
        /// Bearer token to use in the authorization header.
        /// </summary>
        public string WebBearerToken { get; set; } = null;

        /// <summary>
        /// User agent to use while crawling.
        /// </summary>
        public string WebUserAgent { get; set; } = null;

        /// <summary>
        /// Starting URL for web crawling.
        /// </summary>
        public string WebStartUrl { get; set; } = null;

        /// <summary>
        /// Boolean indicating whether or not links should be followed.
        /// </summary>
        public bool? WebFollowLinks { get; set; } = null;

        /// <summary>
        /// Boolean indicating whether or not redirects should be followed.
        /// </summary>
        public bool? WebFollowRedirects { get; set; } = null;

        /// <summary>
        /// Boolean indicating whether or not the sitemap contents should be included as links.
        /// </summary>
        public bool? WebIncludeSitemap { get; set; } = null;

        /// <summary>
        /// Boolean indicating whether or not crawling should only consider child links.
        /// </summary>
        public bool? WebRestrictToChildUrls { get; set; } = null;

        /// <summary>
        /// Boolean indicating whether or not crawling should consider links within the same domain.
        /// </summary>
        public bool? WebRestrictToSameDomain { get; set; } = null;

        /// <summary>
        /// Maximum depth to crawl.
        /// </summary>
        public int? WebMaxDepth { get; set; } = null;

        /// <summary>
        /// Maximum number of parallel tasks to use while web crawling.
        /// </summary>
        public int? WebMaxParallelTasks { get; set; } = null;

        /// <summary>
        /// Delay in milliseconds to introduce between crawl operations within the same task.
        /// </summary>
        public int? WebCrawlDelayMs { get; set; } = null;

        #endregion

        /// <summary>
        /// Created.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        #endregion

        #region Private-Members

        private int _Id = 0;
        private string _DiskDirectory = null;
        private string _S3EndpointUrl = null;
        private string _S3BaseUrl = null;
        private string _AzureEndpointUrl = null;
        private int? _NfsUserId = null;
        private int? _NfsGroupId = null;
        private string _NfsVersion = null;

        private List<string> _ValidNfsVersions = new List<string>()
        {
            "V2",
            "V3",
            "V4"
        };

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public DataRepository()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
