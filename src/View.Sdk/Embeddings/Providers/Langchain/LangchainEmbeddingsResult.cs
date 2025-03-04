namespace View.Sdk.Embeddings.Providers.Langchain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using View.Sdk.Embeddings;
    using View.Sdk.Semantic;

    /// <summary>
    /// Lcproxy embeddings result.
    /// </summary>
    public class LangchainEmbeddingsResult
    {
        #region Public-Members

        /// <summary>
        /// Boolean indicating whether or not the operation was successful.
        /// </summary>
        public bool Success { get; set; } = false;

        /// <summary>
        /// HTTP status code.
        /// </summary>
        public int StatusCode { get; set; } = 0;

        /// <summary>
        /// Model used to generate embeddings.
        /// </summary>
        public string Model { get; set; } = null;

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
        /// List of embeddings.
        /// </summary>
        public List<List<float>> Embeddings
        {
            get
            {
                return _Embeddings;
            }
            set
            {
                if (value == null) value = new List<List<float>>();
                _Embeddings = value;
            }
        }

        #endregion

        #region Private-Members

        private List<string> _Contents = new List<string>();
        private List<List<float>> _Embeddings = new List<List<float>>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public LangchainEmbeddingsResult()
        {
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// Create an embeddings result object from the original request and this object, the provider response.
        /// </summary>
        /// <param name="req">Original embeddings request.</param>
        /// <param name="success">Boolean indicating whether or not the request succeeded.</param>
        /// <param name="statusCode">HTTP status code.</param>
        /// <param name="error">API error response.</param>
        /// <returns>Embeddings result.</returns>
        public EmbeddingsResult ToEmbeddingsResult(EmbeddingsRequest req, bool success = true, int statusCode = 200, ApiErrorResponse error = null)
        {
            if (req == null) throw new ArgumentNullException(nameof(req));

            EmbeddingsResult result = new EmbeddingsResult
            {
                Success = success,
                StatusCode = statusCode,
                Error = error,
                SemanticCells = req.SemanticCells,
                ContentEmbeddings = new List<ContentEmbedding>()
            };

            if (req.Contents != null && req.Contents.Count > 0)
            {
                foreach (string content in req.Contents)
                {
                    result.ContentEmbeddings.Add(new ContentEmbedding
                    {
                        Content = content,
                        Embeddings = new List<float>()
                    });
                }
            }

            if (Embeddings != null && Embeddings.Count > 0)
            {
                for (int i = 0; i < Embeddings.Count; i++)
                {
                    result.ContentEmbeddings[i].Embeddings = Embeddings[i];
                }
            }

            if (result.SemanticCells != null
                && result.SemanticCells.Count > 0
                && result.ContentEmbeddings != null
                && result.ContentEmbeddings.Count > 0)
            {
                foreach (SemanticChunk chunk in SemanticCell.AllChunks(result.SemanticCells))
                {
                    if (result.ContentEmbeddings.Any(c => c.Content.Equals(chunk.Content)))
                    {
                        chunk.Embeddings = result.ContentEmbeddings.First(c => c.Content.Equals(chunk.Content)).Embeddings;
                    }
                }
            }

            return result;
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}
