namespace View.Sdk.Configuration.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Methods for working with blobs.
    /// </summary>
    public interface IBlobMethods
    {
        /// <summary>
        /// Create a blob.
        /// </summary>
        /// <param name="blob">Blob.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Blob.</returns>
        Task<Blob> Create(Blob blob, CancellationToken token = default);

        /// <summary>
        /// Retrieve a blob.
        /// </summary>
        /// <param name="blobGuid">Blob GUID.</param>
        /// <param name="inclData">Include blob data in response.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Blob.</returns>
        Task<Blob> Retrieve(string blobGuid, bool inclData = false, CancellationToken token = default);

        /// <summary>
        /// Read a public blob.
        /// </summary>
        /// <param name="blobGuid">Blob GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Blob.</returns>
        Task<Blob> Read(string blobGuid, CancellationToken token = default);

        /// <summary>
        /// Retrieve multiple blobs.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of blobs.</returns>
        Task<List<Blob>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Update a blob.
        /// </summary>
        /// <param name="blob">Blob.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Blob.</returns>
        Task<Blob> Update(Blob blob, CancellationToken token = default);

        /// <summary>
        /// Delete a blob.
        /// </summary>
        /// <param name="blobGuid">Blob GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Boolean indicating success.</returns>
        Task<bool> Delete(string blobGuid, CancellationToken token = default);

        /// <summary>
        /// Enumerate blobs.
        /// </summary>
        /// <param name="maxKeys">Maximum number of keys to return.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumeration result.</returns>
        Task<EnumerationResult<Blob>> Enumerate(int maxKeys = 5, CancellationToken token = default);

        /// <summary>
        /// Check if a blob exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid guid, CancellationToken token = default);
    }
}
