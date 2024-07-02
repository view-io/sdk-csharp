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
        /// Search results.
        /// </summary>
        public SearchResult Results { get; set; } = null;

        /// <summary>
        /// Embeddings rule.
        /// </summary>
        public EmbeddingsRule EmbeddingsRule { get; set; } = null;

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
