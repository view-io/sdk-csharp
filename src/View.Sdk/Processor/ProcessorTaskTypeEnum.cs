namespace View.Sdk.Processor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// Processor task type.
    /// </summary>
    public enum ProcessorTaskTypeEnum
    {
        /// <summary>
        /// Success.
        /// </summary>
        [EnumMember(Value = "Completion")]
        Success,

        /// <summary>
        /// Failure.
        /// </summary>
        [EnumMember(Value = "Failure")]
        Failure,

        /// <summary>
        /// Chunking.
        /// </summary>
        [EnumMember(Value = "Chunking")]
        Chunking,

        /// <summary>
        /// EmbeddingsGeneration.
        /// </summary>
        [EnumMember(Value = "EmbeddingsGeneration")]
        EmbeddingsGeneration,

        /// <summary>
        /// FindEmbeddings.
        /// </summary>
        [EnumMember(Value = "FindEmbeddings")]
        FindEmbeddings,

        /// <summary>
        /// Initiate.
        /// </summary>
        [EnumMember(Value = "Initiate")]
        Initiate,

        /// <summary>
        /// PersistGraph.
        /// </summary>
        [EnumMember(Value = "PersistGraph")]
        PersistGraph,

        /// <summary>
        /// PersistUdr.
        /// </summary>
        [EnumMember(Value = "PersistUdr")]
        PersistUdr,

        /// <summary>
        /// PersistEmbeddings.
        /// </summary>
        [EnumMember(Value = "PersistEmbeddings")]
        PersistEmbeddings,

        /// <summary>
        /// SemanticCellExtraction.
        /// </summary>
        [EnumMember(Value = "SemanticCellExtraction")]
        SemanticCellExtraction,

        /// <summary>
        /// TypeDetection.
        /// </summary>
        [EnumMember(Value = "TypeDetection")]
        TypeDetection,

        /// <summary>
        /// UdrGeneration.
        /// </summary>
        [EnumMember(Value = "UdrGeneration")]
        UdrGeneration,
    }
}
