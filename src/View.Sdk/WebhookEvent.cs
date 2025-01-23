namespace View.Sdk
{
    using System;
    using System.Security.Cryptography;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Webhook event.
    /// </summary>
    public class WebhookEvent
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
        /// Target GUID.
        /// </summary>
        public string TargetGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Rule GUID.
        /// </summary>
        public string RuleGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Event type.
        /// </summary>
        public WebhookEventTypeEnum EventType { get; set; } = WebhookEventTypeEnum.Unknown;

        /// <summary>
        /// Content length.
        /// </summary>
        public int ContentLength
        {
            get
            {
                return _ContentLength;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(ContentLength));
                _ContentLength = value;
            }
        }

        /// <summary>
        /// Timeout in milliseconds.
        /// </summary>
        public int TimeoutMs
        {
            get
            {
                return _TimeoutMs;
            }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(TimeoutMs));
                _TimeoutMs = value;
            }
        }

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
        /// HTTP status to expect on success.
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
        /// Retry interval in milliseconds.
        /// </summary>
        public int RetryIntervalMs
        {
            get
            {
                return _RetryIntervalMs;
            }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(RetryIntervalMs));
                _RetryIntervalMs = value;
            }
        }

        /// <summary>
        /// Attempt number.
        /// </summary>
        public int Attempt
        {
            get
            {
                return _Attempt;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(Attempt));
                _Attempt = value;
            }
        }

        /// <summary>
        /// Maximum attempts.
        /// </summary>
        public int MaxAttempts
        {
            get
            {
                return _MaxAttempts;
            }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(MaxAttempts));
                _MaxAttempts = value;
            }
        }

        /// <summary>
        /// HTTP status last received.
        /// </summary>
        public int HttpStatus
        {
            get
            {
                return _HttpStatus;
            }
            set
            {
                if (value < 0 || value > 599) throw new ArgumentOutOfRangeException(nameof(HttpStatus));
                _HttpStatus = value;
            }
        }

        /// <summary>
        /// Timestamp from creation, in UTC time.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Timestamp when added, UTC.
        /// </summary>
        public DateTime AddedUtc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Timestamp when last attempted, UTC.
        /// </summary>
        public DateTime? LastAttemptUtc { get; set; } = null;

        /// <summary>
        /// Timestamp for next attempt, UTC.
        /// </summary>
        public DateTime? NextAttemptUtc { get; set; } = null;

        /// <summary>
        /// Timestamp for last failure, UTC.
        /// </summary>
        public DateTime? LastFailureUtc { get; set; } = null;

        /// <summary>
        /// Timestamp for success, UTC.
        /// </summary>
        public DateTime? SuccessUtc { get; set; } = null;

        /// <summary>
        /// Timestamp for failed, UTC.
        /// </summary>
        public DateTime? FailedUtc { get; set; } = null;

        #endregion

        #region Private-Members

        private string _Url = null;
        private Uri _Uri = null;
        private string _ContentType = "application/json";
        private int _ContentLength = 0;
        private int _ExpectStatus = 200;
        private int _RetryIntervalMs = 10000;
        private int _HttpStatus = 0;
        private int _Attempt = 0;
        private int _MaxAttempts = 5;
        private int _TimeoutMs = (60 * 1000); // 1 minute

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public WebhookEvent()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
