namespace View.Sdk.Orchestrator.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for data flow methods.
    /// </summary>
    public interface IDataFlowMethods
    {
        /// <summary>
        /// Create a data flow.
        /// </summary>
        /// <param name="flow">DataFlow.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>DataFlow.</returns>
        public Task<DataFlow> Create(DataFlow flow, CancellationToken token = default);

        /// <summary>
        /// Check if a data flow exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read a data flow.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>DataFlow.</returns>
        public Task<DataFlow> Retrieve(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read data flows.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>DataFlows.</returns>
        public Task<List<DataFlow>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Delete a data flow.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(Guid guid, CancellationToken token = default);
    }
}