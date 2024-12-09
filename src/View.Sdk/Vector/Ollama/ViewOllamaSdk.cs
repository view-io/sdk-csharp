namespace View.Sdk.Vector.Ollama
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
    using View.Sdk.Semantic;
    using View.Sdk.Vector.Ollama;
    using View.Sdk.Vector.Langchain;

    /// <summary>
    /// View Ollama SDK.
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
            string endpoint = "http://localhost:7869",
            string apiKey = null,
            Action<SeverityEnum, string> logger = null) : base(
                EmbeddingsGeneratorEnum.Ollama,
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
        public override async Task<bool> LoadModels(List<string> models, CancellationToken token = default)
        {
            if (models == null) throw new ArgumentNullException(nameof(models));
            foreach (string model in models)
            {
                bool success = await LoadModel(model, token).ConfigureAwait(false);
                if (!success) return false;
            }
            return true;
        }

        /// <inheritdoc />
        public override async Task<bool> LoadModel(string model, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(model)) return false;
            string url = Endpoint + "api/pull";

            using (RestRequest req = new RestRequest(url, HttpMethod.Post))
            {
                req.ContentType = "application/json";

                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("model", model);

                string json = _Serializer.SerializeJson(dict, true);

                using (RestResponse resp = await req.SendAsync(json, token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode >= 200 && resp.StatusCode <= 299) return true;
                    return false;
                }
            }
        }

        /// <inheritdoc />
        public override async Task<EmbeddingsResult> GenerateEmbeddings(EmbeddingsRequest embedRequest, int timeoutMs = 30000, CancellationToken token = default)
        {
            if (embedRequest == null) throw new ArgumentNullException(nameof(embedRequest));
            if (timeoutMs < 1) throw new ArgumentOutOfRangeException(nameof(timeoutMs));
            if (String.IsNullOrEmpty(embedRequest.Model)) embedRequest.Model = _DefaultModel;
            string url = Endpoint + "api/embed";

            using (RestRequest req = new RestRequest(url, HttpMethod.Post))
            {
                req.ContentType = "application/json";
                req.TimeoutMilliseconds = timeoutMs;

                string json = Serializer.SerializeJson(OllamaEmbeddingsRequest.FromEmbeddingsRequest(embedRequest), true);

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
                                OllamaEmbeddingsResult embedResult = Serializer.DeserializeJson<OllamaEmbeddingsResult>(resp.DataAsString);
                                return embedResult.ToEmbeddingsResult(OllamaEmbeddingsRequest.FromEmbeddingsRequest(embedRequest));
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

        /// <inheritdoc />
        public override async Task<List<ModelInformation>> ListModels(CancellationToken token = default)
        {
            string url = Endpoint + "api/tags";

            using (RestRequest req = new RestRequest(url, HttpMethod.Get))
            {
                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
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
                                OllamaModelResult result = _Serializer.DeserializeJson<OllamaModelResult>(resp.DataAsString);
                                return ModelInformation.FromOllamaResponse(result);
                            }
                            else
                            {
                                Logger?.Invoke(SeverityEnum.Warn, "no data from " + url);
                                return null;
                            }
                        }
                        else
                        {
                            Logger?.Invoke(SeverityEnum.Warn, "status " + resp.StatusCode + " received from " + url + ": " + Environment.NewLine + resp.DataAsString);
                            return null;
                        }
                    }
                }
            }
        }

        /// <inheritdoc />
        public override async Task<bool> DeleteModel(string name, CancellationToken token = default)
        {
            string url = Endpoint + "api/delete";

            using (RestRequest req = new RestRequest(url, HttpMethod.Delete))
            {
                req.ContentType = "application/json";

                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("name", name);

                string json = _Serializer.SerializeJson(dict, true);

                using (RestResponse resp = await req.SendAsync(json, token).ConfigureAwait(false))
                {
                    if (resp == null)
                    {
                        Logger?.Invoke(SeverityEnum.Warn, "no response from " + url);
                        return false;
                    }
                    else
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            return true;
                        }
                        else
                        {
                            Logger?.Invoke(SeverityEnum.Warn, "status " + resp.StatusCode + " received from " + url + ": " + Environment.NewLine + resp.DataAsString);
                            return false;
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
