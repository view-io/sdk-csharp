namespace View.Sdk.Vector
{
    using System;

    /// <summary>
    /// Vector query request.
    /// </summary>
    public class VectorQueryRequest
    {
        #region Public-Members

        /// <summary>
        /// Query.
        /// </summary>
        public string Query { get; set; } = null;

        /// <summary>
        /// Vector repository GUID.
        /// </summary>
        public Guid VectorRepositoryGUID { get; set; } = Guid.NewGuid();

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public VectorQueryRequest()
        {
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
