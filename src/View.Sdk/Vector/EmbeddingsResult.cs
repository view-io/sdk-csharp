namespace View.Sdk.Vector
{
    using System;
    using System.Collections.Generic;
    using Timestamps;
    using View.Sdk.Vector;

    /// <summary>
    /// Embeddings result.
    /// </summary>
    public class EmbeddingsResult
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
        /// Model used to generate embeddings.
        /// </summary>
        public string Model { get; set; } = null;

        /// <summary>
        /// Result.
        /// </summary>
        public List<EmbeddingsMap> Result
        {
            get
            {
                return _Result;
            }
            set
            {
                if (value == null) value = new List<EmbeddingsMap>();
                _Result = value;
            }
        }

        #endregion

        #region Private-Members

        private int _StatusCode = 200;
        private List<EmbeddingsMap> _Result = new List<EmbeddingsMap>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public EmbeddingsResult()
        {
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
