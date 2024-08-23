using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace View.Sdk.Vector
{
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

        /// <summary>
        /// Build embeddings maps from content and Ollama result.
        /// </summary>
        /// <param name="content">Content.</param>
        /// <param name="ollamaResult">Ollama result.</param>
        /// <returns>List of embeddings maps.</returns>
        public static List<EmbeddingsMap> ToEmbeddingsMaps(List<string> content, OllamaEmbeddingsResult ollamaResult)
        {
            List<EmbeddingsMap> ret = new List<EmbeddingsMap>();

            if (content == null || content.Count < 1) return ret;
            if (ollamaResult == null || ollamaResult.Embeddings == null || ollamaResult.Embeddings.Count != content.Count) return ret;

            for (int i = 0; i < content.Count; i++)
            {
                EmbeddingsMap map = new EmbeddingsMap
                {
                    Content = content[i],
                    Embeddings = ollamaResult.Embeddings[i]
                };

                ret.Add(map);
            }

            return ret;
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
