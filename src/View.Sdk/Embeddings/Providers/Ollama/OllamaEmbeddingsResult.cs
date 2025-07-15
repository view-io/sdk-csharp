namespace View.Sdk.Embeddings.Providers.Ollama
{
    using Microsoft.VisualBasic;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection.Metadata;
    using System.Text;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using View.Sdk.Embeddings;
    using View.Sdk.Semantic;

    /// <summary>
    /// Ollama embeddings result.
    /// </summary>
    public class OllamaEmbeddingsResult
    {
        #region Public-Members

        /// <summary>
        /// Model.
        /// </summary>
        [JsonPropertyName("model")]
        public string Model { get; set; } = null;

        /// <summary>
        /// List of embeddings.
        /// </summary>
        [JsonPropertyName("embeddings")]
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

        private List<List<float>> _Embeddings { get; set; } = new List<List<float>>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public OllamaEmbeddingsResult()
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
