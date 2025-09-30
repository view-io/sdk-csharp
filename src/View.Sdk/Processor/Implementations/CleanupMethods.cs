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
    /// Cleanup methods implementation.
    /// </summary>
    public class CleanupMethods : ICleanupMethods
    {
        #region Private-Members

        private readonly ViewSdkBase _Sdk;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="sdk">View SDK base instance.</param>
        public CleanupMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc/>
        public async Task<CleanupResult> Process(
            TenantMetadata tenant,
            Collection collection,
            StoragePool pool,
            BucketMetadata bucket,
            ObjectMetadata obj,
            MetadataRule mdRule,
            EmbeddingsRule embedRule,
            VectorRepository vectorRepo,
            GraphRepository graphRepo,
            bool async = false,
            CancellationToken token = default)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (mdRule == null) throw new ArgumentNullException(nameof(mdRule));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/processing/cleanup";

            using (RestRequest req = new RestRequest(url, HttpMethod.Post))
            {
                try
                {
                    req.TimeoutMilliseconds = _Sdk.TimeoutMs;
                    req.ContentType = "application/json";
                    req.Authorization.BearerToken = _Sdk.AccessKey;

                    CleanupRequest cleanupReq = new CleanupRequest
                    {
                        Async = async,
                        Tenant = tenant,
                        Collection = collection,
                        Pool = pool,
                        Bucket = bucket,
                        Object = obj,
                        MetadataRule = mdRule,
                        EmbeddingsRule = embedRule,
                        VectorRepository = vectorRepo,
                        GraphRepository = graphRepo
                    };

                    string json = _Sdk.Serializer.SerializeJson(cleanupReq, true);
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
                                        CleanupResult cleanupResp = _Sdk.Serializer.DeserializeJson<CleanupResult>(responseData);
                                        return cleanupResp;
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
                                        CleanupResult cleanupResp = _Sdk.Serializer.DeserializeJson<CleanupResult>(responseData);
                                        return cleanupResp;
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
                catch (HttpRequestException hre)
                {
                    _Sdk.Log(SeverityEnum.Warn, "exception while interacting with " + url + ": " + hre.Message);
                    return new CleanupResult
                    {
                        Success = false,
                        Error = new ApiErrorResponse(ApiErrorEnum.InternalError, null, null)
                    };
                }
            }
        }

        #endregion
    }
}
