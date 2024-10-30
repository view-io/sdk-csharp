namespace View.Sdk
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Schedule interval enum.
    /// </summary>
    public enum ScheduleIntervalEnum
    {
        /// <summary>
        /// OneTime.
        /// </summary>
        [EnumMember(Value = "OneTime")]
        OneTime,
        /// <summary>
        /// SecondsInterval.
        /// </summary>
        [EnumMember(Value = "SecondsInterval")]
        SecondsInterval,
        /// <summary>
        /// MinutesInterval.
        /// </summary>
        [EnumMember(Value = "MinutesInterval")]
        MinutesInterval,
        /// <summary>
        /// HoursInterval.
        /// </summary>
        [EnumMember(Value = "HoursInterval")]
        HoursInterval,
        /// <summary>
        /// DaysInterval.
        /// </summary>
        [EnumMember(Value = "DaysInterval")]
        DaysInterval
    }
}
