namespace View.Sdk.Lexi
{
    using RestWrapper;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Reflection.Metadata;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Serializer;
    using View.Sdk;
    using View.Sdk.Shared.Search;

    /// <summary>
    /// View Lexi search SDK.
    /// </summary>
    public class ViewLexiSdk
    {
        #region Public-Members

        /// <summary>
        /// Method to invoke to send log messages.
        /// </summary>
        public Action<string> Logger { get; set; } = null;
         
        #endregion

        #region Private-Members

        private string _Header = "[ViewLexiSdk] ";
        private Uri _Uri = null;
        private string _TenantGuid = Guid.NewGuid().ToString();
        private string _Endpoint = "http://localhost:8201/";
        private SerializationHelper _Serializer = new SerializationHelper();
        
        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="tenantGuid">Tenant GUID.</param>
        /// <param name="endpoint">Endpoint URL.</param>
        public ViewLexiSdk(string tenantGuid, string endpoint = "http://localhost:8201/")
        {
            if (string.IsNullOrEmpty(tenantGuid)) throw new ArgumentNullException(nameof(tenantGuid));
            if (string.IsNullOrEmpty(endpoint)) throw new ArgumentNullException(nameof(endpoint));

            _TenantGuid = tenantGuid;
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

        #region Collections

        /// <summary>
        /// Retrieve collections.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of collection.</returns>
        public async Task<List<Collection>> RetrieveCollections(CancellationToken token = default)
        {
            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/collections";

            using (RestRequest req = new RestRequest(url))
            {
                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log("success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                return _Serializer.DeserializeJson<List<Collection>>(resp.DataAsString);
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
        /// Retrieve a collection.
        /// </summary>
        /// <param name="collectionGuid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Collection.</returns>
        public async Task<Collection> RetrieveCollection(string collectionGuid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(collectionGuid)) throw new ArgumentNullException(nameof(collectionGuid));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/collections/" + collectionGuid;

            using (RestRequest req = new RestRequest(url))
            {
                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log("success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                return _Serializer.DeserializeJson<Collection>(resp.DataAsString);
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
        /// Retrieve collection statistics.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Collection statistics.</returns>
        public async Task<CollectionStatistics> RetrieveCollectionStatistics(string collectionGuid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(collectionGuid)) throw new ArgumentNullException(nameof(collectionGuid));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/collections/" + collectionGuid + "?stats";

            using (RestRequest req = new RestRequest(url))
            {
                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log("success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                return _Serializer.DeserializeJson<CollectionStatistics>(resp.DataAsString);
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
        /// Create collection.
        /// </summary>
        /// <param name="collection">Collection.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Collection.</returns>
        public async Task<Collection> CreateCollection(Collection collection, CancellationToken token = default)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/collections";

            using (RestRequest req = new RestRequest(url, HttpMethod.Put))
            {
                req.ContentType = Constants.JsonContentType;

                using (RestResponse resp = await req.SendAsync(_Serializer.SerializeJson(collection, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log("success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            return _Serializer.DeserializeJson<Collection>(resp.DataAsString);
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
        /// Delete collection.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteCollection(string collectionGuid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(collectionGuid)) throw new ArgumentNullException(nameof(collectionGuid));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/collections/" + collectionGuid;

            using (RestRequest req = new RestRequest(url, HttpMethod.Delete))
            {
                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log("success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                        }
                        else
                        {
                            Log("non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                        }
                    }
                    else
                    {
                        Log("no response from " + url);
                    }

                    return;
                }
            }
        }

        #endregion

        #region Source-Documents

        /// <summary>
        /// Retrieve documents.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of source documents.</returns>
        public async Task<List<SourceDocument>> RetrieveDocuments(string collectionGuid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(collectionGuid)) throw new ArgumentNullException(nameof(collectionGuid));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/collections/" + collectionGuid + "/documents";

            using (RestRequest req = new RestRequest(url))
            {
                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log("success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                return _Serializer.DeserializeJson<List<SourceDocument>>(resp.DataAsString);
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            Log("non-success status from " + url + ": " + resp.StatusCode);
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
        /// Retrieve document.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="documentGuid">Document GUID.</param>
        /// <param name="includeData">Flag to indiate whether or not source document data should be included.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Source document.</returns>
        public async Task<SourceDocument> RetrieveDocument(string collectionGuid, string documentGuid, bool includeData = false, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(collectionGuid)) throw new ArgumentNullException(nameof(collectionGuid));
            if (String.IsNullOrEmpty(documentGuid)) throw new ArgumentNullException(nameof(documentGuid));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/collections/" + collectionGuid + "/documents/" + documentGuid;

            if (includeData) url += "?incldata";

            using (RestRequest req = new RestRequest(url))
            {
                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log("success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                return _Serializer.DeserializeJson<SourceDocument>(resp.DataAsString);
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
        /// Retrieve document statistics.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="documentGuid">Document GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Source document statistics.</returns>
        public async Task<SourceDocumentStatistics> RetrieveDocumentStatistics(string collectionGuid, string documentGuid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(collectionGuid)) throw new ArgumentNullException(nameof(collectionGuid));
            if (String.IsNullOrEmpty(documentGuid)) throw new ArgumentNullException(nameof(documentGuid));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/collections/" + collectionGuid + "/documents/" + documentGuid + "?stats";

            using (RestRequest req = new RestRequest(url))
            {
                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log("success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                return _Serializer.DeserializeJson<SourceDocumentStatistics>(resp.DataAsString);
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
        /// Upload a document.
        /// </summary>
        /// <param name="document">Source document.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Source document.</returns>
        public async Task<SourceDocument> UploadDocument(SourceDocument document, CancellationToken token = default)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/collections/" + document.CollectionGUID + "/documents";

            using (RestRequest req = new RestRequest(url, HttpMethod.Put))
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
                                return _Serializer.DeserializeJson<SourceDocument>(resp.DataAsString);
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
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="documentGuid">Document GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteDocument(string collectionGuid, string documentGuid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(collectionGuid)) throw new ArgumentNullException(nameof(collectionGuid));
            if (String.IsNullOrEmpty(documentGuid)) throw new ArgumentNullException(nameof(documentGuid));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/collections/" + collectionGuid + "/documents/" + documentGuid;

            using (RestRequest req = new RestRequest(url, HttpMethod.Delete))
            {
                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log("success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                        }
                        else
                        {
                            Log("non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                        }
                    }
                    else
                    {
                        Log("no response from " + url);
                    }

                    return;
                }
            }
        }

        #endregion

        #region Enumerate

        /// <summary>
        /// Enumerate a collection.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="query">Query.</param>
        /// <param name="token">Token.</param>
        /// <returns>Enumeration result.</returns>
        public async Task<EnumerationResult> Enumerate(string collectionGuid, EnumerationQuery query, CancellationToken token = default)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));
            if (String.IsNullOrEmpty(collectionGuid)) throw new ArgumentNullException(nameof(collectionGuid));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/collections/" + collectionGuid + "?enumerate";

            using (RestRequest req = new RestRequest(url, HttpMethod.Post))
            {
                req.ContentType = Constants.JsonContentType;

                using (RestResponse resp = await req.SendAsync(_Serializer.SerializeJson(query, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log("success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                return _Serializer.DeserializeJson<EnumerationResult>(resp.DataAsString);
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

        #endregion

        #region Search

        /// <summary>
        /// Search a collection.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="query">Query.</param>
        /// <param name="token"></param>
        /// <returns>Search result.</returns>
        public async Task<SearchResult> Search(string collectionGuid, SearchQuery query, CancellationToken token = default)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));
            if (String.IsNullOrEmpty(collectionGuid)) throw new ArgumentNullException(nameof(collectionGuid));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/collections/" + collectionGuid + "?search";

            using (RestRequest req = new RestRequest(url, HttpMethod.Post))
            {
                req.ContentType = Constants.JsonContentType;

                using (RestResponse resp = await req.SendAsync(_Serializer.SerializeJson(query, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log("success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                return _Serializer.DeserializeJson<SearchResult>(resp.DataAsString);
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

        #endregion

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
