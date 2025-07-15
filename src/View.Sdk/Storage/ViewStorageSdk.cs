namespace View.Sdk.Storage
{
    using System;
    using View.Sdk;
    using View.Sdk.Storage.Implementations;
    using View.Sdk.Storage.Interfaces;

    /// <summary>
    /// View Storage SDK.
    /// </summary>
    public class ViewStorageSdk : ViewSdkBase, IDisposable
    {
        #region Public-Members

        /// <summary>
        /// Bucket methods.
        /// </summary>
        public IBucketMethods Bucket { get; set; }
        
        /// <summary>
        /// Object methods.
        /// </summary>
        public IObjectMethods Object { get; set; }
        
        /// <summary>
        /// Multipart upload methods.
        /// </summary>
        public IMultipartUploadMethods MultipartUpload { get; set; }

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
        public ViewStorageSdk(Guid tenantGuid, string accessKey, string endpoint = "http://localhost:8001/") : base(tenantGuid, accessKey, endpoint)
        {
            Header = "[ViewStorageSdk] ";
            Bucket = new BucketMethods(this);
            Object = new ObjectMethods(this);
            MultipartUpload = new MultipartUploadMethods(this);
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
