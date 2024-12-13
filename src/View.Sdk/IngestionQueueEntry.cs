namespace View.Sdk
{
    using System;
    using System.Text.Json.Serialization;
    using View.Sdk;

    /// <summary>
    /// Ingestion queue entry.
    /// </summary>
    public class IngestionQueueEntry
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
        /// Collection GUID.
        /// </summary>
        public string CollectionGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Source document GUID.
        /// </summary>
        public string SourceDocumentGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Document type.
        /// </summary>
        public DocumentTypeEnum DocumentType { get; set; } = DocumentTypeEnum.Unknown;

        /// <summary>
        /// Key.
        /// </summary>
        public string ObjectKey { get; set; } = null;

        /// <summary>
        /// Version.
        /// </summary>
        public string ObjectVersion { get; set; } = "1";

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
        /// Processing message.
        /// </summary>
        public string Message { get; set; } = null;

        /// <summary>
        /// Start timestamp, in UTC.
        /// </summary>
        public DateTime? StartUtc { get; set; } = null;

        /// <summary>
        /// Success timestamp, in UTC.
        /// </summary>
        public DateTime? SuccessUtc { get; set; } = null;

        /// <summary>
        /// Failure timestamp, in UTC.
        /// </summary>
        public DateTime? FailureUtc { get; set; } = null;

        /// <summary>
        /// Total runtime of ingestion, in milliseconds.
        /// </summary>
        public decimal? TotalMs { get; set; } = null;

        /// <summary>
        /// Total runtime of term processing, in milliseconds.
        /// </summary>
        public decimal? TermProcessingMs { get; set; } = null;

        /// <summary>
        /// Total runtime of schema processing, in milliseconds.
        /// </summary>
        public decimal? SchemaProcessingMs { get; set; } = null;

        /// <summary>
        /// Creation timestamp, in UTC.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        #endregion

        #region Private-Members

        private int _Id = 0;
        private long _ContentLength = 0;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public IngestionQueueEntry()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}