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
    /// Processor task state.
    /// </summary>
    public enum ProcessorTaskStateEnum
    {
        /// <summary>
        /// TypeDetection.
        /// </summary>
        [EnumMember(Value = "TypeDetection")]
        TypeDetection,

        /// <summary>
        /// Atomization.
        /// </summary>
        [EnumMember(Value = "Atomization")]
        Atomization,

        /// <summary>
        /// SemanticCellExtraction.
        /// </summary>
        [EnumMember(Value = "SemanticCellExtraction")]
        SemanticCellExtraction,

        /// <summary>
        /// UdrGeneration.
        /// </summary>
        [EnumMember(Value = "UdrGeneration")]
        UdrGeneration,

        /// <summary>
        /// EmbeddingsGeneration.
        /// </summary>
        [EnumMember(Value = "EmbeddingsGeneration")]
        EmbeddingsGeneration,

        /// <summary>
        /// PersistUdr.
        /// </summary>
        [EnumMember(Value = "PersistUdr")]
        PersistUdr,

        /// <summary>
        /// PersistVector.
        /// </summary>
        [EnumMember(Value = "PersistVector")]
        PersistVector,

        /// <summary>
        /// PersistGraph.
        /// </summary>
        [EnumMember(Value = "PersistGraph")]
        PersistGraph,

        /// <summary>
        /// Success.
        /// </summary>
        [EnumMember(Value = "Success")]
        Success,

        /// <summary>
        /// Failure.
        /// </summary>
        [EnumMember(Value = "Failure")]
        Failure
    }
}
