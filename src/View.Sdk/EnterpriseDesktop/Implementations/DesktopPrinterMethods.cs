namespace View.Sdk.EnterpriseDesktop.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.EnterpriseDesktop.Interfaces;

    /// <summary>
    /// Desktop printer methods implementation.
    /// </summary>
    public class DesktopPrinterMethods : IDesktopPrinterMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Desktop printer methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public DesktopPrinterMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<List<DesktopPrinter>> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/enterprisedesktop/printers";
            return await _Sdk.RetrieveMany<DesktopPrinter>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<DesktopPrinter> Retrieve(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/enterprisedesktop/printers/" + guid.ToString();
            return await _Sdk.Retrieve<DesktopPrinter>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Exists(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/enterprisedesktop/printers/" + guid.ToString();
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<DesktopPrinter> Create(DesktopPrinter printer, CancellationToken token = default)
        {
            if (printer == null) throw new ArgumentNullException(nameof(printer));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/enterprisedesktop/printers";
            return await _Sdk.Post<DesktopPrinter>(url, printer, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<DesktopPrinter> Update(DesktopPrinter printer, CancellationToken token = default)
        {
            if (printer == null) throw new ArgumentNullException(nameof(printer));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/enterprisedesktop/printers/" + printer.GUID;
            return await _Sdk.Update<DesktopPrinter>(url, printer, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/enterprisedesktop/printers/" + guid.ToString();
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}