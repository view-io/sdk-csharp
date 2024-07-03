namespace View.Sdk
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Authentication result.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AuthenticationResultEnum
    {
        /// <summary>
        /// Success.
        /// </summary>
        [EnumMember(Value = "Success")]
        Success,
        /// <summary>
        /// NotFound.
        /// </summary>
        [EnumMember(Value = "NotFound")]
        NotFound,
        /// <summary>
        /// Inactive.
        /// </summary>
        [EnumMember(Value = "Inactive")]
        Inactive,
        /// <summary>
        /// Invalid.
        /// </summary>
        [EnumMember(Value = "Invalid")]
        Invalid
    }
}
