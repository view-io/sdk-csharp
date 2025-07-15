namespace View.Sdk.Configuration.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Authentication methods interface.
    /// </summary>
    public interface IAuthenticationMethods
    {
        /// <summary>
        /// Retrieve tenants.
        /// </summary>
        /// <remarks>
        /// Required headers:
        /// - <c>x-email</c>: email address.
        /// </remarks>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of tenant metadata.</returns>
        Task<List<TenantMetadata>> RetrieveTenants(CancellationToken token = default);

        /// <summary>
        /// Generate authentication token using password.
        /// </summary>
        /// <remarks>
        /// Required headers:
        /// - <c>x-email</c>: User's email address.
        /// - <c>x-password</c>: Plain-text password.
        /// </remarks>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Authentication token.</returns>
        Task<AuthenticationToken> GenerateTokenWithPassword(CancellationToken token = default);

        /// <summary>
        /// Generate authentication token using SHA-256 password hash.
        /// </summary>
        /// <remarks>
        /// Required headers:
        /// - <c>x-email</c>: User's email address.
        /// - <c>x-password-sha256</c>: SHA-256 hash of the password.
        /// - <c>x-tenant-guid</c>: Tenant GUID.
        /// </remarks>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Authentication token.</returns>
        Task<AuthenticationToken> GenerateTokenWithPasswordSha256(CancellationToken token = default);

        /// <summary>
        /// Generate administrator authentication token using password.
        /// </summary>
        /// <remarks>
        /// Required headers:
        /// - <c>x-email</c>: Administrator's email address.
        /// - <c>x-password</c>: Plain-text password.
        /// </remarks>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Authentication token.</returns>
        Task<AuthenticationToken> GenerateAdminTokenWithPassword(CancellationToken token = default);

        /// <summary>
        /// Generate administrator authentication token using SHA-256 password hash.
        /// </summary>
        /// <remarks>
        /// Required headers:
        /// - <c>x-email</c>: Administrator's email address.
        /// - <c>x-password-sha256</c>: SHA-256 hash of the password.
        /// </remarks>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Authentication token.</returns>
        Task<AuthenticationToken> GenerateAdminTokenWithPasswordSha256(CancellationToken token = default);

        /// <summary>
        /// Validate an existing authentication token.
        /// </summary>
        /// <remarks>
        /// Required headers:
        /// - <c>x-token</c>: The authentication token to validate.
        /// </remarks>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Authentication token with validity information.</returns>
        Task<AuthenticationToken> ValidateToken(CancellationToken token = default);

        /// <summary>
        /// Retrieve authentication token details.
        /// </summary>
        /// <remarks>
        /// Required headers:
        /// - <c>x-token</c>: The authentication token.
        /// </remarks>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Authentication token with details.</returns>
        Task<AuthenticationToken> RetrieveTokenDetails(CancellationToken token = default);
    }
}
