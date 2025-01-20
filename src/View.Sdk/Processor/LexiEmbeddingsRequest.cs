namespace View.Sdk
{
    using System;

    /// <summary>
    /// Lexi embeddings request.
    /// </summary>
    public class LexiEmbeddingsRequest
    {
        #region Public-Members

        /// <summary>
        /// Processor request GUID.
        /// </summary>
        public Guid GUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Tenant metadata.
        /// </summary>
        public TenantMetadata Tenant { get; set; } = null;

        /// <summary>
        /// Collection.
        /// </summary>
        public Collection Collection { get; set; } = null;

        /// <summary>
        /// Search results.
        /// </summary>
        public SearchResult Results { get; set; } = null;

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
        public LexiEmbeddingsRequest()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
