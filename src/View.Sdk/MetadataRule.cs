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
        public string GUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public string TenantGUID { get; set; } = null;

        /// <summary>
        /// Bucket GUID.
        /// </summary>
        public string BucketGUID { get; set; } = null;

        /// <summary>
        /// Owner GUID.
        /// </summary>
        public string OwnerGUID { get; set; } = Guid.NewGuid().ToString();

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
        /// Data flow endpoint.
        /// </summary>
        public string DataFlowEndpoint { get; set; } = "http://localhost:8501/processor";

        #region Type-Detection

        /// <summary>
        /// Type detector endpoint.
        /// </summary>
        public string TypeDetectorEndpoint { get; set; } = "http://localhost:8501/processor/typedetector";

        #endregion

        #region Semantic-Cell-Extraction

        /// <summary>
        /// Semantic cell extraction endpoint.
        /// </summary>
        public string SemanticCellEndpoint { get; set; } = "http://localhost:8341/";

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
                if (value > _MaxChunkContentLength) throw new ArgumentException("ShiftSize must be equal to or less than MaxChunkContentLength.");
                _ShiftSize = value;
            }
        }

        #endregion

        #region UDR

        /// <summary>
        /// UDR endpoint.
        /// </summary>
        public string UdrEndpoint { get; set; } = "http://localhost:8321/";

        #endregion

        #region Data-Catalog

        /// <summary>
        /// Data catalog type.
        /// </summary>
        public DataCatalogTypeEnum DataCatalogType { get; set; } = DataCatalogTypeEnum.Lexi;

        /// <summary>
        /// Data catalog endpoint.
        /// </summary>
        public string DataCatalogEndpoint { get; set; } = "http://localhost:8201/";

        /// <summary>
        /// Data catalog collection identifier.
        /// </summary>
        public string DataCatalogCollection { get; set; } = null;

        #endregion

        #region Graph

        /// <summary>
        /// Graph repository GUID.
        /// </summary>
        public string GraphRepositoryGUID { get; set; } = null;

        #endregion

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

        /// <summary>
        /// Target bucket GUID.
        /// </summary>
        public string TargetBucketGUID { get; set; } = null;

        /*
         * 
         * 
         * If we want to extend to allow configurability, e.g. enabling/disabling capabilities
         * or otherwise specifying config parameters, we can add attributes here pointing to
         * an object by GUID, bucket GUID, and tenant GUID containing those parameters.
         * 
         * Further, if we want to specify where the results should go, we could do the same.
         * 
         * 
         * 
         */

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
        private int _MaxChunkContentLength = 512;
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