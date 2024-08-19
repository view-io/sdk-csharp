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
    using System.Reflection;
    using System.Runtime.InteropServices;

    /// <summary>
    /// View Langchain Proxy SDK.
    /// </summary>
    public class ViewLcproxySdk
    {
        #region Public-Members

        /// <summary>
        /// Endpoint URL.  Default is http://localhost:8301/.
        /// </summary>
        public string Endpoint
        {
            get
            {
                return _Endpoint;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(Endpoint));
                Uri uri = new Uri(value);
                if (!value.EndsWith("/")) value += "/";
                _Endpoint = value;
            }
        }

        /// <summary>
        /// API key for Huggingface.
        /// </summary>
        public string ApiKey
        {
            get
            {
                return _ApiKey;
            }
            set
            {
                _ApiKey = value;
            }
        }

        /// <summary>
        /// Maximum number of retries to perform on any given task.
        /// </summary>
        public int MaxRetries
        {
            get
            {
                return _MaxRetries;
            }
            private set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(MaxRetries));
                _MaxRetries = value;
            }
        }

        /// <summary>
        /// Logger.
        /// </summary>
        public Action<SeverityEnum, string> Logger { get; set; } = null;

        #endregion

        #region Private-Members

        private Serializer _Serializer = new Serializer();
        private string _Endpoint = "http://localhost:8301/";
        private string _ApiKey = null;
        private int _MaxRetries = 3;
        private int _FailureCount = 0;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="endpoint">Endpoint URL.  Default is http://localhost:8301/.</param>
        /// <param name="apiKey">API key.</param>
        /// <param name="logger">Logger.</param>
        /// <param name="maxRetries">Maximum number of retries before failing the operation.</param>
        public ViewLcproxySdk(
            string endpoint = "http://localhost:8301/", 
            string apiKey = null,
            Action<SeverityEnum, string> logger = null,
            int maxRetries = 3)
        {
            Endpoint = endpoint;
            ApiKey = apiKey;
            Logger = logger;
            MaxRetries = maxRetries;
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

        /// <summary>
        /// Preload models.
        /// </summary>
        /// <param name="models">List of models.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public async Task<bool> PreloadModels(
            List<string> models, 
            CancellationToken token = default)
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
                    dict.Add("ApiKey", _ApiKey);

                    string json = _Serializer.SerializeJson(dict, true);

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
            if (string.IsNullOrEmpty(model)) throw new ArgumentNullException(nameof(model));
            if (string.IsNullOrEmpty(text)) throw new ArgumentNullException(nameof(text));

            string url = Endpoint + "v1.0/embeddings/";

            while (_FailureCount < MaxRetries)
            {
                try
                {
                    using (RestRequest req = new RestRequest(url, HttpMethod.Post))
                    {
                        req.ContentType = "application/json";

                        Dictionary<string, object> dict = new Dictionary<string, object>();
                        dict.Add("Model", model);
                        dict.Add("Text", text);
                        dict.Add("ApiKey", _ApiKey);

                        string json = _Serializer.SerializeJson(dict, true);

                        using (RestResponse resp = await req.SendAsync(json, token).ConfigureAwait(false))
                        {
                            if (resp == null)
                            {
                                Logger?.Invoke(SeverityEnum.Warn, "no response from " + url);
                                Interlocked.Increment(ref _FailureCount);
                            }
                            else
                            {
                                if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                                {
                                    if (!String.IsNullOrEmpty(resp.DataAsString))
                                    {
                                        EmbeddingsResult result = _Serializer.DeserializeJson<EmbeddingsResult>(resp.DataAsString);
                                        result.Success = true;
                                        result.Model = model;
                                        result.Url = url;
                                        result.StatusCode = resp.StatusCode;
                                        return result;
                                    }
                                    else
                                    {
                                        EmbeddingsResult result = new EmbeddingsResult
                                        {
                                            Success = false,
                                            Model = model,
                                            Url = url,
                                            StatusCode = resp.StatusCode
                                        };

                                        return result;
                                    }
                                }
                                else
                                {
                                    Logger?.Invoke(SeverityEnum.Warn, "status " + resp.StatusCode + " received from " + url + ": " + Environment.NewLine + resp.DataAsString);
                                    Interlocked.Increment(ref _FailureCount);
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger?.Invoke(SeverityEnum.Warn, "exception while generating embeddings: " + Environment.NewLine + e.ToString());
                    Interlocked.Increment(ref _FailureCount);
                }
            }

            Logger?.Invoke(SeverityEnum.Warn, "maximum failure count (" + _MaxRetries + ") exceeded for " + url);
            throw new ExternalException("Maximum failure count (" + _MaxRetries + ") exceeded for " + url + ".");
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}
