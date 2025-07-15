namespace View.Sdk.Configuration.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for deployment type methods.
    /// </summary>
    public interface IDeploymentTypeMethods
    {
        /// <summary>
        /// Retrieve deployment type information.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Deployment type information.</returns>
        public Task<DeploymentType> Retrieve(CancellationToken token = default);
    }
}