namespace View.Sdk.Semantic
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Semantic cell type.
    /// </summary>
    public enum SemanticCellTypeEnum
    {
        /// <summary>
        /// Text.
        /// </summary>
        [EnumMember(Value = "Text")]
        Text,
        /// <summary>
        /// Table.
        /// </summary>
        [EnumMember(Value = "Table")]
        Table,
        /// <summary>
        /// List.
        /// </summary>
        [EnumMember(Value = "List")]
        List,
        /// <summary>
        /// Binary.
        /// </summary>
        [EnumMember(Value = "Binary")]
        Binary
    }
}
