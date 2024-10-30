namespace View.Sdk
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Trigger type.
    /// </summary>
    public enum TriggerTypeEnum
    {
        /// <summary>
        /// HTTP.
        /// </summary>
        [EnumMember(Value = "HTTP")]
        HTTP,
    }
}
