namespace View.Sdk.Storage
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Bucket tag.
    /// </summary>
    public class BucketTag
    {
        #region Public-Members

        /// <summary>
        /// GUID.
        /// </summary>
        public Guid GUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public Guid TenantGUID { get; set; } = Guid.Empty;

        /// <summary>
        /// Bucket GUID.
        /// </summary>
        public Guid BucketGUID { get; set; } = Guid.Empty;

        /// <summary>
        /// Owner GUID.
        /// </summary>
        public Guid OwnerGUID { get; set; } = Guid.Empty;

        /// <summary>
        /// Tag key.
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Tag value.
        /// </summary>
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// Created UTC time.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public BucketTag()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}