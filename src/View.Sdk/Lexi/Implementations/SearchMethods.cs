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

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/collections/" + collectionGuid + "/documents?search";
            return await ExecuteSearchRequest(url, query, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<SearchResult> SearchIncludeData(Guid collectionGuid, CollectionSearchRequest query, CancellationToken token = default)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/collections/" + collectionGuid + "/documents?search&incldata";
            return await ExecuteSearchRequest(url, query, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<SearchResult> SearchIncludeTopTerms(Guid collectionGuid, CollectionSearchRequest query, CancellationToken token = default)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/collections/" + collectionGuid + "/documents?search&incltopterms";
            return await ExecuteSearchRequest(url, query, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<SearchResult> SearchAsync(Guid collectionGuid, CollectionSearchRequest query, CancellationToken token = default)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/collections/" + collectionGuid + "/documents?search&async";
            return await ExecuteSearchRequest(url, query, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<EnumerationResult<SourceDocument>> Enumerate(Guid collectionGuid, CollectionSearchRequest query, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/collections/" + collectionGuid + "/documents?enumerate";
            
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
                                return _Sdk.Serializer.DeserializeJson<EnumerationResult<SourceDocument>>(resp.DataAsString);
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

        /// <summary>
        /// Execute a search request to the specified URL with the given query.
        /// </summary>
        /// <param name="url">The API endpoint URL.</param>
        /// <param name="query">The search query.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Search result.</returns>
        private async Task<SearchResult> ExecuteSearchRequest(string url, CollectionSearchRequest query, CancellationToken token = default)
        {
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
    }
}