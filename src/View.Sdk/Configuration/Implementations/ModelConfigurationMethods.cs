namespace View.Sdk.Configuration.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Configuration.Interfaces;

    /// <summary>
    /// Model configuration methods.
    /// </summary>
    public class ModelConfigurationMethods : IModelConfigurationMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Model configuration methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public ModelConfigurationMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<EnumerationResult<ModelConfiguration>> Enumerate(int maxKeys = 5, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v2.0/tenants/" + _Sdk.TenantGUID + "/modelconfigs?max-keys=" + maxKeys;
            return await _Sdk.Enumerate<ModelConfiguration>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<ModelConfiguration>> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/modelconfigs/";
            return await _Sdk.RetrieveMany<ModelConfiguration>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<ModelConfiguration> Retrieve(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/modelconfigs/" + guid;
            return await _Sdk.Retrieve<ModelConfiguration>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<ModelConfiguration> Create(ModelConfiguration modelConfiguration, CancellationToken token = default)
        {
            if (modelConfiguration == null) throw new ArgumentNullException(nameof(modelConfiguration));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/modelconfigs";
            return await _Sdk.Create<ModelConfiguration>(url, modelConfiguration, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<ModelConfiguration> Update(ModelConfiguration modelConfiguration, CancellationToken token = default)
        {
            if (modelConfiguration == null) throw new ArgumentNullException(nameof(modelConfiguration));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/modelconfigs/" + modelConfiguration.GUID;
            return await _Sdk.Update<ModelConfiguration>(url, modelConfiguration, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Exists(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/modelconfigs/" + guid;
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/modelconfigs/" + guid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}
