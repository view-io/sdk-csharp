namespace View.Sdk.Configuration.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Methods for working with roles.
    /// </summary>
    public interface IRoleMethods
    {
        /// <summary>
        /// Create a role.
        /// </summary>
        /// <param name="role">Role.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Role.</returns>
        Task<Role> Create(Role role, CancellationToken token = default);

        /// <summary>
        /// Retrieve a role.
        /// </summary>
        /// <param name="roleGuid">Role GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Role.</returns>
        Task<Role> Retrieve(string roleGuid, CancellationToken token = default);

        /// <summary>
        /// Retrieve multiple roles.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of roles.</returns>
        Task<List<Role>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Update a role.
        /// </summary>
        /// <param name="role">Role.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Role.</returns>
        Task<Role> Update(Role role, CancellationToken token = default);

        /// <summary>
        /// Delete a role.
        /// </summary>
        /// <param name="roleGuid">Role GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Boolean indicating success.</returns>
        Task<bool> Delete(string roleGuid, CancellationToken token = default);

        /// <summary>
        /// Check if a role exists.
        /// </summary>
        /// <param name="roleGuid">Role GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Boolean indicating if the role exists.</returns>
        Task<bool> Exists(string roleGuid, CancellationToken token = default);

        /// <summary>
        /// Enumerate roles.
        /// </summary>
        /// <param name="maxKeys">Maximum number of keys to return.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumeration result.</returns>
        Task<EnumerationResult<Role>> Enumerate(int maxKeys = 5, CancellationToken token = default);
    }
}