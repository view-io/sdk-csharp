namespace View.Sdk
{
    using System;
    using System.Reflection.Emit;
    using System.Security.Principal;
    using System.Text;
    using View.Sdk.Serialization;

    /// <summary>
    /// Authentication token details.
    /// </summary>
    public class AuthenticationToken
    {
        #region Public-Members

        /// <summary>
        /// Timestamp when the token was issued, in UTC time.
        /// </summary>
        public DateTime TimestampUtc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Timestamp when the token will expire, in UTC time.
        /// </summary>
        public DateTime ExpirationUtc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Boolean to indicate if the token is expired.
        /// </summary>
        public bool IsExpired
        {
            get
            {
                return TimestampUtc > ExpirationUtc;
            }
        }

        /// <summary>
        /// Administrator.
        /// </summary>
        public Administrator Administrator { get; set; } = null;

        /// <summary>
        /// Tenant.
        /// </summary>
        public TenantMetadata Tenant { get; set; } = null;

        /// <summary>
        /// User.
        /// </summary>
        public UserMaster User { get; set; } = null;

        /// <summary>
        /// Token.
        /// </summary>
        public string Token { get; set; } = null;

        /// <summary>
        /// Boolean indicating whether or not the token is valid.
        /// </summary>
        public bool Valid { get; set; } = true;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public AuthenticationToken()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
