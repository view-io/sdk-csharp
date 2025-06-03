namespace View.Sdk.Orchestrator.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for data flow log methods.
    /// </summary>
    public interface IDataFlowLogMethods
    {
        /// <summary>
        /// Read data flow logs.
        /// </summary>
        /// <param name="dataFlowGuid">Data flow GUID.</param>
        /// <param name="requestGuid">Request GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of DataFlowLog.</returns>
        public Task<List<DataFlowLog>> RetrieveLogs(Guid dataFlowGuid, Guid requestGuid, CancellationToken token = default);

        /// <summary>
        /// Read data flow logfile.
        /// </summary>
        /// <param name="dataFlowGuid">Data flow GUID.</param>
        /// <param name="requestGuid">Request GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Log file.</returns>
        public Task<string> RetrieveLogfile(Guid dataFlowGuid, Guid requestGuid, CancellationToken token = default);
    }
}