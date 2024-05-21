namespace View.Sdk.Langchain
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using RestWrapper;
    using View.Serializer;
    using View.Sdk.Shared.Embeddings;

    /// <summary>
    /// View Langchain Proxy SDK.
    /// </summary>
    public class ViewLangchainProxySdk
    {
        #region Public-Members

        /// <summary>
        /// Method to invoke to send log messages.
        /// </summary>
        public Action<string> Logger { get; set; } = null;
         
        #endregion

        #region Private-Members

        private string _Header = "[ViewLcProxySdk] ";
        private Uri _Uri = null;
        private string _Endpoint = "http://localhost:8301/";
        private string _ApiKey = null;
        private SerializationHelper _Serializer = new SerializationHelper();
        
        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="endpoint">Endpoint URL.</param>
        /// <param name="apiKey">API key.</param>
        public ViewLangchainProxySdk(string endpoint = "http://localhost:8301/", string apiKey = null)
        {
            if (string.IsNullOrEmpty(endpoint)) throw new ArgumentNullException(nameof(endpoint));

            _Uri = new Uri(endpoint);
            _Endpoint = _Uri.ToString();
            if (!_Endpoint.EndsWith("/")) _Endpoint += "/";

            _ApiKey = apiKey;
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// Validate connectivity.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Boolean indicating success.</returns>
        public async Task<bool> ValidateConnectivity(CancellationToken token = default)
        {
            string url = _Endpoint;

            using (RestRequest req = new RestRequest(url))
            {
                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        Log("success reported from " + url);
                        return true;
                    }
                    else if (resp != null)
                    {
                        Log("non-success reported from " + url + ": " + resp.StatusCode);
                        return false;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Preload models.
        /// </summary>
        /// <param name="models">List of models.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public async Task<bool> PreloadModels(List<string> models, CancellationToken token = default)
        {
            if (models == null || models.Count < 1) throw new ArgumentNullException(nameof(models));

            string url = _Endpoint + "v1.0/preload/";

            using (RestRequest req = new RestRequest(url, HttpMethod.Post))
            {
                req.ContentType = Constants.JsonContentType;

                PreloadRequest preloadReq = new PreloadRequest
                {
                    Models = models,
                    ApiKey = _ApiKey
                };

                using (RestResponse resp = await req.SendAsync(_Serializer.SerializeJson(preloadReq, true), token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        Log("success preloading models from " + url + ": " + resp.StatusCode);
                        return true;
                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                        return false;
                    }
                    else
                    {
                        Log("no response from " + url);
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
            if (String.IsNullOrEmpty(model)) throw new ArgumentNullException(nameof(model));
            if (String.IsNullOrEmpty(text)) throw new ArgumentNullException(nameof(text));

            string url = _Endpoint + "v1.0/embeddings/";

            using (RestRequest req = new RestRequest(url, HttpMethod.Post))
            {
                req.ContentType = Constants.JsonContentType;

                EmbeddingsRequest embeddingsReq = new EmbeddingsRequest
                {
                    Model = model,
                    Text = text,
                    ApiKey = _ApiKey
                };

                using (RestResponse resp = await req.SendAsync(_Serializer.SerializeJson(embeddingsReq, true), token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        Log("success status from " + url + ": " + resp.StatusCode);
                        EmbeddingsResult result = _Serializer.DeserializeJson<EmbeddingsResult>(resp.DataAsString);
                        result.StatusCode = resp.StatusCode;
                        return result;
                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode + Environment.NewLine + resp.DataAsString);
                        EmbeddingsResult result = new EmbeddingsResult();
                        result.Success = false;
                        result.Url = url;
                        result.Model = model;
                        result.Embeddings = null;
                        result.StatusCode = resp.StatusCode;
                        return result;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return null;
                    }
                }
            }
        }

        #endregion

        #region Private-Methods

        private void Log(string msg)
        {
            if (String.IsNullOrEmpty(msg)) return;
            Logger?.Invoke(_Header + msg);
        }

        #endregion
    }
}
