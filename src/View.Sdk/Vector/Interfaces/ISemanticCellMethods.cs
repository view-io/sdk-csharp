namespace View.Sdk.Vector.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Semantic;

    /// <summary>
    /// Interface for semantic cell methods.
    /// </summary>
    public interface ISemanticCellMethods
    {
        /// <summary>
        /// Read semantic cells for a given document.
        /// </summary>
        /// <param name="repoGuid">Vector repository GUID.</param>
        /// <param name="docGuid">Document GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Semantic cells.</returns>
        Task<List<SemanticCell>> ReadMany(
            Guid repoGuid,
            Guid docGuid,
            CancellationToken token = default);

        /// <summary>
        /// Read a specific semantic cell for a given document.
        /// </summary>
        /// <param name="repoGuid">Vector repository GUID.</param>
        /// <param name="docGuid">Document GUID.</param>
        /// <param name="cellGuid">Cell GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Semantic cell.</returns>
        Task<SemanticCell> Read(
            Guid repoGuid,
            Guid docGuid,
            Guid cellGuid,
            CancellationToken token = default);
    }
}