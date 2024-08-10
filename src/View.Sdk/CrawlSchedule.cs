namespace View.Sdk
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Crawl schedule.
    /// </summary>
    public class CrawlSchedule
    {
        #region Public-Members

        /// <summary>
        /// ID.
        /// </summary>
        [JsonIgnore]
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(Id));
                _Id = value;
            }
        }

        /// <summary>
        /// GUID.
        /// </summary>
        public string GUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public string TenantGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; } = "My schedule";

        /// <summary>
        /// Schedule interval.
        /// </summary>
        public ScheduleIntervalEnum Schedule { get; set; } = ScheduleIntervalEnum.DaysInterval;

        /// <summary>
        /// Interval.
        /// </summary>
        public int Interval
        { 
            get
            {
                return _Interval;
            }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(Interval));
                _Interval = value;
            }
        }

        /// <summary>
        /// Created.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        #endregion

        #region Private-Members

        private int _Id = 0;
        private int _Interval = 1;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public CrawlSchedule()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
