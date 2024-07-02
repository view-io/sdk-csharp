namespace View.Sdk
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// HTTP method enum.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum HttpMethodEnum
    {
        /// <summary>
        /// UNKNOWN.
        /// </summary>
        [EnumMember(Value = "UNKNOWN")]
        UNKNOWN,
        /// <summary>
        /// GET.
        /// </summary>
        [EnumMember(Value = "GET")]
        GET,
        /// <summary>
        /// PUT.
        /// </summary>
        [EnumMember(Value = "PUT")]
        PUT,
        /// <summary>
        /// POST.
        /// </summary>
        [EnumMember(Value = "POST")]
        POST,
        /// <summary>
        /// DELETE.
        /// </summary>
        [EnumMember(Value = "DELETE")]
        DELETE,
        /// <summary>
        /// PATCH.
        /// </summary>
        [EnumMember(Value = "PATCH")]
        PATCH,
    }
}
