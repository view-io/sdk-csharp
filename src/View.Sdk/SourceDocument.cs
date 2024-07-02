namespace View.Sdk
{
    using System;

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
        /// Creation timestamp, in UTC time.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

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
