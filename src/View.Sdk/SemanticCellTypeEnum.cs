namespace View.Sdk
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Semantic cell type.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SemanticCellTypeEnum
    {
        /// <summary>
        /// Text.
        /// </summary>
        [EnumMember(Value = "Text")]
        Text,
    }
}
