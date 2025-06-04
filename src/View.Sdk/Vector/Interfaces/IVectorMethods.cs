namespace View.Sdk.Vector.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Embeddings;

    /// <summary>
    /// Interface for search and enumerate methods.
    /// </summary>
    public interface IVectorMethods
    {
        /// <summary>
        /// Vector query.
        /// </summary>
        /// <param name="queryReq">Query request.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        Task<string> Query(
            VectorQueryRequest queryReq,
            CancellationToken token = default);

        /// <summary>
        /// Vector search.
        /// </summary>
        /// <param name="searchReq">Search request.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of vector chunks.</returns>
        Task<List<VectorChunk>> Search(
            VectorSearchRequest searchReq,
            CancellationToken token = default);

        /// <summary>
        /// Find existing embeddings.
        /// </summary>
        /// <param name="findReq">Find embeddings request.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Find embeddings result.</returns>
        Task<FindEmbeddingsResult> Find(
            FindEmbeddingsRequest findReq,
            CancellationToken token = default);
    }
}