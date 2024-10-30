namespace View.Sdk
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Crawler state.
    /// </summary>
    public enum CrawlStateEnum
    {
        /// <summary>
        /// Not started.
        /// </summary>
        [EnumMember(Value = "NotStarted")]
        NotStarted,
        /// <summary>
        /// Starting.
        /// </summary>
        [EnumMember(Value = "Starting")]
        Starting,
        /// <summary>
        /// Stopped.
        /// </summary>
        [EnumMember(Value = "Stopped")]
        Stopped,
        /// <summary>
        /// Canceled.
        /// </summary>
        [EnumMember(Value = "Canceled")]
        Canceled,
        /// <summary>
        /// Enumerating.
        /// </summary>
        [EnumMember(Value = "Enumerating")]
        Enumerating,
        /// <summary>
        /// Retrieving.
        /// </summary>
        [EnumMember(Value = "Retrieving")]
        Retrieving,
        /// <summary>
        /// Deleting.
        /// </summary>
        [EnumMember(Value = "Deleting")]
        Deleting,
        /// <summary>
        /// Success.
        /// </summary>
        [EnumMember(Value = "Success")]
        Success,
        /// <summary>
        /// Failed.
        /// </summary>
        [EnumMember(Value = "Failed")]
        Failed
    }
}
