namespace View.Sdk.Vector
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// OpenAI result.
    /// </summary>
    public class OpenAiResult<T>
    {
        #region Public-Members

        /// <summary>
        /// Data.
        /// </summary>
        [JsonPropertyName("data")]
        public List<T> Data
        {
            get
            {
                return _Data;
            }
            set
            {
                if (value == null) value = new List<T>();
                _Data = value;
            }
        }

        /// <summary>
        /// Model name.
        /// </summary>
        [JsonPropertyName("model")]
        public string ModelName { get; set; } = null;

        /// <summary>
        /// Object type.
        /// </summary>
        [JsonPropertyName("object")]
        public string ObjectType { get; set; } = null;

        /// <summary>
        /// Usage details.
        /// </summary>
        [JsonPropertyName("usage")]
        public object Usage { get; set; } = null;

        #endregion

        #region Private-Members

        private List<T> _Data = new List<T>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public OpenAiResult()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
