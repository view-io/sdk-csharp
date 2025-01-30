namespace View.Sdk
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Software instance type.
    /// </summary>
    public enum SoftwareInstanceTypeEnum
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        [EnumMember(Value = "Unknown")]
        Unknown,
        /// <summary>
        /// ConfigServer.
        /// </summary>
        [EnumMember(Value = "ConfigServer")]
        ConfigServer,
        /// <summary>
        /// DataConnectorServer.
        /// </summary>
        [EnumMember(Value = "DataConnectorServer")]
        DataConnectorServer,
        /// <summary>
        /// DirectorServer.
        /// </summary>
        [EnumMember(Value = "DirectorServer")]
        DirectorServer,
        /// <summary>
        /// DocumentProcessorServer.
        /// </summary>
        [EnumMember(Value = "DocumentProcessorServer")]
        DocumentProcessorServer,
        /// <summary>
        /// EmbeddingsServer.
        /// </summary>
        [EnumMember(Value = "EmbeddingsServer")]
        EmbeddingsServer,
        /// <summary>
        /// LexiServer.
        /// </summary>
        [EnumMember(Value = "LexiServer")]
        LexiServer,
        /// <summary>
        /// OrchestratorServer.
        /// </summary>
        [EnumMember(Value = "OrchestratorServer")]
        OrchestratorServer,
        /// <summary>
        /// ProcessorServer.
        /// </summary>
        [EnumMember(Value = "ProcessorServer")]
        ProcessorServer,
        /// <summary>
        /// SemanticCellExtractorServer.
        /// </summary>
        [EnumMember(Value = "SemanticCellExtractorServer")]
        SemanticCellExtractorServer,
        /// <summary>
        /// StorageServer.
        /// </summary>
        [EnumMember(Value = "StorageServer")]
        StorageServer,
        /// <summary>
        /// SwitchboardServer.
        /// </summary>
        [EnumMember(Value = "SwitchboardServer")]
        SwitchboardServer,
        /// <summary>
        /// VectorServer.
        /// </summary>
        [EnumMember(Value = "VectorServer")]
        VectorServer,
    }
}
