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
    /// Lexi embeddings methods implementation.
    /// </summary>
    public class LexiEmbeddingsMethods : ILexiEmbeddingsMethods
    {
        #region Private-Members

        private readonly ViewSdkBase _Sdk;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="sdk">View SDK base instance.</param>
        public LexiEmbeddingsMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc/>
        public async Task<LexiEmbeddingsResult> Process(
             SearchResult results,
             EmbeddingsRule embedRule,
             VectorRepository vectorRepo,
             CancellationToken token = default)
        {
            if (results == null) throw new ArgumentNullException(nameof(results));
            if (embedRule == null) throw new ArgumentNullException(nameof(embedRule));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/processing/lexiprocessing";

            try
            {
                using (RestRequest req = new RestRequest(url, HttpMethod.Post))
                {
                    req.TimeoutMilliseconds = _Sdk.TimeoutMs;
                    req.ContentType = "application/json";
                    req.Authorization.BearerToken = _Sdk.AccessKey;

                    LexiEmbeddingsRequest procReq = new LexiEmbeddingsRequest
                    {
                        Results = results,
                        EmbeddingsRule = embedRule,
                        VectorRepository = vectorRepo
                    };

                    string json = _Sdk.Serializer.SerializeJson(procReq, true);

                    if (_Sdk.LogRequests) _Sdk.Log(SeverityEnum.Debug, "request body: " + Environment.NewLine + json);

                    using (RestResponse resp = await req.SendAsync(json, token).ConfigureAwait(false))
                    {
                        if (resp != null)
                        {
                            string responseData = await _Sdk.ReadResponse(resp, url, token).ConfigureAwait(false);

                            if (_Sdk.LogResponses) _Sdk.Log(SeverityEnum.Debug, "response (status " + resp.StatusCode + "):" + Environment.NewLine + responseData);

                            if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                            {
                                _Sdk.Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");

                                if (!String.IsNullOrEmpty(responseData))
                                {
                                    try
                                    {
                                        LexiEmbeddingsResult procResp = _Sdk.Serializer.DeserializeJson<LexiEmbeddingsResult>(responseData);
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
                                        LexiEmbeddingsResult procResp = _Sdk.Serializer.DeserializeJson<LexiEmbeddingsResult>(responseData);
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
                return new LexiEmbeddingsResult
                {
                    Success = false,
                    Error = new ApiErrorResponse(ApiErrorEnum.InternalError, null, null)
                };
            }
        }

        #endregion
    }
}
