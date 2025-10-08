namespace View.Sdk.Configuration.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Configuration.Interfaces;

    /// <summary>
    /// Model profile methods.
    /// </summary>
    public class ModelProfileMethods : IModelProfileMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Model profile methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public ModelProfileMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<EnumerationResult<ModelProfile>> Enumerate(int maxKeys = 5, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v2.0/tenants/" + _Sdk.TenantGUID + "/modelprofiles?max-keys=" + maxKeys;
            return await _Sdk.Enumerate<ModelProfile>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<ModelProfile>> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/modelprofiles/";
            return await _Sdk.RetrieveMany<ModelProfile>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<ModelProfile> Retrieve(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/modelprofiles/" + guid;
            return await _Sdk.Retrieve<ModelProfile>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<ModelProfile> Create(ModelProfile modelProfile, CancellationToken token = default)
        {
            if (modelProfile == null) throw new ArgumentNullException(nameof(modelProfile));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/modelprofiles";
            return await _Sdk.Create<ModelProfile>(url, modelProfile, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<ModelProfile> Update(ModelProfile modelProfile, CancellationToken token = default)
        {
            if (modelProfile == null) throw new ArgumentNullException(nameof(modelProfile));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/modelprofiles/" + modelProfile.GUID;
            return await _Sdk.Update<ModelProfile>(url, modelProfile, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Exists(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/modelprofiles/" + guid;
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/modelprofiles/" + guid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}
