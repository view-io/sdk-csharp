namespace View.Sdk.Shared.Udr
{
    using System;
    using System.Security.Cryptography;
    using System.Text.Json.Serialization;
    using View.Sdk.Shared.Processing;


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

        /// <summary>
        /// UDR endpoint.
        /// </summary>
        public string UdrEndpoint { get; set; } = "http://localhost:8321/";

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