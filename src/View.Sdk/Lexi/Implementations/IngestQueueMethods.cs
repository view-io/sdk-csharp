namespace View.Sdk.Lexi.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Lexi.Interfaces;

    /// <summary>
    /// Ingest queue methods
    /// </summary>
    public class IngestQueueMethods : IIngestQueueMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Ingest queue methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public IngestQueueMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<bool> Exists(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/ingestqueue/" + guid;
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<IngestionQueueEntry> Retrieve(Guid guid, bool includeStats = false, CancellationToken token = default)
        {
            string url;
            if (includeStats)
                url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/ingestqueue?stats";
            else
                url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/ingestqueue/" + guid;

            return await _Sdk.Retrieve<IngestionQueueEntry>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<IngestionQueueEntry>> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/ingestqueue";
            return await _Sdk.RetrieveMany<IngestionQueueEntry>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid guid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/ingestqueue/" + guid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}