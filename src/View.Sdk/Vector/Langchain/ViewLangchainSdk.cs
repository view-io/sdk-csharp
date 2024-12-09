namespace View.Sdk.Vector.Langchain
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using RestWrapper;
    using View.Sdk;
    using View.Sdk.Semantic;
    using View.Sdk.Serialization;

    /// <summary>
    /// View Langchain SDK.
    /// </summary>
    public class ViewLangchainSdk : EmbeddingsProviderSdkBase, IDisposable
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private string _DefaultModel = "all-MiniLM-L6-v2";

        #endregion

        #region Constructors-and-Factories

        /// <inheritdoc />
        public ViewLangchainSdk(
            string endpoint = "http://localhost:8000/v1.0/tenants/default/embeddings",
            string apiKey = null,
            Action<SeverityEnum, string> logger = null) : base(
                EmbeddingsGeneratorEnum.LCProxy,
                endpoint,
                apiKey,
                logger)
        {
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public override async Task<bool> ValidateConnectivity(CancellationToken token = default)
        {
            try
            {
                using (RestRequest req = new RestRequest(Endpoint, HttpMethod.Head))
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
        public override async Task<bool> LoadModels(List<string> models, CancellationToken token = default)
        {
            if (models == null || models.Count < 1) throw new ArgumentNullException(nameof(models));

            string url = Endpoint + "v1.0/preload/";

            try
            {
                using (RestRequest req = new RestRequest(url, HttpMethod.Post))
                {
                    req.ContentType = "application/json";

                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    dict.Add("Models", models);
                    dict.Add("ApiKey", ApiKey);

                    string json = Serializer.SerializeJson(dict, true);

                    using (RestResponse resp = await req.SendAsync(json, token).ConfigureAwait(false))
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
        public override async Task<EmbeddingsResult> GenerateEmbeddings(EmbeddingsRequest embedRequest, int timeoutMs = 30000, CancellationToken token = default)
        {
            if (embedRequest == null) throw new ArgumentNullException(nameof(embedRequest));
            if (timeoutMs < 1) throw new ArgumentOutOfRangeException(nameof(timeoutMs));
            if (String.IsNullOrEmpty(embedRequest.Model)) embedRequest.Model = _DefaultModel;
            string url = Endpoint + "v1.0/embeddings/";
            
            using (RestRequest req = new RestRequest(url, HttpMethod.Post))
            {
                req.ContentType = "application/json";
                req.TimeoutMilliseconds = timeoutMs;

                string json = Serializer.SerializeJson(LangchainEmbeddingsRequest.FromEmbeddingsRequest(embedRequest), true);
                using (RestResponse resp = await req.SendAsync(json, token).ConfigureAwait(false))
                {
                    if (resp == null)
                    {
                        Logger?.Invoke(SeverityEnum.Warn, "no response from " + url);
                        return null;
                    }
                    else
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            if (!string.IsNullOrEmpty(resp.DataAsString))
                            {
                                LangchainEmbeddingsResult embedResult = Serializer.DeserializeJson<LangchainEmbeddingsResult>(resp.DataAsString);
                                embedResult.Success = true;
                                return embedResult.ToEmbeddingsResult();
                            }
                            else
                            {
                                Logger?.Invoke(SeverityEnum.Warn, "no data received from " + url);
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
                            Logger?.Invoke(SeverityEnum.Warn, "status " + resp.StatusCode + " received from " + url + ": " + Environment.NewLine + resp.DataAsString);
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
