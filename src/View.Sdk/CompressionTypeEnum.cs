namespace View.Sdk
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Compression type.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CompressionTypeEnum
    {
        /// <summary>
        /// None.
        /// </summary>
        [EnumMember(Value = "None")]
        None,
        /// <summary>
        /// Gzip.
        /// </summary>
        [EnumMember(Value = "Gzip")]
        Gzip,
        /// <summary>
        /// Deflate.
        /// </summary>
        [EnumMember(Value = "Deflate")]
        Deflate,
        /// <summary>
        /// Adaptive.
        /// </summary>
        [EnumMember(Value = "Adaptive")]
        Adaptive
    }
}
