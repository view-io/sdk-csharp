﻿namespace View.Sdk
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
        /// Administrator GUID.
        /// </summary>
        public string AdministratorGUID { get; set; } = null;

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public string TenantGUID { get; set; } = null;

        /// <summary>
        /// User.
        /// </summary>
        public string UserGUID { get; set; } = null;

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
