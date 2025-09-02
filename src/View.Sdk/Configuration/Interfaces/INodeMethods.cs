namespace View.Sdk.Configuration.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for node methods.
    /// </summary>
    public interface INodeMethods
    {

        /// <summary>
        /// Check if a node exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read a node.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Node.</returns>
        public Task<Node> Retrieve(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read nodes.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Nodes.</returns>
        public Task<List<Node>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Update a node.
        /// </summary>
        /// <param name="node">Node.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Node.</returns>
        public Task<Node> Update(Node node, CancellationToken token = default);

        /// <summary>
        /// Delete a node.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Enumerate nodes with pagination support.
        /// </summary>
        /// <param name="maxKeys">Maximum number of keys to return in the response. Default is 5.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>EnumerationResult containing nodes.</returns>
        public Task<EnumerationResult<Node>> Enumerate(int maxKeys = 5, CancellationToken token = default);
    }
}
