namespace View.Sdk
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Crawl operation, i.e. an execution of a crawl plan.
    /// </summary>
    public class CrawlOperation
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
        /// Crawl plan GUID.
        /// </summary>
        public string CrawlPlanGUID { get; set; } = null;

        /// <summary>
        /// Crawl schedule GUID.
        /// </summary>
        public string CrawlScheduleGUID { get; set; } = null;

        /// <summary>
        /// Crawl filter GUID.
        /// </summary>
        public string CrawlFilterGUID { get; set; } = null;

        /// <summary>
        /// Data repository GUID.
        /// </summary>
        public string DataRepositoryGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// View endpoint GUID.
        /// </summary>
        public string ViewEndpointGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Metadata rule GUID.
        /// </summary>
        public string MetadataRuleGUID { get; set; } = null;

        /// <summary>
        /// Embeddings rule GUID.
        /// </summary>
        public string EmbeddingsRuleGUID { get; set; } = null;

        /// <summary>
        /// Data flow endpoint.
        /// </summary>
        public string DataFlowEndpoint { get; set; } = null;

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; } = "My crawl operation";

        /// <summary>
        /// Number of objects enumerated.
        /// </summary>
        public long ObjectsEnumerated
        { 
            get
            {
                return _ObjectsEnumerated;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(ObjectsEnumerated));
                _ObjectsEnumerated = value;
            }
        }

        /// <summary>
        /// Number of bytes enumerated.
        /// </summary>
        public long BytesEnumerated
        {
            get
            {
                return _BytesEnumerated;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(BytesEnumerated));
                _BytesEnumerated = value;
            }
        }

        /// <summary>
        /// Number of objects added since last enumeration.
        /// </summary>
        public long ObjectsAdded
        {
            get
            {
                return _ObjectsAdded;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(ObjectsAdded));
                _ObjectsAdded = value;
            }
        }

        /// <summary>
        /// Number of bytes added since last enumeration.
        /// </summary>
        public long BytesAdded
        {
            get
            {
                return _BytesAdded;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(BytesAdded));
                _BytesAdded = value;
            }
        }

        /// <summary>
        /// Number of objects updated since last enumeration.
        /// </summary>
        public long ObjectsUpdated
        {
            get
            {
                return _ObjectsUpdated;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(ObjectsUpdated));
                _ObjectsUpdated = value;
            }
        }

        /// <summary>
        /// Number of bytes updated since last enumeration.
        /// </summary>
        public long BytesUpdated
        {
            get
            {
                return _BytesUpdated;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(BytesUpdated));
                _BytesUpdated = value;
            }
        }

        /// <summary>
        /// Number of objects deleted since last enumeration.
        /// </summary>
        public long ObjectsDeleted
        {
            get
            {
                return _ObjectsDeleted;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(ObjectsDeleted));
                _ObjectsDeleted = value;
            }
        }

        /// <summary>
        /// Number of bytes deleted since last enumeration.
        /// </summary>
        public long BytesDeleted
        {
            get
            {
                return _BytesDeleted;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(BytesDeleted));
                _BytesDeleted = value;
            }
        }

        /// <summary>
        /// Enumeration file.
        /// </summary>
        public string EnumerationFile { get; set; } = null;

        /// <summary>
        /// Number of objects successfully written.
        /// </summary>
        public long ObjectsSuccess
        {
            get
            {
                return _ObjectsSuccess;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(ObjectsSuccess));
                _ObjectsSuccess = value;
            }
        }

        /// <summary>
        /// Number of bytes successfully written
        /// </summary>
        public long BytesSuccess
        {
            get
            {
                return _BytesSuccess;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(BytesSuccess));
                _BytesSuccess = value;
            }
        }

        /// <summary>
        /// Number of objects unable to be written.
        /// </summary>
        public long ObjectsFailed
        {
            get
            {
                return _ObjectsFailed;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(ObjectsFailed));
                _ObjectsFailed = value;
            }
        }

        /// <summary>
        /// Number of bytes unable to be written.
        /// </summary>
        public long BytesFailed
        {
            get
            {
                return _BytesFailed;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(BytesFailed));
                _BytesFailed = value;
            }
        }

        /// <summary>
        /// Crawl state.
        /// </summary>
        public CrawlStateEnum State { get; set; } = CrawlStateEnum.NotStarted;

        /// <summary>
        /// Created.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Started.
        /// </summary>
        public DateTime? StartUtc { get; set; } = null;

        /// <summary>
        /// Start of enumeration.
        /// </summary>
        public DateTime? StartEnumerationUtc { get; set; } = null;

        /// <summary>
        /// Start of retrieval.
        /// </summary>
        public DateTime? StartRetrievalUtc { get; set; } = null;

        /// <summary>
        /// Finished enumeration.
        /// </summary>
        public DateTime? FinishEnumerationUtc { get; set; } = null;

        /// <summary>
        /// Finished retrieval.
        /// </summary>
        public DateTime? FinishRetrievalUtc { get; set; } = null;

        /// <summary>
        /// Finished.
        /// </summary>
        public DateTime? FinishUtc { get; set; } = null;

        /// <summary>
        /// Additional data.
        /// </summary>
        public string AdditionalData { get; set; } = null;

        #endregion

        #region Private-Members

        private int _Id = 0;
        private long _ObjectsEnumerated = 0;
        private long _BytesEnumerated = 0;
        private long _ObjectsAdded = 0;
        private long _BytesAdded = 0;
        private long _ObjectsUpdated = 0;
        private long _BytesUpdated = 0;
        private long _ObjectsDeleted = 0;
        private long _BytesDeleted = 0;
        private long _ObjectsSuccess = 0;
        private long _BytesSuccess = 0;
        private long _ObjectsFailed = 0;
        private long _BytesFailed = 0;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public CrawlOperation()
        {

        }

        #endregion

        #region Public-Methods
         
        #endregion

        #region Private-Methods

        #endregion
    }
}
