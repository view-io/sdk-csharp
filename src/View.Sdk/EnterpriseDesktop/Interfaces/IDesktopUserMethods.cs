namespace View.Sdk.EnterpriseDesktop.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for desktop user methods.
    /// </summary>
    public interface IDesktopUserMethods
    {
        /// <summary>
        /// Retrieve many desktop users.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of desktop users.</returns>
        public Task<List<DesktopUser>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Retrieve a desktop user.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Desktop user.</returns>
        public Task<DesktopUser> Retrieve(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Check if a desktop user exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Create a desktop user.
        /// </summary>
        /// <param name="user">Desktop user.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Created desktop user.</returns>
        public Task<DesktopUser> Create(DesktopUser user, CancellationToken token = default);

        /// <summary>
        /// Update a desktop user.
        /// </summary>
        /// <param name="user">Desktop user.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Updated desktop user.</returns>
        public Task<DesktopUser> Update(DesktopUser user, CancellationToken token = default);

        /// <summary>
        /// Delete a desktop user.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>>True if deleted.</returns>
        public Task<bool> Delete(Guid guid, CancellationToken token = default);
    }
}