﻿namespace View.Sdk.Processor
{
    using RestWrapper;
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Serialization;
    using View.Sdk;

    /// <summary>
    /// View document processor SDK.
    /// </summary>
    public class ViewUdrGeneratorSdk : ViewSdkBase, IDisposable
    {
        #region Public-Members

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="tenantGuid">Tenant GUID.</param>
        /// <param name="accessKey">Access key.</param>
        /// <param name="endpoint">Endpoint URL, i.e. http://localhost:8000/v1.0/tenants/tenant-guid/processing/udr.</param>
        public ViewUdrGeneratorSdk(Guid tenantGuid, string accessKey, string endpoint = "http://localhost:8000/v1.0/tenants/default/processing/udr") : base(tenantGuid, accessKey, endpoint)
        {
            Header = "[ViewUdrGeneratorSdk] ";
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// Process document.
        /// </summary>
        /// <param name="doc">Document request.</param>
        /// <param name="filename">Filename containing data.  Setting this value will overwrite the 'Data' property in the document request.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Document response.</returns>
        public async Task<UdrDocument> ProcessDocument(UdrDocumentRequest doc, string filename = null, CancellationToken token = default)
        {
            if (doc == null) throw new ArgumentNullException(nameof(doc));

            if (!String.IsNullOrEmpty(filename))
                doc.Data = await File.ReadAllBytesAsync(filename, token).ConfigureAwait(false);

            string url = Endpoint + "v1.0/document";
            
            using (RestRequest req = new RestRequest(url, HttpMethod.Put, "application/json"))
            {
                req.TimeoutMilliseconds = TimeoutMs;
                req.ContentType = "application/json";
                req.Authorization.BearerToken = AccessKey;

                using (RestResponse resp = await req.SendAsync(Serializer.SerializeJson(doc, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                Log(SeverityEnum.Debug, "deserializing response body");
                                UdrDocument docResp = Serializer.DeserializeJson<UdrDocument>(resp.DataAsString);
                                return docResp;
                            }
                            else
                            {
                                Log(SeverityEnum.Debug, "empty response body, returning null");
                                return null;
                            }
                        }
                        else
                        {
                            Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(resp.DataAsString))
                            {
                                Log(SeverityEnum.Debug, "deserializing response body");
                                UdrDocument docResp = Serializer.DeserializeJson<UdrDocument>(resp.DataAsString);
                                return docResp;
                            }
                            else
                            {
                                Log(SeverityEnum.Debug, "empty response body, returning null");
                                return null;
                            }
                        }
                    }
                    else
                    {
                        Log(SeverityEnum.Warn, "no response from " + url);
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
        public async Task<UdrDocument> ProcessDataTable(UdrDataTableRequest dt, string filename = null, CancellationToken token = default)
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

            string url = Endpoint + "v1.0/datatable";

            using (RestRequest req = new RestRequest(url, HttpMethod.Put, "application/json"))
            {
                req.TimeoutMilliseconds = TimeoutMs;
                req.ContentType = "application/json";
                req.Authorization.BearerToken = AccessKey;

                using (RestResponse resp = await req.SendAsync(Serializer.SerializeJson(dt, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode == 200)
                        {
                            Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.ContentLength + " bytes");
                            UdrDocument docResp = Serializer.DeserializeJson<UdrDocument>(resp.DataAsString);
                            return docResp;
                        }
                        else
                        {
                            Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode);
                            UdrDocument docResp = Serializer.DeserializeJson<UdrDocument>(resp.DataAsString);
                            return docResp;
                        }
                    }
                    else
                    {
                        Log(SeverityEnum.Warn, "no response from " + url);
                        return null;
                    }
                }
            }
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}
