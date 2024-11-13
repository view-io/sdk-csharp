namespace View.Sdk.Vector.Ollama
{
    using Microsoft.VisualBasic;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection.Metadata;
    using System.Text;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using View.Sdk.Vector;

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
        /// Create an embeddings result from this object.
        /// </summary>
        /// <param name="req">Embeddings request.</param>
        /// <returns>Embeddings result.</returns>
        public EmbeddingsResult ToEmbeddingsResult(OllamaEmbeddingsRequest req)
        {
            EmbeddingsResult er = new EmbeddingsResult();
            er.Success = true;
            er.StatusCode = 200;
            er.Model = Model;
            er.Result = new List<EmbeddingsMap>();
            if (req.Contents != null && req.Contents.Count > 0 &&
                Embeddings != null && Embeddings.Count > 0)
            {
                if (req.Contents.Count != Embeddings.Count) throw new InvalidOperationException("The number of content elements does not match the number of embeddings elements.");

                for (int i = 0; i < req.Contents.Count; i++)
                    er.Result.Add(new EmbeddingsMap { Content = req.Contents[i], Embeddings = Embeddings[i] });
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
