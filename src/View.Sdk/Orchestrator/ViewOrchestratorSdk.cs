namespace View.Sdk.Orchestrator
{
    using RestWrapper;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk;
    using View.Sdk.Orchestrator.Implementations;
    using View.Sdk.Orchestrator.Interfaces;

    /// <summary>
    /// View Orchestrator SDK.
    /// </summary>
    public class ViewOrchestratorSdk : ViewSdkBase, IDisposable
    {
        #region Public-Members

        /// <summary>
        /// Trigger methods.
        /// </summary>
        public ITriggerMethods Trigger { get; private set; }

        /// <summary>
        /// Step methods.
        /// </summary>
        public IStepMethods Step { get; private set; }

        /// <summary>
        /// Data flow methods.
        /// </summary>
        public IDataFlowMethods DataFlow { get; private set; }

        /// <summary>
        /// Data flow log methods.
        /// </summary>
        public IDataFlowLogMethods DataFlowLog { get; private set; }

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
        public ViewOrchestratorSdk(Guid tenantGuid, string accessKey, string endpoint = "http://localhost:8000/") : base(tenantGuid, accessKey, endpoint)
        {
            Header = "[ViewOrchestratorSdk] ";
            Trigger = new TriggerMethods(this);
            Step = new StepMethods(this);
            DataFlow = new DataFlowMethods(this);
            DataFlowLog = new DataFlowLogMethods(this);
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
