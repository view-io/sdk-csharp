namespace View.Sdk.Processor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Message queue type.
    /// </summary>
    public enum MqMessageTypeEnum
    {
        /// <summary>
        /// TypeDetectionRequest.
        /// </summary>
        [EnumMember(Value = "TypeDetectionRequest")]
        TypeDetectionRequest,

        /// <summary>
        /// TypeDetectionResponse.
        /// </summary>
        [EnumMember(Value = "TypeDetectionResponse")]
        TypeDetectionResponse,

        /// <summary>
        /// AtomExtractionRequest.
        /// </summary>
        [EnumMember(Value = "AtomExtractionRequest")]
        AtomExtractionRequest,

        /// <summary>
        /// AtomExtractionResponse.
        /// </summary>
        [EnumMember(Value = "AtomExtractionResponse")]
        AtomExtractionResponse,

        /// <summary>
        /// SemanticCellExtractionRequest.
        /// </summary>
        [EnumMember(Value = "SemanticCellExtractionRequest")]
        SemanticCellExtractionRequest,

        /// <summary>
        /// SemanticCellExtractionResponse.
        /// </summary>
        [EnumMember(Value = "SemanticCellExtractionResponse")]
        SemanticCellExtractionResponse,

        /// <summary>
        /// UdrGenerationRequest.
        /// </summary>
        [EnumMember(Value = "UdrGenerationRequest")]
        UdrGenerationRequest,

        /// <summary>
        /// UdrGenerationResponse.
        /// </summary>
        [EnumMember(Value = "UdrGenerationResponse")]
        UdrGenerationResponse,

        /// <summary>
        /// EmbeddingsFindRequest.
        /// </summary>
        [EnumMember(Value = "EmbeddingsFindRequest")]
        EmbeddingsFindRequest,

        /// <summary>
        /// EmbeddingsFindResponse.
        /// </summary>
        [EnumMember(Value = "EmbeddingsFindResponse")]
        EmbeddingsFindResponse,

        /// <summary>
        /// EmbeddingsGenerationRequest.
        /// </summary>
        [EnumMember(Value = "EmbeddingsGenerationRequest")]
        EmbeddingsGenerationRequest,

        /// <summary>
        /// EmbeddingsGenerationResponse.
        /// </summary>
        [EnumMember(Value = "EmbeddingsGenerationResponse")]
        EmbeddingsGenerationResponse,

        /// <summary>
        /// PersistDataCatalogRequest.
        /// </summary>
        [EnumMember(Value = "PersistDataCatalogRequest")]
        PersistDataCatalogRequest,

        /// <summary>
        /// PersistDataCatalogResponse.
        /// </summary>
        [EnumMember(Value = "PersistDataCatalogResponse")]
        PersistDataCatalogResponse,

        /// <summary>
        /// PersistVectorRequest.
        /// </summary>
        [EnumMember(Value = "PersistVectorRequest")]
        PersistVectorRequest,

        /// <summary>
        /// PersistVectorResponse.
        /// </summary>
        [EnumMember(Value = "PersistVectorResponse")]
        PersistVectorResponse,

        /// <summary>
        /// PersistGraphRequest.
        /// </summary>
        [EnumMember(Value = "PersistGraphRequest")]
        PersistGraphRequest,

        /// <summary>
        /// PersistGraphResponse.
        /// </summary>
        [EnumMember(Value = "PersistGraphResponse")]
        PersistGraphResponse
    }
}
