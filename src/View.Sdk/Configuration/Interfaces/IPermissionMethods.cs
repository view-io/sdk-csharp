namespace View.Sdk.Configuration.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for permission methods.
    /// </summary>
    public interface IPermissionMethods
    {
        /// <summary>
        /// Create a permission.
        /// </summary>
        /// <param name="permission">Permission.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Permission.</returns>
        public Task<Permission> Create(Permission permission, CancellationToken token = default);

        /// <summary>
        /// Check if a permission exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read a permission.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Permission.</returns>
        public Task<Permission> Retrieve(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read permissions.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Permissions.</returns>
        public Task<List<Permission>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Update a permission.
        /// </summary>
        /// <param name="permission">Permission.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Permission.</returns>
        public Task<Permission> Update(Permission permission, CancellationToken token = default);

        /// <summary>
        /// Delete a permission.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Enumerate permissions with pagination support.
        /// </summary>
        /// <param name="maxKeys">Maximum number of keys to return in a single operation.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumeration result containing permissions.</returns>
        public Task<EnumerationResult<Permission>> Enumerate(int maxKeys = 5, CancellationToken token = default);
    }
}