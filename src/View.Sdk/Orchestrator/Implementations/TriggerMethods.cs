namespace View.Sdk.Orchestrator.Implementations
{
    using RestWrapper;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk;
    using View.Sdk.Orchestrator.Interfaces;

    /// <summary>
    /// Trigger methods
    /// </summary>
    public class TriggerMethods : ITriggerMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Trigger methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public TriggerMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<Trigger> Create(Trigger trigger, CancellationToken token = default)
        {
            if (trigger == null) throw new ArgumentNullException(nameof(trigger));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/triggers";
            return await _Sdk.Create<Trigger>(url, trigger, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Exists(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/triggers/" + guid;
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Trigger> Retrieve(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/triggers/" + guid;
            return await _Sdk.Retrieve<Trigger>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<Trigger>> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/triggers";
            return await _Sdk.RetrieveMany<Trigger>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Trigger> Update(Trigger trigger, CancellationToken token = default)
        {
            if (trigger == null) throw new ArgumentNullException(nameof(trigger));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/triggers/" + trigger.GUID;
            return await _Sdk.Update<Trigger>(url, trigger, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/triggers/" + guid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}