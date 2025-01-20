namespace View.Sdk
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text.Json.Serialization;
    using View.Sdk;

    /// <summary>
    /// Object lock.
    /// </summary>
    public class ObjectLock
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
        /// Node GUID.
        /// </summary>
        public Guid NodeGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Bucket GUID.
        /// </summary>
        public Guid BucketGUID { get; set; } = Guid.NewGuid();
        
        /// <summary>
        /// Owner GUID.
        /// </summary>
        public Guid OwnerGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Object GUID.
        /// </summary>
        public Guid ObjectGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Key.
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Version.
        /// </summary>
        public string Version { get; set; } = string.Empty;

        /// <summary>
        /// Boolean indicating if this is a read lock.
        /// </summary>
        public bool IsReadLock { get; set; } = false;

        /// <summary>
        /// Boolean indicating if this is a write lock.
        /// </summary>
        public bool IsWriteLock { get; set; } = false;

        /// <summary>
        /// Creation timestamp, in UTC.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public ObjectLock()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}