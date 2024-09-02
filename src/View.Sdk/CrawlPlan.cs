namespace View.Sdk
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Crawl plan, i.e. the top-level scheduled task with links to repository, schedule, and filter.
    /// </summary>
    public class CrawlPlan
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
        /// Data repository GUID.
        /// </summary>
        public string DataRepositoryGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// View endpoint GUID.
        /// </summary>
        public string ViewEndpointGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Crawl schedule GUID.
        /// </summary>
        public string CrawlScheduleGUID { get; set; } = null;

        /// <summary>
        /// Crawl filter GUID.
        /// </summary>
        public string CrawlFilterGUID { get; set; } = null;

        /// <summary>
        /// Metadata rule GUID.
        /// </summary>
        public string MetadataRuleGUID { get; set; } = null;

        /// <summary>
        /// Embeddings rule GUID.
        /// </summary>
        public string EmbeddingsRuleGUID { get; set; } = null;

        /// <summary>
        /// Data flow endpoint for processing.
        /// </summary>
        public string ProcessingEndpoint { get; set; } = null;

        /// <summary>
        /// Data flow endpoint for cleanup processing.
        /// </summary>
        public string CleanupEndpoint { get; set; } = null;

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; } = "My crawl operation";

        /// <summary>
        /// Directory where enumerations are stored.
        /// </summary>
        public string EnumerationDirectory
        {
            get
            {
                return _EnumerationDirectory;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(EnumerationDirectory));
                value = value.Replace("\\", "/");
                if (!value.EndsWith("/")) value += "/";
                _EnumerationDirectory = value;
            }
        }

        /// <summary>
        /// Number of enumerations to retain.
        /// </summary>
        public int EnumerationsToRetain
        {
            get
            {
                return _EnumerationsToRetain;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(EnumerationsToRetain));
                _EnumerationsToRetain = value;
            }
        }

        /// <summary>
        /// Maximum number of tasks to use while writing data to View.
        /// </summary>
        public int MaxDrainTasks
        {
            get
            {
                return _MaxDrainTasks;
            }
            set
            {
                if (value < 1) throw new ArgumentNullException(nameof(MaxDrainTasks));
                _MaxDrainTasks = value;
            }
        }

        /// <summary>
        /// Boolean flag indicating whether or not new files should be uploaded to the specified View endpoint.
        /// </summary>
        public bool ProcessAdditions { get; set; } = true;

        /// <summary>
        /// Boolean flag indicating whether or not deleted files should be deleted on the specified View endpoint.
        /// </summary>
        public bool ProcessDeletions { get; set; } = false;

        /// <summary>
        /// Boolean flag indicating whether or not updated files should be uploaded to the specified View endpoint.
        /// </summary>
        public bool ProcessUpdates { get; set; } = true;

        /// <summary>
        /// Created.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        #endregion

        #region Private-Members

        private int _Id = 0;
        private string _EnumerationDirectory = "./enumerations/";
        private int _EnumerationsToRetain = 30;
        private int _MaxDrainTasks = 8;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public CrawlPlan()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
