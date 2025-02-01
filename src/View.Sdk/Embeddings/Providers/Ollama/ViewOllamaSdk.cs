namespace View.Sdk.Embeddings.Providers.Ollama
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using RestWrapper;
    using View.Sdk;
    using View.Sdk.Serialization;
    using View.Sdk.Embeddings;
    using View.Sdk.Embeddings.Providers;

    /// <summary>
    /// View Ollama SDK.  This SDK interacts directly with an underlying Ollama microservice.
    /// </summary>
    public class ViewOllamaSdk : EmbeddingsProviderSdkBase, IDisposable
    {
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

        #region Public-Members

        #endregion

        #region Private-Members

        private Serializer _Serializer = new Serializer();
        private string _DefaultModel = "llama3";

        #endregion

        #region Constructors-and-Factories

        /// <inheritdoc />
        public ViewOllamaSdk(
            Guid tenantGuid,
            string baseUrl = "http://localhost:11434/",
            string apiKey = null) : base(
                tenantGuid,
                EmbeddingsGeneratorEnum.Ollama,
                baseUrl,
                apiKey)
        {
            Header = "[OllamaSdk] ";
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public override async Task<bool> ValidateConnectivity(CancellationToken token = default)
        {
            try
            {
                using (RestRequest req = new RestRequest(BaseUrl, HttpMethod.Head))
                {
                    using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                    {
                        if (resp != null && resp.StatusCode >= 200 && resp.StatusCode <= 299) return true;
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <inheritdoc />
        public override async Task<EmbeddingsResult> GenerateEmbeddings(
            EmbeddingsRequest embedRequest,
            int timeoutMs = 30000,
            CancellationToken token = default)
        {
            if (embedRequest == null) throw new ArgumentNullException(nameof(embedRequest));
            if (timeoutMs < 1) throw new ArgumentOutOfRangeException(nameof(timeoutMs));
            if (string.IsNullOrEmpty(embedRequest.Model)) embedRequest.Model = _DefaultModel;

            string url = BaseUrl + "api/embed";

            using (RestRequest req = new RestRequest(url, HttpMethod.Post))
            {
                req.ContentType = "application/json";
                req.TimeoutMilliseconds = timeoutMs;
                req.Authorization.BearerToken = ApiKey;

                string json = Serializer.SerializeJson(OllamaEmbeddingsRequest.FromEmbeddingsRequest(embedRequest), true);
                if (LogRequests) Log(SeverityEnum.Debug, "request:" + Environment.NewLine + json);

                using (RestResponse resp = await req.SendAsync(json, token).ConfigureAwait(false))
                {
                    if (resp == null)
                    {
                        Log(SeverityEnum.Warn, "no response from " + url);
                        return new EmbeddingsResult
                        {
                            Success = false,
                            StatusCode = resp.StatusCode,
                            Error = new ApiErrorResponse(ApiErrorEnum.NoEmbeddingsConnectivity, null, "No connectivity to embeddings provider at " + url + ".")
                        };
                    }
                    else
                    {
                        if (LogResponses) Log(SeverityEnum.Debug, "response (status " + resp.StatusCode + "): " + Environment.NewLine + resp.DataAsString);

                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            if (!string.IsNullOrEmpty(resp.DataAsString))
                            {
                                Log(SeverityEnum.Debug, "deserializing response body");
                                OllamaEmbeddingsResult ollamaResult = Serializer.DeserializeJson<OllamaEmbeddingsResult>(resp.DataAsString);
                                return ollamaResult.ToEmbeddingsResult(embedRequest, true, resp.StatusCode, null);
                            }
                            else
                            {
                                Log(SeverityEnum.Warn, "no data received from " + url);
                                return new EmbeddingsResult
                                {
                                    Success = false,
                                    StatusCode = resp.StatusCode,
                                    Error = new ApiErrorResponse(ApiErrorEnum.EmbeddingsGenerationFailed, null, "No embeddings returned from the provider.")
                                };
                            }
                        }
                        else
                        {
                            Log(SeverityEnum.Warn, "status " + resp.StatusCode + " received from " + url + ": " + Environment.NewLine + resp.DataAsString);
                            return new EmbeddingsResult
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

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    }
}
