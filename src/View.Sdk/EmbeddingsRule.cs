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
        public string ProcessingEndpoint { get; set; } = "http://nginx-processor:8000/v1.0/tenants/00000000-0000-0000-0000-000000000000/processing";

        /// <summary>
        /// Access key for processing endpoint.
        /// </summary>
        public string ProcessingAccessKey { get; set; } = "default";

        /// <summary>
        /// Chunking server URL.
        /// </summary>
        public string ChunkingServerUrl { get; set; } = "http://nginx-chunker:8000/v1.0/tenants/00000000-0000-0000-0000-000000000000/chunking";

        /// <summary>
        /// Chunking server API key.
        /// </summary>
        public string ChunkingServerApiKey { get; set; } = "default";

        /// <summary>
        /// Tokenization model.
        /// </summary>
        public string TokenizationModel { get; set; } = "sentence-transformers/all-MiniLM-L6-v2";

        /// <summary>
        /// HuggingFace API key.
        /// </summary>
        public string HuggingFaceApiKey { get; set; } = null;

        /// <summary>
        /// Maximum number of parallel chunking tasks, i.e. number of semantic cells to process concurrently.  Default is 16.
        /// </summary>
        public int MaxChunkingTasks
        {
            get
            {
                return _MaxChunkingTasks;
            }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(MaxChunkingTasks));
                _MaxChunkingTasks = value;
            }
        }

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
        /// Token overlap, used to determine overlap amongst neighboring chunks.
        /// </summary>
        public int? TokenOverlap
        {
            get
            {
                return _TokenOverlap;
            }
            set
            {
                if (value != null && value.Value < 0) throw new ArgumentException("TokenOverlap must be greater than or equal to zero when set to a value.");
                _TokenOverlap = value;
            }
        }

        /// <summary>
        /// Embeddings server URL.
        /// </summary>
        public string EmbeddingsServerUrl { get; set; } = "http://nginx-processor:8000/";

        /// <summary>
        /// Embeddings server API key.
        /// </summary>
        public string EmbeddingsServerApiKey { get; set; } = "default";

        /// <summary>
        /// Embeddings generator.
        /// </summary>
        public EmbeddingsGeneratorEnum EmbeddingsGenerator { get; set; } = EmbeddingsGeneratorEnum.LCProxy;

        /// <summary>
        /// Embeddings generator URL.
        /// </summary>
        public string EmbeddingsGeneratorUrl { get; set; } = "http://nginx-embeddings:8000/";

        /// <summary>
        /// Embeddings provider API key.
        /// </summary>
        public string EmbeddingsGeneratorApiKey { get; set; } = "default";

        /// <summary>
        /// Maximum number of chunks to process per embeddings request.  Default is 16.
        /// </summary>
        public int EmbeddingsBatchSize
        {
            get
            {
                return _EmbeddingsBatchSize;
            }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(EmbeddingsBatchSize));
                _EmbeddingsBatchSize = value;
            }
        }

        /// <summary>
        /// Maximum number of parallel embeddings generation tasks.  Default is 16.
        /// </summary>
        public int MaxEmbeddingsTasks
        {
            get
            {
                return _MaxEmbeddingsTasks;
            }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(MaxEmbeddingsTasks));
                _MaxEmbeddingsTasks = value;
            }
        }

        /// <summary>
        /// Maximum number of retries to perform on any given embeddings task.  Default is 3.
        /// </summary>
        public int MaxEmbeddingsRetries
        {
            get
            {
                return _MaxEmbeddingsRetries;
            }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(MaxEmbeddingsRetries));
                _MaxEmbeddingsRetries = value;
            }
        }

        /// <summary>
        /// Maximum number of embedding failures to support before failing the operation.  Default is 3.
        /// </summary>
        public int MaxEmbeddingsFailures
        {
            get
            {
                return _MaxEmbeddingsFailures;
            }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(MaxEmbeddingsFailures));
                _MaxEmbeddingsFailures = value;
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

        private int _MaxChunkingTasks = 16;
        private int _MinChunkContentLength = 2;
        private int _MaxChunkContentLength = 512;
        private int _MaxTokensPerChunk = 256;
        private int? _TokenOverlap = null;

        private int _EmbeddingsBatchSize = 16;
        private int _MaxEmbeddingsTasks = 16;
        private int _MaxContentLength = 16 * 1024 * 1024;
        private int _MaxEmbeddingsRetries = 3;
        private int _MaxEmbeddingsFailures = 3;

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