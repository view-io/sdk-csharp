namespace View.Sdk.Vector
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.VisualBasic;
    using RestWrapper;
    using View.Sdk;
    using View.Sdk.Serialization;
    
    /// <summary>
    /// OpenAI embeddings generator.
    /// </summary>
    public class ViewOpenAiSdk
    {
        #region Public-Members

        /// <summary>
        /// Base URL, e.g. https://api.openai.com/v1/
        /// </summary>
        public string Endpoint
        {
            get
            {
                return _Endpoint;
            }
            set
            {
                if (String.IsNullOrEmpty(_Endpoint)) throw new ArgumentNullException(nameof(Endpoint));
                Uri uri = new Uri(value);
                if (!value.EndsWith("/")) value += "/";
                _Endpoint = value;
            }
        }

        /// <summary>
        /// API key.
        /// </summary>
        public string ApiKey
        {
            get
            {
                return _ApiKey;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(ApiKey));
                _ApiKey = value;
            }
        }

        #endregion

        #region Private-Members

        private Serializer _Serializer = new Serializer();
        private string _Endpoint = "https://api.openai.com/v1/";
        private string _ApiKey = null;
        private string _DefaultModel = "text-embedding-ada-002";

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="endpoint">Base URL.  Default is https://api.openai.com/v1/.</param>
        /// <param name="apiKey">API key.</param>
        public ViewOpenAiSdk(
            string endpoint = null,
            string apiKey = null)
        {
            if (!String.IsNullOrEmpty(endpoint)) Endpoint = endpoint;
            ApiKey = apiKey;
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// Validate connectivity.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if connected.</returns>
        public async Task<bool> ValidateConnectivity(CancellationToken token = default)
        {
            try
            {
                string url = Endpoint + "models";

                using (RestRequest req = new RestRequest(url, HttpMethod.Head))
                {
                    req.Authorization.BearerToken = _ApiKey;

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

        /// <summary>
        /// Generate embeddings.
        /// </summary>
        /// <param name="model">Model.</param>
        /// <param name="text">Text.</param>
        /// <param name="timeoutMs">Timeout in milliseconds.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Embeddings result.</returns>
        public async Task<EmbeddingsResult> Generate(
            string model,
            string text,
            int timeoutMs = 60000,
            CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(model)) model = _DefaultModel;
            if (String.IsNullOrEmpty(text)) throw new ArgumentNullException(nameof(text));
            if (timeoutMs < 1) throw new ArgumentOutOfRangeException(nameof(timeoutMs));

            string url = _Endpoint + "embeddings";

            EmbeddingsResult result = new EmbeddingsResult
            {
                Url = url,
                Model = model
            };

            try
            {
                using (RestRequest req = new RestRequest(url, HttpMethod.Post, "application/json"))
                {
                    req.TimeoutMilliseconds = timeoutMs;
                    req.Authorization.BearerToken = _ApiKey;

                    Dictionary<string, string> body = new Dictionary<string, string>();
                    body["model"] = model;
                    body["input"] = text;

                    using (RestResponse resp = await req.SendAsync(_Serializer.SerializeJson(body, true), token).ConfigureAwait(false))
                    {
                        if (resp == null)
                        {
                            result.StatusCode = 0;
                            result.Success = false;
                        }
                        else if (resp.StatusCode != 200)
                        {
                            result.StatusCode = resp.StatusCode;
                            result.Success = false;
                        }
                        else
                        {
                            OpenAiResult<OpenAiEmbeddingsResult> data = _Serializer.DeserializeJson<OpenAiResult<OpenAiEmbeddingsResult>>(resp.DataAsString);
                            result.StatusCode = resp.StatusCode;
                            result.Success = true;
                            result.Embeddings = data.Data[0].Embeddings;
                        }
                    }
                }
            }
            catch (Exception)
            {
                result.StatusCode = 0;
                result.Success = false;
            }

            return result;
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}
