namespace View.Sdk
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Webhook target.
    /// </summary>
    public class WebhookTarget
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
        public string Name { get; set; } = "My webhook target";

        /// <summary>
        /// URL.
        /// </summary>
        public string Url
        {
            get
            {
                return _Url;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(Url));
                _Uri = new Uri(value);
                _Url = value;
            }
        }

        /// <summary>
        /// URI.
        /// </summary>
        [JsonIgnore]
        public Uri Uri
        {
            get
            {
                return _Uri;
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(Uri));
                _Uri = value;
                _Url = _Uri.ToString();
            }
        }

        /// <summary>
        /// Content type.
        /// </summary>
        public string ContentType
        {
            get
            {
                return _ContentType;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(ContentType));
                _ContentType = value;
            }
        }

        /// <summary>
        /// HTTP status to expect for the request to be considered successful.
        /// </summary>
        public int ExpectStatus
        {
            get
            {
                return _ExpectStatus;
            }
            set
            {
                if (value < 100 || value > 599) throw new ArgumentOutOfRangeException(nameof(ExpectStatus));
                _ExpectStatus = value;
            }
        }

        /// <summary>
        /// Timestamp from creation, in UTC time.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        #endregion

        #region Private-Members

        private string _Url = null;
        private Uri _Uri = null;
        private string _ContentType = "application/json";
        private int _ExpectStatus = 200;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public WebhookTarget()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
