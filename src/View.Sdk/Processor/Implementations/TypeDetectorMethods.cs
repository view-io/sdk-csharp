namespace View.Sdk.Processor.Implementations
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using RestWrapper;
    using View.Sdk;
    using View.Sdk.Processor.Interfaces;

    /// <summary>
    /// Type detector methods implementation.
    /// </summary>
    public class TypeDetectorMethods : ITypeDetectorMethods
    {
        #region Private-Members

        private readonly ViewSdkBase _Sdk;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="sdk">View SDK base instance.</param>
        public TypeDetectorMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc/>
        public async Task<TypeResult> DetectType(
            string jsonContent,
            CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(jsonContent)) throw new ArgumentNullException(nameof(jsonContent));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/processing/typedetection";

            try
            {
                using (RestRequest req = new RestRequest(url, HttpMethod.Post))
                {
                    req.TimeoutMilliseconds = _Sdk.TimeoutMs;
                    req.ContentType = "application/json";
                    req.Authorization.BearerToken = _Sdk.AccessKey;

                    if (_Sdk.LogRequests) _Sdk.Log(SeverityEnum.Debug, "request: " + Environment.NewLine + jsonContent);

                    using (RestResponse resp = await req.SendAsync(jsonContent, token).ConfigureAwait(false))
                    {
                        if (resp != null)
                        {
                            string responseData = await _Sdk.ReadResponse(resp, url, token).ConfigureAwait(false);
                            if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                            {
                                _Sdk.Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");

                                if (!String.IsNullOrEmpty(responseData))
                                {
                                    if (_Sdk.LogResponses) _Sdk.Log(SeverityEnum.Debug, "response body: " + Environment.NewLine + responseData);

                                    _Sdk.Log(SeverityEnum.Debug, "deserializing response body");
                                    TypeResult tr = _Sdk.Serializer.DeserializeJson<TypeResult>(responseData);
                                    return tr;
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

                                if (!String.IsNullOrEmpty(responseData))
                                {
                                    if (_Sdk.LogResponses) _Sdk.Log(SeverityEnum.Warn, "response body: " + Environment.NewLine + responseData);

                                    TypeResult tr = _Sdk.Serializer.DeserializeJson<TypeResult>(responseData);
                                    return tr;
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
            catch (HttpRequestException hre)
            {
                _Sdk.Log(SeverityEnum.Warn, "exception while interacting with " + url + ": " + hre.Message);

                return new TypeResult
                {
                    MimeType = "application/octet-stream",
                    Extension = null,
                    Type = DocumentTypeEnum.Unknown
                };
            }
        }

        #endregion
    }
}
