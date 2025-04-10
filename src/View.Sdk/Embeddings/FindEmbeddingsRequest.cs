namespace View.Sdk.Embeddings
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Find embeddings request.
    /// </summary>
    public class FindEmbeddingsRequest
    {
        #region Public-Members

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public Guid TenantGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Embeddings rule GUID.
        /// </summary>
        public Guid EmbeddingsRuleGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Embeddings rule.
        /// </summary>
        public EmbeddingsRule EmbeddingsRule { get; set; } = null;

        /// <summary>
        /// Vector repository GUID.
        /// </summary>
        public Guid VectorRepositoryGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Vector repository.
        /// </summary>
        public VectorRepository VectorRepository { get; set; } = null;

        /// <summary>
        /// List of embeddings parameters on which to match.
        /// </summary>
        public List<FindEmbeddingsObject> Criteria
        {
            get
            {
                return _Criteria;
            }
            set
            {
                if (value == null) _Criteria = new List<FindEmbeddingsObject>();
                else _Criteria = value;
            }
        }

        #endregion

        #region Private-Members

        private List<FindEmbeddingsObject> _Criteria = new List<FindEmbeddingsObject>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public FindEmbeddingsRequest()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
