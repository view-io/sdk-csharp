namespace View.Sdk.Healthcheck.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for lexi healthcheck methods.
    /// </summary>
    public interface ILexiMethods
    {
        /// <summary>
        /// Check if lexi service exists and is accessible.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists and accessible.</returns>
        Task<bool> Exists(CancellationToken token = default);
    }
}