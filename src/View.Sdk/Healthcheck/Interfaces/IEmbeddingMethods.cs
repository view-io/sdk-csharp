namespace View.Sdk.Healthcheck.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for embedding healthcheck methods.
    /// </summary>
    public interface IEmbeddingMethods
    {
        /// <summary>
        /// Check if embedding service exists and is accessible.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists and accessible.</returns>
        Task<bool> Exists(CancellationToken token = default);
    }
}