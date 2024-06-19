namespace View.Sdk.Shared.Embeddings
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Order by enumeration.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderByEnum
    {
        /// <summary>
        /// CreatedAscending.
        /// </summary>
        [EnumMember(Value = "CreatedAscending")]
        CreatedAscending,
        /// <summary>
        /// CreatedDescending.
        /// </summary>
        [EnumMember(Value = "CreatedDescending")]
        CreatedDescending
    }
}
