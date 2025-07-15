namespace View.Sdk.Configuration.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Configuration.Interfaces;

    /// <summary>
    /// Webhook rule methods
    /// </summary>
    public class WebhookRuleMethods : IWebhookRuleMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Webhook rule methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public WebhookRuleMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<WebhookRule> Create(WebhookRule rule, CancellationToken token = default)
        {
            if (rule == null) throw new ArgumentNullException(nameof(rule));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/webhookrules";
            return await _Sdk.Create<WebhookRule>(url, rule, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Exists(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/webhookrules/" + guid;
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebhookRule> Retrieve(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/webhookrules/" + guid;
            return await _Sdk.Retrieve<WebhookRule>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<WebhookRule>> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/webhookrules";
            return await _Sdk.RetrieveMany<WebhookRule>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebhookRule> Update(WebhookRule rule, CancellationToken token = default)
        {
            if (rule == null) throw new ArgumentNullException(nameof(rule));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/webhookrules/" + rule.GUID;
            return await _Sdk.Update<WebhookRule>(url, rule, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/webhookrules/" + guid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<EnumerationResult<WebhookRule>> Enumerate(int maxKeys = 5, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v2.0/tenants/" + _Sdk.TenantGUID + "/webhookrules?max-keys=" + maxKeys + "&token=" + _Sdk.TenantGUID;
            return await _Sdk.Enumerate<WebhookRule>(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}