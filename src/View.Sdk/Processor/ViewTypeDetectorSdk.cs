namespace View.Sdk.Processor
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using RestWrapper;
    using View.Sdk;
    using View.Sdk.Serialization;

    /// <summary>
    /// View Type Detector SDK.
    /// </summary>
    public class ViewTypeDetectorSdk : ViewSdkBase, IDisposable
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
        /// <param name="endpoint">Endpoint URL, i.e. http://localhost:8000/v1.0/tenants/tenant-guid/processing/typedetection.</param>
        public ViewTypeDetectorSdk(string tenantGuid, string accessKey, string endpoint = "http://localhost:8000/v1.0/tenants/default/processing/typedetection") : base(tenantGuid, accessKey, endpoint)
        {
            Header = "[ViewTypeDetectorSdk] ";
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// Determine a document type.
        /// </summary>
        /// <param name="data">Data.</param>
        /// <param name="contentType">Content-type.  CSV content-types are inferred using this header's value.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>TypeResult.</returns>
        public async Task<TypeResult> Process(
            byte[] data,
            string contentType = null,
            CancellationToken token = default)
        {
            if (data == null || data.Length < 1) throw new ArgumentException("No data supplied for content type detection.");
            if (String.IsNullOrEmpty(contentType)) contentType = "application/octet-stream";

            string url = Endpoint;

            try
            {
                using (RestRequest req = new RestRequest(url, HttpMethod.Post))
                {
                    req.TimeoutMilliseconds = TimeoutMs;
                    req.ContentType = contentType;
                    req.Authorization.BearerToken = AccessKey;

                    if (LogRequests) Log(SeverityEnum.Debug, "request: " + Environment.NewLine + Encoding.UTF8.GetString(data));

                    using (RestResponse resp = await req.SendAsync(data, token).ConfigureAwait(false))
                    {
                        if (resp != null)
                        {
                            if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                            {
                                Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");

                                if (!String.IsNullOrEmpty(resp.DataAsString))
                                {
                                    if (LogResponses) Log(SeverityEnum.Debug, "response body: " + Environment.NewLine + resp.DataAsString);

                                    Log(SeverityEnum.Debug, "deserializing response body");
                                    TypeResult tr = Serializer.DeserializeJson<TypeResult>(resp.DataAsString);
                                    return tr;
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

                                if (!String.IsNullOrEmpty(resp.DataAsString))
                                {
                                    if (LogResponses) Log(SeverityEnum.Warn, "response body: " + Environment.NewLine + resp.DataAsString);

                                    TypeResult tr = Serializer.DeserializeJson<TypeResult>(resp.DataAsString);
                                    return tr;
                                }
                                else
                                {
                                    Log(SeverityEnum.Debug, "empty response body, returning null");
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

                return new TypeResult
                {
                    MimeType = "application/octet-stream",
                    Extension = null,
                    Type = DocumentTypeEnum.Unknown
                };
            }
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}
