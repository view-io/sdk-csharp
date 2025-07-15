namespace View.Sdk.Vector.Implementations
{
    using RestWrapper;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk;
    using View.Sdk.Embeddings;
    using View.Sdk.Vector.Interfaces;

    /// <summary>
    /// Repository methods
    /// </summary>
    public class RepositoryMethods : IRepositoryMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Repository methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public RepositoryMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<EnumerationResult<EmbeddingsDocument>> EnumerateMany(
            EnumerationQuery query,
            CancellationToken token = default)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/vectorrepositories/" + query.VectorRepositoryGUID + "/enumerate";
            return await _Sdk.Post<EnumerationQuery, EnumerationResult<EmbeddingsDocument>>(url, query, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Truncate(
            Guid repoGuid,
            CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/vectorrepositories/" + repoGuid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<VectorRepositoryStatistics> GetStatistics(
            Guid repoGuid,
            CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/vectorrepositories/" + repoGuid + "/stats";
            return await _Sdk.Retrieve<VectorRepositoryStatistics>(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}