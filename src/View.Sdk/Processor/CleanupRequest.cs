namespace View.Sdk.Processor
{
    using System.Xml.Linq;
    using System;

    /// <summary>
    /// Cleanup request.
    /// </summary>
    public class CleanupRequest
    {
        #region Public-Members

        /// <summary>
        /// Cleanup request GUID.
        /// </summary>
        public Guid GUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Boolean indicating whether or not the request should be processed asynchronously.
        /// </summary>
        public bool Async { get; set; } = true;

        /// <summary>
        /// Tenant metadata.
        /// </summary>
        public TenantMetadata Tenant { get; set; } = null;

        /// <summary>
        /// Storage pool.
        /// </summary>
        public StoragePool Pool { get; set; } = null;

        /// <summary>
        /// Bucket metadata.
        /// </summary>
        public BucketMetadata Bucket { get; set; } = null;

        /// <summary>
        /// Data repository.
        /// </summary>
        public DataRepository DataRepository { get; set; } = null;

        /// <summary>
        /// Collection.
        /// </summary>
        public Collection Collection { get; set; } = null;

        /// <summary>
        /// Object metadata.
        /// </summary>
        public ObjectMetadata Object { get; set; } = null;

        /// <summary>
        /// Metadata rule.
        /// </summary>
        public MetadataRule MetadataRule { get; set; } = null;

        /// <summary>
        /// Embeddings rule.
        /// </summary>
        public EmbeddingsRule EmbeddingsRule { get; set; } = null;

        /// <summary>
        /// Vector repository.
        /// </summary>
        public VectorRepository VectorRepository { get; set; } = null;

        /// <summary>
        /// Graph repository.
        /// </summary>
        public GraphRepository GraphRepository { get; set; } = null;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public CleanupRequest()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
