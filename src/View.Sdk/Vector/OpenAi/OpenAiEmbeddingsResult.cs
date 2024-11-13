namespace View.Sdk.Vector.OpenAI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using View.Sdk.Vector;
    using View.Sdk.Vector.Ollama;

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
        /// Create an embeddings result from this object.
        /// </summary>
        /// <param name="req">Embeddings request.</param>
        /// <returns>Embeddings result.</returns>
        public EmbeddingsResult ToEmbeddingsResult(OpenAiEmbeddingsRequest req)
        {
            if (req == null) throw new ArgumentNullException(nameof(req));

            EmbeddingsResult er = new EmbeddingsResult();
            er.Success = true;
            er.StatusCode = 200;
            er.Model = req.Model;
            er.Result = new List<EmbeddingsMap>();
            if (req.Contents != null && req.Contents.Count > 0 &&
                Data != null && Data.Count > 0)
            {
                if (req.Contents.Count != Data.Count) throw new InvalidOperationException("The number of content elements does not match the number of embeddings elements.");

                foreach (OpenAiEmbeddings embed in Data)
                    er.Result.Add(new EmbeddingsMap { Content = req.Contents[embed.Index], Embeddings = embed.Embeddings });
            }

            return er;
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
