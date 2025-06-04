namespace View.Sdk.Vector
{
    using System;
    using View.Sdk;
    using View.Sdk.Vector.Implementations;
    using View.Sdk.Vector.Interfaces;

    /// <summary>
    /// View Vector SDK.
    /// </summary>
    public class ViewVectorSdk : ViewSdkBase, IDisposable
    {
        #region Public-Members

        /// <summary>
        /// Document methods.
        /// </summary>
        public IDocumentMethods Document { get; set; }

        /// <summary>
        /// Repository methods.
        /// </summary>
        public IRepositoryMethods Repository { get; set; }

        /// <summary>
        /// Vector methods.
        /// </summary>
        public IVectorMethods Vector { get; set; }

        /// <summary>
        /// SemanticCell methods.
        /// </summary>
        public ISemanticCellMethods SemanticCell { get; set; }

        /// <summary>
        /// SemanticChunk methods.
        /// </summary>
        public ISemanticChunkMethods SemanticChunk { get; set; }

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="tenantGuid">Tenant GUID.</param>
        /// <param name="accessKey">Access key.</param>
        /// <param name="endpoint">Endpoint URL, i.e. http://localhost:8000/.</param>
        public ViewVectorSdk(Guid tenantGuid, string accessKey, string endpoint = "http://localhost:8000/") : base(tenantGuid, accessKey, endpoint)
        {
            Header = "[ViewVectorSdk] ";
            Document = new DocumentMethods(this);
            Repository = new RepositoryMethods(this);
            Vector = new VectorMethods(this);
            SemanticCell = new SemanticCellMethods(this);
            SemanticChunk = new SemanticChunkMethods(this);
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
