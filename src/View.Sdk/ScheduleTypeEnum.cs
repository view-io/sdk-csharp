namespace View.Sdk
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Schedule type.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ScheduleTypeEnum
    {
        /// <summary>
        /// Scheduled.
        /// </summary>
        [EnumMember(Value = "Scheduled")]
        Scheduled,
        /// <summary>
        /// Interactive.
        /// </summary>
        [EnumMember(Value = "Interactive")]
        Interactive
    }
}
