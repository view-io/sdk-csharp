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
    }
}
