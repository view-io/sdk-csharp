namespace View.Sdk.EnterpriseDesktop
{
    using System;
    using View.Sdk.EnterpriseDesktop.Implementations;
    using View.Sdk.EnterpriseDesktop.Interfaces;

    /// <summary>
    /// View Enterprise Desktop SDK.
    /// </summary>
    public class ViewEnterpriseDesktopSdk : ViewSdkBase, IDisposable
    {
        #region Public-Members

        /// <summary>
        /// Desktop user methods.
        /// </summary>
        public IDesktopUserMethods DesktopUser { get; set; }

        /// <summary>
        /// Desktop group methods.
        /// </summary>
        public IDesktopGroupMethods DesktopGroup { get; set; }

        /// <summary>
        /// Desktop printer methods.
        /// </summary>
        public IDesktopPrinterMethods DesktopPrinter { get; set; }

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
        public ViewEnterpriseDesktopSdk(Guid tenantGuid, string accessKey, string endpoint = "http://localhost:8000/") : base(tenantGuid, accessKey, endpoint)
        {
            DesktopUser = new DesktopUserMethods(this);
            DesktopGroup = new DesktopGroupMethods(this);
            DesktopPrinter = new DesktopPrinterMethods(this);
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
