namespace View.Sdk.Graph
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Graph node type.
    /// </summary>
    public enum GraphNodeTypeEnum
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        [EnumMember(Value = "Unknown")]
        Unknown,
        /// <summary>
        /// Tenant.
        /// </summary>
        [EnumMember(Value = "Tenant")]
        Tenant,
        /// <summary>
        /// StoragePool.
        /// </summary>
        [EnumMember(Value = "StoragePool")]
        StoragePool,
        /// <summary>
        /// BucketMetadata.
        /// </summary>
        [EnumMember(Value = "BucketMetadata")]
        BucketMetadata,
        /// <summary>
        /// ObjectMetadata.
        /// </summary>
        [EnumMember(Value = "ObjectMetadata")]
        ObjectMetadata,
        /// <summary>
        /// Collection.
        /// </summary>
        [EnumMember(Value = "Collection")]
        Collection,
        /// <summary>
        /// SourceDocument.
        /// </summary>
        [EnumMember(Value = "SourceDocument")]
        SourceDocument,
        /// <summary>
        /// VectorRepository.
        /// </summary>
        [EnumMember(Value = "VectorRepository")]
        VectorRepository,
        /// <summary>
        /// SemanticCell.
        /// </summary>
        [EnumMember(Value = "SemanticCell")]
        SemanticCell,
        /// <summary>
        /// SemanticChunk.
        /// </summary>
        [EnumMember(Value = "SemanticChunk")]
        SemanticChunk
    }
}
