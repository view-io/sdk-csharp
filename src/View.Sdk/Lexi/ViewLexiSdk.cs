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
        /// <param name="endpoint">Endpoint URL, i.e. http://localhost:8000/.</param>
        public ViewLexiSdk(Guid tenantGuid, string accessKey, string endpoint = "http://localhost:8000/") : base(tenantGuid, accessKey, endpoint)
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
        public async Task<Collection> RetrieveCollection(Guid collectionGuid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/collections/" + collectionGuid;
            return await Retrieve<Collection>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve collection statistics.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Collection statistics.</returns>
        public async Task<CollectionStatistics> RetrieveCollectionStatistics(Guid collectionGuid, CancellationToken token = default)
        {
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
        /// <returns>True if successful.</returns>
        public async Task<bool> DeleteCollection(Guid collectionGuid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/collections/" + collectionGuid;
            return await Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Source-Documents

        /// <summary>
        /// Retrieve documents.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of source documents.</returns>
        public async Task<List<SourceDocument>> RetrieveDocuments(Guid collectionGuid, CancellationToken token = default)
        {
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
        public async Task<SourceDocument> RetrieveDocument(Guid collectionGuid, Guid documentGuid, bool includeData = false, CancellationToken token = default)
        {
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
        public async Task<SourceDocumentStatistics> RetrieveDocumentStatistics(Guid collectionGuid, Guid documentGuid, CancellationToken token = default)
        {
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
        /// <returns>True if successful.</returns>
        public async Task<bool> DeleteDocument(Guid collectionGuid, Guid documentGuid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/collections/" + collectionGuid + "/documents/" + documentGuid;
            return await Delete(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a document.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="key">Document key.</param>
        /// <param name="version">Document version.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public async Task<bool> DeleteDocument(Guid collectionGuid, string key, string version, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
            if (String.IsNullOrEmpty(version)) throw new ArgumentNullException(nameof(version));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/collections/" + collectionGuid + "/documents?key=" + key + "&versionId=" + version;
            return await Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Ingest-Queue

        /// <summary>
        /// Check if an ingest queue entry exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> ExistsIngestQueueEntry(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/ingestqueue/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read an ingest queue entry.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Ingest queue entry.</returns>
        public async Task<IngestionQueueEntry> RetrieveIngestQueueEntry(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/ingestqueue/" + guid;
            return await Retrieve<IngestionQueueEntry>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read users.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Ingest queue entries.</returns>
        public async Task<List<IngestionQueueEntry>> RetrieveIngestQueueEntries(CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/ingestqueue";
            return await RetrieveMany<IngestionQueueEntry>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete an ingest queue entry.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public async Task<bool> DeleteIngestQueueEntry(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/ingestqueue/" + guid;
            return await Delete(url, token).ConfigureAwait(false);
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
        public async Task<EnumerationResult<SourceDocument>> Enumerate(Guid collectionGuid, EnumerationQuery query, CancellationToken token = default)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/collections/" + collectionGuid + "?enumerate";

            using (RestRequest req = new RestRequest(url, HttpMethod.Post))
            {
                req.TimeoutMilliseconds = TimeoutMs;
                req.ContentType = "application/json";
                req.Authorization.BearerToken = AccessKey;

                using (RestResponse resp = await req.SendAsync(Serializer.SerializeJson(query, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                Log(SeverityEnum.Debug, "deserializing response body");
                                return Serializer.DeserializeJson<EnumerationResult<SourceDocument>>(resp.DataAsString);
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

        #endregion

        #region Search

        /// <summary>
        /// Search a collection.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="query">Query.</param>
        /// <param name="token"></param>
        /// <returns>Search result.</returns>
        public async Task<SearchResult> Search(Guid collectionGuid, CollectionSearchRequest query, CancellationToken token = default)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/collections/" + collectionGuid + "?search";

            using (RestRequest req = new RestRequest(url, HttpMethod.Post))
            {
                req.TimeoutMilliseconds = TimeoutMs;
                req.ContentType = "application/json";
                req.Authorization.BearerToken = AccessKey;

                using (RestResponse resp = await req.SendAsync(Serializer.SerializeJson(query, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                Log(SeverityEnum.Debug, "deserializing response body");
                                return Serializer.DeserializeJson<SearchResult>(resp.DataAsString);
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

        #endregion

        #endregion

        #region Private-Methods

        #endregion
    }
}
