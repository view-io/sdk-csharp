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
        public Guid? TenantGUID { get; set; } = null;

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
                value = uri.Scheme + "://" + uri.Host + ":" + uri.Port + uri.PathAndQuery; // will add trailing slash after port
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

        /// <summary>
        /// Optional xToken header value.
        /// </summary>
        public string XToken { get; set; } = null;

        /// <summary>
        /// Optional email address to include in the request headers. 
        /// </summary>
        public string Email { get; set; } = null;

        /// <summary>
        /// Optional plain-text password to include in the request headers.
        /// </summary>
        public string Password { get; set; } = null;

        /// <summary>
        /// Optional SHA-256 hash of the password to include in the request headers.
        /// </summary>
        public string PasswordSha256 { get; set; } = null;


        #endregion

        #region Internal-Members

        #endregion

        #region Private-Members

        private string _Header = "[ViewSdkBase] ";
        private string _AccessKey = null;
        private string _Endpoint = null;
        private Serializer _Serializer = new Serializer();
        private int _TimeoutMs = 600 * 1000;
        private bool _Disposed = false;

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
        public ViewSdkBase(Guid tenantGuid, string accessKey, string endpoint)
        {
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
        /// <param name="disposing">Disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_Disposed)
            {
                if (disposing)
                {
                    _Serializer = null;
                }

                _Disposed = true;
            }
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
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
                if (!string.IsNullOrWhiteSpace(XToken))
                {
                    req.Headers.Add("x-token", XToken);
                }
                string json = Serializer.SerializeJson(obj, true);
                if (LogRequests) Log(SeverityEnum.Debug, "request: " + Environment.NewLine + json);

                using (RestResponse resp = await req.SendAsync(Serializer.SerializeJson(obj, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        string responseData = await ReadResponseDataAsync(resp, url, token).ConfigureAwait(false);

                        if (LogResponses) Log(SeverityEnum.Debug, "response (status " + resp.StatusCode + "): " + Environment.NewLine + responseData);

                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(responseData))
                            {
                                Log(SeverityEnum.Debug, "deserializing response body");
                                return Serializer.DeserializeJson<T>(responseData);
                            }
                            else
                            {
                                Log(SeverityEnum.Debug, "empty response body, returning null");
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
        /// Execute a logic API.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="url">URL.</param>
        /// <param name="obj">Object.</param>
        /// <param name="token"></param>
        /// <returns>Instance.</returns>
        public async Task<T> Post<T>(string url, T obj, CancellationToken token = default) where T : class
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (String.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));

            using (RestRequest req = new RestRequest(url, HttpMethod.Post))
            {
                req.TimeoutMilliseconds = TimeoutMs;
                req.Authorization.BearerToken = _AccessKey;
                req.ContentType = "application/json";

                string json = Serializer.SerializeJson(obj, true);
                if (LogRequests) Log(SeverityEnum.Debug, "request: " + Environment.NewLine + json);

                using (RestResponse resp = await req.SendAsync(Serializer.SerializeJson(obj, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        string responseData = await ReadResponseDataAsync(resp, url, token).ConfigureAwait(false);

                        if (LogResponses) Log(SeverityEnum.Debug, "response (status " + resp.StatusCode + "): " + Environment.NewLine + responseData);

                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");

                            if (!String.IsNullOrEmpty(responseData))
                            {
                                Log(SeverityEnum.Debug, "deserializing response body");
                                return Serializer.DeserializeJson<T>(responseData);
                            }
                            else
                            {
                                Log(SeverityEnum.Debug, "empty response body, returning null");
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
        /// Execute a logic API.
        /// </summary>
        /// <typeparam name="T1">Input type.</typeparam>
        /// <typeparam name="T2">Output type.</typeparam>
        /// <param name="url">URL.</param>
        /// <param name="obj">Object.</param>
        /// <param name="token"></param>
        /// <returns>Instance.</returns>
        public async Task<T2> Post<T1, T2>(string url, T1 obj, CancellationToken token = default) where T1 : class where T2 : class
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (String.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));

            using (RestRequest req = new RestRequest(url, HttpMethod.Post))
            {
                req.TimeoutMilliseconds = TimeoutMs;
                req.Authorization.BearerToken = _AccessKey;
                req.ContentType = "application/json";

                string json = Serializer.SerializeJson(obj, true);
                if (LogRequests) Log(SeverityEnum.Debug, "request: " + Environment.NewLine + json);

                using (RestResponse resp = await req.SendAsync(Serializer.SerializeJson(obj, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        string responseData = await ReadResponseDataAsync(resp, url, token).ConfigureAwait(false);

                        if (LogResponses) Log(SeverityEnum.Debug, "response (status " + resp.StatusCode + "): " + Environment.NewLine + responseData);

                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");

                            if (!String.IsNullOrEmpty(responseData))
                            {
                                Log(SeverityEnum.Debug, "deserializing response body");
                                return Serializer.DeserializeJson<T2>(responseData);
                            }
                            else
                            {
                                Log(SeverityEnum.Debug, "empty response body, returning null");
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
                        string responseData = null;
                        
                        // Handle chunked transfer encoding
                        if (resp.ChunkedTransferEncoding)
                        {
                            Log(SeverityEnum.Debug, "reading chunked response from " + url);
                            var chunks = new List<string>();
                            ChunkData chunk;
                            while ((chunk = await resp.ReadChunkAsync(token).ConfigureAwait(false)) != null)
                            {
                                if (chunk.Data != null && chunk.Data.Length > 0)
                                {
                                    chunks.Add(System.Text.Encoding.UTF8.GetString(chunk.Data));
                                }
                                if (chunk.IsFinal) break;
                            }
                            responseData = string.Join("", chunks);
                        }
                        else
                        {
                            responseData = resp.DataAsString;
                        }

                        if (LogResponses) Log(SeverityEnum.Debug, "response (status " + resp.StatusCode + "): " + Environment.NewLine + responseData);

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
                if (!string.IsNullOrWhiteSpace(XToken)) req.Headers.Add("x-token", XToken);
                if (!string.IsNullOrWhiteSpace(Email)) req.Headers.Add("x-email", Email);
                if (!string.IsNullOrWhiteSpace(Password)) req.Headers.Add("x-password", Password);
                if (!string.IsNullOrWhiteSpace(PasswordSha256)) req.Headers.Add("x-password-sha256", PasswordSha256);
                if (!string.IsNullOrWhiteSpace(TenantGUID.ToString())) req.Headers.Add("x-tenant-guid", TenantGUID.ToString());

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        string responseData = await ReadResponseDataAsync(resp, url, token).ConfigureAwait(false);

                        if (LogResponses) Log(SeverityEnum.Debug, "response (status " + resp.StatusCode + "): " + Environment.NewLine + responseData);

                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(responseData))
                            {
                                Log(SeverityEnum.Debug, "deserializing response body");
                                return Serializer.DeserializeJson<T>(responseData);
                            }
                            else
                            {
                                Log(SeverityEnum.Debug, "empty response body, returning null");
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

                if (!string.IsNullOrWhiteSpace(XToken))
                {
                    req.Headers.Add("x-token", XToken);
                }

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        string responseData = await ReadResponseDataAsync(resp, url, token).ConfigureAwait(false);

                        if (LogResponses) Log(SeverityEnum.Debug, "response (status " + resp.StatusCode + "): " + Environment.NewLine + responseData);

                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(responseData))
                            {
                                Log(SeverityEnum.Debug, "deserializing response body");
                                return Serializer.DeserializeJson<List<T>>(responseData);
                            }
                            else
                            {
                                Log(SeverityEnum.Debug, "empty response body, returning null");
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
                if (LogRequests) Log(SeverityEnum.Debug, "request: " + Environment.NewLine + json);

                using (RestResponse resp = await req.SendAsync(json, token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        string responseData = await ReadResponseDataAsync(resp, url, token).ConfigureAwait(false);

                        if (LogResponses) Log(SeverityEnum.Debug, "response (status " + resp.StatusCode + "): " + Environment.NewLine + responseData);

                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(responseData))
                            {
                                Log(SeverityEnum.Debug, "deserializing response body");
                                return Serializer.DeserializeJson<T>(responseData);
                            }
                            else
                            {
                                Log(SeverityEnum.Debug, "empty response body, returning null");
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
        /// <returns>True if successful.</returns>
        public async Task<bool> Delete(string url, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));

            using (RestRequest req = new RestRequest(url, HttpMethod.Delete))
            {
                req.TimeoutMilliseconds = TimeoutMs;
                req.Authorization.BearerToken = _AccessKey;
                if (!string.IsNullOrWhiteSpace(XToken))
                {
                    req.Headers.Add("x-token", XToken);
                }
                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        string responseData = null;
                        
                        // Handle chunked transfer encoding
                        if (resp.ChunkedTransferEncoding)
                        {
                            Log(SeverityEnum.Debug, "reading chunked response from " + url);
                            var chunks = new List<string>();
                            ChunkData chunk;
                            while ((chunk = await resp.ReadChunkAsync(token).ConfigureAwait(false)) != null)
                            {
                                if (chunk.Data != null && chunk.Data.Length > 0)
                                {
                                    chunks.Add(System.Text.Encoding.UTF8.GetString(chunk.Data));
                                }
                                if (chunk.IsFinal) break;
                            }
                            responseData = string.Join("", chunks);
                        }
                        else
                        {
                            responseData = resp.DataAsString;
                        }

                        if (LogResponses) Log(SeverityEnum.Debug, "response (status " + resp.StatusCode + "): " + Environment.NewLine + responseData);

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
                    }

                    return false;
                }
            }
        }

        /// <summary>
        /// Delete an object.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="url">URL.</param>
        /// <param name="obj">Object.</param>
        /// <param name="token"></param>
        /// <returns>True if successful.</returns>
        public async Task<bool> Delete<T>(string url, T obj, CancellationToken token = default) where T : class
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (String.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));

            using (RestRequest req = new RestRequest(url, HttpMethod.Delete))
            {
                req.TimeoutMilliseconds = TimeoutMs;
                req.Authorization.BearerToken = _AccessKey;
                req.ContentType = "application/json";

                string json = Serializer.SerializeJson(obj, true);
                if (LogRequests) Log(SeverityEnum.Debug, "request: " + Environment.NewLine + json);

                using (RestResponse resp = await req.SendAsync(Serializer.SerializeJson(obj, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        string responseData = null;
                        
                        // Handle chunked transfer encoding
                        if (resp.ChunkedTransferEncoding)
                        {
                            Log(SeverityEnum.Debug, "reading chunked response from " + url);
                            var chunks = new List<string>();
                            ChunkData chunk;
                            while ((chunk = await resp.ReadChunkAsync(token).ConfigureAwait(false)) != null)
                            {
                                if (chunk.Data != null && chunk.Data.Length > 0)
                                {
                                    chunks.Add(System.Text.Encoding.UTF8.GetString(chunk.Data));
                                }
                                if (chunk.IsFinal) break;
                            }
                            responseData = string.Join("", chunks);
                        }
                        else
                        {
                            responseData = resp.DataAsString;
                        }

                        if (LogResponses) Log(SeverityEnum.Debug, "response (status " + resp.StatusCode + "): " + Environment.NewLine + responseData);

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
        /// Executes a GET request to enumerate a list of objects from the specified URL.
        /// </summary>
        /// <typeparam name="T">The type of items in the enumeration result.</typeparam>
        /// <param name="url">The API endpoint URL to fetch the enumeration from.</param>
        /// <param name="token">Optional cancellation token.</param>
        /// <returns>The deserialized <see cref="EnumerationResult{T}"/> if successful; otherwise, null.</returns>
        public async Task<EnumerationResult<T>> Enumerate<T>(string url, CancellationToken token = default)
        {
            using (RestRequest req = new RestRequest(url))
            {
                req.TimeoutMilliseconds = TimeoutMs;
                req.Authorization.BearerToken = AccessKey;

                if (!string.IsNullOrWhiteSpace(XToken))
                {
                    req.Headers.Add("X-Token", XToken);
                }

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        string responseData = await ReadResponseDataAsync(resp, url, token).ConfigureAwait(false);

                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log(SeverityEnum.Debug, $"success reported from {url}: {resp.StatusCode}, {resp.ContentLength} bytes");
                            if (!string.IsNullOrEmpty(responseData))
                            {
                                Log(SeverityEnum.Debug, "deserializing response body");
                                return Serializer.DeserializeJson<EnumerationResult<T>>(responseData);
                            }

                            Log(SeverityEnum.Debug, "empty response body, returning null");
                            return null;
                        }

                        Log(SeverityEnum.Warn, $"non-success reported from {url}: {resp.StatusCode}, {resp.ContentLength} bytes");
                        return null;
                    }

                    Log(SeverityEnum.Warn, $"no response from {url}");
                    return null;
                }
            }
        }

        /// <summary>
        /// Read response data from RestResponse, handling both chunked and non-chunked responses.
        /// </summary>
        /// <param name="resp">RestResponse object.</param>
        /// <param name="url">URL for logging purposes.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Response data as string.</returns>
        public async Task<string> ReadResponseDataAsync(RestResponse resp, string url, CancellationToken token = default)
        {
            if (resp == null) return null;

            string responseData = null;
            
            // Handle chunked transfer encoding
            if (resp.ChunkedTransferEncoding)
            {
                Log(SeverityEnum.Debug, "reading chunked response from " + url);
                var chunks = new List<string>();
                ChunkData chunk;
                while ((chunk = await resp.ReadChunkAsync(token).ConfigureAwait(false)) != null)
                {
                    if (chunk.Data != null && chunk.Data.Length > 0)
                    {
                        chunks.Add(System.Text.Encoding.UTF8.GetString(chunk.Data));
                    }
                    if (chunk.IsFinal) break;
                }
                responseData = string.Join("", chunks);
            }
            else
            {
                responseData = resp.DataAsString;
            }

            return responseData;
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}
