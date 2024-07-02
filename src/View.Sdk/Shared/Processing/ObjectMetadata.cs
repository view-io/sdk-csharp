namespace View.Sdk.Shared.Processing
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text.Json.Serialization;
    using View.Sdk.Shared.Udr;
    using System.Xml.Linq;

    /// <summary>
    /// Object metadata.
    /// </summary>
    public class ObjectMetadata
    {
        #region Public-Members

        /// <summary>
        /// GUID.
        /// </summary>
        public string GUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Parent GUID.
        /// </summary>
        public string ParentGUID { get; set; } = null;

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public string TenantGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Tenant name.
        /// </summary>
        public string TenantName { get; set; } = null;

        /// <summary>
        /// Node GUID.
        /// </summary>
        public string NodeGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Pool GUID.
        /// </summary>
        public string PoolGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Bucket GUID.
        /// </summary>
        public string BucketGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Bucket name.
        /// </summary>
        public string BucketName { get; set; } = null;

        /// <summary>
        /// Owner GUID.
        /// </summary>
        public string OwnerGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Key.
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Version.
        /// </summary>
        public string Version { get; set; } = string.Empty;

        /// <summary>
        /// Boolean indicating if this is the latest version.
        /// </summary>
        public bool IsLatest { get; set; } = true;

        /// <summary>
        /// Boolean indicating if this is a delete marker.
        /// </summary>
        public bool IsDeleteMarker { get; set; } = false;

        /// <summary>
        /// Boolean indicating if the object is local.
        /// </summary>
        public bool IsLocal { get; set; } = true;

        /// <summary>
        /// Content-type.
        /// </summary>
        public string ContentType { get; set; } = null;

        /// <summary>
        /// Document type.
        /// </summary>
        public DocumentTypeEnum DocumentType { get; set; } = DocumentTypeEnum.Unknown;

        /// <summary>
        /// Content length.
        /// </summary>
        public long ContentLength
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
        /// Source URL.
        /// </summary>
        public string SourceUrl { get; set; } = null;

        /// <summary>
        /// MD5.
        /// </summary>
        public string MD5Hash { get; set; } = string.Empty;

        /// <summary>
        /// SHA1.
        /// </summary>
        public string SHA1Hash { get; set; } = null;

        /// <summary>
        /// SHA256.
        /// </summary>
        public string SHA256Hash { get; set; } = null;

        /// <summary>
        /// Expiration timestamp, in UTC.
        /// </summary>
        public DateTime? ExpirationUtc { get; set; } = null;

        /// <summary>
        /// Creation timestamp, in UTC.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Data.
        /// </summary>
        public byte[] Data
        {
            get
            {
                return _Data;
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(Data));
                _Data = value;
            }
        }

        #endregion

        #region Private-Members

        private long _ContentLength = 0;
        private byte[] _Data = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public ObjectMetadata()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}