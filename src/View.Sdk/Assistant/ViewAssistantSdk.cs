namespace View.Sdk.Assistant
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using RestWrapper;
    using View.Sdk;
    using View.Sdk.Assistant.Implementations;
    using View.Sdk.Assistant.Interfaces;

    /// <summary>
    /// View Assistant SDK.
    /// </summary>
    public class ViewAssistantSdk : ViewSdkBase, IDisposable
    {
        #region Public-Members

        /// <summary>
        /// Assistant configuration methods.
        /// </summary>
        public IConfigMethods Config { get; set; }
        
        /// <summary>
        /// Assistant chat methods.
        /// </summary>
        public IChatMethods Chat { get; set; }

        /// <summary>
        /// Assistant chat thread methods.
        /// </summary>
        public IChatThreadMethods Thread { get; set; }

        /// <summary>
        /// Assistant model methods.
        /// </summary>
        public IModelMethods Model { get; set; }

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="tenantGuid">Tenant GUID.</param>
        /// <param name="accessKey">Access key.</param>
        /// <param name="endpoint">Endpoint URL.</param>
        public ViewAssistantSdk(Guid tenantGuid, string accessKey, string endpoint = "http://localhost:8000/") : base(tenantGuid, accessKey, endpoint)
        { 
            Header = "[ViewAssistantSdk] ";
            Config = new ConfigMethods(this);
            Chat = new ChatMethods(this);
            Thread = new ChatThreadMethods(this);
            Model = new ModelMethods(this);
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
