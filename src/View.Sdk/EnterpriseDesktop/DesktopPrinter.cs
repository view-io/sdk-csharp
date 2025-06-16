namespace View.Sdk.EnterpriseDesktop
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Desktop printer.
    /// </summary>
    public class DesktopPrinter
    {
        #region Public-Members

        /// <summary>
        /// GUID.
        /// </summary>
        public Guid? GUID { get; set; } = null;

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; } = null;

        /// <summary>
        /// Description.
        /// </summary>
        public string Description { get; set; } = null;

        /// <summary>
        /// Async.
        /// </summary>
        public bool? Async { get; set; } = null;

        /// <summary>
        /// Tenant.
        /// </summary>
        public Tenant Tenant { get; set; } = null;

        /// <summary>
        /// Collection.
        /// </summary>
        public DesktopCollection Collection { get; set; } = null;

        /// <summary>
        /// Bucket.
        /// </summary>
        public object Bucket { get; set; } = null;

        /// <summary>
        /// Metadata rule.
        /// </summary>
        public DesktopMetadataRule MetadataRule { get; set; } = null;

        /// <summary>
        /// Embeddings rule.
        /// </summary>
        public DesktopEmbeddingsRule EmbeddingsRule { get; set; } = null;

        /// <summary>
        /// Vector repository.
        /// </summary>
        public DesktopVectorRepository VectorRepository { get; set; } = null;

        /// <summary>
        /// Graph repository.
        /// </summary>
        public DesktopGraphRepository GraphRepository { get; set; } = null;

        /// <summary>
        /// Created UTC timestamp.
        /// </summary>
        public DateTime? CreatedUTC { get; set; } = null;

        /// <summary>
        /// Error detail.
        /// </summary>
        [JsonPropertyName("detail")]
        public object Detail { get; set; } = null;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public DesktopPrinter()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }

    /// <summary>
    /// Tenant information.
    /// </summary>
    public class Tenant
    {
        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public Guid GUID { get; set; }

        /// <summary>
        /// Tenant name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Tenant region.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// S3 base domain.
        /// </summary>
        public string S3BaseDomain { get; set; }

        /// <summary>
        /// Default pool GUID.
        /// </summary>
        public Guid DefaultPoolGUID { get; set; }

        /// <summary>
        /// Is tenant active.
        /// </summary>
        public bool Active { get; set; }
    }


    /// <summary>
    /// Represents a desktop collection.
    /// </summary>
    public class DesktopCollection
    {
        /// <summary>
        /// Collection GUID.
        /// </summary>
        public Guid GUID { get; set; }

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public Guid TenantGUID { get; set; }

        /// <summary>
        /// Collection name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Whether overwrites are allowed.
        /// </summary>
        public bool AllowOverwrites { get; set; }

        /// <summary>
        /// Additional data or notes.
        /// </summary>
        public string AdditionalData { get; set; }
    }



    /// <summary>
    /// Represents a desktop metadata rule.
    /// </summary>
    public class DesktopMetadataRule
    {
        /// <summary>
        /// Metadata rule GUID.
        /// </summary>
        public Guid GUID { get; set; }

        /// <summary>Tenant GUID.</summary>
        public Guid TenantGUID { get; set; }

        /// <summary>
        /// Bucket GUID
        /// .</summary>
        public Guid BucketGUID { get; set; }

        /// <summary>
        /// Owner GUID.
        /// </summary>
        public Guid OwnerGUID { get; set; }

        /// <summary>
        /// Rule name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Accepted content type.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Maximum allowed content length in bytes.
        /// </summary>
        public long MaxContentLength { get; set; }

        /// <summary>
        /// Data flow endpoint URL.
        /// </summary>
        public string DataFlowEndpoint { get; set; }

        /// <summary>
        /// Type detector endpoint URL.
        /// </summary>
        public string TypeDetectorEndpoint { get; set; }

        /// <summary>
        /// Semantic cell service endpoint URL.
        /// </summary>
        public string SemanticCellEndpoint { get; set; }

        /// <summary>
        /// Maximum chunk content length.
        /// </summary>
        public int MaxChunkContentLength { get; set; }

        /// <summary>
        /// Chunk shift size.
        /// </summary>
        public int ShiftSize { get; set; }

        /// <summary>
        /// UDR endpoint URL.
        /// </summary>
        public string UdrEndpoint { get; set; }

        /// <summary>
        /// Top terms limit.
        /// </summary>
        public int TopTerms { get; set; }

        /// <summary>
        /// Case insensitive parsing flag.
        /// </summary>
        public bool CaseInsensitive { get; set; }

        /// <summary>
        /// Include flattened data flag.
        /// </summary>
        public bool IncludeFlattened { get; set; }

        /// <summary>
        /// Data catalog endpoint URL.
        /// </summary>
        public string DataCatalogEndpoint { get; set; }

        /// <summary>
        /// Type of the data catalog (e.g., Lexi).
        /// </summary>
        public string DataCatalogType { get; set; }

        /// <summary>
        /// Data catalog collection GUID.
        /// </summary>
        public Guid DataCatalogCollection { get; set; }

        /// <summary>
        /// Graph repository GUID.
        /// </summary>
        public Guid GraphRepositoryGUID { get; set; }
    }

    /// <summary>
    /// Represents a desktop embeddings rule.
    /// </summary>
    public class DesktopEmbeddingsRule
    {
        /// <summary>
        /// Embeddings rule GUID.
        /// </summary>
        public Guid GUID { get; set; }

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public Guid TenantGUID { get; set; }

        /// <summary>
        /// Bucket GUID.
        /// </summary>
        public Guid BucketGUID { get; set; }

        /// <summary>
        /// Owner GUID.
        /// </summary>
        public Guid OwnerGUID { get; set; }

        /// <summary>
        /// Rule name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Accepted content type.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Graph repository GUID.
        /// </summary>
        public Guid GraphRepositoryGUID { get; set; }

        /// <summary>
        /// Vector repository GUID.
        /// </summary>
        public Guid VectorRepositoryGUID { get; set; }

        /// <summary>
        /// Data flow endpoint URL.
        /// </summary>
        public string DataFlowEndpoint { get; set; }

        /// <summary>
        /// Embeddings generator name.
        /// </summary>
        public string EmbeddingsGenerator { get; set; }

        /// <summary>
        /// Generator service URL.
        /// </summary>
        public string GeneratorUrl { get; set; }

        /// <summary>
        /// Generator API key.
        /// </summary>
        public string GeneratorApiKey { get; set; }

        /// <summary>
        /// Vector store service URL.
        /// </summary>
        public string VectorStoreUrl { get; set; }

        /// <summary>
        /// Maximum allowed content length.
        /// </summary>
        public long MaxContentLength { get; set; }
    }


    /// <summary>
    /// Represents a desktop vector repository.
    /// </summary>
    public class DesktopVectorRepository
    {
        /// <summary>
        /// Repository GUID.
        /// </summary>
        public Guid GUID { get; set; }

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public Guid TenantGUID { get; set; }

        /// <summary>
        /// Repository name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of the repository (e.g., Pgvector).
        /// </summary>
        public string RepositoryType { get; set; }

        /// <summary>
        /// Embedding model used.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Vector dimensionality.
        /// </summary>
        public int Dimensionality { get; set; }

        /// <summary>
        /// Database hostname.
        /// </summary>
        public string DatabaseHostname { get; set; }

        /// <summary>
        /// Database name.
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Table in the vector database.
        /// </summary>
        public string DatabaseTable { get; set; }

        /// <summary>
        /// Database port number.
        /// </summary>
        public int DatabasePort { get; set; }

        /// <summary>
        /// Database user.
        /// </summary>
        public string DatabaseUser { get; set; }

        /// <summary>
        /// Database password.
        /// </summary>
        public string DatabasePassword { get; set; }
    }



    /// <summary>
    /// Represents a desktop graph repository.
    /// </summary>
    public class DesktopGraphRepository
    {
        /// <summary>
        /// Graph repository GUID.
        /// </summary>
        public Guid GUID { get; set; }

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public Guid TenantGUID { get; set; }

        /// <summary>
        /// Repository name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of the repository (e.g., LiteGraph).
        /// </summary>
        public string RepositoryType { get; set; }

        /// <summary>
        /// Endpoint URL of the graph service.
        /// </summary>
        public string EndpointUrl { get; set; }

        /// <summary>
        /// API key for the graph service.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Identifier of the graph.
        /// </summary>
        public Guid GraphIdentifier { get; set; }
    }


}