namespace View.Sdk.Lexi.Implementations
{
    using RestWrapper;
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Lexi.Interfaces;

    /// <summary>
    /// Search methods
    /// </summary>
    public class SearchMethods : ISearchMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Search methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public SearchMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<SearchResult> Search(Guid collectionGuid, CollectionSearchRequest query, CancellationToken token = default)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/collections/" + collectionGuid + "?search";

            using (RestRequest req = new RestRequest(url, HttpMethod.Post))
            {
                req.TimeoutMilliseconds = _Sdk.TimeoutMs;
                req.ContentType = "application/json";
                req.Authorization.BearerToken = _Sdk.AccessKey;

                using (RestResponse resp = await req.SendAsync(_Sdk.Serializer.SerializeJson(query, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            _Sdk.Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                _Sdk.Log(SeverityEnum.Debug, "deserializing response body");
                                return _Sdk.Serializer.DeserializeJson<SearchResult>(resp.DataAsString);
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

        #region Private-Methods

        #endregion
    }
}