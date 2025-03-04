namespace View.Sdk.Processor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Timestamps;
    using View.Sdk.Embeddings;

    /// <summary>
    /// Processor response.
    /// </summary>
    public class ProcessorResponse
    {
        #region Public-Members

        /// <summary>
        /// Processor request GUID.
        /// </summary>
        public Guid GUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Boolean indicating success.
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// Timestamps.
        /// </summary>
        public Timestamp Timestamp { get; set; } = new Timestamp();

        /// <summary>
        /// Error response, if any.
        /// </summary>
        public ApiErrorResponse Error { get; set; } = null;

        /// <summary>
        /// Type result.
        /// </summary>
        public TypeResult Type { get; set; } = null;

        /// <summary>
        /// UDR document.
        /// </summary>
        public UdrDocument Udr { get; set; } = null;

        /// <summary>
        /// Source document in data catalog.
        /// </summary>
        public SourceDocument Source { get; set; } = null;

        /// <summary>
        /// Embeddings document.
        /// </summary>
        public EmbeddingsDocument Vector { get; set; } = null;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public ProcessorResponse()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
