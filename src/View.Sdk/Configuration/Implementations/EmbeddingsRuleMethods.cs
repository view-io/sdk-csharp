namespace View.Sdk.Configuration.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Configuration.Interfaces;

    /// <summary>
    /// Embeddings rule methods
    /// </summary>
    public class EmbeddingsRuleMethods : IEmbeddingsRuleMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Embeddings rule methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public EmbeddingsRuleMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<EmbeddingsRule> Create(EmbeddingsRule rule, CancellationToken token = default)
        {
            if (rule == null) throw new ArgumentNullException(nameof(rule));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/embeddingsrules";
            return await _Sdk.Create<EmbeddingsRule>(url, rule, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Exists(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/embeddingsrules/" + guid;
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<EmbeddingsRule> Retrieve(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/embeddingsrules/" + guid;
            return await _Sdk.Retrieve<EmbeddingsRule>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<EmbeddingsRule>> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/embeddingsrules";
            return await _Sdk.RetrieveMany<EmbeddingsRule>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<EmbeddingsRule> Update(EmbeddingsRule rule, CancellationToken token = default)
        {
            if (rule == null) throw new ArgumentNullException(nameof(rule));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/embeddingsrules/" + rule.GUID;
            return await _Sdk.Update<EmbeddingsRule>(url, rule, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/embeddingsrules/" + guid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<EnumerationResult<EmbeddingsRule>> Enumerate(int maxKeys = 5, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v2.0/tenants/" + _Sdk.TenantGUID + "/embeddingsrules?max-keys=" + maxKeys + "&token=" + _Sdk.TenantGUID;
            return await _Sdk.Enumerate<EmbeddingsRule>(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}