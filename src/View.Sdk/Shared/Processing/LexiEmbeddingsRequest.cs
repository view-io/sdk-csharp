namespace View.Sdk.Shared.Processing
{
    using System.Xml.Linq;
    using System;
    using View.Sdk.Shared.Embeddings;
    using View.Sdk.Shared.Search;
    using View.Sdk.Shared.Udr;

    /// <summary>
    /// Lexi embeddings request.
    /// </summary>
    public class LexiEmbeddingsRequest
    {
        #region Public-Members

        /// <summary>
        /// Search results.
        /// </summary>
        public View.Sdk.Shared.Search.SearchResult Results { get; set; } = null;

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
