namespace View.Sdk.Shared.Embeddings
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Search type.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SearchTypeEnum
    {
        /// <summary>
        /// InnerProduct.
        /// </summary>
        [EnumMember(Value = "InnerProduct")]
        InnerProduct,
        /// <summary>
        /// Cosine.
        /// </summary>
        [EnumMember(Value = "Cosine")]
        Cosine,
        /// <summary>
        /// NearestNeighbor.
        /// </summary>
        [EnumMember(Value = "NearestNeighbor")]
        NearestNeighbor,
    }
}
