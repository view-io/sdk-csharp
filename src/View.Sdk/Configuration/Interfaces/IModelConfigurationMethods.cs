namespace View.Sdk.Configuration.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for model configuration methods.
    /// </summary>
    public interface IModelConfigurationMethods
    {
        /// <summary>
        /// Enumerate model configurations.
        /// </summary>
        /// <param name="maxKeys">Maximum number of keys to retrieve.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumeration result containing model configurations.</returns>
        public Task<EnumerationResult<ModelConfiguration>> Enumerate(int maxKeys = 5, CancellationToken token = default);

        /// <summary>
        /// Retrieve many model configurations.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of model configurations.</returns>
        public Task<List<ModelConfiguration>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Retrieve a model configuration.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Model configuration.</returns>
        public Task<ModelConfiguration> Retrieve(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Create a model configuration.
        /// </summary>
        /// <param name="modelConfiguration">Model configuration.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Created model configuration.</returns>
        public Task<ModelConfiguration> Create(ModelConfiguration modelConfiguration, CancellationToken token = default);

        /// <summary>
        /// Update a model configuration.
        /// </summary>
        /// <param name="modelConfiguration">Model configuration.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Updated model configuration.</returns>
        public Task<ModelConfiguration> Update(ModelConfiguration modelConfiguration, CancellationToken token = default);

        /// <summary>
        /// Check if a model configuration exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Delete a model configuration.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(Guid guid, CancellationToken token = default);
    }
}
