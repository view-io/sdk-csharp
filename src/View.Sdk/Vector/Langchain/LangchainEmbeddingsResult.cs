namespace View.Sdk.Vector.Langchain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection.Metadata;
    using System.Text;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using Timestamps;
    using View.Sdk.Vector;

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
        /// Create an embeddings result from this object.
        /// </summary>
        /// <returns>Embeddings result.</returns>
        public EmbeddingsResult ToEmbeddingsResult()
        {
            EmbeddingsResult er = new EmbeddingsResult();
            er.Success = Success;
            er.StatusCode = StatusCode;
            er.Model = Model;
            er.Result = new List<EmbeddingsMap>();

            if (Contents != null && Contents.Count > 0 &&
                Embeddings != null && Embeddings.Count > 0)
            {
                if (Contents.Count != Embeddings.Count) throw new InvalidOperationException("The number of content elements does not match the number of embeddings elements.");

                for (int i = 0; i < Contents.Count; i++)
                    er.Result.Add(new EmbeddingsMap { Content = Contents[i], Embeddings = Embeddings[i] });
            }

            return er;
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}
