namespace View.Sdk.Configuration.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Authentication methods interface
    /// </summary>
    public interface IAuthenticationMethods
    {
        /// <summary>
        /// Retrieve tenants.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of tenant metadata.</returns>
        Task<List<TenantMetadata>> RetrieveTenants(CancellationToken token = default);

        /// <summary>
        /// Generate authentication token using password.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Authentication token.</returns>
        Task<AuthenticationToken> GenerateTokenWithPassword(CancellationToken token = default);

        /// <summary>
        /// Generate authentication token using password SHA-256.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Authentication token.</returns>
        Task<AuthenticationToken> GenerateTokenWithPasswordSha256(CancellationToken token = default);

        /// <summary>
        /// Generate administrator token using password.
        /// </summary>
        /// <returns>Authentication token.</returns>
        Task<AuthenticationToken> GenerateAdminTokenWithPassword(CancellationToken token = default);

        /// <summary>
        /// Generate administrator token using password SHA-256.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Authentication token.</returns>
        Task<AuthenticationToken> GenerateAdminTokenWithPasswordSha256(CancellationToken token = default);

        /// <summary>
        /// Validate authentication token.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Authentication token with validity information.</returns>
        Task<AuthenticationToken> ValidateToken(CancellationToken token = default);

        /// <summary>
        /// Retrieve token details.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Authentication token with details.</returns>
        Task<AuthenticationToken> RetrieveTokenDetails(CancellationToken token = default);
    }
}