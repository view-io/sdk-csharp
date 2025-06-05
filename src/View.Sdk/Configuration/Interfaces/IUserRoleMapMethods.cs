namespace View.Sdk.Configuration.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for user role map methods.
    /// </summary>
    public interface IUserRoleMapMethods
    {
        /// <summary>
        /// Create a user role map.
        /// </summary>
        /// <param name="userRoleMap">User role map.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>User role map.</returns>
        public Task<UserRoleMap> Create(UserRoleMap userRoleMap, CancellationToken token = default);

        /// <summary>
        /// Check if a user role map exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read a user role map.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>User role map.</returns>
        public Task<UserRoleMap> Retrieve(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read user role maps.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>User role maps.</returns>
        public Task<List<UserRoleMap>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Update a user role map.
        /// </summary>
        /// <param name="userRoleMap">User role map.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>User role map.</returns>
        public Task<UserRoleMap> Update(UserRoleMap userRoleMap, CancellationToken token = default);

        /// <summary>
        /// Delete a user role map.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Enumerate user role maps.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumeration result containing user role maps.</returns>
        public Task<EnumerationResult<UserRoleMap>> Enumerate(CancellationToken token = default);
    }
}