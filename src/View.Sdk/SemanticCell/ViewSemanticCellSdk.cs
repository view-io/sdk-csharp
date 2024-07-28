namespace View.Sdk.Processor
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.VisualBasic;
    using RestWrapper;
    using View.Sdk;
    using View.Serializer;

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
        public ViewSemanticCellSdk(string endpoint = "http://localhost:9341/") : base(endpoint)
        {
            Header = "[ViewSemanticCellSdk] ";
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// Determine a document type.
        /// </summary>
        /// <param name="docType">Document type.</param>
        /// <param name="data">Data.</param>
        /// <param name="token">Cancellation token.</param>
        /// <param name="maxChunkContentLength">Maximum chunk content length.</param>
        /// <param name="shiftSize">Shift size.</param>
        /// <returns>Semantic cell response.</returns>
        public async Task<SemanticCellResponse> Process(
            DocumentTypeEnum docType,
            byte[] data,
            CancellationToken token = default,
            int maxChunkContentLength = 512,
            int shiftSize = 512)
        {
            if (data == null || data.Length < 1) throw new ArgumentException("No data supplied for semantic cell extraction.");

            string url = Endpoint + "v1.0/document";

            SemanticCellRequest scReq = new SemanticCellRequest
            {
                DocumentType = docType,
                Data = data,
                MaxChunkContentLength = maxChunkContentLength,
                ShiftSize = shiftSize
            };

            try
            {
                using (RestRequest req = new RestRequest(url, HttpMethod.Put, "application/json"))
                {
                    if (LogRequests) Log(SeverityEnum.Debug, "request body: " + Environment.NewLine + Encoding.UTF8.GetString(data));

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

        #endregion

        #region Private-Methods

        #endregion
    }
}
