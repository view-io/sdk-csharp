namespace View.Sdk.Storage.Implementations
{
    using RestWrapper;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Storage.Interfaces;

    /// <summary>
    /// Object methods implementation.
    /// </summary>
    public class ObjectMethods : IObjectMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Object methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public ObjectMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods
        
        #region Basic-Object-Operations

        /// <inheritdoc />
        public async Task<bool> Exists(string bucketGuid, string objectKey, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));
            if (string.IsNullOrEmpty(objectKey)) throw new ArgumentNullException(nameof(objectKey));
            
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid + "/objects/" + objectKey;
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<string> Retrieve(string bucketGuid, string objectKey, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));
            if (string.IsNullOrEmpty(objectKey)) throw new ArgumentNullException(nameof(objectKey));
            
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid + "/objects/" + objectKey;
            using (RestRequest req = new RestRequest(url))
            {
                req.TimeoutMilliseconds = _Sdk.TimeoutMs;
                req.Authorization.BearerToken = _Sdk.AccessKey;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (_Sdk.LogResponses) _Sdk.Log(SeverityEnum.Debug, "response (status " + resp.StatusCode + "): " + Environment.NewLine + resp.DataAsString);

                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            _Sdk.Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                _Sdk.Log(SeverityEnum.Debug, "deserializing response body");
                                return resp.DataAsString;
                            }
                            else
                            {
                                _Sdk.Log(SeverityEnum.Debug, "empty response body, returning null");
                                return null;
                            }
                        }
                        else
                        {
                            _Sdk.Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            return null;
                        }
                    }
                    else
                    {
                        _Sdk.Log(SeverityEnum.Warn, "no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <inheritdoc />
        public async Task<string> RetrieveRange(string bucketGuid, string objectKey, int startByte, int endByte, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));
            if (string.IsNullOrEmpty(objectKey)) throw new ArgumentNullException(nameof(objectKey));
            if (startByte < 0) throw new ArgumentOutOfRangeException(nameof(startByte));
            if (endByte < startByte) throw new ArgumentOutOfRangeException(nameof(endByte));
            
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid + "/objects/" + objectKey;
            
            using (RestWrapper.RestRequest req = new RestWrapper.RestRequest(url))
            {
                req.TimeoutMilliseconds = _Sdk.TimeoutMs;
                req.Authorization.BearerToken = _Sdk.AccessKey;
                
                // Add Range header
                req.Headers.Add("Range", $"bytes={startByte}-{endByte}");
                
                using (RestWrapper.RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode >= 200 && resp.StatusCode <= 299)
                    {
                        _Sdk.Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                        return resp.DataAsString;
                    }
                    else if (resp != null)
                    {
                        _Sdk.Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                        return null;
                    }
                    else
                    {
                        _Sdk.Log(SeverityEnum.Warn, "no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <inheritdoc />
        public async Task<ObjectMetadata> RetrieveMetadata(string bucketGuid, string objectKey, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));
            if (string.IsNullOrEmpty(objectKey)) throw new ArgumentNullException(nameof(objectKey));
            
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid + "/objects/" + objectKey + "?md";
            return await _Sdk.Retrieve<ObjectMetadata>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(string bucketGuid, string objectKey, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));
            if (string.IsNullOrEmpty(objectKey)) throw new ArgumentNullException(nameof(objectKey));
            
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid + "/objects/" + objectKey;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<ObjectMetadata> CreateNonChunked(string bucketGuid, string objectKey, string data, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));
            if (string.IsNullOrEmpty(objectKey)) throw new ArgumentNullException(nameof(objectKey));
            if (data == null) throw new ArgumentNullException(nameof(data));
            
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid + "/objects/" + objectKey;

            using (RestWrapper.RestRequest req = new RestWrapper.RestRequest(url, System.Net.Http.HttpMethod.Put))
            {
                req.TimeoutMilliseconds = _Sdk.TimeoutMs;
                req.Authorization.BearerToken = _Sdk.AccessKey;
                req.ContentType = "application/octet-stream";

                using (RestWrapper.RestResponse resp = await req.SendAsync(data, token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode >= 200 && resp.StatusCode <= 299)
                    {
                        _Sdk.Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                        return _Sdk.Serializer.DeserializeJson<ObjectMetadata>(resp.DataAsString);
                    }
                    else if (resp != null)
                    {
                        _Sdk.Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                        return null;
                    }
                    else
                    {
                        _Sdk.Log(SeverityEnum.Warn, "no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <inheritdoc />
        public async Task<ObjectMetadata> CreateChunked(string bucketGuid, string objectKey, string data, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));
            if (string.IsNullOrEmpty(objectKey)) throw new ArgumentNullException(nameof(objectKey));
            if (data == null) throw new ArgumentNullException(nameof(data));
            
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid + "/objects/" + objectKey;
            
            using (RestWrapper.RestRequest req = new RestWrapper.RestRequest(url, System.Net.Http.HttpMethod.Put))
            {
                req.TimeoutMilliseconds = _Sdk.TimeoutMs;
                req.Authorization.BearerToken = _Sdk.AccessKey;
                if (!string.IsNullOrWhiteSpace(_Sdk.XToken)) req.Headers.Add("x-token", _Sdk.XToken);
                req.ContentType = "application/octet-stream";
                req.Headers.Add("x-amz-content-sha256", "STREAMING-AWS4-HMAC-SHA256-PAYLOAD");
                
                using (RestWrapper.RestResponse resp = await req.SendAsync(data, token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode >= 200 && resp.StatusCode <= 299)
                    {
                        _Sdk.Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                        return _Sdk.Serializer.DeserializeJson<ObjectMetadata>(resp.DataAsString);
                    }
                    else if (resp != null)
                    {
                        _Sdk.Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                        return null;
                    }
                    else
                    {
                        _Sdk.Log(SeverityEnum.Warn, "no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <inheritdoc />
        public async Task<ObjectMetadata> CreateExpiration(string bucketGuid, string objectKey, Expiration expiration, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));
            if (string.IsNullOrEmpty(objectKey)) throw new ArgumentNullException(nameof(objectKey));
            if (expiration == null) throw new ArgumentNullException(nameof(expiration));
            if (expiration.ExpirationUtc == null) throw new ArgumentException("ExpirationUtc must be set", nameof(expiration));
            
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid + "/objects/" + objectKey + "?expiration";
            
            using (RestWrapper.RestRequest req = new RestWrapper.RestRequest(url, System.Net.Http.HttpMethod.Put))
            {
                req.TimeoutMilliseconds = _Sdk.TimeoutMs;
                req.Authorization.BearerToken = _Sdk.AccessKey;
                if (!string.IsNullOrWhiteSpace(_Sdk.XToken)) req.Headers.Add("x-token", _Sdk.XToken);
                req.ContentType = "application/json";
                
                string json = _Sdk.Serializer.SerializeJson(expiration, true);
                
                using (RestWrapper.RestResponse resp = await req.SendAsync(json, token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode >= 200 && resp.StatusCode <= 299)
                    {
                        _Sdk.Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                        return _Sdk.Serializer.DeserializeJson<ObjectMetadata>(resp.DataAsString);
                    }
                    else if (resp != null)
                    {
                        _Sdk.Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                        return null;
                    }
                    else
                    {
                        _Sdk.Log(SeverityEnum.Warn, "no response from " + url);
                        return null;
                    }
                }
            }
        }
        
        #endregion

        #region ACL-Operations

        /// <inheritdoc />
        public async Task<BucketAcl> RetrieveACL(string bucketGuid, string objectKey, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));
            if (string.IsNullOrEmpty(objectKey)) throw new ArgumentNullException(nameof(objectKey));
            
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid + "/objects/" + objectKey + "?acl";
            return await _Sdk.Retrieve<BucketAcl>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<BucketAclEntry>> CreateACL(string bucketGuid, string objectKey, BucketAcl acl, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));
            if (string.IsNullOrEmpty(objectKey)) throw new ArgumentNullException(nameof(objectKey));
            if (acl == null) throw new ArgumentNullException(nameof(acl));
            
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid + "/objects/" + objectKey + "?acl";
            using (RestRequest req = new RestRequest(url, HttpMethod.Put))
            {
                req.TimeoutMilliseconds = _Sdk.TimeoutMs;
                req.Authorization.BearerToken = _Sdk.AccessKey;
                req.ContentType = "application/json";
                string json = _Sdk.Serializer.SerializeJson(acl, true);
                if (_Sdk.LogRequests) _Sdk.Log(SeverityEnum.Debug, "request: " + Environment.NewLine + json);

                using (RestResponse resp = await req.SendAsync(_Sdk.Serializer.SerializeJson(acl, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (_Sdk.LogResponses) _Sdk.Log(SeverityEnum.Debug, "response (status " + resp.StatusCode + "): " + Environment.NewLine + resp.DataAsString);

                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            _Sdk.Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                _Sdk.Log(SeverityEnum.Debug, "deserializing response body");
                                return _Sdk.Serializer.DeserializeJson<List<BucketAclEntry>>(resp.DataAsString);
                            }
                            else
                            {
                                _Sdk.Log(SeverityEnum.Debug, "empty response body, returning null");
                                return null;
                            }
                        }
                        else
                        {
                            _Sdk.Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            return null;
                        }
                    }
                    else
                    {
                        _Sdk.Log(SeverityEnum.Warn, "no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <inheritdoc />
        public async Task<bool> DeleteACL(string bucketGuid, string objectKey, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));
            if (string.IsNullOrEmpty(objectKey)) throw new ArgumentNullException(nameof(objectKey));
            
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid + "/objects/" + objectKey + "?acl";
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }
        
        #endregion

        #region Tag-Operations

        /// <inheritdoc />
        public async Task<List<BucketTag>> CreateTags(string bucketGuid, string objectKey, List<BucketTag> tags, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));
            if (string.IsNullOrEmpty(objectKey)) throw new ArgumentNullException(nameof(objectKey));
            if (tags == null) throw new ArgumentNullException(nameof(tags));
            
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid + "/objects/" + objectKey + "?tags";
            
            using (RestWrapper.RestRequest req = new RestWrapper.RestRequest(url, System.Net.Http.HttpMethod.Put))
            {
                req.TimeoutMilliseconds = _Sdk.TimeoutMs;
                req.Authorization.BearerToken = _Sdk.AccessKey;
                if (!string.IsNullOrWhiteSpace(_Sdk.XToken)) req.Headers.Add("x-token", _Sdk.XToken);
                req.ContentType = "application/json";
                
                string json = _Sdk.Serializer.SerializeJson(tags, true);
                
                using (RestWrapper.RestResponse resp = await req.SendAsync(json, token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode >= 200 && resp.StatusCode <= 299)
                    {
                        _Sdk.Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                        return _Sdk.Serializer.DeserializeJson<List<BucketTag>>(resp.DataAsString);
                    }
                    else if (resp != null)
                    {
                        _Sdk.Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                        return null;
                    }
                    else
                    {
                        _Sdk.Log(SeverityEnum.Warn, "no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <inheritdoc />
        public async Task<List<BucketTag>> RetrieveTags(string bucketGuid, string objectKey, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));
            if (string.IsNullOrEmpty(objectKey)) throw new ArgumentNullException(nameof(objectKey));
            
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid + "/objects/" + objectKey + "?tags";
            return await _Sdk.Retrieve<List<BucketTag>>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteTags(string bucketGuid, string objectKey, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));
            if (string.IsNullOrEmpty(objectKey)) throw new ArgumentNullException(nameof(objectKey));
            
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid + "/objects/" + objectKey + "?tags";
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Reprocess-Operations

        /// <inheritdoc />
        public async Task<bool> Reprocess(string bucketGuid, string objectKey, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));
            if (string.IsNullOrEmpty(objectKey)) throw new ArgumentNullException(nameof(objectKey));
            
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid + "/objects/" + objectKey + "?reprocess";
            
            using (RestWrapper.RestRequest req = new RestWrapper.RestRequest(url, System.Net.Http.HttpMethod.Put))
            {
                req.TimeoutMilliseconds = _Sdk.TimeoutMs;
                req.Authorization.BearerToken = _Sdk.AccessKey;
                if (!string.IsNullOrWhiteSpace(_Sdk.XToken)) req.Headers.Add("x-token", _Sdk.XToken);
                
                using (RestWrapper.RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode >= 200 && resp.StatusCode <= 299)
                    {
                        _Sdk.Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                        return true;
                    }
                    else if (resp != null)
                    {
                        _Sdk.Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                        return false;
                    }
                    else
                    {
                        _Sdk.Log(SeverityEnum.Warn, "no response from " + url);
                        return false;
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