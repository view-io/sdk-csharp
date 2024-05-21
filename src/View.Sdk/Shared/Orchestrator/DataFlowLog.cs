namespace View.Sdk.Shared.Orchestrator
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Data flow log.
    /// </summary>
    public class DataFlowLog
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
        /// Data flow GUID.
        /// </summary>
        public string DataFlowGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Request GUID.
        /// </summary>
        public string RequestGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Trigger GUID.
        /// </summary>
        public string TriggerGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Step GUID.
        /// </summary>
        public string StepGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Start time, in UTC.
        /// </summary>
        public DateTime StartUtc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// End time, in UTC.
        /// </summary>
        public DateTime? EndUtc { get; set; } = null;

        /// <summary>
        /// Run time, in milliseconds.
        /// </summary>
        public decimal TotalMs
        {
            get
            {
                return _TotalMs;
            }
            set
            {
                if (value < 0) value = 0;
                _TotalMs = value;
            }
        }

        /// <summary>
        /// Result, e.g. Success, Failure, Exception.
        /// </summary>
        public StepResultEnum Result { get; set; } = StepResultEnum.Success;

        /// <summary>
        /// GUID of the next step, if any.
        /// </summary>
        public string NextStepGUID { get; set; } = null;

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

        private decimal _TotalMs = 0;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public DataFlowLog()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
