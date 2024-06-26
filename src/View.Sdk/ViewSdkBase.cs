namespace View.Sdk
{
    using RestWrapper;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Threading;
    using View.Serializer;

    /// <summary>
    /// View SDK base class.
    /// </summary>
    public class ViewSdkBase
    {
        #region Public-Members

        /// <summary>
        /// Method to invoke to send log messages.
        /// </summary>
        public Action<Severity, string> Logger { get; set; } = null;

        /// <summary>
        /// Header to prepend to log messages.
        /// </summary>
        public string Header
        {
            get
            {
                return _Header;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    _Header = value;
                }
                else
                {
                    if (!value.EndsWith(" ")) value += " ";
                    _Header = value;
                }
            }
        }

        /// <summary>
        /// Endpoint URL.
        /// </summary>
        public string Endpoint
        {
            get
            {
                return _Endpoint;
            }
            set
            {
                Uri uri = new Uri(value);
                if (!value.EndsWith("/")) value += "/";
                _Endpoint = value;
            }
        }

        /// <summary>
        /// Serialization helper.
        /// </summary>
        public SerializationHelper Serializer
        {
            get
            {
                return _Serializer;
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(Serializer));
                _Serializer = value;
            }
        }

        #endregion

        #region Internal-Members

        #endregion

        #region Private-Members

        private string _Header = "[ViewSdkBase] ";
        private string _Endpoint = null;
        private SerializationHelper _Serializer = new SerializationHelper();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        public ViewSdkBase(string endpoint)
        {
            if (String.IsNullOrEmpty(endpoint)) throw new ArgumentNullException(nameof(endpoint));

            Endpoint = endpoint;
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// Emit a log message.
        /// </summary>
        /// <param name="sev">Severity.</param>
        /// <param name="msg">Message.</param>
        public void Log(Severity sev, string msg)
        {
            if (String.IsNullOrEmpty(msg)) return;
            Logger?.Invoke(sev, _Header + msg);
        }

        /// <summary>
        /// Validate connectivity.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Boolean indicating success.</returns>
        public async Task<bool> ValidateConnectivity(CancellationToken token = default)
        {
            string url = _Endpoint;

            using (RestRequest req = new RestRequest(url, HttpMethod.Head))
            {
                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        Log(Severity.Debug, "success reported from " + url);
                        return true;
                    }
                    else if (resp != null)
                    {
                        Log(Severity.Warn, "non-success reported from " + url + ": " + resp.StatusCode);
                        return false;
                    }
                    else
                    {
                        Log(Severity.Warn, "no response from " + url);
                        return false;
                    }
                }
            }
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}
