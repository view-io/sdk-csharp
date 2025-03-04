namespace View.Sdk.Embeddings
{
    using System;
    using System.Collections.Generic;
    using View.Sdk.Semantic;

    /// <summary>
    /// Embeddings result.
    /// </summary>
    public class EmbeddingsResult
    {
        #region Public-Members

        /// <summary>
        /// Boolean indicating whether or not the operation was successful.
        /// </summary>
        public bool Success { get; set; } = false;

        /// <summary>
        /// HTTP status code.
        /// </summary>
        public int StatusCode
        {
            get
            {
                return _StatusCode;
            }
            set
            {
                if (value < 0 || value > 599) throw new ArgumentOutOfRangeException(nameof(StatusCode));
                _StatusCode = value;
            }
        }

        /// <summary>
        /// Error response.
        /// </summary>
        public ApiErrorResponse Error { get; set; } = null;

        /// <summary>
        /// The number of batches processed.
        /// </summary>
        public int BatchCount
        {
            get
            {
                return _BatchCount;
            }
            set
            {
                if (_BatchCount < 0) throw new ArgumentOutOfRangeException(nameof(BatchCount));
                _BatchCount = value;
            }
        }

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
                if (value == null) value = new List<SemanticCell>();
                _SemanticCells = value;
            }
        }

        /// <summary>
        /// Content embeddings.
        /// </summary>
        public List<ContentEmbedding> ContentEmbeddings
        {
            get
            {
                return _ContentEmbeddings;
            }
            set
            {
                if (value == null) value = new List<ContentEmbedding>();
                _ContentEmbeddings = value;
            }
        }

        #endregion

        #region Private-Members

        private int _StatusCode = 200;
        private int _BatchCount = 0;
        private List<SemanticCell> _SemanticCells = new List<SemanticCell>();
        private List<ContentEmbedding> _ContentEmbeddings = new List<ContentEmbedding>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public EmbeddingsResult()
        {
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
