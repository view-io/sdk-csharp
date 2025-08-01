namespace View.Sdk.Healthcheck.Implementations
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk;
    using View.Sdk.Healthcheck.Interfaces;

    /// <summary>
    /// Embedding healthcheck methods.
    /// </summary>
    public class EmbeddingMethods : IEmbeddingMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Embedding healthcheck methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public EmbeddingMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<bool> Exists(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "healthcheck/embedding";
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}