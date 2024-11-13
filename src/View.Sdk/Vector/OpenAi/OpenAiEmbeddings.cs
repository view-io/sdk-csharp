namespace View.Sdk.Vector.OpenAI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using View.Sdk.Vector;

    /// <summary>
    /// OpenAI embeddings.
    /// </summary>
    public class OpenAiEmbeddings
    {
        #region Public-Members

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

        #endregion

        #region Private-Members

        private List<float> _Embeddings = new List<float>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public OpenAiEmbeddings()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
