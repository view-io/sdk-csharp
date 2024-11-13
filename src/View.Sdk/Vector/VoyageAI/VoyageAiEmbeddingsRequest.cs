namespace View.Sdk.Vector.VoyageAI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using View.Sdk.Vector;
    using View.Sdk.Vector.Langchain;
    using View.Sdk.Vector.OpenAI;

    /// <summary>
    /// VoyageAI embeddings request.
    /// </summary>
    public class VoyageAiEmbeddingsRequest
    {
        #region Public-Members

        /// <summary>
        /// Model used to generate embeddings.
        /// </summary>
        [JsonPropertyName("model")]
        public string Model { get; set; } = null;

        /// <summary>
        /// Contents.
        /// </summary>
        [JsonPropertyName("input")]
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
        public VoyageAiEmbeddingsRequest()
        {

        }

        /// <summary>
        /// Instantiate from embeddings request.
        /// </summary>
        /// <param name="req">Embeddings request.</param>
        /// <returns>Voyage AI embeddings request.</returns>
        public static VoyageAiEmbeddingsRequest FromEmbeddingsRequest(EmbeddingsRequest req)
        {
            if (req == null) throw new ArgumentNullException(nameof(req));

            return new VoyageAiEmbeddingsRequest
            {
                Model = req.Model,
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
