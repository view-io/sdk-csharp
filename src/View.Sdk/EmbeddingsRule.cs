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
        /// Graph repository GUID.
        /// </summary>
        public Guid? GraphRepositoryGUID { get; set; } = null;

        /// <summary>
        /// Vector repository GUID.
        /// </summary>
        public Guid? VectorRepositoryGUID { get; set; } = null;

        /// <summary>
        /// Data flow endpoint for processing.
        /// </summary>
        public string ProcessingEndpoint { get; set; } = "http://localhost:8000/v1.0/tenants/default/processing";

        /// <summary>
        /// Access key for processing endpoint.
        /// </summary>
        public string ProcessingAccessKey { get; set; } = "default";

        /// <summary>
        /// Embeddings generator.
        /// </summary>
        public EmbeddingsGeneratorEnum EmbeddingsGenerator { get; set; } = EmbeddingsGeneratorEnum.LCProxy;

        /// <summary>
        /// Embeddings generator URL.
        /// </summary>
        public string EmbeddingsGeneratorUrl { get; set; } = "http://localhost:8000/v1.0/tenants/default/embeddings";

        /// <summary>
        /// Embeddings provider API key.
        /// </summary>
        public string EmbeddingsGeneratorApiKey { get; set; } = "default";

        /// <summary>
        /// Maximum number of chunks to process per request.  Default is 16.
        /// </summary>
        public int BatchSize
        {
            get
            {
                return _BatchSize;
            }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(BatchSize));
                _BatchSize = value;
            }
        }

        /// <summary>
        /// Maximum number of parallel embeddings generation tasks.  Default is 16.
        /// </summary>
        public int MaxGeneratorTasks
        {
            get
            {
                return _MaxGeneratorTasks;
            }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(MaxGeneratorTasks));
                _MaxGeneratorTasks = value;
            }
        }

        /// <summary>
        /// Maximum number of retries to perform on any given task.  Default is 3.
        /// </summary>
        public int MaxRetries
        {
            get
            {
                return _MaxRetries;
            }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(MaxRetries));
                _MaxRetries = value;
            }
        }

        /// <summary>
        /// Maximum number of failures to support before failing the operation.  Default is 3.
        /// </summary>
        public int MaxFailures
        {
            get
            {
                return _MaxFailures;
            }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(MaxFailures));
                _MaxFailures = value;
            }
        }

        /// <summary>
        /// Vector store URL.
        /// </summary>
        public string VectorStoreUrl { get; set; } = "http://localhost:8000/";

        /// <summary>
        /// Vector store access key.
        /// </summary>
        public string VectorStoreAccessKey { get; set; } = "default";

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
        private int _BatchSize = 16;
        private int _MaxGeneratorTasks = 16;
        private int _MaxContentLength = 16 * 1024 * 1024;
        private int _MaxRetries = 3;
        private int _MaxFailures = 3;

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