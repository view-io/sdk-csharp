namespace View.Sdk.Embeddings
{
    using System;
    using System.Collections.Generic;
    using Timestamps;
    using View.Sdk.Semantic;

    /// <summary>
    /// Embeddings request.
    /// </summary>
    public class EmbeddingsRequest
    {
        #region Public-Members

        /// <summary>
        /// Embeddings rule.
        /// </summary>
        public EmbeddingsRule EmbeddingsRule
        {
            get
            {
                return _EmbeddingsRule;
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(EmbeddingsRule));
                _EmbeddingsRule = value;
            }
        }

        /// <summary>
        /// Model.
        /// </summary>
        public string Model
        {
            get
            {
                return _Model;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(Model));
                _Model = value;
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
        /// Contents.
        /// </summary>
        public List<string> Contents
        {
            get
            {
                return _Contents;
            }
            set
            {
                if (value == null) value = new List<string>();
                _Contents = value;
            }
        }

        /// <summary>
        /// Embeddings.
        /// </summary>
        public List<List<float>> Embeddings { get; set; } = null;

        #endregion

        #region Private-Members

        private EmbeddingsRule _EmbeddingsRule = null;
        private string _Model = null;
        private List<SemanticCell> _SemanticCells = new List<SemanticCell>();
        private List<string> _Contents = new List<string>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public EmbeddingsRequest()
        {
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
