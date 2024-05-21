namespace View.Sdk.DocumentProcessor
{
    using RestWrapper;
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Serializer;
    using View.Sdk.Shared.Udr;

    /// <summary>
    /// View document processor SDK.
    /// </summary>
    public class ViewDocumentProcessorSdk
    {
        #region Public-Members

        /// <summary>
        /// Method to invoke to send log messages.
        /// </summary>
        public Action<string> Logger { get; set; } = null;
         
        #endregion

        #region Private-Members

        private string _Header = "[ViewDocumentProcessorSdk] ";
        private Uri _Uri = null;
        private string _Endpoint = "http://localhost:8321/";
        private SerializationHelper _Serializer = new SerializationHelper();
        
        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="endpoint">Endpoint URL of the form http://localhost:8321/.</param>
        public ViewDocumentProcessorSdk(string endpoint = "http://localhost:8321/")
        {
            if (string.IsNullOrEmpty(endpoint)) throw new ArgumentNullException(nameof(endpoint));

            _Uri = new Uri(endpoint);
            _Endpoint = _Uri.ToString();
            if (!_Endpoint.EndsWith("/")) _Endpoint += "/";
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// Validate connectivity.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Boolean indicating success.</returns>
        public async Task<bool> ValidateConnectivity(CancellationToken token = default)
        {
            string url = _Endpoint;

            using (RestRequest req = new RestRequest(url))
            {
                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        Log("success reported from " + url);
                        return true;
                    }
                    else if (resp != null)
                    {
                        Log("non-success reported from " + url + ": " + resp.StatusCode);
                        return false;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Process document.
        /// </summary>
        /// <param name="doc">Document request.</param>
        /// <param name="filename">Filename containing data.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Document response.</returns>
        public async Task<UdrDocument> ProcessDocument(UdrDocumentRequest doc, string filename = null, CancellationToken token = default)
        {
            if (doc == null) throw new ArgumentNullException(nameof(doc));

            if (String.IsNullOrEmpty(filename) && (doc.Data == null || doc.Data.Length < 1))
                throw new ArgumentException("Either a filename must be supplied or the document request 'Data' property must be populated.");

            if (!String.IsNullOrEmpty(filename))
                doc.Data = await File.ReadAllBytesAsync(filename, token).ConfigureAwait(false);

            string url = _Endpoint + "v1.0/document";

            using (RestRequest req = new RestRequest(url, HttpMethod.Put, Constants.JsonContentType))
            {
                using (RestResponse resp = await req.SendAsync(_Serializer.SerializeJson(doc, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode == 200)
                        {
                            Log("success reported from " + url + ": " + resp.ContentLength + " bytes");
                            UdrDocument docResp = _Serializer.DeserializeJson<UdrDocument>(resp.DataAsString);
                            return docResp;
                        }
                        else
                        {
                            Log("non-success reported from " + url + ": " + resp.StatusCode);
                            UdrDocument docResp = _Serializer.DeserializeJson<UdrDocument>(resp.DataAsString);
                            return docResp;
                        }
                    }
                    else
                    {
                        Log("no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Process data table.
        /// </summary>
        /// <param name="dt">Data table request.</param>
        /// <param name="filename">Filename containing data.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Document response.</returns>
        public async Task<UdrDocument> ProcessDataTable(DataTableRequest dt, string filename = null, CancellationToken token = default)
        {
            if (dt == null) throw new ArgumentNullException(nameof(dt));
            if (String.IsNullOrEmpty(dt.DatabaseType)) throw new ArgumentNullException(nameof(dt.DatabaseType));

            if (dt.DatabaseType.Equals("Sqlite")
                && String.IsNullOrEmpty(filename))
                throw new ArgumentException("A filename must be supplied when using Sqlite.");

            if (!dt.DatabaseType.Equals("Sqlite")
                && !String.IsNullOrEmpty(filename))
                throw new ArgumentException("A filename should not be supplied when using a database type other than Sqlite.");

            if (!String.IsNullOrEmpty(filename))
                dt.SqliteFileData = await File.ReadAllBytesAsync(filename, token).ConfigureAwait(false);

            string url = _Endpoint + "v1.0/datatable";

            using (RestRequest req = new RestRequest(url, HttpMethod.Put, Constants.JsonContentType))
            {
                using (RestResponse resp = await req.SendAsync(_Serializer.SerializeJson(dt, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode == 200)
                        {
                            Log("success reported from " + url + ": " + resp.ContentLength + " bytes");
                            UdrDocument docResp = _Serializer.DeserializeJson<UdrDocument>(resp.DataAsString);
                            return docResp;
                        }
                        else
                        {
                            Log("non-success reported from " + url + ": " + resp.StatusCode);
                            UdrDocument docResp = _Serializer.DeserializeJson<UdrDocument>(resp.DataAsString);
                            return docResp;
                        }
                    }
                    else
                    {
                        Log("no response from " + url);
                        return null;
                    }
                }
            }
        }

        #endregion

        #region Private-Methods

        private void Log(string msg)
        {
            if (String.IsNullOrEmpty(msg)) return;
            Logger?.Invoke(_Header + msg);
        }

        #endregion
    }
}
