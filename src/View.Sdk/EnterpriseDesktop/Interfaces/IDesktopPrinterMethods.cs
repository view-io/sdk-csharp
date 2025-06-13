namespace View.Sdk.EnterpriseDesktop.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for desktop printer methods.
    /// </summary>
    public interface IDesktopPrinterMethods
    {
        /// <summary>
        /// Retrieve many desktop printers.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of desktop printers.</returns>
        public Task<List<DesktopPrinter>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Retrieve a desktop printer.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Desktop printer.</returns>
        public Task<DesktopPrinter> Retrieve(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Check if a desktop printer exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Create a desktop printer.
        /// </summary>
        /// <param name="printer">Desktop printer.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Created desktop printer.</returns>
        public Task<DesktopPrinter> Create(DesktopPrinter printer, CancellationToken token = default);

        /// <summary>
        /// Update a desktop printer.
        /// </summary>
        /// <param name="printer">Desktop printer.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Updated desktop printer.</returns>
        public Task<DesktopPrinter> Update(DesktopPrinter printer, CancellationToken token = default);

        /// <summary>
        /// Delete a desktop printer.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>>True if deleted.</returns>
        public Task<bool> Delete(Guid guid, CancellationToken token = default);
    }
}