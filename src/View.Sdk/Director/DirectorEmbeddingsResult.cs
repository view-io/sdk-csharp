namespace View.Sdk.Director
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Director embeddings result.
    /// </summary>
    public class DirectorEmbeddingsResult
    {        
        #region Public-Members

        /// <summary>
        /// Boolean indicating whether or not the operation was successful.
        /// </summary>
        public bool Success { get; set; } = false;

        /// <summary>
        /// HTTP status code.
        /// </summary>
        public int StatusCode
        {
            get
            {
                return _StatusCode;
            }
            set
            {
                if (value < 0 || value > 599) throw new ArgumentOutOfRangeException(nameof(StatusCode));
                _StatusCode = value;
            }
        }

        /// <summary>
        /// Error response.
        /// </summary>
        public ApiErrorResponse Error { get; set; } = null;

        /// <summary>
        /// Model used for generating embeddings.
        /// </summary>
        public string Model { get; set; } = null;

        /// <summary>
        /// List of embeddings results.
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

        private int _StatusCode = 200;
        private List<List<float>> _Embeddings = new List<List<float>>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Director embeddings result.
        /// </summary>
        public DirectorEmbeddingsResult()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}