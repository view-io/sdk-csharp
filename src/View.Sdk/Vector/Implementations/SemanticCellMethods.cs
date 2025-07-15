namespace View.Sdk.Vector.Implementations
{
    using RestWrapper;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk;
    using View.Sdk.Semantic;
    using View.Sdk.Vector.Interfaces;

    /// <summary>
    /// Semantic cell methods
    /// </summary>
    public class SemanticCellMethods : ISemanticCellMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Semantic cell methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public SemanticCellMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<List<SemanticCell>> ReadMany(
            Guid repoGuid,
            Guid docGuid,
            CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/vectorrepositories/" + repoGuid + "/documents/" + docGuid + "/cells";
            return await _Sdk.RetrieveMany<SemanticCell>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<SemanticCell> Read(
            Guid repoGuid,
            Guid docGuid,
            Guid cellGuid,
            CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/vectorrepositories/" + repoGuid + "/documents/" + docGuid + "/cells/" + cellGuid;
            return await _Sdk.Retrieve<SemanticCell>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Exists(
            Guid repoGuid,
            Guid docGuid,
            Guid cellGuid,
            CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/vectorrepositories/" + repoGuid + "/documents/" + docGuid + "/cells/" + cellGuid;
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}