namespace View.Sdk.Embeddings.Providers.OpenAI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json.Serialization;
    using View.Sdk.Embeddings;
    using View.Sdk.Semantic;

    /// <summary>
    /// OpenAI embeddings result.
    /// </summary>
    public class OpenAiEmbeddingsResult
    {
        #region Public-Members

        /// <summary>
        /// Object type.
        /// </summary>
        [JsonPropertyName("object")]
        public string Object { get; set; } = null;

        /// <summary>
        /// Data.
        /// </summary>
        [JsonPropertyName("data")]
        public List<OpenAiEmbeddings> Data
        {
            get
            {
                return _Data;
            }
            set
            {
                if (value == null) value = new List<OpenAiEmbeddings>();
                _Data = value;
            }
        }

        #endregion

        #region Private-Members

        private List<OpenAiEmbeddings> _Data = new List<OpenAiEmbeddings>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public OpenAiEmbeddingsResult()
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
        public GenerateEmbeddingsResult ToEmbeddingsResult(GenerateEmbeddingsRequest req, bool success = true, int statusCode = 200, ApiErrorResponse error = null)
        {
            if (req == null) throw new ArgumentNullException(nameof(req));

            GenerateEmbeddingsResult result = new GenerateEmbeddingsResult
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

            if (Data != null && Data.Count > 0)
            {
                foreach (OpenAiEmbeddings embed in Data)
                {
                    result.ContentEmbeddings[embed.Index].Embeddings = embed.Embeddings;
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
