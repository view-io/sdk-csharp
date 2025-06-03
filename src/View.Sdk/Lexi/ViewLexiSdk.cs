namespace View.Sdk.Lexi
{
    using RestWrapper;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk;
    using View.Sdk.Lexi.Implementations;
    using View.Sdk.Lexi.Interfaces;
    using View.Sdk.Serialization;

    /// <summary>
    /// View Lexi search SDK.
    /// </summary>
    public class ViewLexiSdk : ViewSdkBase, IDisposable
    {
        #region Public-Members

        /// <summary>
        /// Collection methods.
        /// </summary>
        public ICollectionMethods Collection { get; set; }

        /// <summary>
        /// Source document methods.
        /// </summary>
        public ISourceDocumentMethods SourceDocument { get; set; }

        /// <summary>
        /// Ingest queue methods.
        /// </summary>
        public IIngestQueueMethods IngestQueue { get; set; }

        /// <summary>
        /// Enumerate methods.
        /// </summary>
        public IEnumerateMethods Enumerate { get; set; }

        /// <summary>
        /// Search methods.
        /// </summary>
        public ISearchMethods Search { get; set; }

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
        public ViewLexiSdk(Guid tenantGuid, string accessKey, string endpoint = "http://localhost:8000/") : base(tenantGuid, accessKey, endpoint)
        {
            Header = "[ViewLexiSdk] ";
            Collection = new CollectionMethods(this);
            SourceDocument = new SourceDocumentMethods(this);
            IngestQueue = new IngestQueueMethods(this);
            Enumerate = new EnumerateMethods(this);
            Search = new SearchMethods(this);
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
