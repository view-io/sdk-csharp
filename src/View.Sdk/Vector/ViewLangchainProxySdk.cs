namespace View.Sdk.Vector
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using RestWrapper;
    using View.Sdk.Serialization;
    using View.Sdk;

    /// <summary>
    /// View Langchain Proxy SDK.
    /// </summary>
    public class ViewLangchainProxySdk : ViewSdkBase
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private string _ApiKey = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="endpoint">Endpoint URL.</param>
        /// <param name="apiKey">API key.</param>
        public ViewLangchainProxySdk(string endpoint = "http://localhost:8301/", string apiKey = null) : base(endpoint)
        {
            Header = "[ViewLcProxySdk] ";

            _ApiKey = apiKey;
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// Preload models.
        /// </summary>
        /// <param name="models">List of models.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public async Task<bool> PreloadModels(List<string> models, CancellationToken token = default)
        {
            if (models == null || models.Count < 1) throw new ArgumentNullException(nameof(models));

            string url = Endpoint + "v1.0/preload/";

            using (RestRequest req = new RestRequest(url, HttpMethod.Post))
            {
                req.ContentType = "application/json";

                PreloadRequest preloadReq = new PreloadRequest
                {
                    Models = models,
                    ApiKey = _ApiKey
                };

                using (RestResponse resp = await req.SendAsync(Serializer.SerializeJson(preloadReq, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            return true;
                        }
                        else
                        {
                            Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
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

        /// <summary>
        /// Generate embeddings.
        /// </summary>
        /// <param name="model">Model.</param>
        /// <param name="text">Text.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task<EmbeddingsResult> GenerateEmbeddings(string model, string text, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(model)) throw new ArgumentNullException(nameof(model));
            if (string.IsNullOrEmpty(text)) throw new ArgumentNullException(nameof(text));

            string url = Endpoint + "v1.0/embeddings/";

            using (RestRequest req = new RestRequest(url, HttpMethod.Post))
            {
                req.ContentType = "application/json";

                EmbeddingsRequest embeddingsReq = new EmbeddingsRequest
                {
                    Model = model,
                    Text = text,
                    ApiKey = _ApiKey
                };

                using (RestResponse resp = await req.SendAsync(Serializer.SerializeJson(embeddingsReq, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!string.IsNullOrEmpty(resp.DataAsString))
                            {
                                EmbeddingsResult result = Serializer.DeserializeJson<EmbeddingsResult>(resp.DataAsString);
                                result.StatusCode = resp.StatusCode;
                                return result;
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            EmbeddingsResult result = new EmbeddingsResult();
                            result.Success = false;
                            result.Url = url;
                            result.Model = model;
                            result.Embeddings = null;
                            result.StatusCode = resp.StatusCode;
                            return result;
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

        #endregion

        #region Private-Methods

        #endregion
    }
}
