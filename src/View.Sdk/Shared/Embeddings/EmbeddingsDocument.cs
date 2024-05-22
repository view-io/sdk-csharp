namespace View.Sdk.Shared.Embeddings
{
    using System;
    using System.Collections.Generic;
    using Timestamps;
    using View.Sdk.Shared.Embeddings;

    /// <summary>
    /// Embeddings document.
    /// </summary>
    public class EmbeddingsDocument
    {
        #region Public-Members

        /// <summary>
        /// Indicates if the parser was successful.
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// Exception, if any.
        /// </summary>
        public Exception Exception { get; set; } = null;

        /// <summary>
        /// Timestamps.
        /// </summary>
        public Timestamp Timestamp { get; set; } = new Timestamp();

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public string TenantGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Collection GUID.
        /// </summary>
        public string CollectionGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Bucket GUID.
        /// </summary>
        public string BucketGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Object GUID.
        /// </summary>
        public string ObjectGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Object key.
        /// </summary>
        public string ObjectKey { get; set; } = null;

        /// <summary>
        /// Object version.
        /// </summary>
        public string ObjectVersion { get; set; } = null;

        /// <summary>
        /// Embeddings rule.
        /// </summary>
        public EmbeddingsRule EmbeddingsRule { get; set; } = null;

        /// <summary>
        /// Semantic cells.
        /// </summary>
        public List<SemanticCell> SemanticCells
        {
            get
            {
                return _SemanticCells;
            }
            set
            {
                if (value == null) _SemanticCells = new List<SemanticCell>();
                else _SemanticCells = value;
            }
        }

        #endregion

        #region Private-Members

        private List<SemanticCell> _SemanticCells = new List<SemanticCell>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public EmbeddingsDocument()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
