namespace View.Sdk.Crawler.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Crawler.Interfaces;

    /// <summary>
    /// Crawl filter methods.
    /// </summary>
    public class CrawlFilterMethods : ICrawlFilterMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Crawl filter methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public CrawlFilterMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<CrawlFilter> Create(CrawlFilter filter, CancellationToken token = default)
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/crawlfilters";
            return await _Sdk.Create<CrawlFilter>(url, filter, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Exists(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/crawlfilters/" + guid;
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CrawlFilter> Retrieve(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/crawlfilters/" + guid;
            return await _Sdk.Retrieve<CrawlFilter>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<CrawlFilter>> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/crawlfilters";
            return await _Sdk.RetrieveMany<CrawlFilter>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CrawlFilter> Update(CrawlFilter filter, CancellationToken token = default)
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/crawlfilters/" + filter.GUID;
            return await _Sdk.Update<CrawlFilter>(url, filter, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/crawlfilters/" + guid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<EnumerationResult<CrawlFilter>> Enumerate(int maxKeys = 5, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v2.0/tenants/" + _Sdk.TenantGUID + "/crawlfilters/?max-keys=" + maxKeys;
            return await _Sdk.Enumerate<CrawlFilter>(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}