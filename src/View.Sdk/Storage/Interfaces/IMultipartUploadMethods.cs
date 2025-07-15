namespace View.Sdk.Storage.Interfaces
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Storage;

    /// <summary>
    /// Defines operations for managing multipart uploads and their parts.
    /// </summary>
    public interface IMultipartUploadMethods
    {
        #region Upload-Operations

        /// <summary>
        /// Initiates a new multipart upload session for a specified object in the given bucket.
        /// </summary>
        /// <param name="bucketGuid">The unique identifier of the bucket.</param>
        /// <param name="obj">Metadata describing the multipart upload.</param>
        /// <param name="token">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation, returning metadata for the initiated multipart upload.</returns>
        Task<MultipartUploadMetadata> Create(string bucketGuid, MultipartUploadMetadata obj, CancellationToken token = default);

        /// <summary>
        /// Retrieves all ongoing multipart uploads for the specified bucket.
        /// </summary>
        /// <param name="bucketGuid">The unique identifier of the bucket.</param>
        /// <param name="token">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation, returning a list of multipart upload metadata.</returns>
        Task<List<MultipartUploadMetadata>> RetrieveMany(string bucketGuid, CancellationToken token = default);

        /// <summary>
        /// Retrieves the metadata for a specific multipart upload.
        /// </summary>
        /// <param name="bucketGuid">The unique identifier of the bucket.</param>
        /// <param name="uploadGuid">The unique identifier of the multipart upload.</param>
        /// <param name="token">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation, returning metadata for the specified multipart upload.</returns>
        Task<MultipartUploadMetadata> Retrieve(string bucketGuid, string uploadGuid, CancellationToken token = default);

        /// <summary>
        /// Cancels and deletes a multipart upload.
        /// </summary>
        /// <param name="bucketGuid">The unique identifier of the bucket.</param>
        /// <param name="uploadGuid">The unique identifier of the multipart upload.</param>
        /// <param name="token">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation, returning true if the deletion was successful.</returns>
        Task<bool> DeleteUpload(string bucketGuid, string uploadGuid, CancellationToken token = default);

        /// <summary>
        /// Completes the multipart upload by assembling previously uploaded parts.
        /// </summary>
        /// <param name="bucketGuid">The unique identifier of the bucket.</param>
        /// <param name="uploadGuid">The unique identifier of the multipart upload.</param>
        /// <param name="data">The finalization data or part list used for assembly.</param>
        /// <param name="token">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation, returning the metadata of the completed object.</returns>
        Task<ObjectMetadata> CompleteUpload(string bucketGuid, string uploadGuid, string data, CancellationToken token = default);

        #endregion

        #region Part-Operations

        /// <summary>
        /// Uploads a single part of a multipart upload.
        /// </summary>
        /// <param name="bucketGuid">The unique identifier of the bucket.</param>
        /// <param name="uploadGuid">The unique identifier of the multipart upload.</param>
        /// <param name="partNumber">The sequence number of the part being uploaded.</param>
        /// <param name="data">The string data representing the part's content.</param>
        /// <param name="token">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation, returning metadata for the uploaded part.</returns>
        Task<PartMetadata> UploadPart(string bucketGuid, string uploadGuid, int partNumber, string data, CancellationToken token = default);

        /// <summary>
        /// Retrieves the data for a specific part of a multipart upload.
        /// </summary>
        /// <param name="bucketGuid">The unique identifier of the bucket.</param>
        /// <param name="uploadGuid">The unique identifier of the multipart upload.</param>
        /// <param name="partNumber">The sequence number of the part to retrieve.</param>
        /// <param name="token">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation, returning the string data of the requested part.</returns>
        Task<string> RetrievePart(string bucketGuid, string uploadGuid, int partNumber, CancellationToken token = default);

        /// <summary>
        /// Deletes a specific part from a multipart upload.
        /// </summary>
        /// <param name="bucketGuid">The unique identifier of the bucket.</param>
        /// <param name="uploadGuid">The unique identifier of the multipart upload.</param>
        /// <param name="partNumber">The sequence number of the part to delete.</param>
        /// <param name="token">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation, returning true if the deletion was successful.</returns>
        Task<bool> DeletePart(string bucketGuid, string uploadGuid, int partNumber, CancellationToken token = default);

        #endregion
    }
}
