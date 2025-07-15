namespace View.Sdk.Crawler.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Crawler.Interfaces;

    /// <summary>
    /// Crawl plan methods.
    /// </summary>
    public class CrawlPlanMethods : ICrawlPlanMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Crawl plan methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public CrawlPlanMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<CrawlPlan> Create(CrawlPlan plan, CancellationToken token = default)
        {
            if (plan == null) throw new ArgumentNullException(nameof(plan));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/crawlplans";
            return await _Sdk.Create<CrawlPlan>(url, plan, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Exists(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/crawlplans/" + guid;
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CrawlPlan> Retrieve(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/crawlplans/" + guid;
            return await _Sdk.Retrieve<CrawlPlan>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<CrawlPlan>> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/crawlplans/";
            return await _Sdk.RetrieveMany<CrawlPlan>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CrawlPlan> Update(CrawlPlan plan, CancellationToken token = default)
        {
            if (plan == null) throw new ArgumentNullException(nameof(plan));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/crawlplans/" + plan.GUID;
            return await _Sdk.Update<CrawlPlan>(url, plan, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/crawlplans/" + guid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<EnumerationResult<CrawlPlan>> Enumerate(int maxKeys = 5, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v2.0/tenants/" + _Sdk.TenantGUID + "/crawlplans/?max-keys=" + maxKeys;
            return await _Sdk.Enumerate<CrawlPlan>(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}