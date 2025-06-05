namespace View.Sdk.Crawler
{
    using System;

    /// <summary>
    /// Request object for crawl operation actions.
    /// </summary>
    public class CrawlOperationRequest
    {
        #region Public-Members

        /// <summary>
        /// GUID of the crawl operation.
        /// </summary>
        public Guid? GUID { get; set; } = null;

        /// <summary>
        /// Name of the crawl operation.
        /// </summary>
        public string Name { get; set; } = null;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate the object.
        /// </summary>
        public CrawlOperationRequest()
        {
        }

        /// <summary>
        /// Instantiate the object.
        /// </summary>
        /// <param name="name">Name of the crawl operation.</param>
        public CrawlOperationRequest(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Instantiate the object.
        /// </summary>
        /// <param name="guid">GUID of the crawl operation.</param>
        /// <param name="name">Name of the crawl operation.</param>
        public CrawlOperationRequest(Guid guid, string name)
        {
            GUID = guid;
            Name = name;
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}