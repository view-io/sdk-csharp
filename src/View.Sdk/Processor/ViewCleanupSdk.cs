﻿namespace View.Sdk.Processor
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
    using System.Net.Sockets;

    /// <summary>
    /// View Cleanup Pipeline SDK.
    /// </summary>
    public class ViewCleanupSdk : ViewSdkBase
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
        /// <param name="endpoint">Endpoint URL, i.e. http://localhost:8000/v1.0/tenants/tenant-guid/processing/cleanup.</param>
        public ViewCleanupSdk(Guid tenantGuid, string accessKey, string endpoint = "http://localhost:8000/v1.0/tenants/default/processing/cleanup") : base(tenantGuid, accessKey, endpoint)
        {
            Header = "[ViewProcessorSdk] ";
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// Cleanup a document.  This variant is used by the storage server.
        /// </summary>
        /// <param name="requestGuid">Request GUID.</param>
        /// <param name="tenant">Tenant metadata.</param>
        /// <param name="collection">Collection metadata.</param>
        /// <param name="bucket">Bucket metadata.</param>
        /// <param name="pool">Storage pool metadata.</param>
        /// <param name="obj">Object metadata.</param>
        /// <param name="mdRule">Metadata rule.</param>
        /// <param name="embedRule">Embeddings rule.</param>
        /// <param name="vectorRepo">Vector repository.</param>
        /// <param name="graphRepo">Graph repository.</param>
        /// <param name="async">Boolean indicating if the task should be performed asynchronously.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Processor response.</returns>
        public async Task<CleanupResult> Process(
            Guid requestGuid,
            TenantMetadata tenant,
            Collection collection,
            StoragePool pool,
            BucketMetadata bucket,
            ObjectMetadata obj, 
            MetadataRule mdRule, 
            EmbeddingsRule embedRule, 
            VectorRepository vectorRepo,
            GraphRepository graphRepo,
            bool async = false,
            CancellationToken token = default)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (mdRule == null) throw new ArgumentNullException(nameof(mdRule));

            string url = Endpoint;

            try
            {
                using (RestRequest req = new RestRequest(url, HttpMethod.Post))
                {
                    req.TimeoutMilliseconds = TimeoutMs;
                    req.ContentType = "application/json";
                    req.Authorization.BearerToken = AccessKey;

                    CleanupRequest cleanupReq = new CleanupRequest
                    {
                        GUID = requestGuid,
                        Async = async,
                        Tenant = tenant,
                        Collection = collection,
                        Pool = pool,
                        Bucket = bucket,
                        Object = obj,
                        MetadataRule = mdRule,
                        EmbeddingsRule = embedRule,
                        VectorRepository = vectorRepo,
                        GraphRepository = graphRepo
                    };

                    string json = Serializer.SerializeJson(cleanupReq, true);
                    if (LogRequests) Log(SeverityEnum.Debug, "request: " + Environment.NewLine + json);

                    using (RestResponse resp = await req.SendAsync(json, token).ConfigureAwait(false))
                    {
                        if (resp != null)
                        {
                            if (LogResponses) Log(SeverityEnum.Debug, "response (status " + resp.StatusCode + "):" + Environment.NewLine + resp.DataAsString);

                            if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                            {
                                Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");

                                if (!String.IsNullOrEmpty(resp.DataAsString))
                                {
                                    try
                                    {
                                        CleanupResult cleanupResp = Serializer.DeserializeJson<CleanupResult>(resp.DataAsString);
                                        return cleanupResp;
                                    }
                                    catch (Exception)
                                    {
                                        Log(SeverityEnum.Warn, "unable to deserialize response body, returning null");
                                        return null;
                                    }
                                }
                                else
                                {
                                    return null;
                                }
                            }
                            else
                            {
                                Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");

                                if (!String.IsNullOrEmpty(resp.DataAsString))
                                {
                                    try
                                    {
                                        CleanupResult cleanupResp = Serializer.DeserializeJson<CleanupResult>(resp.DataAsString);
                                        return cleanupResp;
                                    }
                                    catch (Exception)
                                    {
                                        Log(SeverityEnum.Warn, "unable to deserialize response body, returning null");
                                        return null;
                                    }
                                }
                                else
                                {
                                    return null;
                                }
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
            catch (HttpRequestException hre)
            {
                Log(SeverityEnum.Warn, "exception while interacting with " + url + ": " + hre.Message);
                return new CleanupResult
                {
                    Success = false,
                    Error = new ApiErrorResponse(ApiErrorEnum.InternalError, null, null)
                };
            }
        }

        /// <summary>
        /// Cleanup a document.  This variant is used by the data crawler.
        /// </summary>
        /// <param name="requestGuid">Request GUID.</param>
        /// <param name="tenant">Tenant metadata.</param>
        /// <param name="collection">Collection metadata.</param>
        /// <param name="repo">Data repository.</param>
        /// <param name="obj">Object metadata.</param>
        /// <param name="mdRule">Metadata rule.</param>
        /// <param name="embedRule">Embeddings rule.</param>
        /// <param name="vectorRepo">Vector repository.</param>
        /// <param name="graphRepo">Graph repository.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Processor response.</returns>
        public async Task<CleanupResult> Process(
            Guid requestGuid,
            TenantMetadata tenant,
            Collection collection,
            DataRepository repo,
            ObjectMetadata obj,
            MetadataRule mdRule,
            EmbeddingsRule embedRule,
            VectorRepository vectorRepo,
            GraphRepository graphRepo,
            CancellationToken token = default)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (mdRule == null) throw new ArgumentNullException(nameof(mdRule));

            string url = Endpoint;

            try
            {
                using (RestRequest req = new RestRequest(url, HttpMethod.Post))
                {
                    req.TimeoutMilliseconds = TimeoutMs;
                    req.ContentType = "application/json";
                    req.Authorization.BearerToken = AccessKey;

                    CleanupRequest cleanupReq = new CleanupRequest
                    {
                        GUID = requestGuid,
                        Tenant = tenant,
                        Collection = collection,
                        DataRepository = repo,
                        Object = obj,
                        MetadataRule = mdRule,
                        EmbeddingsRule = embedRule,
                        VectorRepository = vectorRepo,
                        GraphRepository = graphRepo
                    };

                    string json = Serializer.SerializeJson(cleanupReq, true);
                    if (LogRequests) Log(SeverityEnum.Debug, "request: " + Environment.NewLine + json);

                    using (RestResponse resp = await req.SendAsync(json, token).ConfigureAwait(false))
                    {
                        if (resp != null)
                        {
                            if (LogResponses) Log(SeverityEnum.Debug, "response (status " + resp.StatusCode + "):" + Environment.NewLine + resp.DataAsString);

                            if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                            {
                                Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");

                                if (!String.IsNullOrEmpty(resp.DataAsString))
                                {
                                    try
                                    {
                                        CleanupResult cleanupResp = Serializer.DeserializeJson<CleanupResult>(resp.DataAsString);
                                        return cleanupResp;
                                    }
                                    catch (Exception)
                                    {
                                        Log(SeverityEnum.Warn, "unable to deserialize response body, returning null");
                                        return null;
                                    }
                                }
                                else
                                {
                                    return null;
                                }
                            }
                            else
                            {
                                Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");

                                if (!String.IsNullOrEmpty(resp.DataAsString))
                                {
                                    try
                                    {
                                        CleanupResult cleanupResp = Serializer.DeserializeJson<CleanupResult>(resp.DataAsString);
                                        return cleanupResp;
                                    }
                                    catch (Exception)
                                    {
                                        Log(SeverityEnum.Warn, "unable to deserialize response body, returning null");
                                        return null;
                                    }
                                }
                                else
                                {
                                    return null;
                                }
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
            catch (HttpRequestException hre)
            {
                Log(SeverityEnum.Warn, "exception while interacting with " + url + ": " + hre.Message);
                return new CleanupResult
                {
                    Success = false,
                    Error = new ApiErrorResponse(ApiErrorEnum.InternalError, null, null)
                };
            }
        }

        #endregion

        #region Private-Methods
         
        #endregion
    }
}
