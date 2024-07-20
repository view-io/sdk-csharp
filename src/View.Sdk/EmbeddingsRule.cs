namespace View.Sdk
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Embeddings rule.
    /// </summary>
    public class EmbeddingsRule
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
        /// Target bucket GUID.
        /// </summary>
        public string TargetBucketGUID { get; set; } = null;

        /// <summary>
        /// Graph repository GUID.
        /// </summary>
        public string GraphRepositoryGUID { get; set; } = null;

        /// <summary>
        /// Vector repository GUID.
        /// </summary>
        public string VectorRepositoryGUID { get; set; } = null;

        /// <summary>
        /// Data flow endpoint.
        /// </summary>
        public string DataFlowEndpoint { get; set; } = "http://localhost:8501/processor";

        /// <summary>
        /// Embeddings generator.
        /// </summary>
        public EmbeddingsGeneratorEnum EmbeddingsGenerator { get; set; } = EmbeddingsGeneratorEnum.LCProxy;

        /// <summary>
        /// Embeddings generator URL.
        /// </summary>
        public string GeneratorUrl { get; set; } = "http://localhost:8301/";

        /// <summary>
        /// Embeddings provider API key.
        /// </summary>
        public string GeneratorApiKey { get; set; } = null;

        /// <summary>
        /// Vector store URL.
        /// </summary>
        public string VectorStoreUrl { get; set; } = "http://localhost:8311/";

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

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate the object.
        /// </summary>
        public EmbeddingsRule()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Members

        #endregion
    }
}