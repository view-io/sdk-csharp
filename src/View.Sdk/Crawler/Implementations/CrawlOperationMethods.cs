namespace View.Sdk.Crawler.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Crawler;
    using View.Sdk.Crawler.Interfaces;

    /// <summary>
    /// Crawl operation methods.
    /// </summary>
    public class CrawlOperationMethods : ICrawlOperationMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Crawl operation methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public CrawlOperationMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<EnumerationResult<CrawlOperation>> Enumerate(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v2.0/tenants/" + _Sdk.TenantGUID + "/crawloperations/";
            return await _Sdk.Enumerate<CrawlOperation>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<CrawlOperation>> RetrieveAll(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/crawloperations/";
            return await _Sdk.RetrieveMany<CrawlOperation>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CrawlOperation> Retrieve(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/crawloperations/" + guid;
            return await _Sdk.Retrieve<CrawlOperation>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CrawlEnumeration> RetrieveEnumeration(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/crawloperations/" + guid + "/enumeration";
            return await _Sdk.Retrieve<CrawlEnumeration>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CrawlOperation> Start(CrawlOperationRequest request, CancellationToken token = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.GUID == Guid.Empty) throw new ArgumentException("GUID cannot be empty", nameof(request));
            if (String.IsNullOrEmpty(request.Name)) throw new ArgumentException("Name cannot be null or empty", nameof(request));
            
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/crawloperations/" + request.GUID + "/start";
            return await _Sdk.Post<CrawlOperationRequest, CrawlOperation>(url, request, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CrawlOperation> Stop(CrawlOperationRequest request, CancellationToken token = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (String.IsNullOrEmpty(request.Name)) throw new ArgumentException("Name cannot be null or empty", nameof(request));
            
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/crawloperations/" + request.GUID + "/stop";
            return await _Sdk.Post<CrawlOperationRequest, CrawlOperation>(url, request, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid guid, CancellationToken token = default)
        {
            if (guid == Guid.Empty) throw new ArgumentNullException(nameof(guid));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/crawloperations/" + guid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Exists(Guid guid, CancellationToken token = default)
        {
            if (guid == Guid.Empty) throw new ArgumentNullException(nameof(guid));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/crawloperations/" + guid;
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}