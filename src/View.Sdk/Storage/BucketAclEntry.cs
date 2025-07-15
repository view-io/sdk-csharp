namespace View.Sdk.Storage
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Bucket access control list entry.
    /// </summary>
    public class BucketAclEntry
    {
        #region Public-Members

        /// <summary>
        /// GUID.
        /// </summary>
        public string GUID { get; set; } = string.Empty;

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public string TenantGUID { get; set; } = string.Empty;

        /// <summary>
        /// Bucket GUID.
        /// </summary>
        public string BucketGUID { get; set; } = string.Empty;

        /// <summary>
        /// Owner GUID.
        /// </summary>
        public string OwnerGUID { get; set; } = string.Empty;

        /// <summary>
        /// User GUID.
        /// </summary>
        public string UserGUID { get; set; } = string.Empty;

        /// <summary>
        /// Canonical user.
        /// </summary>
        public string CanonicalUser { get; set; } = string.Empty;

        /// <summary>
        /// Enable read permission.
        /// </summary>
        public bool EnableRead { get; set; } = false;

        /// <summary>
        /// Enable read ACP permission.
        /// </summary>
        public bool EnableReadAcp { get; set; } = false;

        /// <summary>
        /// Enable write permission.
        /// </summary>
        public bool EnableWrite { get; set; } = false;

        /// <summary>
        /// Enable write ACP permission.
        /// </summary>
        public bool EnableWriteAcp { get; set; } = false;

        /// <summary>
        /// Full control permission.
        /// </summary>
        public bool FullControl { get; set; } = false;

        /// <summary>
        /// Creation time, in UTC.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public BucketAclEntry()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}