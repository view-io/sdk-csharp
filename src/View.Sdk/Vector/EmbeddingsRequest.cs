namespace View.Sdk.Vector
{
    using System;
    using System.Collections.Generic;
    using Timestamps;
    using View.Sdk.Vector;

    /// <summary>
    /// Embeddings request.
    /// </summary>
    public class EmbeddingsRequest
    {
        #region Public-Members

        /// <summary>
        /// Model used to generate embeddings.
        /// </summary>
        public string Model { get; set; } = null;

        /// <summary>
        /// API key.
        /// </summary>
        public string ApiKey { get; set; } = null;

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

        #endregion

        #region Private-Members

        private List<string> _Contents = new List<string>();

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
