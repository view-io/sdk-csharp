namespace View.Sdk.Configuration.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for role permission map methods.
    /// </summary>
    public interface IRolePermissionMapMethods
    {
        /// <summary>
        /// Create a role permission map.
        /// </summary>
        /// <param name="rolePermissionMap">Role permission map.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Role permission map.</returns>
        public Task<RolePermissionMap> Create(RolePermissionMap rolePermissionMap, CancellationToken token = default);

        /// <summary>
        /// Check if a role permission map exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read a role permission map.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Role permission map.</returns>
        public Task<RolePermissionMap> Retrieve(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read role permission maps.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Role permission maps.</returns>
        public Task<List<RolePermissionMap>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Update a role permission map.
        /// </summary>
        /// <param name="rolePermissionMap">Role permission map.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Role permission map.</returns>
        public Task<RolePermissionMap> Update(RolePermissionMap rolePermissionMap, CancellationToken token = default);

        /// <summary>
        /// Delete a role permission map.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Enumerate role permission maps.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumeration result containing role permission maps.</returns>
        public Task<EnumerationResult<RolePermissionMap>> Enumerate(CancellationToken token = default);
    }
}