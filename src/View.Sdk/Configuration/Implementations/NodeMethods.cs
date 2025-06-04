namespace View.Sdk.Configuration.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using RestWrapper;
    using View.Sdk.Configuration.Interfaces;

    internal class NodeMethods : INodeMethods
    {

        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Node methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public NodeMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<Node> Create(Node node, CancellationToken token = default)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            string url = _Sdk.Endpoint + "v1.0/nodes";
            return await _Sdk.Create<Node>(url, node, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Exists(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/nodes/" + guid;
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Node> Retrieve(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/nodes/" + guid;
            return await _Sdk.Retrieve<Node>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<Node>> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/nodes";
            return await _Sdk.RetrieveMany<Node>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Node> Update(Node node, CancellationToken token = default)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            string url = _Sdk.Endpoint + "v1.0/nodes/" + node.GUID;
            return await _Sdk.Update<Node>(url, node, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/nodes/" + guid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<EnumerationResult<Node>> Enumerate(int maxKeys = 5, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v2.0/nodes?max-keys=" + maxKeys + "&token=" + _Sdk.TenantGUID;
            return await _Sdk.Enumerate<Node>(url, token).ConfigureAwait(false);      
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}
