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
    using System.Collections;
    using View.Sdk.Semantic;
    using System.Linq;
    using System.Reflection.Metadata;

    /// <summary>
    /// View Vector SDK.
    /// </summary>
    public class ViewVectorSdk : ViewSdkBase, IDisposable
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
        public ViewVectorSdk(string tenantGuid, string accessKey, string endpoint = "http://localhost:8311/") : base(tenantGuid, accessKey, endpoint)
        {
            Header = "[ViewVectorSdk] ";
        }

        #endregion

        #region Public-Methods

        #region Search-and-Enumerate

        /// <summary>
        /// Vector query.
        /// </summary>
        /// <param name="queryReq">Query request.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task<string> VectorQuery(
            VectorQueryRequest queryReq,
            CancellationToken token = default)
        {
            if (queryReq == null) throw new ArgumentNullException(nameof(queryReq));
            if (String.IsNullOrEmpty(queryReq.VectorRepositoryGUID)) throw new ArgumentNullException(nameof(queryReq.VectorRepositoryGUID));

            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/vectorrepositories/" + queryReq.VectorRepositoryGUID + "/query";

            using (RestRequest req = new RestRequest(url, HttpMethod.Post))
            {
                req.TimeoutMilliseconds = TimeoutMs;
                req.ContentType = "application/json";

                using (RestResponse resp = await req.SendAsync(Serializer.SerializeJson(queryReq, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            return resp.DataAsString;
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
        /// Vector search.
        /// </summary>
        /// <param name="searchReq">Search request.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of vector chunks.</returns>
        public async Task<List<VectorChunk>> VectorSearch(
            VectorSearchRequest searchReq,
            CancellationToken token = default)
        {
            if (searchReq == null) throw new ArgumentNullException(nameof(searchReq));
            if (String.IsNullOrEmpty(searchReq.VectorRepositoryGUID)) throw new ArgumentNullException(nameof(searchReq.VectorRepositoryGUID));

            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/vectorrepositories/" + searchReq.VectorRepositoryGUID + "/search";

            using (RestRequest req = new RestRequest(url, HttpMethod.Post))
            {
                req.TimeoutMilliseconds = TimeoutMs;
                req.ContentType = "application/json";

                using (RestResponse resp = await req.SendAsync(Serializer.SerializeJson(searchReq, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                return Serializer.DeserializeJson<List<VectorChunk>>(resp.DataAsString);
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
        /// Find existing embeddings.
        /// </summary>
        /// <param name="findReq">Find embeddings request.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Find embeddings result.</returns>
        public async Task<FindEmbeddingsResult> FindEmbeddings(
            FindEmbeddingsRequest findReq,
            CancellationToken token = default)
        {
            if (findReq == null) throw new ArgumentNullException(nameof(findReq));
            if (String.IsNullOrEmpty(findReq.VectorRepositoryGUID)) throw new ArgumentNullException(nameof(findReq.VectorRepositoryGUID));

            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/vectorrepositories/" + findReq.VectorRepositoryGUID + "/find";

            using (RestRequest req = new RestRequest(url, HttpMethod.Post))
            {
                req.TimeoutMilliseconds = TimeoutMs;
                req.ContentType = "application/json";

                using (RestResponse resp = await req.SendAsync(Serializer.SerializeJson(findReq, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                return Serializer.DeserializeJson<FindEmbeddingsResult>(resp.DataAsString);
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

        #region Repositories

        /// <summary>
        /// Enumerate documents.
        /// </summary>
        /// <param name="query">Enumeration query.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumeration result.</returns>
        public async Task<EnumerationResult<EmbeddingsDocument>> EnumerateDocuments(
            EnumerationQuery query,
            CancellationToken token = default)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/vectorrepositories/" + query.VectorRepositoryGUID + "/enumerate";

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
                                return Serializer.DeserializeJson<EnumerationResult<EmbeddingsDocument>>(resp.DataAsString);
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
        /// Truncate repository.
        /// </summary>
        /// <param name="repoGuid">Vector repository GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if deleted.</returns>
        public async Task<bool> TruncateRepository(
            string repoGuid,
            CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(repoGuid)) throw new ArgumentNullException(nameof(repoGuid));

            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/vectorrepositories/" + repoGuid;

            using (RestRequest req = new RestRequest(url, HttpMethod.Delete))
            {
                req.TimeoutMilliseconds = TimeoutMs;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
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
        /// Get statistics for a repository.
        /// </summary>
        /// <param name="repoGuid">Vector repository GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Statistics.</returns>
        public async Task<VectorRepositoryStatistics> GetStatistics(
            string repoGuid,
            CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(repoGuid)) throw new ArgumentNullException(nameof(repoGuid));

            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/vectorrepositories/" + repoGuid + "/stats";

            using (RestRequest req = new RestRequest(url, HttpMethod.Get))
            {
                req.TimeoutMilliseconds = TimeoutMs;
                req.ContentType = "application/json";

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                return Serializer.DeserializeJson<VectorRepositoryStatistics>(resp.DataAsString);
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

        #region Documents

        /// <summary>
        /// Check if a document exists.
        /// </summary>
        /// <param name="repoGuid">Vector repository GUID.</param>
        /// <param name="docGuid">Document GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if deleted.</returns>
        public async Task<bool> DocumentExists(
            string repoGuid,
            string docGuid,
            CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(repoGuid)) throw new ArgumentNullException(nameof(repoGuid));
            if (String.IsNullOrEmpty(docGuid)) throw new ArgumentNullException(nameof(docGuid));

            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/vectorrepositories/" + repoGuid + "/documents/" + docGuid;

            using (RestRequest req = new RestRequest(url, HttpMethod.Head))
            {
                req.TimeoutMilliseconds = TimeoutMs;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
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
        /// Write a document.
        /// </summary>
        /// <param name="document">Embeddings document.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task<EmbeddingsDocument> WriteDocument(
            EmbeddingsDocument document,
            CancellationToken token = default)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));
            if (String.IsNullOrEmpty(document.VectorRepositoryGUID)) throw new ArgumentNullException(nameof(document.VectorRepositoryGUID));

            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/vectorrepositories/" + document.VectorRepositoryGUID + "/documents";

            using (RestRequest req = new RestRequest(url, HttpMethod.Post))
            {
                req.TimeoutMilliseconds = TimeoutMs;
                req.ContentType = "application/json";

                using (RestResponse resp = await req.SendAsync(Serializer.SerializeJson(document, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                return Serializer.DeserializeJson<EmbeddingsDocument>(resp.DataAsString);
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
        /// Delete a document.
        /// </summary>
        /// <param name="repoGuid">Vector repository GUID.</param>
        /// <param name="docGuid">Document GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if deleted.</returns>
        public async Task<bool> DeleteDocument(
            string repoGuid,
            string docGuid,
            CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(repoGuid)) throw new ArgumentNullException(nameof(repoGuid));
            if (String.IsNullOrEmpty(docGuid)) throw new ArgumentNullException(nameof(docGuid));

            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/vectorrepositories/" + repoGuid + "/documents/" + docGuid;

            using (RestRequest req = new RestRequest(url, HttpMethod.Delete))
            {
                req.TimeoutMilliseconds = TimeoutMs;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
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
        /// Delete documents by filter.
        /// </summary>
        /// <param name="repoGuid">Repository GUID.</param>
        /// <param name="deleteRequest">Delete request.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public async Task<bool> DeleteDocumentsByFilter(
            string repoGuid,
            VectorDeleteRequest deleteRequest,
            CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(repoGuid)) throw new ArgumentNullException(nameof(repoGuid));
            if (deleteRequest == null) throw new ArgumentNullException(nameof(deleteRequest));

            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/vectorrepositories/" + repoGuid + "/documents";

            using (RestRequest req = new RestRequest(url, HttpMethod.Delete))
            {
                req.TimeoutMilliseconds = TimeoutMs;
                req.ContentType = "application/json";

                using (RestResponse resp = await req.SendAsync(Serializer.SerializeJson(deleteRequest, true), token).ConfigureAwait(false))
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

        #endregion

        #region Semantic-Cells

        /// <summary>
        /// Read semantic cells for a given document.
        /// </summary>
        /// <param name="repoGuid">Vector repository GUID.</param>
        /// <param name="docGuid">Document GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Semantic cells.</returns>
        public async Task<List<SemanticCell>> ReadSemanticCells(
            string repoGuid,
            string docGuid,
            CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(repoGuid)) throw new ArgumentNullException(nameof(repoGuid));
            if (String.IsNullOrEmpty(docGuid)) throw new ArgumentNullException(nameof(docGuid));

            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/vectorrepositories/" + repoGuid + "/documents/" + docGuid + "/cells";

            using (RestRequest req = new RestRequest(url, HttpMethod.Get))
            {
                req.TimeoutMilliseconds = TimeoutMs;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                return Serializer.DeserializeJson<List<SemanticCell>>(resp.DataAsString);
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
        /// Read a specific semantic cell for a given document.
        /// </summary>
        /// <param name="repoGuid">Vector repository GUID.</param>
        /// <param name="docGuid">Document GUID.</param>
        /// <param name="cellGuid">Cell GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Semantic cell.</returns>
        public async Task<SemanticCell> ReadSemanticCell(
            string repoGuid,
            string docGuid,
            string cellGuid,
            CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(repoGuid)) throw new ArgumentNullException(nameof(repoGuid));
            if (String.IsNullOrEmpty(docGuid)) throw new ArgumentNullException(nameof(docGuid));
            if (String.IsNullOrEmpty(cellGuid)) throw new ArgumentNullException(nameof(cellGuid));

            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/vectorrepositories/" + repoGuid + "/documents/" + docGuid + "/cells/" + cellGuid;

            using (RestRequest req = new RestRequest(url, HttpMethod.Get))
            {
                req.TimeoutMilliseconds = TimeoutMs;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                return Serializer.DeserializeJson<SemanticCell>(resp.DataAsString);
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

        #region Semantic-Chunks

        /// <summary>
        /// Read semantic chunks for a given document.
        /// </summary>
        /// <param name="repoGuid">Vector repository GUID.</param>
        /// <param name="docGuid">Document GUID.</param>
        /// <param name="cellGuid">Cell GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Semantic chunks.</returns>
        public async Task<List<SemanticChunk>> ReadSemanticChunks(
            string repoGuid,
            string docGuid,
            string cellGuid,
            CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(repoGuid)) throw new ArgumentNullException(nameof(repoGuid));
            if (String.IsNullOrEmpty(docGuid)) throw new ArgumentNullException(nameof(docGuid));
            if (String.IsNullOrEmpty(cellGuid)) throw new ArgumentNullException(nameof(cellGuid));

            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/vectorrepositories/" + repoGuid + "/documents/" + docGuid + "/cells/" + cellGuid + "/chunks";

            using (RestRequest req = new RestRequest(url, HttpMethod.Get))
            {
                req.TimeoutMilliseconds = TimeoutMs;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                return Serializer.DeserializeJson<List<SemanticChunk>>(resp.DataAsString);
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
        /// Read a specific semantic chunk for a given document.
        /// </summary>
        /// <param name="repoGuid">Vector repository GUID.</param>
        /// <param name="docGuid">Document GUID.</param>
        /// <param name="cellGuid">Cell GUID.</param>
        /// <param name="chunkGuid">Chunk GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Semantic chunk.</returns>
        public async Task<SemanticChunk> ReadSemanticChunk(
            string repoGuid,
            string docGuid,
            string cellGuid,
            string chunkGuid,
            CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(repoGuid)) throw new ArgumentNullException(nameof(repoGuid));
            if (String.IsNullOrEmpty(docGuid)) throw new ArgumentNullException(nameof(docGuid));
            if (String.IsNullOrEmpty(cellGuid)) throw new ArgumentNullException(nameof(cellGuid));
            if (String.IsNullOrEmpty(chunkGuid)) throw new ArgumentNullException(nameof(chunkGuid));

            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/vectorrepositories/" + repoGuid + "/documents/" + docGuid + "/cells/" + cellGuid + "/chunks/" + chunkGuid;

            using (RestRequest req = new RestRequest(url, HttpMethod.Get))
            {
                req.TimeoutMilliseconds = TimeoutMs;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                return Serializer.DeserializeJson<SemanticChunk>(resp.DataAsString);
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
