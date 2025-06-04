namespace View.Sdk.Configuration.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Configuration.Interfaces;

    /// <summary>
    /// Data repository methods
    /// </summary>
    public class DataRepositoryMethods : IDataRepositoryMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Data repository methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public DataRepositoryMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<DataRepository> Create(DataRepository repository, CancellationToken token = default)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/datarepositories";
            return await _Sdk.Create<DataRepository>(url, repository, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<DataRepository> Retrieve(string repositoryGuid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/datarepositories/" + repositoryGuid;
            return await _Sdk.Retrieve<DataRepository>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<DataRepository>> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/datarepositories";
            return await _Sdk.RetrieveMany<DataRepository>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<DataRepository> Update(DataRepository repository, CancellationToken token = default)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/datarepositories/" + repository.GUID;
            return await _Sdk.Update<DataRepository>(url, repository, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(string repositoryGuid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(repositoryGuid)) throw new ArgumentNullException(nameof(repositoryGuid));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/datarepositories/" + repositoryGuid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<EnumerationResult<DataRepository>> Enumerate(int maxKeys = 5, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v2.0/tenants/" + _Sdk.TenantGUID + "/datarepositories?max-keys=" + maxKeys + "&token=" + _Sdk.TenantGUID;
            return await _Sdk.Enumerate<DataRepository>(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}