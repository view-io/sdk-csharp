namespace View.Sdk.Processor
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using RestWrapper;
    using View.Sdk;

    /// <summary>
    /// View SDK for generating embeddings with Lexi search results.
    /// </summary>
    public class ViewLexiEmbeddingsSdk : ViewSdkBase, IDisposable
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
        /// <param name="endpoint">Endpoint URL, i.e. http://localhost:8000/v1.0/tenants/tenant-guid/processing/lexiprocessing.</param>
        public ViewLexiEmbeddingsSdk(string tenantGuid, string accessKey, string endpoint = "http://localhost:8000/v1.0/tenants/default/processing/lexiprocessing") : base(tenantGuid, accessKey, endpoint)
        {
            Header = "[ViewLexiEmbeddingsSdk] ";
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// Process a document.
        /// </summary>
        /// <param name="requestGuid">Request GUID.</param>
        /// <param name="tenant">Tenant metadata.</param>
        /// <param name="collection">Collection.</param>
        /// <param name="results">Search results.</param>
        /// <param name="embedRule">Embeddings rule.</param>
        /// <param name="vectorRepo">Vector repository.</param>
        /// <param name="graphRepo">Graph repository.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task<LexiEmbeddingsResponse> Process(
            string requestGuid,
            TenantMetadata tenant,
            Collection collection,
            SearchResult results, 
            EmbeddingsRule embedRule,
            VectorRepository vectorRepo,
            GraphRepository graphRepo,
            CancellationToken token = default)
        {
            if (results == null) throw new ArgumentNullException(nameof(results));
            if (embedRule == null) throw new ArgumentNullException(nameof(embedRule));

            string url = Endpoint;

            try
            {
                using (RestRequest req = new RestRequest(url, HttpMethod.Post))
                {
                    req.TimeoutMilliseconds = TimeoutMs;
                    req.ContentType = "application/json";
                    req.Authorization.BearerToken = AccessKey;

                    LexiEmbeddingsRequest procReq = new LexiEmbeddingsRequest
                    {
                        GUID = requestGuid,
                        Tenant = tenant,
                        Collection = collection,
                        Results = results,
                        EmbeddingsRule = embedRule,
                        VectorRepository = vectorRepo,
                        GraphRepository = graphRepo
                    };

                    string json = Serializer.SerializeJson(procReq, true);

                    if (LogRequests) Log(SeverityEnum.Debug, "request body: " + Environment.NewLine + json);

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
                                        LexiEmbeddingsResponse procResp = Serializer.DeserializeJson<LexiEmbeddingsResponse>(resp.DataAsString);
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
                                        LexiEmbeddingsResponse procResp = Serializer.DeserializeJson<LexiEmbeddingsResponse>(resp.DataAsString);
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
                return new LexiEmbeddingsResponse
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
