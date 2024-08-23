namespace View.Sdk.Vector
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

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

        /// <summary>
        /// Build embeddings maps from content and OpenAI result.
        /// </summary>
        /// <param name="content">Content.</param>
        /// <param name="openAiResult">OpenAI result.</param>
        /// <returns>List of embeddings maps.</returns>
        public static List<EmbeddingsMap> ToEmbeddingsMaps(List<string> content, OpenAiEmbeddingsResult openAiResult)
        {
            List<EmbeddingsMap> ret = new List<EmbeddingsMap>();

            if (content == null || content.Count < 1) return ret;
            if (openAiResult == null || openAiResult.Data == null || openAiResult.Data.Count != content.Count) return ret;

            for (int i = 0; i < content.Count; i++)
            {
                EmbeddingsMap map = new EmbeddingsMap
                {
                    Content = content[i],
                    Embeddings = openAiResult.Data[i].Embeddings
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

        #region Public-Embedded-Classes

        /// <summary>
        /// OpenAI embeddings.
        /// </summary>
        public class OpenAiEmbeddings
        {
            /// <summary>
            /// Embeddings.
            /// </summary>
            [JsonPropertyName("embedding")]
            public List<float> Embeddings
            {
                get
                {
                    return _Embeddings;
                }
                set
                {
                    if (value == null) value = new List<float>();
                    _Embeddings = value;
                }
            }

            /// <summary>
            /// Index.
            /// </summary>
            [JsonPropertyName("index")]
            public int Index { get; set; } = 0;

            /// <summary>
            /// Object type.
            /// </summary>
            [JsonPropertyName("object")]
            public string ObjectType { get; set; } = null;

            private List<float> _Embeddings = new List<float>();

            /// <summary>
            /// Instantiate.
            /// </summary>
            public OpenAiEmbeddings()
            {

            }
        }

        #endregion
    }
}
