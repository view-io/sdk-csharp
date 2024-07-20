namespace View.Sdk
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Source document.
    /// </summary> 
    public class SourceDocument
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
        /// Bucket GUID.
        /// </summary>
        public string BucketGUID { get; set; } = null;

        /// <summary>
        /// Collection GUID.
        /// </summary>
        public string CollectionGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Object GUID.
        /// </summary>
        public string ObjectGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Data flow request GUID.
        /// </summary>
        public string DataFlowRequestGUID { get; set; } = null;

        /// <summary>
        /// Graph repository GUID.
        /// </summary>
        public string GraphRepositoryGUID { get; set; } = null;

        /// <summary>
        /// Graph node identifier.
        /// </summary>
        public string GraphNodeIdentifier { get; set; } = null;

        /// <summary>
        /// Key.
        /// </summary>
        public string ObjectKey { get; set; } = null;

        /// <summary>
        /// Version.
        /// </summary>
        public string ObjectVersion { get; set; } = "1";

        /// <summary>
        /// Content-type.
        /// </summary>
        public string ContentType { get; set; } = "application/octet-stream";

        /// <summary>
        /// Document type.
        /// </summary>
        public DocumentTypeEnum DocumentType { get; set; } = DocumentTypeEnum.Unknown;

        /// <summary>
        /// Source URL.
        /// </summary>
        public string SourceUrl { get; set; } = null;

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
        /// Creation timestamp, in UTC time.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Expiration timestamp, if any, in UTC time.
        /// </summary>
        public DateTime? ExpirationUtc { get; set; } = null;

        /// <summary>
        /// UDR document.
        /// </summary>
        public UdrDocument UdrDocument { get; set; } = null;

        #endregion

        #region Private-Members

        private long _ContentLength = 0;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public SourceDocument()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
