namespace View.Sdk.Configuration.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Configuration.Interfaces;

    /// <summary>
    /// Vector repository methods
    /// </summary>
    public class VectorRepositoryMethods : IVectorRepositoryMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Vector repository methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public VectorRepositoryMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<VectorRepository> Create(VectorRepository repo, CancellationToken token = default)
        {
            if (repo == null) throw new ArgumentNullException(nameof(repo));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/vectorrepositories";
            return await _Sdk.Create<VectorRepository>(url, repo, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Exists(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/vectorrepositories/" + guid;
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<VectorRepository> Retrieve(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/vectorrepositories/" + guid;
            return await _Sdk.Retrieve<VectorRepository>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<VectorRepository>> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/vectorrepositories";
            return await _Sdk.RetrieveMany<VectorRepository>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<VectorRepository> Update(VectorRepository repo, CancellationToken token = default)
        {
            if (repo == null) throw new ArgumentNullException(nameof(repo));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/vectorrepositories/" + repo.GUID;
            return await _Sdk.Update<VectorRepository>(url, repo, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/vectorrepositories/" + guid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<EnumerationResult<VectorRepository>> Enumerate(int maxKeys = 5, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v2.0/tenants/" + _Sdk.TenantGUID + "/vectorrepositories?max-keys=" + maxKeys + "&token=" + _Sdk.TenantGUID;
            return await _Sdk.Enumerate<VectorRepository>(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}