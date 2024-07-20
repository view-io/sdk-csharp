namespace View.Sdk
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Vector search type.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum VectorSearchTypeEnum
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
