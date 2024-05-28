namespace View.Sdk.Pgvector
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
    /// View PostgreSQL Pgvector Proxy SDK.
    /// </summary>
    public class ViewPgvProxySdk
    {
        #region Public-Members

        /// <summary>
        /// Method to invoke to send log messages.
        /// </summary>
        public Action<string> Logger { get; set; } = null;
         
        #endregion

        #region Private-Members

        private string _Header = "[ViewPgvProxySdk] ";
        private Uri _Uri = null;
        private string _Endpoint = "http://localhost:8311/";
        private SerializationHelper _Serializer = new SerializationHelper();
        
        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="endpoint">Endpoint URL.</param>
        public ViewPgvProxySdk(string endpoint = "http://localhost:8311/")
        {
            if (string.IsNullOrEmpty(endpoint)) throw new ArgumentNullException(nameof(endpoint));

            _Uri = new Uri(endpoint);
            _Endpoint = _Uri.ToString();
            if (!_Endpoint.EndsWith("/")) _Endpoint += "/";
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
        /// Write a document.
        /// </summary>
        /// <param name="settings">Database settings.</param>
        /// <param name="document">Embeddings document.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task<List<EmbeddingsDocument>> WriteDocument(DatabaseSettings settings, EmbeddingsDocument document, CancellationToken token = default)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            if (document == null) throw new ArgumentNullException(nameof(document));

            string url = _Endpoint + "v1.0/documents";

            using (RestRequest req = new RestRequest(url, HttpMethod.Post))
            {
                req.ContentType = Constants.JsonContentType;

                using (RestResponse resp = await req.SendAsync(_Serializer.SerializeJson(document, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log("success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                return _Serializer.DeserializeJson<List<EmbeddingsDocument>>(resp.DataAsString);
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            Log("non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            return null;
                        }
                    }
                    else
                    {
                        Log("no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Delete a document.
        /// </summary>
        /// <param name="settings">Database settings.</param>
        /// <param name="delReq">Delete request.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task<bool> DeleteDocument(DatabaseSettings settings, DeleteRequest delReq, CancellationToken token = default)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            if (delReq == null) throw new ArgumentNullException(nameof(delReq));

            string url = _Endpoint + "v1.0/documents";

            using (RestRequest req = new RestRequest(url, HttpMethod.Delete))
            {
                req.ContentType = Constants.JsonContentType;

                using (RestResponse resp = await req.SendAsync(_Serializer.SerializeJson(delReq, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log("success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            return true;
                        }
                        else
                        {
                            Log("non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            return false;
                        }
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
        /// Truncate table.
        /// </summary>
        /// <param name="settings">Database settings.</param>
        /// <param name="delReq">Delete request.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task<bool> TruncateTable(DatabaseSettings settings, DeleteRequest delReq, CancellationToken token = default)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            if (delReq == null) throw new ArgumentNullException(nameof(delReq));

            string url = _Endpoint + "v1.0/documents?truncate";

            using (RestRequest req = new RestRequest(url, HttpMethod.Delete))
            {
                req.ContentType = Constants.JsonContentType;

                using (RestResponse resp = await req.SendAsync(_Serializer.SerializeJson(delReq, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        Log("success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            return true;
                        }
                        else
                        {
                            Log("non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            return false;
                        }
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
        /// Similarity search.
        /// </summary>
        /// <param name="settings">Database settings.</param>
        /// <param name="searchReq">Search request.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task<List<EmbeddingsDocument>> SimilaritySearch(DatabaseSettings settings, SearchRequest searchReq, CancellationToken token = default)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            if (searchReq == null) throw new ArgumentNullException(nameof(searchReq));

            string url = _Endpoint + "v1.0/search/similarity";

            using (RestRequest req = new RestRequest(url, HttpMethod.Put))
            {
                req.ContentType = Constants.JsonContentType;

                using (RestResponse resp = await req.SendAsync(_Serializer.SerializeJson(searchReq, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log("success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                return _Serializer.DeserializeJson<List<EmbeddingsDocument>>(resp.DataAsString);
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            Log("non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            return null;
                        }
                    }
                    else
                    {
                        Log("no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Raw query.
        /// </summary>
        /// <param name="settings">Database settings.</param>
        /// <param name="queryReq">Query request.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task<string> RawQuery(DatabaseSettings settings, QueryRequest queryReq, CancellationToken token = default)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            if (queryReq == null) throw new ArgumentNullException(nameof(queryReq));

            string url = _Endpoint + "v1.0/query";

            using (RestRequest req = new RestRequest(url, HttpMethod.Post))
            {
                req.ContentType = Constants.JsonContentType;

                using (RestResponse resp = await req.SendAsync(_Serializer.SerializeJson(queryReq, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log("success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            return resp.DataAsString;
                        }
                        else
                        {
                            Log("non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            return null;
                        }
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
