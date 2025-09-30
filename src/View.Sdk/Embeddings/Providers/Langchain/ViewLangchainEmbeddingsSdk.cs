namespace View.Sdk.Embeddings.Providers.Langchain
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using RestWrapper;
    using View.Sdk;
    using View.Sdk.Embeddings;
    using View.Sdk.Embeddings.Providers;
    using View.Sdk.Semantic;
    using View.Sdk.Serialization;

    /// <summary>
    /// View Langchain embeddings SDK.  This SDK interacts directly with the underlying View Embeddings microservice that acts as a Langchain proxy.
    /// </summary>
    public class ViewLangchainEmbeddingsSdk : EmbeddingsProviderSdkBase, IDisposable
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private string _DefaultModel = "all-MiniLM-L6-v2";

        #endregion

        #region Constructors-and-Factories

        /// <inheritdoc />
        public ViewLangchainEmbeddingsSdk(
            Guid tenantGuid,
            string baseUrl = "http://localhost:8000/",
            string apiKey = null) : base(
                tenantGuid,
                EmbeddingsGeneratorEnum.LCProxy,
                baseUrl,
                apiKey)
        {
            Header = "[LangchainSdk] ";
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public override async Task<bool> ValidateConnectivity(CancellationToken token = default)
        {
            string url = BaseUrl;

            try
            {
                using (RestRequest req = new RestRequest(url, HttpMethod.Get))
                {
                    req.Authorization.BearerToken = ApiKey;

                    using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                    {
                        if (resp != null)
                        {
                            if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                            {
                                return true;
                            }
                            else
                            {
                                Log(SeverityEnum.Warn, "non-success response " + resp.StatusCode + " from " + url);
                                return false;
                            }
                        }
                        else
                        {
                            Log(SeverityEnum.Warn, "no response from " + url);
                            return false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log(SeverityEnum.Warn, "exception while connecting to " + url + Environment.NewLine + e.ToString());
                return false;
            }
        }

        /// <inheritdoc />
        public override async Task<GenerateEmbeddingsResult> GenerateEmbeddings(
            GenerateEmbeddingsRequest embedRequest,
            int timeoutMs = 30000,
            CancellationToken token = default)
        {
            if (embedRequest == null) throw new ArgumentNullException(nameof(embedRequest));
            if (timeoutMs < 1) throw new ArgumentOutOfRangeException(nameof(timeoutMs));
            if (string.IsNullOrEmpty(embedRequest.Model)) embedRequest.Model = _DefaultModel;

            string url = embedRequest.EmbeddingsRule.EmbeddingsGeneratorUrl + "v1.0/tenants/" + TenantGUID + "/embeddings/";

            using (RestRequest req = new RestRequest(url, HttpMethod.Post))
            {
                req.ContentType = "application/json";
                req.TimeoutMilliseconds = timeoutMs;
                req.Authorization.BearerToken = ApiKey;

                string json = Serializer.SerializeJson(LangchainEmbeddingsRequest.FromEmbeddingsRequest(embedRequest), true);
                if (LogRequests) Log(SeverityEnum.Debug, "request:" + Environment.NewLine + json);

                using (RestResponse resp = await req.SendAsync(json, token).ConfigureAwait(false))
                {
                    if (resp == null)
                    {
                        Log(SeverityEnum.Warn, "no response from " + url);
                        return new GenerateEmbeddingsResult
                        {
                            Success = false,
                            StatusCode = 0,
                            Error = new ApiErrorResponse(ApiErrorEnum.NoEmbeddingsConnectivity, null, "No connectivity to embeddings provider at " + url + ".")
                        };
                    }
                    else
                    {
                        string responseData = await ReadResponseDataAsync(resp, url, token).ConfigureAwait(false);

                        if (LogResponses) Log(SeverityEnum.Debug, "response (status " + resp.StatusCode + "): " + Environment.NewLine + responseData);

                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            if (!string.IsNullOrEmpty(responseData))
                            {
                                Log(SeverityEnum.Debug, "deserializing response body");
                                LangchainEmbeddingsResult langchainResult = Serializer.DeserializeJson<LangchainEmbeddingsResult>(responseData);
                                return langchainResult.ToEmbeddingsResult(embedRequest, true, resp.StatusCode, null);
                            }
                            else
                            {
                                Log(SeverityEnum.Warn, "no data received from " + url);
                                return new GenerateEmbeddingsResult
                                {
                                    Success = false,
                                    StatusCode = resp.StatusCode,
                                    Error = new ApiErrorResponse(ApiErrorEnum.EmbeddingsGenerationFailed, null, "No embeddings returned from the provider.")
                                };
                            }
                        }
                        else
                        {
                            Log(SeverityEnum.Warn, "status " + resp.StatusCode + " received from " + url + ": " + Environment.NewLine + responseData);
                            return new GenerateEmbeddingsResult
                            {
                                Success = false,
                                StatusCode = resp.StatusCode,
                                Error = new ApiErrorResponse(ApiErrorEnum.EmbeddingsGenerationFailed, null, "Failure reported by the embeddings provider.")
                            };
                        }
                    }
                }
            }
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}
