namespace View.Sdk.Configuration.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using RestWrapper;
    using View.Sdk.Configuration.Interfaces;

    /// <summary>
    /// Authentication methods
    /// </summary>
    public class AuthenticationMethods : IAuthenticationMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Authentication methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public AuthenticationMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<List<TenantMetadata>> RetrieveTenants(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/token/tenants";
            return await _Sdk.Retrieve<List<TenantMetadata>>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<AuthenticationToken> GenerateTokenWithPassword(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/token";
            return await _Sdk.Retrieve<AuthenticationToken>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<AuthenticationToken> GenerateTokenWithPasswordSha256(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/token";
            return await _Sdk.Retrieve<AuthenticationToken>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<AuthenticationToken> GenerateAdminTokenWithPassword(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/token";
            return await _Sdk.Retrieve<AuthenticationToken>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<AuthenticationToken> GenerateAdminTokenWithPasswordSha256(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/token";
            return await _Sdk.Retrieve<AuthenticationToken>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<AuthenticationToken> ValidateToken(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/token/validate";
            return await _Sdk.Retrieve<AuthenticationToken>(url, token).ConfigureAwait(false);  
        }

        /// <inheritdoc />
        public async Task<AuthenticationToken> RetrieveTokenDetails(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/token/details";
            return await _Sdk.Retrieve<AuthenticationToken>(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}