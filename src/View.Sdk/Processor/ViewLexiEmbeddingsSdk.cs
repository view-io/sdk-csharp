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

        /// <summary>
        /// Enable or disable logging of request bodies.
        /// </summary>
        public bool LogRequests { get; set; } = false;

        /// <summary>
        /// Enable or disable logging of response bodies.
        /// </summary>
        public bool LogResponses { get; set; } = false;

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
        /// <param name="results">Search results.</param>
        /// <param name="embedRule">Embeddings rule.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task<LexiEmbeddingsResponse> Process(
            SearchResult results, 
            EmbeddingsRule embedRule, 
            CancellationToken token = default)
        {
            if (results == null) throw new ArgumentNullException(nameof(results));
            if (embedRule == null) throw new ArgumentNullException(nameof(embedRule));

            string url = Endpoint;

            try
            {
                using (RestRequest req = new RestRequest(url, HttpMethod.Post))
                {
                    req.ContentType = "application/json";

                    LexiEmbeddingsRequest procReq = new LexiEmbeddingsRequest
                    {
                        Results = results,
                        EmbeddingsRule = embedRule
                    };

                    string json = Serializer.SerializeJson(procReq, true);

                    if (LogRequests) Log(Severity.Debug, "request body: " + Environment.NewLine + json);

                    using (RestResponse resp = await req.SendAsync(json, token).ConfigureAwait(false))
                    {
                        if (resp != null)
                        {
                            if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                            {
                                Log(Severity.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");

                                if (!String.IsNullOrEmpty(resp.DataAsString))
                                {
                                    if (LogResponses) Log(Severity.Debug, "response body: " + Environment.NewLine + resp.DataAsString);

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
                                Log(Severity.Warn, "non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");

                                if (!String.IsNullOrEmpty(resp.DataAsString))
                                {
                                    if (LogResponses) Log(Severity.Debug, "response body: " + Environment.NewLine + resp.DataAsString);

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
                            Log(Severity.Warn, "no response from " + url);
                            return null;
                        }
                    }
                }
            }
            catch (HttpRequestException hre)
            {
                Log(Severity.Warn, "exception while interacting with " + url + ": " + hre.Message);
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
