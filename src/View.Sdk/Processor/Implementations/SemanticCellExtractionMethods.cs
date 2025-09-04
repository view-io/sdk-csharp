namespace View.Sdk.Processor.Implementations
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using RestWrapper;
    using View.Sdk.Processor;
    using View.Sdk.Processor.Interfaces;
    using View.Sdk.Semantic;

    /// <summary>
    /// Semantic cell extraction methods implementation.
    /// </summary>
    public class SemanticCellExtractionMethods : ISemanticCellExtractionMethods
    {
        #region Private-Members

        private readonly ViewSdkBase _Sdk;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public SemanticCellExtractionMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc/>
        public async Task<SemanticCellResult> Process(SemanticCellExtractionRequest request, CancellationToken token = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/processing/semanticcell";
            try
            {
                using (RestRequest req = new RestRequest(url, HttpMethod.Post, "application/json"))
                {
                    req.TimeoutMilliseconds = _Sdk.TimeoutMs;
                    req.ContentType = "application/json";
                    req.Authorization.BearerToken = _Sdk.AccessKey;

                    string json = _Sdk.Serializer.SerializeJson(request, true);

                    if (_Sdk.LogRequests) _Sdk.Log(SeverityEnum.Debug, "request body: " + Environment.NewLine + json);

                    using (RestResponse resp = await req.SendAsync(json, token).ConfigureAwait(false))
                    {
                        if (resp != null)
                        {
                            if (_Sdk.LogResponses) _Sdk.Log(SeverityEnum.Debug, "response (status " + resp.StatusCode + "):" + Environment.NewLine + resp.DataAsString);

                            if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                            {
                                _Sdk.Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");

                                if (!String.IsNullOrEmpty(resp.DataAsString))
                                {
                                    try
                                    {
                                        SemanticCellResult procResp = _Sdk.Serializer.DeserializeJson<SemanticCellResult>(resp.DataAsString);
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

                                if (!String.IsNullOrEmpty(resp.DataAsString))
                                {
                                    try
                                    {
                                        SemanticCellResult procResp = _Sdk.Serializer.DeserializeJson<SemanticCellResult>(resp.DataAsString);
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
                return new SemanticCellResult
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
