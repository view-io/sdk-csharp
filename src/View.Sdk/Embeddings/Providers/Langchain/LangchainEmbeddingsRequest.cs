namespace View.Sdk.Embeddings.Providers.Langchain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection.Metadata;
    using System.Text;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using Timestamps;
    using View.Sdk.Embeddings;

    /// <summary>
    /// Langchain embeddings request.
    /// </summary>
    public class LangchainEmbeddingsRequest
    {
        #region Public-Members

        /// <summary>
        /// Model used to generate embeddings.
        /// </summary>
        public string Model { get; set; } = null;

        /// <summary>
        /// API key for Huggingface.
        /// </summary>
        public string ApiKey { get; set; } = null;

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

        #endregion

        #region Private-Members

        private List<string> _Contents = new List<string>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public LangchainEmbeddingsRequest()
        {
        }

        /// <summary>
        /// Instantiate from embeddings request.
        /// </summary>
        /// <param name="req">Embeddings request.</param>
        /// <returns>Langchain embeddings request.</returns>
        public static LangchainEmbeddingsRequest FromEmbeddingsRequest(EmbeddingsRequest req)
        {
            if (req == null) throw new ArgumentNullException(nameof(req));

            return new LangchainEmbeddingsRequest
            {
                Model = req.Model,
                ApiKey = req.ApiKey,
                Contents = req.Contents
            };
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
