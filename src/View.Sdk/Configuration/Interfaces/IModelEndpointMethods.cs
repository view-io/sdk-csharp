namespace View.Sdk.Configuration.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for model endpoint methods.
    /// </summary>
    public interface IModelEndpointMethods
    {
        /// <summary>
        /// Enumerate model endpoints.
        /// </summary>
        /// <param name="maxKeys">Maximum number of keys to retrieve.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumeration result containing model endpoints.</returns>
        public Task<EnumerationResult<ModelEndpoint>> Enumerate(int maxKeys = 5, CancellationToken token = default);

        /// <summary>
        /// Retrieve many model endpoints.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of model endpoints.</returns>
        public Task<List<ModelEndpoint>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Retrieve a model endpoint.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Model endpoint.</returns>
        public Task<ModelEndpoint> Retrieve(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Create a model endpoint.
        /// </summary>
        /// <param name="modelEndpoint">Model endpoint.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Created model endpoint.</returns>
        public Task<ModelEndpoint> Create(ModelEndpoint modelEndpoint, CancellationToken token = default);

        /// <summary>
        /// Update a model endpoint.
        /// </summary>
        /// <param name="modelEndpoint">Model endpoint.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Updated model endpoint.</returns>
        public Task<ModelEndpoint> Update(ModelEndpoint modelEndpoint, CancellationToken token = default);

        /// <summary>
        /// Check if a model endpoint exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Delete a model endpoint.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(Guid guid, CancellationToken token = default);
    }
}
