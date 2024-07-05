namespace View.Sdk
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Webhook rule.
    /// </summary>
    public class WebhookRule
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
        /// Name.
        /// </summary>
        public string Name { get; set; } = null;

        /// <summary>
        /// Event type.
        /// </summary>
        public WebhookEventTypeEnum EventType { get; set; } = WebhookEventTypeEnum.Unknown;

        /// <summary>
        /// Maximum number of attempts.
        /// </summary>
        public int MaxAttempts
        {
            get
            {
                return _MaxAttempts;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(MaxAttempts));
                _MaxAttempts = value;
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
        /// Timestamp from creation, in UTC time.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        #endregion

        #region Private-Members

        private int _MaxAttempts = 10;
        private int _RetryIntervalMs = (30 * 1000); // 30 seconds
        private int _TimeoutMs = (60 * 1000); // 1 minute

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public WebhookRule()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
