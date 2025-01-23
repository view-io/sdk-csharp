namespace View.Sdk.Vector.VoyageAI
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using RestWrapper;
    using View.Sdk;
    using View.Sdk.Serialization;

    /// <summary>
    /// VoyageAI embeddings generator.
    /// </summary>
    public class ViewVoyageAiSdk : EmbeddingsProviderSdkBase, IDisposable
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private string _DefaultModel = "voyage-large-2-instruct";

        #endregion

        #region Constructors-and-Factories

        /// <inheritdoc />
        public ViewVoyageAiSdk(
            string tenantGuid,
            string baseUrl,
            string apiKey = null) : base(
                tenantGuid,
                EmbeddingsGeneratorEnum.VoyageAI,
                baseUrl,
                apiKey)
        {
            Header = "[VoyageAiSdk] ";
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public override async Task<bool> ValidateConnectivity(CancellationToken token = default)
        {
            string url = BaseUrl + "healthz";

            try
            {
                using (RestRequest req = new RestRequest(BaseUrl, HttpMethod.Get))
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
        public override Task<List<ModelInformation>> ListModels(CancellationToken token = default)
        {
            throw new InvalidOperationException("This API is not implemented for this embeddings generator.");
        }

        /// <inheritdoc />
        public override Task<bool> LoadModels(List<string> models, CancellationToken token = default)
        {
            throw new InvalidOperationException("This API is not implemented for this embeddings generator.");
        }

        /// <inheritdoc />
        public override Task<bool> DeleteModel(string name, CancellationToken token = default)
        {
            throw new InvalidOperationException("This API is not implemented for this embeddings generator.");
        }

        /// <inheritdoc />
        public override Task<bool> LoadModel(string model, CancellationToken token = default)
        {
            throw new InvalidOperationException("This API is not implemented for this embeddings generator.");
        }

        /// <inheritdoc />
        public override async Task<EmbeddingsResult> GenerateEmbeddings(
            EmbeddingsRequest embedRequest, 
            int timeoutMs = 30000, 
            CancellationToken token = default)
        {
            if (embedRequest == null) throw new ArgumentNullException(nameof(embedRequest));
            if (timeoutMs < 1) throw new ArgumentOutOfRangeException(nameof(timeoutMs));
            if (String.IsNullOrEmpty(embedRequest.Model)) embedRequest.Model = _DefaultModel;

            string url = BaseUrl + "v1/embeddings";

            using (RestRequest req = new RestRequest(url, HttpMethod.Post))
            {
                req.ContentType = "application/json";
                req.TimeoutMilliseconds = timeoutMs;
                req.Authorization.BearerToken = ApiKey;

                string json = Serializer.SerializeJson(VoyageAiEmbeddingsRequest.FromEmbeddingsRequest(embedRequest), true);
                if (LogRequests) Log(SeverityEnum.Debug, "request:" + Environment.NewLine + json);

                using (RestResponse resp = await req.SendAsync(json, token).ConfigureAwait(false))
                {
                    if (resp == null)
                    {
                        Log(SeverityEnum.Warn, "no response from " + url);
                        return null;
                    }
                    else
                    {
                        if (LogResponses) Log(SeverityEnum.Debug, "response (status " + resp.StatusCode + "): " + Environment.NewLine + resp.DataAsString);

                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                Log(SeverityEnum.Debug, "deserializing response body");
                                VoyageAiEmbeddingsResult embedResult = Serializer.DeserializeJson<VoyageAiEmbeddingsResult>(resp.DataAsString);
                                return embedResult.ToEmbeddingsResult(VoyageAiEmbeddingsRequest.FromEmbeddingsRequest(embedRequest));
                            }
                            else
                            {
                                Log(SeverityEnum.Warn, "no data received from " + url);
                                return new EmbeddingsResult
                                {
                                    Success = false,
                                    StatusCode = resp.StatusCode,
                                    Model = embedRequest.Model
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
                                Model = embedRequest.Model
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
