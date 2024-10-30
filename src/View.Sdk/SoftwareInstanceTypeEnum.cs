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
        /// DocumentProcessorServer.
        /// </summary>
        [EnumMember(Value = "DocumentProcessorServer")]
        DocumentProcessorServer,
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
        /// VectorServer.
        /// </summary>
        [EnumMember(Value = "VectorServer")]
        VectorServer,
        /// <summary>
        /// StorageServer.
        /// </summary>
        [EnumMember(Value = "StorageServer")]
        StorageServer,
        /// <summary>
        /// SemanticCellExtractorServer.
        /// </summary>
        [EnumMember(Value = "SemanticCellExtractorServer")]
        SemanticCellExtractorServer
    }
}
