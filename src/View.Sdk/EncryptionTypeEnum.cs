namespace View.Sdk
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Encryption type.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EncryptionTypeEnum
    {
        /// <summary>
        /// No encryption.
        /// </summary>
        [EnumMember(Value = "None")]
        None,
        /// <summary>
        /// Local encryption.
        /// </summary>
        [EnumMember(Value = "Local")]
        Local
    }
}