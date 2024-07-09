namespace View.Sdk
{
    using System.Xml.Linq;
    using System;

    /// <summary>
    /// Processor request.
    /// </summary>
    public class ProcessorRequest
    {
        #region Public-Members

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

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public ProcessorRequest()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
