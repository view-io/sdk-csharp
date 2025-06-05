namespace View.Sdk.Crawler
{
    using System;
    using View.Sdk.Crawler.Implementations;
    using View.Sdk.Crawler.Interfaces;

    /// <summary>
    /// View Crawler SDK.
    /// </summary>
    public class ViewCrawlerSdk : ViewSdkBase, IDisposable
    {
        #region Public-Members

        /// <summary>
        /// Data repository methods.
        /// </summary>
        public IDataRepositoryMethods DataRepository { get; set; }

        /// <summary>
        /// Crawl schedule methods.
        /// </summary>
        public ICrawlScheduleMethods CrawlSchedule { get; set; }

        /// <summary>
        /// Crawl filter methods.
        /// </summary>
        public ICrawlFilterMethods CrawlFilter { get; set; }

        /// <summary>
        /// Crawl plan methods.
        /// </summary>
        public ICrawlPlanMethods CrawlPlan { get; set; }

        /// <summary>
        /// Crawl operation methods.
        /// </summary>
        public ICrawlOperationMethods CrawlOperation { get; set; }

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
        public ViewCrawlerSdk(Guid tenantGuid, string accessKey, string endpoint = "http://localhost:8000/") : base(tenantGuid, accessKey, endpoint)
        {
            Header = "[ViewCrawlerSdk] ";
            DataRepository = new DataRepositoryMethods(this);
            CrawlSchedule = new CrawlScheduleMethods(this);
            CrawlFilter = new CrawlFilterMethods(this);
            CrawlPlan = new CrawlPlanMethods(this);
            CrawlOperation = new CrawlOperationMethods(this);
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
