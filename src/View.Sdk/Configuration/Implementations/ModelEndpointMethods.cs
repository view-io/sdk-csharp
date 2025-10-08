namespace View.Sdk.Configuration.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Configuration.Interfaces;

    /// <summary>
    /// Model endpoint methods.
    /// </summary>
    public class ModelEndpointMethods : IModelEndpointMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Model endpoint methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public ModelEndpointMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<EnumerationResult<ModelEndpoint>> Enumerate(int maxKeys = 5, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v2.0/tenants/" + _Sdk.TenantGUID + "/modelendpoints?max-keys=" + maxKeys;
            return await _Sdk.Enumerate<ModelEndpoint>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<ModelEndpoint>> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/modelendpoints/";
            return await _Sdk.RetrieveMany<ModelEndpoint>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<ModelEndpoint> Retrieve(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/modelendpoints/" + guid;
            return await _Sdk.Retrieve<ModelEndpoint>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<ModelEndpoint> Create(ModelEndpoint modelEndpoint, CancellationToken token = default)
        {
            if (modelEndpoint == null) throw new ArgumentNullException(nameof(modelEndpoint));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/modelendpoints";
            return await _Sdk.Create<ModelEndpoint>(url, modelEndpoint, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<ModelEndpoint> Update(ModelEndpoint modelEndpoint, CancellationToken token = default)
        {
            if (modelEndpoint == null) throw new ArgumentNullException(nameof(modelEndpoint));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/modelendpoints/" + modelEndpoint.GUID;
            return await _Sdk.Update<ModelEndpoint>(url, modelEndpoint, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Exists(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/modelendpoints/" + guid;
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/modelendpoints/" + guid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}
