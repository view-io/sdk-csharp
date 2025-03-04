namespace View.Sdk.Embeddings.Providers.Ollama
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// Ollama model result.
    /// </summary>
    public class OllamaModelResult
    {
        #region Public-Members

        /// <summary>
        /// List of model details.
        /// </summary>
        [JsonPropertyName("models")]
        public List<OllamaModelDetail> Models
        {
            get
            {
                return _Models;
            }
            set
            {
                if (value == null) value = new List<OllamaModelDetail>();
                _Models = value;
            }
        }

        #endregion

        #region Private-Members

        private List<OllamaModelDetail> _Models { get; set; } = new List<OllamaModelDetail>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public OllamaModelResult()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion

        #region Public-Embedded-Classes

        /// <summary>
        /// Ollama model detail.
        /// </summary>
        public class OllamaModelDetail
        {
            /// <summary>
            /// Name.
            /// </summary>
            [JsonPropertyName("name")]
            public string Name { get; set; } = null;

            /// <summary>
            /// Model.
            /// </summary>
            [JsonPropertyName("model")]
            public string Model { get; set; } = null;

            /// <summary>
            /// Last modified timestamp, in UTC time.
            /// </summary>
            [JsonPropertyName("modified_at")]
            public DateTime? LastModifiedUtc { get; set; } = null;

            /// <summary>
            /// Size.
            /// </summary>
            [JsonPropertyName("size")]
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
            /// Digest.
            /// </summary>
            [JsonPropertyName("digest")]
            public string Digest { get; set; } = null;

            /// <summary>
            /// Details.
            /// </summary>
            [JsonPropertyName("details")]
            public object Details { get; set; } = null;

            private long _Size = 0;

            /// <summary>
            /// Instantiate.
            /// </summary>
            public OllamaModelDetail()
            {

            }
        }

        #endregion
    }
}
