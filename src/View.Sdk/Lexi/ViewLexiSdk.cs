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
    using View.Sdk.Serialization;
    using View.Sdk;

    /// <summary>
    /// View Lexi search SDK.
    /// </summary>
    public class ViewLexiSdk : ViewSdkBase, IDisposable
    {
        #region Public-Members

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="tenantGuid">Tenant GUID.</param>
        /// <param name="accessKey">Access key.</param>
        /// <param name="endpoint">Endpoint URL.</param>
        public ViewLexiSdk(string tenantGuid, string accessKey, string endpoint = "http://localhost:8201/") : base(tenantGuid, accessKey, endpoint)
        {
            Header = "[ViewLexiSdk] ";
        }

        #endregion

        #region Public-Methods

        #region Collections

        /// <summary>
        /// Retrieve collections.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of collection.</returns>
        public async Task<List<Collection>> RetrieveCollections(CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/collections";
            return await RetrieveMany<Collection>(url, token).ConfigureAwait(false);
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
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/collections/" + collectionGuid;
            return await Retrieve<Collection>(url, token).ConfigureAwait(false);
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
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/collections/" + collectionGuid + "?stats";
            return await Retrieve<CollectionStatistics>(url, token).ConfigureAwait(false);
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
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/collections";
            return await Create<Collection>(url, collection, token).ConfigureAwait(false);
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
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/collections/" + collectionGuid;
            await Delete(url, token).ConfigureAwait(false);
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
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/collections/" + collectionGuid + "/documents";
            return await RetrieveMany<SourceDocument>(url, token).ConfigureAwait(false);
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
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/collections/" + collectionGuid + "/documents/" + documentGuid;
            if (includeData) url += "?incldata";
            return await Retrieve<SourceDocument>(url, token).ConfigureAwait(false);
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
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/collections/" + collectionGuid + "/documents/" + documentGuid + "?stats";
            return await Retrieve<SourceDocumentStatistics>(url, token).ConfigureAwait(false);
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
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/collections/" + document.CollectionGUID + "/documents";
            return await Create<SourceDocument>(url, document, token).ConfigureAwait(false);
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
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/collections/" + collectionGuid + "/documents/" + documentGuid;
            await Delete(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a document.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="key">Document key.</param>
        /// <param name="version">Document version.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteDocument(string collectionGuid, string key, string version, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(collectionGuid)) throw new ArgumentNullException(nameof(collectionGuid));
            if (String.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
            if (String.IsNullOrEmpty(version)) throw new ArgumentNullException(nameof(version));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/collections/" + collectionGuid + "/documents?key=" + key + "&versionId=" + version;
            await Delete(url, token).ConfigureAwait(false);
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
        public async Task<EnumerationResult<SourceDocument>> Enumerate(string collectionGuid, EnumerationQuery query, CancellationToken token = default)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));
            if (String.IsNullOrEmpty(collectionGuid)) throw new ArgumentNullException(nameof(collectionGuid));

            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/collections/" + collectionGuid + "?enumerate";

            using (RestRequest req = new RestRequest(url, HttpMethod.Post))
            {
                req.TimeoutMilliseconds = TimeoutMs;
                req.ContentType = "application/json";

                using (RestResponse resp = await req.SendAsync(Serializer.SerializeJson(query, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                return Serializer.DeserializeJson<EnumerationResult<SourceDocument>>(resp.DataAsString);
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

        #endregion

        #region Search

        /// <summary>
        /// Search a collection.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="query">Query.</param>
        /// <param name="token"></param>
        /// <returns>Search result.</returns>
        public async Task<SearchResult> Search(string collectionGuid, CollectionSearchRequest query, CancellationToken token = default)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));
            if (String.IsNullOrEmpty(collectionGuid)) throw new ArgumentNullException(nameof(collectionGuid));

            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/collections/" + collectionGuid + "?search";

            using (RestRequest req = new RestRequest(url, HttpMethod.Post))
            {
                req.TimeoutMilliseconds = TimeoutMs;
                req.ContentType = "application/json";

                using (RestResponse resp = await req.SendAsync(Serializer.SerializeJson(query, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                return Serializer.DeserializeJson<SearchResult>(resp.DataAsString);
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

        #endregion

        #endregion

        #region Private-Methods

        #endregion
    }
}
