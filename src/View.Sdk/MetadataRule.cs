namespace View.Sdk
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Metadata rule.
    /// </summary>
    public class MetadataRule
    {
        #region Public-Members

        /// <summary>
        /// GUID.
        /// </summary>
        [JsonPropertyOrder(-1)]
        public Guid GUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public Guid TenantGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Bucket GUID.
        /// </summary>
        public Guid? BucketGUID { get; set; } = null;

        /// <summary>
        /// Owner GUID.
        /// </summary>
        public Guid OwnerGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Graph repository GUID.
        /// </summary>
        public Guid? GraphRepositoryGUID { get; set; } = null;

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; } = null;

        /// <summary>
        /// Content-type.
        /// </summary>
        public string ContentType { get; set; } = "text/plain";

        /// <summary>
        /// Prefix.
        /// </summary>
        public string Prefix { get; set; } = null;

        /// <summary>
        /// Suffix.
        /// </summary>
        public string Suffix { get; set; } = null;

        /// <summary>
        /// Data flow endpoint for processing.
        /// </summary>
        public string ProcessingEndpoint { get; set; } = "http://localhost:8000/v1.0/tenants/default/processing";

        /// <summary>
        /// Access key for processing endpoint.
        /// </summary>
        public string ProcessingAccessKey { get; set; } = "default";

        /// <summary>
        /// Data flow endpoint for cleanup processing.
        /// </summary>
        public string CleanupEndpoint { get; set; } = "http://localhost:8000/v1.0/tenants/default/processing/cleanup";

        /// <summary>
        /// Access key for cleanup endpoint.
        /// </summary>
        public string CleanupAccessKey { get; set; } = "default";

        #region Semantic-Cell-Extraction

        /// <summary>
        /// Minimum chunk content length.  Minimum is 1.
        /// </summary>
        public int MinChunkContentLength
        {
            get
            {
                return _MinChunkContentLength;
            }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(MinChunkContentLength));
                _MinChunkContentLength = value;
            }
        }

        /// <summary>
        /// Maximum chunk content length.  Minimum is 256 and maximum is 16384.
        /// </summary>
        public int MaxChunkContentLength
        {
            get
            {
                return _MaxChunkContentLength;
            }
            set
            {
                if (value < 256 || value > 16384) throw new ArgumentOutOfRangeException(nameof(MaxChunkContentLength));
                _MaxChunkContentLength = value;
            }
        }

        /// <summary>
        /// Maximum number of tokens per chunk.
        /// </summary>
        public int MaxTokensPerChunk
        {
            get
            {
                return _MaxTokensPerChunk;
            }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(MaxTokensPerChunk));
                _MaxTokensPerChunk = value;
            }
        }

        /// <summary>
        /// Shift size, used to determine overlap amongst neighboring chunks.
        /// When set to the same value as the maximum chunk content length, no overlap will exist amongst neighboring chunks.
        /// When set to a smaller amount than the maximum chunk content length, overlap will exist amongst neighboring chunks.
        /// This value must be equal to or less than the maximum chunk content length.
        /// </summary>
        public int ShiftSize
        {
            get
            {
                return _ShiftSize;
            }
            set
            {
                if (value < 1 || value > _MaxChunkContentLength) 
                    throw new ArgumentException("ShiftSize must be equal to or less than MaxChunkContentLength and be greater than zero.");
                
                _ShiftSize = value;
            }
        }

        /// <summary>
        /// Boolean indicating whether or not text should be extracted from images.
        /// </summary>
        public bool ImageTextExtraction { get; set; } = true;

        #endregion

        #region UDR

        /// <summary>
        /// Number of top terms to request.
        /// </summary>
        public int TopTerms
        {
            get
            {
                return _TopTerms;
            }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(TopTerms));
                _TopTerms = value;
            }
        }

        /// <summary>
        /// Enable or disable case insensitive text processing.
        /// </summary>
        public bool CaseInsensitive { get; set; } = true;

        /// <summary>
        /// Enable or disable inclusion of flattened representation of processed content.
        /// </summary>
        public bool IncludeFlattened { get; set; } = true;

        #endregion

        #region Data-Catalog

        /// <summary>
        /// Data catalog type.
        /// </summary>
        public DataCatalogTypeEnum DataCatalogType { get; set; } = DataCatalogTypeEnum.Lexi;

        /// <summary>
        /// Data catalog endpoint.
        /// </summary>
        public string DataCatalogEndpoint { get; set; } = "http://localhost:8000/";

        /// <summary>
        /// Data catalog access key.
        /// </summary>
        public string DataCatalogAccessKey { get; set; } = "default";

        /// <summary>
        /// Data catalog collection identifier.
        /// </summary>
        public string DataCatalogCollection { get; set; } = null;

        #endregion

        /// <summary>
        /// Maximum content length.
        /// </summary>
        public int MaxContentLength
        {
            get
            {
                return _MaxContentLength;
            }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(MaxContentLength));
                _MaxContentLength = value;
            }
        }

        /// <summary>
        /// The number of minutes to retain the generated document.
        /// </summary>
        public int? RetentionMinutes
        {
            get
            {
                return _RetentionMinutes;
            }
            set
            {
                if (value != null && value.Value < 1) throw new ArgumentOutOfRangeException(nameof(RetentionMinutes));
                _RetentionMinutes = value;
            }
        }

        /// <summary>
        /// Creation timestamp.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        #endregion

        #region Private-Members

        private int? _RetentionMinutes = null;
        private int _MaxContentLength = 16 * 1024 * 1024;
        private int _TopTerms = 25;
        private int _MinChunkContentLength = 2;
        private int _MaxChunkContentLength = 512;
        private int _MaxTokensPerChunk = 256;
        private int _ShiftSize = 512;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate the object.
        /// </summary>
        public MetadataRule()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Members

        #endregion
    }
}