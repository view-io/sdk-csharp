namespace View.Sdk
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// View storage server endpoint.
    /// </summary>
    public class ViewEndpoint
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
        /// Owner GUID.
        /// </summary>
        public string OwnerGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; } = "My View endpoint";

        /// <summary>
        /// Boolean flag to enable or disable SSL.
        /// </summary>
        public bool UseSsl { get; set; } = false;

        /// <summary>
        /// S3 URL.
        /// </summary>
        public string S3Url
        {
            get
            {
                return _S3Url;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(S3Url));
                Uri uri = new Uri(value);
                if (!value.EndsWith("/")) value += "/";
                _S3Url = value;
            }
        }

        /// <summary>
        /// S3 URI.
        /// </summary>
        [JsonIgnore]
        public Uri S3Uri
        {
            get
            {
                return new Uri(S3Url);
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(S3Uri));
                S3Url = S3Uri.ToString();
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
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(S3BaseUrl));
                if (!value.Contains("{bucket}")) throw new ArgumentException("Supplied S3 base URL is missing {bucket} from URL");
                if (!value.Contains("{key}")) throw new ArgumentException("Supplied S3 base URL is missing {key} from URL");

                Uri uri = new Uri(value);
                if (!value.EndsWith("/")) value += "/";
                _S3BaseUrl = value;
            }
        }

        /// <summary>
        /// REST URL.
        /// </summary>
        public string RestUrl
        {
            get
            {
                return _RestUrl;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(RestUrl));
                Uri uri = new Uri(value);
                if (!value.EndsWith("/")) value += "/";
                _RestUrl = value;
            }
        }

        /// <summary>
        /// REST URI.
        /// </summary>
        [JsonIgnore]
        public Uri RestUri
        {
            get
            {
                return new Uri(RestUrl);
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(RestUri));
                RestUrl = RestUri.ToString();
            }
        }

        /// <summary>
        /// Bucket name.
        /// </summary>
        public string BucketName { get; set; } = "data";

        /// <summary>
        /// Region.
        /// </summary>
        public string Region { get; set; } = "us-west-1";

        /// <summary>
        /// Access key.
        /// </summary>
        public string AccessKey { get; set; } = null;

        /// <summary>
        /// Secret key.
        /// </summary>
        public string SecretKey { get; set; } = null;

        /// <summary>
        /// API key.
        /// </summary>
        public string ApiKey { get; set; } = null;

        /// <summary>
        /// Created.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        #endregion

        #region Private-Members

        private string _S3Url = "http://localhost:8002/";
        private string _S3BaseUrl = "http://localhost:8002/{bucket}/{key}";
        private string _RestUrl = "http://localhost:8001/";

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public ViewEndpoint()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
