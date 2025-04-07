namespace View.Sdk.Processor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// Processor task.
    /// </summary>
    public class ProcessorTask
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
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(Id));
                _Id = value;
            }
        }

        /// <summary>
        /// GUID.
        /// </summary>
        public Guid GUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public Guid TenantGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Bucket GUID.
        /// </summary>
        public Guid? BucketGUID { get; set; } = null;

        /// <summary>
        /// Object GUID.
        /// </summary>
        public Guid ObjectGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Data repository GUID.
        /// </summary>
        public Guid? DataRepositoryGUID { get; set; } = null;

        /// <summary>
        /// Collection GUID.
        /// </summary>
        public Guid? CollectionGUID { get; set; } = null;

        /// <summary>
        /// Metadata rule GUID.
        /// </summary>
        public Guid MetadataRuleGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Embeddings rule GUID.
        /// </summary>
        public Guid EmbeddingsRuleGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Vector repository GUID.
        /// </summary>
        public Guid VectorRepositoryGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Graph repository GUID.
        /// </summary>
        public Guid GraphRepositoryGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Key.
        /// </summary>
        public string Key { get; set; } = null;

        /// <summary>
        /// Version.
        /// </summary>
        public string Version { get; set; } = null;

        /// <summary>
        /// State.
        /// </summary>
        public ProcessorTaskTypeEnum State { get; set; } = ProcessorTaskTypeEnum.TypeDetection;

        /// <summary>
        /// UTC timestamp from last update.
        /// </summary>
        public DateTime LastUpdateUtc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// UTC timestamp from creation.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        #endregion

        #region Private-Members

        private int _Id = 0;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Processor task.
        /// </summary>
        public ProcessorTask()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
