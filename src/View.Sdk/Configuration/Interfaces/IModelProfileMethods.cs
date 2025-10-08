namespace View.Sdk.Configuration.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for model profile methods.
    /// </summary>
    public interface IModelProfileMethods
    {
        /// <summary>
        /// Enumerate model profiles.
        /// </summary>
        /// <param name="maxKeys">Maximum number of keys to retrieve.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumeration result containing model profiles.</returns>
        public Task<EnumerationResult<ModelProfile>> Enumerate(int maxKeys = 5, CancellationToken token = default);

        /// <summary>
        /// Retrieve many model profiles.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of model profiles.</returns>
        public Task<List<ModelProfile>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Retrieve a model profile.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Model profile.</returns>
        public Task<ModelProfile> Retrieve(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Create a model profile.
        /// </summary>
        /// <param name="modelProfile">Model profile.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Created model profile.</returns>
        public Task<ModelProfile> Create(ModelProfile modelProfile, CancellationToken token = default);

        /// <summary>
        /// Update a model profile.
        /// </summary>
        /// <param name="modelProfile">Model profile.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Updated model profile.</returns>
        public Task<ModelProfile> Update(ModelProfile modelProfile, CancellationToken token = default);

        /// <summary>
        /// Check if a model profile exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Delete a model profile.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(Guid guid, CancellationToken token = default);
    }
}
