namespace View.Sdk
{
    using System;

    /// <summary>
    /// Embeddings request.
    /// </summary>
    public class EmbeddingsRequest
    {
        #region Public-Members

        /// <summary>
        /// Model.
        /// </summary>
        public string Model
        {
            get
            {
                return _Model;
            }
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(Model));
                _Model = value;
            }
        }

        /// <summary>
        /// Text.
        /// </summary>
        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(Text));
                _Text = value;
            }
        }

        /// <summary>
        /// Endpoint URL, e.g. http://localhost:8301/.
        /// </summary>
        public string Endpoint
        {
            get
            {
                return _Endpoint;
            }
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(Endpoint));
                _Endpoint = new Uri(value).ToString();
            }
        }

        /// <summary>
        /// API key.
        /// </summary>
        public string ApiKey { get; set; } = null;

        #endregion

        #region Private-Members

        private string _Model = "all-MiniLM-L6-v2";
        private string _Text = null;
        private string _Endpoint = new Uri("http://localhost:8301/").ToString();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public EmbeddingsRequest()
        {
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
