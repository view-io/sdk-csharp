namespace View.Sdk.Shared.Processing
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// API error codes.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ApiErrorEnum
    {
        /// <summary>
        /// No connectivity to the type detection endpoint.
        /// </summary>
        [EnumMember(Value = "NoTypeDetectorConnectivity")]
        NoTypeDetectorConnectivity,
        /// <summary>
        /// Unable to discern type of supplied data.
        /// </summary>
        [EnumMember(Value = "UnknownTypeSupplied")]
        UnknownTypeSupplied,

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
        /// Unknown embeddings generator type.
        /// </summary>
        [EnumMember(Value = "UnknownEmbeddingsGeneratorType")]
        UnknownEmbeddingsGeneratorType,
        /// <summary>
        /// Embeddings persistence failed.
        /// </summary>
        [EnumMember(Value = "EmbeddingsPersistFailed")]
        EmbeddingsPersistFailed,

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
        /// Request too large.
        /// </summary>
        [EnumMember(Value = "TooLarge")]
        TooLarge
    }
}
