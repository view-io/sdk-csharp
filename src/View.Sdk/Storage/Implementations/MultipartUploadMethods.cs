namespace View.Sdk.Storage.Implementations
{
    using RestWrapper;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Storage.Interfaces;

    /// <summary>
    /// Multipart upload methods implementation.
    /// </summary>
    public class MultipartUploadMethods : IMultipartUploadMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Multipart upload methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public MultipartUploadMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods
        
        #region Upload-Operations

        /// <inheritdoc />
        public async Task<MultipartUploadMetadata> Create(string bucketGuid, MultipartUploadMetadata obj, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));
            
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid + "/uploads";
            return await _Sdk.Create<MultipartUploadMetadata>(url, obj, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<MultipartUploadMetadata>> RetrieveMany(string bucketGuid, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));
            
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid + "/uploads";
            return await _Sdk.RetrieveMany<MultipartUploadMetadata>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<MultipartUploadMetadata> Retrieve(string bucketGuid, string uploadGuid, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));
            if (string.IsNullOrEmpty(uploadGuid)) throw new ArgumentNullException(nameof(uploadGuid));
            
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid + "/uploads/" + uploadGuid;
            return await _Sdk.Retrieve<MultipartUploadMetadata>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteUpload(string bucketGuid, string uploadGuid, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));
            if (string.IsNullOrEmpty(uploadGuid)) throw new ArgumentNullException(nameof(uploadGuid));
            
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid + "/uploads/" + uploadGuid;
            return await _Sdk.Delete(url,token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<ObjectMetadata> CompleteUpload(string bucketGuid, string uploadGuid, string data, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));
            if (string.IsNullOrEmpty(uploadGuid)) throw new ArgumentNullException(nameof(uploadGuid));
            
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid + "/uploads/" + uploadGuid;

            using (RestRequest req = new RestRequest(url, HttpMethod.Post))
            {
                req.TimeoutMilliseconds = _Sdk.TimeoutMs;
                req.Authorization.BearerToken = _Sdk.AccessKey;
                req.ContentType = "application/json";
                if (_Sdk.LogRequests) _Sdk.Log(SeverityEnum.Debug, "request: " + Environment.NewLine + data);

                using (RestResponse resp = await req.SendAsync(data, token).ConfigureAwait(false))
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
                                return _Sdk.Serializer.DeserializeJson<ObjectMetadata>(resp.DataAsString);
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

        #endregion

        #region Part-Operations

        /// <inheritdoc />
        public async Task<PartMetadata> UploadPart(string bucketGuid, string uploadGuid, int partNumber, string data, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));
            if (string.IsNullOrEmpty(uploadGuid)) throw new ArgumentNullException(nameof(uploadGuid));
            if (string.IsNullOrEmpty(data)) throw new ArgumentNullException(nameof(data));
            
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid + "/uploads/" + uploadGuid + "/parts?partNumber=" + partNumber;
            using (RestRequest req = new RestRequest(url, HttpMethod.Put))
            {
                req.TimeoutMilliseconds = _Sdk.TimeoutMs;
                req.Authorization.BearerToken = _Sdk.AccessKey;
                req.ContentType = "application/json";
                if (_Sdk.LogRequests) _Sdk.Log(SeverityEnum.Debug, "request: " + Environment.NewLine + data);

                using (RestResponse resp = await req.SendAsync(data, token).ConfigureAwait(false))
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
                                return _Sdk.Serializer.DeserializeJson<PartMetadata>(resp.DataAsString);
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
        public async Task<string> RetrievePart(string bucketGuid, string objectKey, int partNumber, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));
            if (string.IsNullOrEmpty(objectKey)) throw new ArgumentNullException(nameof(objectKey));
            if (partNumber < 1) throw new ArgumentOutOfRangeException(nameof(partNumber));
            
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid + "/uploads/" + objectKey + "?partNumber=" + partNumber;
            return await _Sdk.Retrieve<string>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> DeletePart(string bucketGuid, string uploadGuid, int partNumber, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(bucketGuid)) throw new ArgumentNullException(nameof(bucketGuid));
            if (string.IsNullOrEmpty(uploadGuid)) throw new ArgumentNullException(nameof(uploadGuid));
            
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/buckets/" + bucketGuid + "/uploads/" + uploadGuid + "?partNumber=" + partNumber;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #endregion

        #region Private-Methods

        #endregion
    }
}