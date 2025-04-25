namespace View.Sdk.Processor
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using RestWrapper;
    using View.Sdk.Serialization;
    using View.Sdk;
    using System.Net.Sockets;

    /// <summary>
    /// View Processing Pipeline SDK.
    /// </summary>
    public class ViewProcessorSdk : ViewSdkBase
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
        /// <param name="endpoint">Endpoint URL, i.e. http://localhost:8000/v1.0/tenants/tenant-guid/processing.</param>
        public ViewProcessorSdk(Guid tenantGuid, string accessKey, string endpoint = "http://localhost:8000/v1.0/tenants/default/processing") : base(tenantGuid, accessKey, endpoint)
        {
            Header = "[ViewProcessorSdk] ";
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// Process a document.  This variant is used by the storage server.
        /// </summary>
        /// <param name="requestGuid">Request GUID.</param>
        /// <param name="mdRuleGuid">Metadata rule GUID.</param>
        /// <param name="embedRuleGuid">Embeddings rule GUID.</param>
        /// <param name="obj">Object metadata.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Processor response.</returns>
        public async Task<ProcessorResult> Process(
            Guid requestGuid,
            Guid mdRuleGuid,
            Guid embedRuleGuid,
            ObjectMetadata obj,
            CancellationToken token = default)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            string url = Endpoint;

            try
            {
                using (RestRequest req = new RestRequest(url, HttpMethod.Post))
                {
                    req.TimeoutMilliseconds = TimeoutMs;
                    req.ContentType = "application/json";
                    req.Authorization.BearerToken = AccessKey;

                    ProcessorRequest procReq = new ProcessorRequest
                    {
                        GUID = requestGuid,
                        MetadataRuleGUID = mdRuleGuid,
                        EmbeddingsRuleGUID = embedRuleGuid,
                        Object = obj
                    };

                    string json = Serializer.SerializeJson(procReq, true);
                    if (LogRequests) Log(SeverityEnum.Debug, "request: " + Environment.NewLine + json);

                    using (RestResponse resp = await req.SendAsync(json, token).ConfigureAwait(false))
                    {
                        if (resp != null)
                        {
                            if (LogResponses) Log(SeverityEnum.Debug, "response (status " + resp.StatusCode + "):" + Environment.NewLine + resp.DataAsString);

                            if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                            {
                                Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");

                                if (!String.IsNullOrEmpty(resp.DataAsString))
                                {
                                    try
                                    {
                                        ProcessorResult procResp = Serializer.DeserializeJson<ProcessorResult>(resp.DataAsString);
                                        return procResp;
                                    }
                                    catch (Exception)
                                    {
                                        Log(SeverityEnum.Warn, "unable to deserialize response body, returning null");
                                        return null;
                                    }
                                }
                                else
                                {
                                    return null;
                                }
                            }
                            else
                            {
                                Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");

                                if (!String.IsNullOrEmpty(resp.DataAsString))
                                {
                                    try
                                    {
                                        ProcessorResult procResp = Serializer.DeserializeJson<ProcessorResult>(resp.DataAsString);
                                        return procResp;
                                    }
                                    catch (Exception)
                                    {
                                        Log(SeverityEnum.Warn, "unable to deserialize response body, returning null");
                                        return null;
                                    }
                                }
                                else
                                {
                                    return null;
                                }
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
            catch (HttpRequestException hre)
            {
                Log(SeverityEnum.Warn, "exception while interacting with " + url + ": " + hre.Message);
                return new ProcessorResult
                {
                    Success = false,
                    Error = new ApiErrorResponse(ApiErrorEnum.InternalError, null, null)
                };
            }
        }

        #endregion

        #region Private-Methods
         
        #endregion
    }
}
