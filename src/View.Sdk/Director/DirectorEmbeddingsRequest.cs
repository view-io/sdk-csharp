namespace View.Sdk.Director
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Director embeddings request.
    /// </summary>
    public class DirectorEmbeddingsRequest
    {        
        #region Public-Members

        /// <summary>
        /// Model to use for generating embeddings.
        /// </summary>
        public string Model
        {
            get
            {
                return _Model;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(Model));
                _Model = value;
            }
        }

        /// <summary>
        /// API key for the embeddings service.
        /// </summary>
        public string ApiKey { get; set; } = "";

        /// <summary>
        /// Contents to generate embeddings for.
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

        #endregion

        #region Private-Members

        private string _Model = null;
        private List<string> _Contents = new List<string>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Director embeddings request.
        /// </summary>
        public DirectorEmbeddingsRequest()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}