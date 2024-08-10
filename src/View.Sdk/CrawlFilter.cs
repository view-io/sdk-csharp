namespace View.Sdk
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Crawl filter.
    /// </summary>
    public class CrawlFilter
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
        public string Name { get; set; } = "My filter";

        /// <summary>
        /// Prefix.
        /// </summary>
        public string Prefix { get; set; } = null;

        /// <summary>
        /// Suffix.
        /// </summary>
        public string Suffix { get; set; } = null;

        /// <summary>
        /// Minimum size.
        /// </summary>
        public int? MinimumSize
        {
            get
            {
                return _MinimumSize;
            }
            set
            {
                if (value != null && value.Value < 0) throw new ArgumentOutOfRangeException(nameof(MinimumSize));
                _MinimumSize = value;
            }
        }

        /// <summary>
        /// Maximum size.
        /// </summary>
        public int? MaximumSize
        {
            get
            {
                return _MaximumSize;
            }
            set
            {
                if (value != null && value.Value < 0) throw new ArgumentOutOfRangeException(nameof(MaximumSize));
                _MaximumSize = value;
            }
        }

        /// <summary>
        /// Include or exclude subdirectories.
        /// </summary>
        public bool IncludeSubdirectories { get; set; } = true;

        /// <summary>
        /// Content type.
        /// </summary>
        public string ContentType { get; set; } = null;

        /// <summary>
        /// Created.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        #endregion

        #region Private-Members

        private int _Id = 0;
        private int? _MinimumSize = 0;
        private int? _MaximumSize = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public CrawlFilter()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
