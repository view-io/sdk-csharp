namespace View.Sdk.Orchestrator.Implementations
{
    using RestWrapper;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk;
    using View.Sdk.Orchestrator.Interfaces;

    /// <summary>
    /// Data flow log methods
    /// </summary>
    public class DataFlowLogMethods : IDataFlowLogMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Data flow log methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public DataFlowLogMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<List<DataFlowLog>> RetrieveLogs(Guid dataFlowGuid, Guid requestGuid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/dataflows/" + dataFlowGuid + "/logs?request=" + requestGuid;
            return await _Sdk.RetrieveMany<DataFlowLog>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<string> RetrieveLogfile(Guid dataFlowGuid, Guid requestGuid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/dataflows/" + dataFlowGuid + "/logfile?request=" + requestGuid;

            using (RestRequest req = new RestRequest(url))
            {
                req.TimeoutMilliseconds = _Sdk.TimeoutMs;
                req.Authorization.BearerToken = _Sdk.AccessKey;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            _Sdk.Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            return resp.DataAsString;
                        }
                        else
                        {
                            _Sdk.Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            return null;
                        }
                    }
                    else
                    {
                        _Sdk.Log(SeverityEnum.Warn, "no response from " + url);
                        return null;
                    }
                }
            }
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}