namespace View.Sdk.Processor.Implementations
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using RestWrapper;
    using View.Sdk;
    using View.Sdk.Processor.Interfaces;

    /// <summary>
    /// UDR generator methods implementation.
    /// </summary>
    public class UdrGeneratorMethods : IUdrGeneratorMethods
    {
        #region Private-Members

        private readonly ViewSdkBase _Sdk;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="sdk">View SDK base instance.</param>
        public UdrGeneratorMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc/>
        public async Task<UdrDocument> GenerateUdr(UdrDocumentRequest doc, string filename = null, CancellationToken token = default)
        {
            if (doc == null) throw new ArgumentNullException(nameof(doc));

            if (!String.IsNullOrEmpty(filename))
                doc.Data = Convert.ToBase64String(await File.ReadAllBytesAsync(filename, token).ConfigureAwait(false));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/processing/udr-generator";

            using (RestRequest req = new RestRequest(url, HttpMethod.Put, "application/json"))
            {
                req.TimeoutMilliseconds = _Sdk.TimeoutMs;
                req.ContentType = "application/json";
                req.Authorization.BearerToken = _Sdk.AccessKey;

                using (RestResponse resp = await req.SendAsync(_Sdk.Serializer.SerializeJson(doc, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            _Sdk.Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                _Sdk.Log(SeverityEnum.Debug, "deserializing response body");
                                UdrDocument docResp = _Sdk.Serializer.DeserializeJson<UdrDocument>(resp.DataAsString);
                                return docResp;
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
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                _Sdk.Log(SeverityEnum.Debug, "deserializing response body");
                                UdrDocument docResp = _Sdk.Serializer.DeserializeJson<UdrDocument>(resp.DataAsString);
                                return docResp;
                            }
                            else
                            {
                                _Sdk.Log(SeverityEnum.Debug, "empty response body, returning null");
                                return null;
                            }
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
    }
}
