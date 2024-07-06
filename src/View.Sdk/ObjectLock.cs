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
        public string GUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public string TenantGUID { get; set; } = Guid.NewGuid().ToString();
        
        /// <summary>
        /// Node GUID.
        /// </summary>
        public string NodeGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Bucket GUID.
        /// </summary>
        public string BucketGUID { get; set; } = Guid.NewGuid().ToString();
        
        /// <summary>
        /// Owner GUID.
        /// </summary>
        public string OwnerGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Object GUID.
        /// </summary>
        public string ObjectGUID { get; set; } = Guid.NewGuid().ToString();

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