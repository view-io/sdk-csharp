﻿namespace View.Sdk.Orchestrator
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
    public class ViewOrchestratorSdk : ViewSdkBase, IDisposable
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
        /// <param name="endpoint">Endpoint URL, i.e. http://localhost:8000/.</param>
        public ViewOrchestratorSdk(Guid tenantGuid, string accessKey, string endpoint = "http://localhost:8000/") : base(tenantGuid, accessKey, endpoint)
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
        public async Task<bool> ExistsTrigger(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/triggers/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a trigger.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Trigger.</returns>
        public async Task<Trigger> RetrieveTrigger(Guid guid, CancellationToken token = default)
        {
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
        /// <returns>True if successful.</returns>
        public async Task<bool> DeleteTrigger(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/triggers/" + guid;
            return await Delete(url, token).ConfigureAwait(false);
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
        public async Task<bool> ExistsStep(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/steps/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a step.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Step.</returns>
        public async Task<StepMetadata> RetrieveStep(Guid guid, CancellationToken token = default)
        {
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
        /// <returns>True if successful.</returns>
        public async Task<bool> DeleteStep(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/steps/" + guid;
            return await Delete(url, token).ConfigureAwait(false);
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
        public async Task<bool> ExistsDataFlow(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/dataflows/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a data flow.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>DataFlow.</returns>
        public async Task<DataFlow> RetrieveDataFlow(Guid guid, CancellationToken token = default)
        {
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
        /// <returns>True if successful.</returns>
        public async Task<bool> DeleteDataFlow(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/dataflows/" + guid;
            return await Delete(url, token).ConfigureAwait(false);
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
        public async Task<List<DataFlowLog>> RetrieveDataFlowLogs(Guid dataFlowGuid, Guid requestGuid, CancellationToken token = default)
        {
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
        public async Task<string> RetrieveDataFlowLogfile(Guid dataFlowGuid, Guid requestGuid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/dataflows/" + dataFlowGuid + "/logfile?request=" + requestGuid;

            using (RestRequest req = new RestRequest(url))
            {
                req.TimeoutMilliseconds = TimeoutMs;
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
