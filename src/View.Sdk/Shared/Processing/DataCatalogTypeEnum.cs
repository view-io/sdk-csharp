namespace View.Sdk.Shared.Processing
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Data catalog type.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DataCatalogTypeEnum
    {
        /// <summary>
        /// Lexi.
        /// </summary>
        [EnumMember(Value = "Lexi")]
        Lexi,
    }
}
