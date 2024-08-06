namespace View.Sdk.Graph
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using LiteGraph.Sdk;

    /// <summary>
    /// View LiteGraph SDK.
    /// </summary>
    public class ViewLitegraphSdk
    {
        #region Public-Members

        /// <summary>
        /// Endpoint.
        /// </summary>
        public string Endpoint
        {
            get
            {
                return _Endpoint;
            }
        }

        #endregion

        #region Private-Members

        private LiteGraphSdk _Sdk = null;
        private string _Endpoint = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        public ViewLitegraphSdk(string endpoint = "http://localhost:8701/")
        {
            if (String.IsNullOrEmpty(endpoint)) throw new ArgumentNullException(nameof(endpoint));

            _Endpoint = endpoint;
            _Sdk = new LiteGraphSdk(_Endpoint);
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
