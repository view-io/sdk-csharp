namespace View.Sdk
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Model endpoint.
    /// </summary>
    public class ModelEndpoint
    {
        #region Public-Members

        /// <summary>
        /// GUID.
        /// </summary>
        public Guid GUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public Guid TenantGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; } = "My model endpoint";

        /// <summary>
        /// Endpoint URL, e.g. http://localhost:8000/
        /// </summary>
        public string EndpointUrl
        {
            get
            {
                return _EndpointUrl;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    value = value.Replace("\\", "/");
                    if (!value.EndsWith("/")) value += "/";
                }
                _EndpointUrl = value;
            }
        }

        /// <summary>
        /// Bearer token.
        /// </summary>
        public string BearerToken { get; set; } = null;

        /// <summary>
        /// Model API type.
        /// </summary>
        public ModelApiTypeEnum ApiType { get; set; } = ModelApiTypeEnum.Ollama;

        /// <summary>
        /// Request timeout.  Default is 30000 (30 seconds).
        /// </summary>
        public int TimeoutMs
        {
            get => _TimeoutMs;
            set
            {
                if (value > 1000) _TimeoutMs = value;
                else throw new ArgumentOutOfRangeException(nameof(TimeoutMs));
            }
        }

        /// <summary>
        /// Additional data.
        /// </summary>
        public string AdditionalData { get; set; } = null;

        /// <summary>
        /// Active.
        /// </summary>
        public bool Active { get; set; } = true;

        /// <summary>
        /// Created.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// List of string labels for the backend; these are used in load-balancing decisions.  
        /// When a request is received with a label, only backends with matching labels will be considered.
        /// </summary>
        public List<string> Labels
        {
            get
            {
                return _Labels;
            }
            set
            {
                if (value == null) value = new List<string>();
                _Labels = value;
            }
        }

        #endregion

        #region Private-Members

        private string _EndpointUrl = "http://localhost:11434/";
        private int _TimeoutMs = 30000;
        private List<string> _Labels = new List<string>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Model endpoint.
        /// </summary>
        public ModelEndpoint()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
