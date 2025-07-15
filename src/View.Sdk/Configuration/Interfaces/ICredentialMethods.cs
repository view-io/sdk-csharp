namespace View.Sdk.Configuration.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for credential methods.
    /// </summary>
    public interface ICredentialMethods
    {
        /// <summary>
        /// Create a credential.
        /// </summary>
        /// <param name="cred">Credential.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Credential.</returns>
        public Task<Credential> Create(Credential cred, CancellationToken token = default);

        /// <summary>
        /// Check if a credential exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read a credential.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Credential.</returns>
        public Task<Credential> Retrieve(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read nodes.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Nodes.</returns>
        public Task<List<Credential>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Update a credential.
        /// </summary>
        /// <param name="cred">Credential.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Credential.</returns>
        public Task<Credential> Update(Credential cred, CancellationToken token = default);

        /// <summary>
        /// Delete a credential.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Enumerate credentials with pagination support.
        /// </summary>
        /// <param name="maxKeys">Maximum number of keys to return.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumeration result containing credentials.</returns>
        public Task<EnumerationResult<Credential>> Enumerate(int maxKeys = 5, CancellationToken token = default);
    }
}
