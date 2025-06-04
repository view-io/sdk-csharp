namespace View.Sdk
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text.Json.Serialization;
    using System.Xml.Linq;
    using SerializableDataTables;
    /// <summary>
    /// Object metadata.
    /// </summary>
    public class ObjectMetadata
    {
        #region Public-Members

        /// <summary>
        /// GUID.
        /// </summary>
        public Guid GUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Parent GUID.
        /// </summary>
        public Guid? ParentGUID { get; set; } = null;

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public Guid TenantGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Tenant name.
        /// </summary>
        public string TenantName { get; set; } = null;

        /// <summary>
        /// Node GUID.
        /// </summary>
        public Guid NodeGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Pool GUID.
        /// </summary>
        public Guid PoolGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Bucket GUID.
        /// </summary>
        public Guid BucketGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Bucket name.
        /// </summary>
        public string BucketName { get; set; } = null;

        /// <summary>
        /// Owner GUID.
        /// </summary>
        public Guid OwnerGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Encryption key GUID.
        /// </summary>
        public Guid? EncryptionKeyGUID { get; set; } = null;

        /// <summary>
        /// Data catalog document GUID.
        /// </summary>
        public Guid? DataCatalogDocumentGUID { get; set; } = null;

        /// <summary>
        /// Data repository GUID.
        /// </summary>
        public Guid? DataRepositoryGUID { get; set; } = null;

        /// <summary>
        /// Graph repository GUID.
        /// </summary>
        public Guid? GraphRepositoryGUID { get; set; } = null;

        /// <summary>
        /// Graph node identifier.
        /// </summary>
        public string GraphNodeIdentifier { get; set; } = null;

        /// <summary>
        /// Data flow request GUID.
        /// </summary>
        public Guid? DataFlowRequestGUID { get; set; } = null;

        /// <summary>
        /// Boolean indicating if the data flow was successful in processing the object.
        /// </summary>
        public bool? DataFlowSuccess { get; set; } = null;

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
        /// Last access timestamp, in UTC.
        /// </summary>
        public DateTime LastAccessUtc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Last modified timestamp, in UTC.
        /// </summary>
        public DateTime LastModifiedUtc { get; set; } = DateTime.UtcNow;

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
                _Data = value;
            }
        }

        /// <summary>
        /// Data table.
        /// </summary>
        public SerializableDataTable DataTable { get; set; } = null;

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