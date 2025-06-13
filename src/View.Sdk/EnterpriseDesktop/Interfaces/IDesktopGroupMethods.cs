namespace View.Sdk.EnterpriseDesktop.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for desktop group methods.
    /// </summary>
    public interface IDesktopGroupMethods
    {
        /// <summary>
        /// Retrieve many desktop groups.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of desktop groups.</returns>
        public Task<List<DesktopGroup>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Retrieve a desktop group.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Desktop group.</returns>
        public Task<DesktopGroup> Retrieve(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Check if a desktop group exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Create a desktop group.
        /// </summary>
        /// <param name="group">Desktop group.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Created desktop group.</returns>
        public Task<DesktopGroup> Create(DesktopGroup group, CancellationToken token = default);

        /// <summary>
        /// Update a desktop group.
        /// </summary>
        /// <param name="group">Desktop group.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Updated desktop group.</returns>
        public Task<DesktopGroup> Update(DesktopGroup group, CancellationToken token = default);

        /// <summary>
        /// Delete a desktop group.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>>True if deleted.</returns>
        public Task<bool> Delete(Guid guid, CancellationToken token = default);
    }
}