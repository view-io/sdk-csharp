namespace View.Sdk.Embeddings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using View.Sdk.Embeddings.Providers.Ollama;

    /// <summary>
    /// Model information.
    /// </summary>
    public class ModelInformation
    {
        #region Public-Members

        /// <summary>
        /// Model.
        /// </summary>
        public string Model { get; set; } = null;

        /// <summary>
        /// Size.
        /// </summary>
        public long Size
        {
            get
            {
                return _Size;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(Size));
                _Size = value;
            }
        }

        /// <summary>
        /// Last modified timestamp, in UTC time.
        /// </summary>
        public DateTime? LastModifiedUtc { get; set; } = null;

        /// <summary>
        /// MD5 hash.
        /// </summary>
        public string MD5Hash { get; set; } = null;

        /// <summary>
        /// SHA1 hash.
        /// </summary>
        public string SHA1Hash { get; set; } = null;

        /// <summary>
        /// SHA256 hash.
        /// </summary>
        public string SHA256Hash { get; set; } = null;

        #endregion

        #region Private-Members

        private long _Size = 0;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public ModelInformation()
        {

        }

        /// <summary>
        /// Instantiate from Ollama model response.
        /// </summary>
        /// <param name="resp">Response.</param>
        /// <returns>List of model information.</returns>
        public static List<ModelInformation> FromOllamaResponse(OllamaModelResult resp)
        {
            List<ModelInformation> ret = new List<ModelInformation>();
            if (resp == null || resp.Models == null || resp.Models.Count < 1) return ret;

            foreach (OllamaModelResult.OllamaModelDetail model in resp.Models)
            {
                ModelInformation info = new ModelInformation
                {
                    Model = model.Model,
                    Size = model.Size,
                    LastModifiedUtc = model.LastModifiedUtc,
                    SHA256Hash = model.Digest
                };

                ret.Add(info);
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
