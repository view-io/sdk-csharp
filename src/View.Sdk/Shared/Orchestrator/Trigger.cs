namespace View.Sdk.Shared.Orchestrator
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Data flow trigger, i.e. the event that invokes a data flow.
    /// </summary>
    public class Trigger
    {
        #region Public-Members

        /// <summary>
        /// GUID.
        /// </summary>
        public string GUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public string TenantGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Type.
        /// </summary>
        public TriggerTypeEnum TriggerType { get; set; } = TriggerTypeEnum.HTTP;

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; } = "My trigger";

        /// <summary>
        /// HTTP method.
        /// </summary>
        public HttpMethodEnum? HttpMethod { get; set; } = null;

        /// <summary>
        /// HTTP relative URL prefix, i.e. /mydataflow will match /mydataflow1 and /mydataflow/1.
        /// </summary>
        public string HttpUrlPrefix
        {
            get
            {
                return _HttpUrlPrefix;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    if (!value.StartsWith("/")) value = "/" + value;

                    if (_ReservedUrlPrefixes.Contains(value))
                        throw new ArgumentException("Supplied HttpUrlPrefix is reserved and cannot be used.");
                }

                _HttpUrlPrefix = value;
            }
        }

        /// <summary>
        /// Notes.
        /// </summary>
        public string Notes { get; set; } = null;

        /// <summary>
        /// Creation timestamp, in UTC time.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        #endregion

        #region Private-Members

        private string _HttpUrlPrefix = null;

        private List<string> _ReservedUrlPrefixes = new List<string>
        {
            "/v1.0"
        };

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public Trigger()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
