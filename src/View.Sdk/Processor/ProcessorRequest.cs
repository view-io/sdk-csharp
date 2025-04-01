namespace View.Sdk.Processor
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
        /// Processor request GUID.
        /// </summary>
        public Guid GUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Metadata rule GUID.
        /// </summary>
        public Guid MetadataRuleGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Embeddings rule GUID.
        /// </summary>
        public Guid EmbeddingsRuleGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Object metadata.
        /// </summary>
        public ObjectMetadata Object { get; set; } = null;

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
