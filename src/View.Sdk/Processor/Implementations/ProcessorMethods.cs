namespace View.Sdk.Processor.Implementations
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using RestWrapper;
    using View.Sdk;
    using View.Sdk.Processor.Interfaces;

    /// <summary>
    /// Processor methods implementation.
    /// </summary>
    public class ProcessorMethods : IProcessorMethods
    {
        #region Private-Members

        private readonly ViewSdkBase _Sdk;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="sdk">View SDK base instance.</param>
        public ProcessorMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc/>
        public async Task<ProcessorResult> Process(
            Guid mdRuleGuid,
            Guid embedRuleGuid,
            ObjectMetadata obj,
            CancellationToken token = default)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/processing";

            try
            {
                using (RestRequest req = new RestRequest(url, HttpMethod.Post))
                {
                    req.TimeoutMilliseconds = _Sdk.TimeoutMs;
                    req.ContentType = "application/json";
                    req.Authorization.BearerToken = _Sdk.AccessKey;

                    ProcessorRequest procReq = new ProcessorRequest
                    {
                        MetadataRuleGUID = mdRuleGuid,
                        EmbeddingsRuleGUID = embedRuleGuid,
                        Object = obj
                    };

                    string json = _Sdk.Serializer.SerializeJson(procReq, true);
                    if (_Sdk.LogRequests) _Sdk.Log(SeverityEnum.Debug, "request: " + Environment.NewLine + json);

                    using (RestResponse resp = await req.SendAsync(json, token).ConfigureAwait(false))
                    {
                        if (resp != null)
                        {
                            string responseData = await _Sdk.ReadResponseDataAsync(resp, url, token).ConfigureAwait(false);

                            if (_Sdk.LogResponses) _Sdk.Log(SeverityEnum.Debug, "response (status " + resp.StatusCode + "):" + Environment.NewLine + responseData);

                            if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                            {
                                _Sdk.Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");

                                if (!String.IsNullOrEmpty(responseData))
                                {
                                    try
                                    {
                                        ProcessorResult procResp = _Sdk.Serializer.DeserializeJson<ProcessorResult>(responseData);
                                        return procResp;
                                    }
                                    catch (Exception)
                                    {
                                        _Sdk.Log(SeverityEnum.Warn, "unable to deserialize response body, returning null");
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
                                _Sdk.Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");

                                if (!String.IsNullOrEmpty(responseData))
                                {
                                    try
                                    {
                                        ProcessorResult procResp = _Sdk.Serializer.DeserializeJson<ProcessorResult>(responseData);
                                        return procResp;
                                    }
                                    catch (Exception)
                                    {
                                        _Sdk.Log(SeverityEnum.Warn, "unable to deserialize response body, returning null");
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
                            _Sdk.Log(SeverityEnum.Warn, "no response from " + url);
                            return null;
                        }
                    }
                }
            }
            catch (HttpRequestException hre)
            {
                _Sdk.Log(SeverityEnum.Warn, "exception while interacting with " + url + ": " + hre.Message);
                return new ProcessorResult
                {
                    Success = false,
                    Error = new ApiErrorResponse(ApiErrorEnum.InternalError, null, null)
                };
            }
        }

        /// <inheritdoc/>
        public async Task<EnumerationResult<ProcessorTask>> Enumerate(
            int maxKeys = 5,
            CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/processortasks?max-keys=" + maxKeys;
            return await _Sdk.Enumerate<ProcessorTask>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<ProcessorTask> Retrieve(
            Guid guid,
            CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/processortasks/" + guid;
            return await _Sdk.Retrieve<ProcessorTask>(url, token).ConfigureAwait(false);
        }

        #endregion
    }
}
