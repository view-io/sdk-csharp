namespace View.Sdk
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Webhook event type enumeration.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum WebhookEventTypeEnum
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        [EnumMember(Value = "Unknown")]
        Unknown,

        /// <summary>
        /// ObjectWrite.
        /// </summary>
        [EnumMember(Value = "ObjectWrite")]
        ObjectWrite,
        /// <summary>
        /// UdrDocumentGenerated.
        /// </summary>
        [EnumMember(Value = "UdrDocumentGenerated")]
        UdrDocumentGenerated,
        /// <summary>
        /// EmbeddingsGenerated.
        /// </summary>
        [EnumMember(Value = "EmbeddingsGenerated")]
        EmbeddingsGenerated,
    }
}
