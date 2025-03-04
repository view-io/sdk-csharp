namespace View.Sdk
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Web authentication type.
    /// </summary>
    public enum WebAuthenticationTypeEnum
    {
        /// <summary>
        /// None.
        /// </summary>
        [EnumMember(Value = "None")]
        None,
        /// <summary>
        /// Basic.
        /// </summary>
        [EnumMember(Value = "Basic")]
        Basic,
        /// <summary>
        /// API key.
        /// </summary>
        [EnumMember(Value = "ApiKey")]
        ApiKey,
        /// <summary>
        /// Bearer token.
        /// </summary>
        [EnumMember(Value = "BearerToken")]
        BearerToken
    }
}
