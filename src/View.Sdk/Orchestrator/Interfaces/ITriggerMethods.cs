namespace View.Sdk.Orchestrator.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for trigger methods.
    /// </summary>
    public interface ITriggerMethods
    {
        /// <summary>
        /// Create a trigger.
        /// </summary>
        /// <param name="trigger">Trigger.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Trigger.</returns>
        public Task<Trigger> Create(Trigger trigger, CancellationToken token = default);

        /// <summary>
        /// Check if a trigger exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read a trigger.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Trigger.</returns>
        public Task<Trigger> Retrieve(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read triggers.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Triggers.</returns>
        public Task<List<Trigger>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Update a trigger.
        /// </summary>
        /// <param name="trigger">Trigger.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Trigger.</returns>
        public Task<Trigger> Update(Trigger trigger, CancellationToken token = default);

        /// <summary>
        /// Delete a trigger.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(Guid guid, CancellationToken token = default);
    }
}