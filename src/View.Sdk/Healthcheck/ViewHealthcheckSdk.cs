namespace View.Sdk.Healthcheck
{
    using System;
    using View.Sdk.Healthcheck.Implementations;
    using View.Sdk.Healthcheck.Interfaces;

    /// <summary>
    /// View Healthcheck SDK.
    /// </summary>
    public class ViewHealthcheckSdk : ViewSdkBase, IDisposable
    {
        #region Public-Members

        /// <summary>
        /// Switchboard healthcheck methods.
        /// </summary>
        public ISwitchboardMethods Switchboard { get; set; }

        /// <summary>
        /// Configuration healthcheck methods.
        /// </summary>
        public IConfigMethods Config { get; set; }

        /// <summary>
        /// Storage healthcheck methods.
        /// </summary>
        public IStorageMethods Storage { get; set; }

        /// <summary>
        /// Vector healthcheck methods.
        /// </summary>
        public IVectorMethods Vector { get; set; }

        /// <summary>
        /// Processor healthcheck methods.
        /// </summary>
        public IProcessorMethods Processor { get; set; }

        /// <summary>
        /// Assistant healthcheck methods.
        /// </summary>
        public IAssistantMethods Assistant { get; set; }

        /// <summary>
        /// Orchestrator healthcheck methods.
        /// </summary>
        public IOrchestratorMethods Orchestrator { get; set; }

        /// <summary>
        /// Crawler healthcheck methods.
        /// </summary>
        public ICrawlerMethods Crawler { get; set; }

        /// <summary>
        /// Lexi healthcheck methods.
        /// </summary>
        public ILexiMethods Lexi { get; set; }

        /// <summary>
        /// Embedding healthcheck methods.
        /// </summary>
        public IEmbeddingMethods Embedding { get; set; }

        /// <summary>
        /// Director healthcheck methods.
        /// </summary>
        public IDirectorMethods Director { get; set; }

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
        public ViewHealthcheckSdk(Guid tenantGuid, string accessKey, string endpoint = "http://localhost:8000/") : base(tenantGuid, accessKey, endpoint)
        {
            Header = "[ViewHealthcheckSdk] ";
            Switchboard = new SwitchboardMethods(this);
            Config = new ConfigMethods(this);
            Storage = new StorageMethods(this);
            Vector = new VectorMethods(this);
            Processor = new ProcessorMethods(this);
            Assistant = new AssistantMethods(this);
            Orchestrator = new OrchestratorMethods(this);
            Crawler = new CrawlerMethods(this);
            Lexi = new LexiMethods(this);
            Embedding = new EmbeddingMethods(this);
            Director = new DirectorMethods(this);
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
