namespace View.Sdk
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
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
        public Guid GUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public Guid TenantGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Data flow GUID.
        /// </summary>
        public Guid DataFlowGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Request GUID.
        /// </summary>
        public Guid RequestGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Trigger GUID.
        /// </summary>
        public Guid TriggerGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Step GUID.
        /// </summary>
        public Guid StepGUID { get; set; } = Guid.NewGuid();

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
        public Guid? NextStepGUID { get; set; } = null;

        /// <summary>
        /// Notes.
        /// </summary>
        public string Notes { get; set; } = null;

        /// <summary>
        /// Timestamp when logs will expire, in UTC time.
        /// </summary>
        public DateTime LogExpirationUtc { get; set; } = DateTime.UtcNow.AddDays(7);

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
