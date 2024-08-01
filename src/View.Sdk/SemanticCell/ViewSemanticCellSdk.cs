namespace View.Sdk.Processor
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
    public class ViewSemanticCellSdk : ViewSdkBase
    {
        #region Public-Members

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="endpoint">Endpoint URL.</param>
        public ViewSemanticCellSdk(string endpoint = "http://localhost:8341/") : base(endpoint)
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
        public async Task<SemanticCellResponse> Process(
            SemanticCellRequest scReq,
            CancellationToken token = default)
        {
            if (scReq == null) throw new ArgumentNullException(nameof(scReq));
            if (scReq.Data == null || scReq.Data.Length < 1) throw new ArgumentException("No data supplied for semantic cell extraction.");
            if (scReq.MetadataRule == null) throw new ArgumentNullException(nameof(scReq.MetadataRule));
            if (String.IsNullOrEmpty(scReq.MetadataRule.SemanticCellEndpoint)) throw new ArgumentNullException(nameof(scReq.MetadataRule.SemanticCellEndpoint));

            string url = Endpoint + "v1.0/document";
            string json = Serializer.SerializeJson(scReq, true);

            try
            {
                using (RestRequest req = new RestRequest(url, HttpMethod.Put, "application/json"))
                {
                    if (LogRequests) Log(SeverityEnum.Debug, "request body: " + Environment.NewLine + json);

                    using (RestResponse resp = await req.SendAsync(Serializer.SerializeJson(scReq, true), token).ConfigureAwait(false))
                    {
                        if (resp != null)
                        {
                            if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                            {
                                Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");

                                if (!string.IsNullOrEmpty(resp.DataAsString))
                                {
                                    if (LogResponses) Log(SeverityEnum.Debug, "response body: " + Environment.NewLine + resp.DataAsString);

                                    SemanticCellResponse scr = Serializer.DeserializeJson<SemanticCellResponse>(resp.DataAsString);
                                    return scr;
                                }
                                else
                                {
                                    return null;
                                }
                            }
                            else
                            {
                                Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");

                                if (!string.IsNullOrEmpty(resp.DataAsString))
                                {
                                    if (LogResponses) Log(SeverityEnum.Warn, "response body: " + Environment.NewLine + resp.DataAsString);

                                    SemanticCellResponse scr = Serializer.DeserializeJson<SemanticCellResponse>(resp.DataAsString);
                                    return scr;
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
                return null;
            }
        }

        /// <summary>
        /// Extract semantic cells from a document.
        /// </summary>
        /// <param name="docType">Document type.</param>
        /// <param name="mdRule">Metadata rule.</param>
        /// <param name="data">Data.</param>
        /// <param name="maxChunkContentLength">Maximum chunk content length.</param>
        /// <param name="shiftSize">Shift size.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Semantic cell response.</returns>
        public async Task<SemanticCellResponse> Process(
            DocumentTypeEnum docType,
            MetadataRule mdRule,
            byte[] data,
            int maxChunkContentLength = 512,
            int shiftSize = 512,
            CancellationToken token = default)
        {
            if (data == null || data.Length < 1) throw new ArgumentException("No data supplied for semantic cell extraction.");
            if (mdRule == null) throw new ArgumentNullException(nameof(mdRule));

            string url = Endpoint + "v1.0/document";

            SemanticCellRequest scReq = new SemanticCellRequest
            {
                DocumentType = docType,
                MetadataRule = mdRule,
                Data = data,
                MaxChunkContentLength = maxChunkContentLength,
                ShiftSize = shiftSize
            };

            return await Process(scReq);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}
