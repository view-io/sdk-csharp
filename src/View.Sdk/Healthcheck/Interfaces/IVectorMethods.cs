namespace View.Sdk.Healthcheck.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for vector healthcheck methods.
    /// </summary>
    public interface IVectorMethods
    {
        /// <summary>
        /// Check if vector service exists and is accessible.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists and accessible.</returns>
        Task<bool> Exists(CancellationToken token = default);
    }
}