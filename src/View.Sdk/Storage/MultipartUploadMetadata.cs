namespace View.Sdk.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Multipart upload metadata.
    /// </summary>
    public class MultipartUploadMetadata
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
        /// Pool GUID.
        /// </summary>
        public Guid PoolGUID { get; set; } = Guid.Empty;

        /// <summary>
        /// Node GUID.
        /// </summary>
        public Guid NodeGUID { get; set; } = Guid.Empty;

        /// <summary>
        /// Owner GUID.
        /// </summary>
        public Guid OwnerGUID { get; set; } = Guid.Empty;

        /// <summary>
        /// Upload GUID.
        /// </summary>
        public Guid UploadGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Object key.
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Started UTC time.
        /// </summary>
        public DateTime StartedUtc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Last access UTC time.
        /// </summary>
        public DateTime LastAccessUtc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Created UTC time.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Expiration UTC time.
        /// </summary>
        public DateTime ExpirationUtc { get; set; } = DateTime.UtcNow.AddDays(7);

        /// <summary>
        /// Owner information.
        /// </summary>
        public UserMaster Owner { get; set; } = null;

        /// <summary>
        /// Parts list.
        /// </summary>
        public List<PartMetadata> Parts { get; set; } = new List<PartMetadata>();

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public MultipartUploadMetadata()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}