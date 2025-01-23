namespace View.Sdk.Storage
{
    using RestWrapper;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Serialization;
    using View.Sdk;

    /// <summary>
    /// View Storage SDK.
    /// </summary>
    public class ViewStorageSdk : ViewSdkBase, IDisposable
    {
        #region Public-Members

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="tenantGuid">Tenant GUID.</param>
        /// <param name="accessKey">Access key.</param>
        /// <param name="endpoint">Endpoint URL, i.e. http://localhost:8001.</param>
        public ViewStorageSdk(string tenantGuid, string accessKey, string endpoint = "http://localhost:8001/") : base(tenantGuid, accessKey, endpoint)
        {
            Header = "[ViewStorageSdk] ";
        }

        #endregion

        #region Public-Methods

        #region Buckets

        #endregion

        #region Objects

        #endregion

        #region Multipart-Uploads

        #endregion

        #region Tags

        #endregion

        #region ACLs

        #endregion

        #endregion

        #region Private-Methods

        #endregion
    }
}
