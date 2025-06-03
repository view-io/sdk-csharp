namespace View.Sdk.Orchestrator.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for step methods.
    /// </summary>
    public interface IStepMethods
    {
        /// <summary>
        /// Create a step.
        /// </summary>
        /// <param name="step">Step.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Step.</returns>
        public Task<StepMetadata> Create(StepMetadata step, CancellationToken token = default);

        /// <summary>
        /// Check if a step exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read a step.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Step.</returns>
        public Task<StepMetadata> Retrieve(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read steps.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Steps.</returns>
        public Task<List<StepMetadata>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Delete a step.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(Guid guid, CancellationToken token = default);
    }
}