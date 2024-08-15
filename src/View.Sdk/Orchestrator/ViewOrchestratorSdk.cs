namespace View.Sdk.Orchestrator
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
    /// View Orchestrator SDK.
    /// </summary>
    public class ViewOrchestratorSdk : ViewSdkBase
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
        /// <param name="endpoint">Endpoint URL.</param>
        public ViewOrchestratorSdk(string tenantGuid, string accessKey, string endpoint = "http://localhost:8501/") : base(tenantGuid, accessKey, endpoint)
        {
            Header = "[ViewOrchestratorSdk] ";
        }

        #endregion

        #region Public-Methods

        #region Triggers

        /// <summary>
        /// Create a trigger.
        /// </summary>
        /// <param name="trigger">Trigger.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Trigger.</returns>
        public async Task<Trigger> CreateTrigger(Trigger trigger, CancellationToken token = default)
        {
            if (trigger == null) throw new ArgumentNullException(nameof(trigger));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/triggers";
            return await Create<Trigger>(url, trigger, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if a trigger exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> ExistsTrigger(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/triggers/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a trigger.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Trigger.</returns>
        public async Task<Trigger> RetrieveTrigger(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/triggers/" + guid;
            return await Retrieve<Trigger>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read triggers.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Triggers.</returns>
        public async Task<List<Trigger>> RetrieveTriggers(CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/triggers";
            return await RetrieveMany<Trigger>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a trigger.
        /// </summary>
        /// <param name="trigger">Trigger.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Trigger.</returns>
        public async Task<Trigger> UpdateTrigger(Trigger trigger, CancellationToken token = default)
        {
            if (trigger == null) throw new ArgumentNullException(nameof(trigger));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/triggers/" + trigger.GUID;
            return await Update<Trigger>(url, trigger, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a trigger.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteTrigger(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/triggers/" + guid;
            await Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Steps

        /// <summary>
        /// Create a step.
        /// </summary>
        /// <param name="step">Step.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Step.</returns>
        public async Task<StepMetadata> CreateStep(StepMetadata step, CancellationToken token = default)
        {
            if (step == null) throw new ArgumentNullException(nameof(step));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/steps";
            return await Create<StepMetadata>(url, step, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if a step exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> ExistsStep(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/steps/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a step.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Step.</returns>
        public async Task<StepMetadata> RetrieveStep(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/steps/" + guid;
            return await Retrieve<StepMetadata>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read steps.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Steps.</returns>
        public async Task<List<StepMetadata>> RetrieveSteps(CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/steps";
            return await RetrieveMany<StepMetadata>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a step.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteStep(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/steps/" + guid;
            await Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region DataFlows

        /// <summary>
        /// Create a data flow.
        /// </summary>
        /// <param name="flow">DataFlow.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>DataFlow.</returns>
        public async Task<DataFlow> CreateDataFlow(DataFlow flow, CancellationToken token = default)
        {
            if (flow == null) throw new ArgumentNullException(nameof(flow));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/dataflows";
            return await Create<DataFlow>(url, flow, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if a data flow exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> ExistsDataFlow(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/dataflows/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a data flow.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>DataFlow.</returns>
        public async Task<DataFlow> RetrieveDataFlow(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/dataflows/" + guid;
            return await Retrieve<DataFlow>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read data flows.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>DataFlows.</returns>
        public async Task<List<DataFlow>> RetrieveDataFlows(CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/dataflows";
            return await RetrieveMany<DataFlow>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a data flow.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteDataFlow(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/dataflows/" + guid;
            await Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region DataFlow-Logs

        /// <summary>
        /// Read data flow logs.
        /// </summary>
        /// <param name="dataFlowGuid">Data flow GUID.</param>
        /// <param name="requestGuid">Request GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of DataFlowLog.</returns>
        public async Task<List<DataFlowLog>> RetrieveDataFlowLogs(string dataFlowGuid, string requestGuid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(dataFlowGuid)) throw new ArgumentNullException(nameof(dataFlowGuid));
            if (String.IsNullOrEmpty(requestGuid)) throw new ArgumentNullException(nameof(requestGuid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/dataflows/" + dataFlowGuid + "/logs?request=" + requestGuid;
            return await RetrieveMany<DataFlowLog>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read data flow logfile.
        /// </summary>
        /// <param name="dataFlowGuid">Data flow GUID.</param>
        /// <param name="requestGuid">Request GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Log file.</returns>
        public async Task<string> RetrieveDataFlowLogfile(string dataFlowGuid, string requestGuid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(dataFlowGuid)) throw new ArgumentNullException(nameof(dataFlowGuid));
            if (String.IsNullOrEmpty(requestGuid)) throw new ArgumentNullException(nameof(requestGuid));

            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/dataflows/" + dataFlowGuid + "/logfile?request=" + requestGuid;

            using (RestRequest req = new RestRequest(url))
            {
                req.TimeoutMilliseconds = TimeoutMilliseconds;
                req.Authorization.BearerToken = AccessKey;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            return resp.DataAsString;
                        }
                        else
                        {
                            Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            return null;
                        }
                    }
                    else
                    {
                        Log(SeverityEnum.Warn, "no response from " + url);
                        return null;
                    }
                }
            }
        }

        #endregion

        #endregion

        #region Private-Methods

        #endregion
    }
}
