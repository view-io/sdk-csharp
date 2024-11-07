namespace View.Sdk.Vector
{
    using System.Collections.Generic;
    using Timestamps;

    /// <summary>
    /// Find embeddings result.
    /// </summary>
    public class FindEmbeddingsResult
    {
        #region Public-Members

        /// <summary>
        /// Timestamps.
        /// </summary>
        public Timestamp Timestamp { get; set; } = new Timestamp();

        /// <summary>
        /// List of found embeddings records.
        /// </summary>
        public List<FindEmbeddingsObject> Existing
        {
            get
            {
                return _Existing;
            }
            set
            {
                if (value == null) _Existing = new List<FindEmbeddingsObject>();
                else _Existing = value;
            }
        }

        /// <summary>
        /// List of missing embeddings records.
        /// </summary>
        public List<FindEmbeddingsObject> Missing
        {
            get
            {
                return _Missing;
            }
            set
            {
                if (value == null) _Missing = new List<FindEmbeddingsObject>();
                else _Missing = value;
            }
        }

        #endregion

        #region Private-Members

        private List<FindEmbeddingsObject> _Existing = new List<FindEmbeddingsObject>();
        private List<FindEmbeddingsObject> _Missing = new List<FindEmbeddingsObject>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public FindEmbeddingsResult()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
