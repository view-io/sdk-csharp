namespace View.Sdk
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Threading;
    using RestWrapper;
    using View.Sdk.Serialization;
    using System.Collections.Generic;

    /// <summary>
    /// View SDK base class.
    /// </summary>
    public class ViewSdkBase : IDisposable
    {
        #region Public-Members

        /// <summary>
        /// Enable or disable logging of request bodies.
        /// </summary>
        public bool LogRequests { get; set; } = false;

        /// <summary>
        /// Enable or disable logging of response bodies.
        /// </summary>
        public bool LogResponses { get; set; } = false;

        /// <summary>
        /// Method to invoke to send log messages.
        /// </summary>
        public Action<SeverityEnum, string> Logger { get; set; } = null;

        /// <summary>
        /// Header to prepend to log messages.
        /// </summary>
        public string Header
        {
            get
            {
                return _Header;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    _Header = value;
                }
                else
                {
                    if (!value.EndsWith(" ")) value += " ";
                    _Header = value;
                }
            }
        }

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public string TenantGUID
        {
            get
            {
                return _TenantGUID;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(TenantGUID));
                _TenantGUID = value;
            }
        }

        /// <summary>
        /// Access key.
        /// </summary>
        public string AccessKey
        {
            get
            {
                return _AccessKey;
            }
            set
            {
                _AccessKey = value;
            }
        }

        /// <summary>
        /// Endpoint URL.
        /// </summary>
        public string Endpoint
        {
            get
            {
                return _Endpoint;
            }
            set
            {
                Uri uri = new Uri(value);
                value = uri.Scheme + "://" + uri.Host + ":" + uri.Port + uri.PathAndQuery;
                _Endpoint = value;
            }
        }

        /// <summary>
        /// Serialization helper.
        /// </summary>
        public Serializer Serializer
        {
            get
            {
                return _Serializer;
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(Serializer));
                _Serializer = value;
            }
        }

        /// <summary>
        /// Timeout, in milliseconds.  Default is 600 seconds.
        /// </summary>
        public int TimeoutMs
        {
            get
            {
                return _TimeoutMs;
            }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(TimeoutMs));
                _TimeoutMs = value;
            }
        }

        #endregion

        #region Internal-Members

        #endregion

        #region Private-Members

        private string _Header = "[ViewSdkBase] ";
        private string _TenantGUID = null;
        private string _AccessKey = null;
        private string _Endpoint = null;
        private Serializer _Serializer = new Serializer();
        private int _TimeoutMs = 600 * 1000;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        public ViewSdkBase(string endpoint)
        {
            if (String.IsNullOrEmpty(endpoint)) throw new ArgumentNullException(nameof(endpoint));

            Endpoint = endpoint;
        }

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="accessKey">Access key.</param>
        /// <param name="endpoint">Endpoint.</param>
        public ViewSdkBase(string accessKey, string endpoint)
        {
            if (String.IsNullOrEmpty(accessKey)) throw new ArgumentNullException(nameof(accessKey));
            if (String.IsNullOrEmpty(endpoint)) throw new ArgumentNullException(nameof(endpoint));

            AccessKey = accessKey;
            Endpoint = endpoint;
        }

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="tenantGuid">Tenant GUID.</param>
        /// <param name="accessKey">Access key.</param>
        /// <param name="endpoint">Endpoint.</param>
        public ViewSdkBase(string tenantGuid, string accessKey, string endpoint)
        {
            if (String.IsNullOrEmpty(tenantGuid)) throw new ArgumentNullException(nameof(tenantGuid));
            if (String.IsNullOrEmpty(endpoint)) throw new ArgumentNullException(nameof(endpoint));

            TenantGUID = tenantGuid;
            AccessKey = accessKey;
            Endpoint = endpoint;
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            _Serializer = null;
        }

        /// <summary>
        /// Emit a log message.
        /// </summary>
        /// <param name="sev">Severity.</param>
        /// <param name="msg">Message.</param>
        public void Log(SeverityEnum sev, string msg)
        {
            if (String.IsNullOrEmpty(msg)) return;
            Logger?.Invoke(sev, _Header + msg);
        }

        /// <summary>
        /// Validate connectivity.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Boolean indicating success.</returns>
        public async Task<bool> ValidateConnectivity(CancellationToken token = default)
        {
            string url = _Endpoint;

            using (RestRequest req = new RestRequest(url, HttpMethod.Head))
            {
                req.TimeoutMilliseconds = TimeoutMs;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        Log(SeverityEnum.Debug, "success reported from " + url);
                        return true;
                    }
                    else if (resp != null)
                    {
                        Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode);
                        return false;
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
        /// Create an object.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="url">URL.</param>
        /// <param name="obj">Object.</param>
        /// <param name="token"></param>
        /// <returns>Instance.</returns>
        public async Task<T> Create<T>(string url, T obj, CancellationToken token = default) where T : class
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (String.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));

            using (RestRequest req = new RestRequest(url, HttpMethod.Put))
            {
                req.TimeoutMilliseconds = TimeoutMs;
                req.Authorization.BearerToken = _AccessKey;
                req.ContentType = "application/json";

                string json = Serializer.SerializeJson(obj, true);
                if (LogRequests) Logger?.Invoke(SeverityEnum.Debug, "request: " + Environment.NewLine + json);

                using (RestResponse resp = await req.SendAsync(Serializer.SerializeJson(obj, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (LogResponses) Logger?.Invoke(SeverityEnum.Debug, "response (status " + resp.StatusCode+ "): " + Environment.NewLine + resp.DataAsString);

                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                return Serializer.DeserializeJson<T>(resp.DataAsString);
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            return null;
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

        /// <summary>
        /// Check if an object exists.
        /// </summary>
        /// <param name="url">URL.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> Exists(string url, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));

            using (RestRequest req = new RestRequest(url, HttpMethod.Head))
            {
                req.TimeoutMilliseconds = TimeoutMs;
                req.Authorization.BearerToken = _AccessKey;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (LogResponses) Logger?.Invoke(SeverityEnum.Debug, "response (status " + resp.StatusCode + "): " + Environment.NewLine + resp.DataAsString);

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
        /// Read an object.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="url">URL.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Instance.</returns>
        public async Task<T> Retrieve<T>(string url, CancellationToken token = default) where T : class
        {
            if (String.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));

            using (RestRequest req = new RestRequest(url))
            {
                req.TimeoutMilliseconds = TimeoutMs;
                req.Authorization.BearerToken = _AccessKey;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (LogResponses) Logger?.Invoke(SeverityEnum.Debug, "response (status " + resp.StatusCode + "): " + Environment.NewLine + resp.DataAsString);

                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                return Serializer.DeserializeJson<T>(resp.DataAsString);
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            return null;
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

        /// <summary>
        /// Read objects.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="url">URL.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List.</returns>
        public async Task<List<T>> RetrieveMany<T>(string url, CancellationToken token = default) where T : class
        {
            if (String.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));

            using (RestRequest req = new RestRequest(url))
            {
                req.TimeoutMilliseconds = TimeoutMs;
                req.Authorization.BearerToken = _AccessKey;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (LogResponses) Logger?.Invoke(SeverityEnum.Debug, "response (status " + resp.StatusCode + "): " + Environment.NewLine + resp.DataAsString);

                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                return Serializer.DeserializeJson<List<T>>(resp.DataAsString);
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            return null;
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

        /// <summary>
        /// Update an object.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="url">URL.</param>
        /// <param name="obj">Object.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Instance.</returns>
        public async Task<T> Update<T>(string url, T obj, CancellationToken token = default) where T : class
        {
            if (String.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            using (RestRequest req = new RestRequest(url, HttpMethod.Put))
            {
                req.TimeoutMilliseconds = TimeoutMs;
                req.Authorization.BearerToken = _AccessKey;
                req.ContentType = "application/json";

                string json = Serializer.SerializeJson(obj, true);
                if (LogRequests) Logger?.Invoke(SeverityEnum.Debug, "request: " + Environment.NewLine + json);

                using (RestResponse resp = await req.SendAsync(json, token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (LogResponses) Logger?.Invoke(SeverityEnum.Debug, "response (status " + resp.StatusCode + "): " + Environment.NewLine + resp.DataAsString);

                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                return Serializer.DeserializeJson<T>(resp.DataAsString);
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            return null;
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

        /// <summary>
        /// Delete an object.
        /// </summary>
        /// <param name="url">URL.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task Delete(string url, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));

            using (RestRequest req = new RestRequest(url, HttpMethod.Delete))
            {
                req.TimeoutMilliseconds = TimeoutMs;
                req.Authorization.BearerToken = _AccessKey;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (LogResponses) Logger?.Invoke(SeverityEnum.Debug, "response (status " + resp.StatusCode + "): " + Environment.NewLine + resp.DataAsString);

                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                        }
                        else
                        {
                            Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                        }
                    }
                    else
                    {
                        Log(SeverityEnum.Warn, "no response from " + url);
                    }

                    return;
                }
            }
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}
