namespace View.Sdk.Vector.Implementations
{
    using RestWrapper;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk;
    using View.Sdk.Embeddings;
    using View.Sdk.Vector.Interfaces;

    /// <summary>
    /// Vector search and enumerate methods
    /// </summary>
    public class VectorMethods : IVectorMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Search and enumerate methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public VectorMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<string> Query(
            VectorQueryRequest queryReq,
            CancellationToken token = default)
        {
            if (queryReq == null) throw new ArgumentNullException(nameof(queryReq));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/vectorrepositories/" + queryReq.VectorRepositoryGUID + "/query";
            return await _Sdk.Post<VectorQueryRequest, string>(url, queryReq, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<VectorChunk>> Search(
            VectorSearchRequest searchReq,
            CancellationToken token = default)
        {
            if (searchReq == null) throw new ArgumentNullException(nameof(searchReq));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/vectorrepositories/" + searchReq.VectorRepositoryGUID + "/search";
            return await _Sdk.Post<VectorSearchRequest, List<VectorChunk>>(url, searchReq, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<FindEmbeddingsResult> Find(
            FindEmbeddingsRequest findReq,
            CancellationToken token = default)
        {
            if (findReq == null) throw new ArgumentNullException(nameof(findReq));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/vectorrepositories/" + findReq.VectorRepositoryGUID + "/find";
            return await _Sdk.Post<FindEmbeddingsRequest, FindEmbeddingsResult>(url, findReq, token).ConfigureAwait(false);
        }
        #endregion

        #region Private-Methods

        #endregion
    }
}