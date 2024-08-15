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
    public class ViewLexiEmbeddingsSdk : ViewSdkBase
    {
        #region Public-Members

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="endpoint">Endpoint URL.</param>
        public ViewLexiEmbeddingsSdk(string endpoint = "http://localhost:8501/lexi/embeddings") : base(endpoint)
        {
            Header = "[ViewLexiEmbeddingsSdk] ";
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// Process a document.
        /// </summary>
        /// <param name="tenant">Tenant metadata.</param>
        /// <param name="collection">Collection.</param>
        /// <param name="results">Search results.</param>
        /// <param name="embedRule">Embeddings rule.</param>
        /// <param name="vectorRepo">Vector repository.</param>
        /// <param name="graphRepo">Graph repository.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task<LexiEmbeddingsResponse> Process(
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
                    req.TimeoutMilliseconds = TimeoutMilliseconds;
                    req.ContentType = "application/json";

                    LexiEmbeddingsRequest procReq = new LexiEmbeddingsRequest
                    {
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
                            if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                            {
                                Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");

                                if (!String.IsNullOrEmpty(resp.DataAsString))
                                {
                                    if (LogResponses) Log(SeverityEnum.Debug, "response body: " + Environment.NewLine + resp.DataAsString);

                                    LexiEmbeddingsResponse procResp = Serializer.DeserializeJson<LexiEmbeddingsResponse>(resp.DataAsString);
                                    return procResp;
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
                                    if (LogResponses) Log(SeverityEnum.Debug, "response body: " + Environment.NewLine + resp.DataAsString);

                                    LexiEmbeddingsResponse procResp = Serializer.DeserializeJson<LexiEmbeddingsResponse>(resp.DataAsString);
                                    return procResp;
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
