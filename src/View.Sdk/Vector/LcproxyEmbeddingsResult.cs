using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Timestamps;

namespace View.Sdk.Vector
{
    /// <summary>
    /// Lcproxy embeddings result.
    /// </summary>
    public class LcproxyEmbeddingsResult
    {
        #region Public-Members

        /// <summary>
        /// Boolean indicating whether or not the operation was successful.
        /// </summary>
        public bool Success { get; set; } = false;

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
        public LcproxyEmbeddingsResult()
        {
        }

        /// <summary>
        /// Build embeddings maps from content and Ollama result.
        /// </summary>
        /// <param name="content">Content.</param>
        /// <param name="lcProxyResult">Ollama result.</param>
        /// <returns>List of embeddings maps.</returns>
        public static List<EmbeddingsMap> ToEmbeddingsMaps(List<string> content, LcproxyEmbeddingsResult lcProxyResult)
        {
            List<EmbeddingsMap> ret = new List<EmbeddingsMap>();

            if (content == null || content.Count < 1) return ret;
            if (lcProxyResult == null || lcProxyResult.Embeddings == null || lcProxyResult.Embeddings.Count != content.Count) return ret;

            for (int i = 0; i < content.Count; i++)
            {
                EmbeddingsMap map = new EmbeddingsMap
                {
                    Content = content[i],
                    Embeddings = lcProxyResult.Embeddings[i]
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
