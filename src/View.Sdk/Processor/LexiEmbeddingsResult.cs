﻿namespace View.Sdk
{
    using System;
    using System.Collections.Generic;
    using Timestamps;
    using View.Sdk.Embeddings;

    /// <summary>
    /// Lexi embeddings result.
    /// </summary>
    public class LexiEmbeddingsResult
    {
        #region Public-Members

        /// <summary>
        /// Processor request GUID.
        /// </summary>
        public Guid GUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Boolean indicating success.
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// Boolean indicating whether or not the task was executed asynchronously.
        /// </summary>
        public bool Async { get; set; } = false;

        /// <summary>
        /// Timestamps.
        /// </summary>
        public Timestamp Timestamp { get; set; } = new Timestamp();

        /// <summary>
        /// Error response, if any.
        /// </summary>
        public ApiErrorResponse Error { get; set; } = null;

        /// <summary>
        /// Embeddings documents.
        /// </summary>
        public List<EmbeddingsDocument> Vector
        {
            get
            {
                return _Vectors;
            }
            set
            {
                if (value == null) value = new List<EmbeddingsDocument>();
                _Vectors = value;
            }
        }

        #endregion

        #region Private-Members

        private List<EmbeddingsDocument> _Vectors = new List<EmbeddingsDocument>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Lexi embeddings result.
        /// </summary>
        public LexiEmbeddingsResult()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}