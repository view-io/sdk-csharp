namespace View.Sdk.Configuration.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Configuration.Interfaces;

    /// <summary>
    /// Webhook target methods
    /// </summary>
    public class WebhookTargetMethods : IWebhookTargetMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Webhook target methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public WebhookTargetMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<WebhookTarget> Create(WebhookTarget target, CancellationToken token = default)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/webhooktargets";
            return await _Sdk.Create<WebhookTarget>(url, target, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Exists(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/webhooktargets/" + guid;
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebhookTarget> Retrieve(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/webhooktargets/" + guid;
            return await _Sdk.Retrieve<WebhookTarget>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<WebhookTarget>> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/webhooktargets";
            return await _Sdk.RetrieveMany<WebhookTarget>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebhookTarget> Update(WebhookTarget target, CancellationToken token = default)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/webhooktargets/" + target.GUID;
            return await _Sdk.Update<WebhookTarget>(url, target, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/webhooktargets/" + guid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<EnumerationResult<WebhookTarget>> Enumerate(int maxKeys = 5, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v2.0/tenants/" + _Sdk.TenantGUID + "/webhooktargets?max-keys=" + maxKeys + "&token=" + _Sdk.TenantGUID;
            return await _Sdk.Enumerate<WebhookTarget>(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}