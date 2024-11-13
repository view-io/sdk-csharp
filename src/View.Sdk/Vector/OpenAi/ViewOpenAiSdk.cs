namespace View.Sdk.Vector.OpenAI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net.Http;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;
    using RestWrapper;
    using View.Sdk;
    using View.Sdk.Serialization;
    using View.Sdk.Semantic;
    using View.Sdk.Vector.Langchain;

    /// <summary>
    /// OpenAI embeddings generator.
    /// </summary>
    public class ViewOpenAiSdk : ViewEmbeddingsSdkBase, IDisposable
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private string _DefaultModel = "text-embedding-ada-002";

        #endregion

        #region Constructors-and-Factories

        /// <inheritdoc />
        public ViewOpenAiSdk(
            string endpoint,
            string apiKey = null,
            Action<SeverityEnum, string> logger = null) : base(
                EmbeddingsGeneratorEnum.OpenAI,
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
                string url = Endpoint + "models";

                using (RestRequest req = new RestRequest(url, HttpMethod.Head))
                {
                    req.Authorization.BearerToken = ApiKey;

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
        public override async Task<EmbeddingsResult> GenerateEmbeddings(EmbeddingsRequest embedRequest, int timeoutMs = 30000, CancellationToken token = default)
        {
            if (embedRequest == null) throw new ArgumentNullException(nameof(embedRequest));
            if (timeoutMs < 1) throw new ArgumentOutOfRangeException(nameof(timeoutMs));
            if (String.IsNullOrEmpty(embedRequest.Model)) embedRequest.Model = _DefaultModel;
            string url = Endpoint + "embeddings";

            using (RestRequest req = new RestRequest(url, HttpMethod.Post))
            {
                req.ContentType = "application/json";
                req.TimeoutMilliseconds = timeoutMs;
                req.Authorization.BearerToken = ApiKey;

                string json = Serializer.SerializeJson(OpenAiEmbeddingsRequest.FromEmbeddingsRequest(embedRequest), true);
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
                                OpenAiEmbeddingsResult embedResult = Serializer.DeserializeJson<OpenAiEmbeddingsResult>(resp.DataAsString);
                                return embedResult.ToEmbeddingsResult(OpenAiEmbeddingsRequest.FromEmbeddingsRequest(embedRequest));
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
