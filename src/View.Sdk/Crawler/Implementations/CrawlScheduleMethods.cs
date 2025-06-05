namespace View.Sdk.Crawler.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Crawler.Interfaces;

    /// <summary>
    /// Crawl schedule methods.
    /// </summary>
    public class CrawlScheduleMethods : ICrawlScheduleMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Crawl schedule methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public CrawlScheduleMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<CrawlSchedule> Create(CrawlSchedule schedule, CancellationToken token = default)
        {
            if (schedule == null) throw new ArgumentNullException(nameof(schedule));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/crawlschedules";
            return await _Sdk.Create<CrawlSchedule>(url, schedule, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Exists(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/crawlschedules/" + guid;
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CrawlSchedule> Retrieve(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/crawlschedules/" + guid;
            return await _Sdk.Retrieve<CrawlSchedule>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<CrawlSchedule>> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/crawlschedules";
            return await _Sdk.RetrieveMany<CrawlSchedule>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CrawlSchedule> Update(CrawlSchedule schedule, CancellationToken token = default)
        {
            if (schedule == null) throw new ArgumentNullException(nameof(schedule));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/crawlschedules";
            return await _Sdk.Update<CrawlSchedule>(url, schedule, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/crawlschedules/" + guid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<EnumerationResult<CrawlSchedule>> Enumerate(int maxKeys = 5, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v2.0/tenants/" + _Sdk.TenantGUID + "/crawlschedules/?max-keys=" + maxKeys;
            return await _Sdk.Enumerate<CrawlSchedule>(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}