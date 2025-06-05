namespace View.Sdk.Configuration.Implementations
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Configuration.Interfaces;

    /// <summary>
    /// Deployment type methods
    /// </summary>
    public class DeploymentTypeMethods : IDeploymentTypeMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Deployment type methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public DeploymentTypeMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<DeploymentType> Retrieve(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "deployment";
            return await _Sdk.Retrieve<DeploymentType>(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}