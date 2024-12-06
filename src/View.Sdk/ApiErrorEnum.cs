namespace View.Sdk
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// API error codes.
    /// </summary>
    public enum ApiErrorEnum
    {
        /// <summary>
        /// No object metadata supplied.
        /// </summary>
        [EnumMember(Value = "NoObjectMetadata")]
        NoObjectMetadata,
        /// <summary>
        /// No object data supplied.
        /// </summary>
        [EnumMember(Value = "NoObjectData")]
        NoObjectData,
        /// <summary>
        /// No metadata rule supplied.
        /// </summary>
        [EnumMember(Value = "NoMetadataRule")]
        NoMetadataRule,
        /// <summary>
        /// Request body missing.
        /// </summary>
        [EnumMember(Value = "RequestBodyMissing")]
        RequestBodyMissing,
        /// <summary>
        /// Required properties were missing.
        /// </summary>
        [EnumMember(Value = "RequiredPropertiesMissing")]
        RequiredPropertiesMissing,

        /// <summary>
        /// No connectivity to the specified graph.
        /// </summary>
        [EnumMember(Value = "NoGraphConnectivity")]
        NoGraphConnectivity,
        /// <summary>
        /// A graph operation has failed.
        /// </summary>
        [EnumMember(Value = "GraphOperationFailed")]
        GraphOperationFailed,

        /// <summary>
        /// No connectivity to the type detection endpoint.
        /// </summary>
        [EnumMember(Value = "NoTypeDetectorConnectivity")]
        NoTypeDetectorConnectivity,
        /// <summary>
        /// Unable to discern type of supplied data.
        /// </summary>
        [EnumMember(Value = "UnknownTypeDetected")]
        UnknownTypeDetected,

        /// <summary>
        /// No connectivity to the UDR endpoint.
        /// </summary>
        [EnumMember(Value = "NoUdrConnectivity")]
        NoUdrConnectivity,
        /// <summary>
        /// UDR generation failed.
        /// </summary>
        [EnumMember(Value = "UdrGenerationFailed")]
        UdrGenerationFailed,

        /// <summary>
        /// No connectivity to the semantic cell extraction endpoint.
        /// </summary>
        [EnumMember(Value = "NoSemanticCellConnectivity")]
        NoSemanticCellConnectivity,
        /// <summary>
        /// Semantic cell extraction failed.
        /// </summary>
        [EnumMember(Value = "SemanticCellExtractionFailed")]
        SemanticCellExtractionFailed,

        /// <summary>
        /// No data catalog connectivity.
        /// </summary>
        [EnumMember(Value = "NoDataCatalogConnectivity")]
        NoDataCatalogConnectivity,
        /// <summary>
        /// Persisting data in the data catalog failed.
        /// </summary>
        [EnumMember(Value = "DataCatalogPersistFailed")]
        DataCatalogPersistFailed,
        /// <summary>
        /// Unknown data catalog type.
        /// </summary>
        [EnumMember(Value = "UnknownDataCatalogType")]
        UnknownDataCatalogType,

        /// <summary>
        /// No embeddings processor connectivity.
        /// </summary>
        [EnumMember(Value = "NoEmbeddingsConnectivity")]
        NoEmbeddingsConnectivity,
        /// <summary>
        /// Unknown embeddings generator type.
        /// </summary>
        [EnumMember(Value = "UnknownEmbeddingsGeneratorType")]
        UnknownEmbeddingsGeneratorType,
        /// <summary>
        /// Embeddings generation failed.
        /// </summary>
        [EnumMember(Value = "EmbeddingsGenerationFailed")]
        EmbeddingsGenerationFailed,

        /// <summary>
        /// No vector repository connectivity.
        /// </summary>
        [EnumMember(Value = "NoVectorConnectivity")]
        NoVectorConnectivity,
        /// <summary>
        /// Unknown vector repository type.
        /// </summary>
        [EnumMember(Value = "UnknownVectorRepositoryType")]
        UnknownVectorRepositoryType,
        /// <summary>
        /// Vector persistence failed.
        /// </summary>
        [EnumMember(Value = "VectorPersistFailed")]
        VectorPersistFailed,

        /// <summary>
        /// Authentication failed.
        /// </summary>
        [EnumMember(Value = "AuthenticationFailed")]
        AuthenticationFailed,
        /// <summary>
        /// Authorization failed.
        /// </summary>
        [EnumMember(Value = "AuthorizationFailed")]
        AuthorizationFailed,
        /// <summary>
        /// Bad gateway.
        /// </summary>
        [EnumMember(Value = "BadGateway")]
        BadGateway,
        /// <summary>
        /// Bad request.
        /// </summary>
        [EnumMember(Value = "BadRequest")]
        BadRequest,
        /// <summary>
        /// Conflict.
        /// </summary>
        [EnumMember(Value = "Conflict")]
        Conflict,
        /// <summary>
        /// DeserializationError.
        /// </summary>
        [EnumMember(Value = "DeserializationError")]
        DeserializationError,
        /// <summary>
        /// Inactive.
        /// </summary>
        [EnumMember(Value = "Inactive")]
        Inactive,
        /// <summary>
        /// Internal error.
        /// </summary>
        [EnumMember(Value = "InternalError")]
        InternalError,
        /// <summary>
        /// Invalid range.
        /// </summary>
        [EnumMember(Value = "InvalidRange")]
        InvalidRange,
        /// <summary>
        /// In use.
        /// </summary>
        [EnumMember(Value = "InUse")]
        InUse,
        /// <summary>
        /// Not empty.
        /// </summary>
        [EnumMember(Value = "NotEmpty")]
        NotEmpty,
        /// <summary>
        /// Not found.
        /// </summary>
        [EnumMember(Value = "NotFound")]
        NotFound,
        /// <summary>
        /// Timeout.
        /// </summary>
        [EnumMember(Value = "Timeout")]
        Timeout,
        /// <summary>
        /// Token expired.
        /// </summary>
        [EnumMember(Value = "TokenExpired")]
        TokenExpired,
        /// <summary>
        /// Request too large.
        /// </summary>
        [EnumMember(Value = "TooLarge")]
        TooLarge
    }
}
