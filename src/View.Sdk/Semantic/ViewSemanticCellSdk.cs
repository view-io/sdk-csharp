namespace View.Sdk.Semantic
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using RestWrapper;
    using View.Sdk;

    /// <summary>
    /// View Semantic Cell SDK.
    /// </summary>
    public class ViewSemanticCellSdk : ViewSdkBase, IDisposable
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
        /// <param name="endpoint">Endpoint URL, i.e. http://localhost:8000/v1.0/tenants/tenant-guid/processing/semanticcell.</param>
        public ViewSemanticCellSdk(Guid tenantGuid, string accessKey, string endpoint = "http://localhost:8000/") : base(tenantGuid, accessKey, endpoint)
        {
            Header = "[ViewSemanticCellSdk] ";
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// Extract semantic cells from a document.
        /// </summary>
        /// <param name="scReq">Semantic cell extraction request.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Semantic cell response.</returns>
        public async Task<SemanticCellResult> Process(
            SemanticCellRequest scReq,
            CancellationToken token = default)
        {
            if (scReq == null) throw new ArgumentNullException(nameof(scReq));
            if (scReq.Data == null || scReq.Data.Length < 1) throw new ArgumentException("No data supplied for semantic cell extraction.");

            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/processing/semanticcell";
            string json = Serializer.SerializeJson(scReq, true);

            try
            {
                using (RestRequest req = new RestRequest(url, HttpMethod.Post, "application/json"))
                {
                    req.TimeoutMilliseconds = TimeoutMs;
                    req.ContentType = "application/json";
                    req.Authorization.BearerToken = AccessKey;

                    if (LogRequests) Log(SeverityEnum.Debug, "request body: " + Environment.NewLine + json);

                    using (RestResponse resp = await req.SendAsync(Serializer.SerializeJson(scReq, true), token).ConfigureAwait(false))
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
                                    SemanticCellResult scr = Serializer.DeserializeJson<SemanticCellResult>(resp.DataAsString);
                                    return scr;
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

                                    Log(SeverityEnum.Debug, "deserializing response body");
                                    SemanticCellResult scr = Serializer.DeserializeJson<SemanticCellResult>(resp.DataAsString);
                                    return scr;
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
                return null;
            }
        }

        /// <summary>
        /// Extract semantic cells from a document.
        /// </summary>
        /// <param name="docType">Document type.</param>
        /// <param name="data">Data.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Semantic cell response.</returns>
        public async Task<SemanticCellResult> Process(
            DocumentTypeEnum docType,
            byte[] data,
            CancellationToken token = default)
        {
            if (data == null || data.Length < 1) throw new ArgumentException("No data supplied for semantic cell extraction.");

            string url = Endpoint;

            SemanticCellRequest scReq = new SemanticCellRequest
            {
                DocumentType = docType,
                Data = data
            };

            return await Process(scReq);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}
