namespace View.Sdk
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// API versions.
    /// </summary>
    public enum ApiVersionEnum
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        [EnumMember(Value = "Unknown")]
        Unknown,
        /// <summary>
        /// V1.0.
        /// </summary>
        [EnumMember(Value = "v1.0")]
        V1_0,
        /// <summary>
        /// V2.0.
        /// </summary>
        [EnumMember(Value = "v2.0")]
        V2_0
    }
}