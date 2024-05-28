namespace View.Sdk.Shared.Orchestrator
{
    using System;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// Data flow, i.e. a collection of steps and paths.
    /// </summary>
    public class DataFlow
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
        /// Trigger GUID.
        /// </summary>
        public string TriggerGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Step GUID.
        /// </summary>
        public string StepGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; } = "My data flow";

        /// <summary>
        /// Notes.
        /// </summary>
        public string Notes { get; set; } = null;

        /// <summary>
        /// Creation timestamp, in UTC time.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Number of days to retain log entries and logfiles.
        /// </summary>
        public int LogRetentionDays
        {
            get
            {
                return _LogRetentionDays;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(LogRetentionDays));
                _LogRetentionDays = value;
            }
        }

        /// <summary>
        /// Entry step.
        /// </summary>
        public StepMetadata Step { get; set; } = null;

        #endregion

        #region Private-Members

        private int _LogRetentionDays = 7;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public DataFlow()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
