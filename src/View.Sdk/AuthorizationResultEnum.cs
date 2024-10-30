namespace View.Sdk
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Authorization result.
    /// </summary>
    public enum AuthorizationResultEnum
    {
        /// <summary>
        /// Permitted.
        /// </summary>
        [EnumMember(Value = "Permitted")]
        Permitted,
        /// <summary>
        /// DeniedImplicit.
        /// </summary>
        [EnumMember(Value = "DeniedImplicit")]
        DeniedImplicit,
        /// <summary>
        /// DeniedExplicit.
        /// </summary>
        [EnumMember(Value = "DeniedExplicit")]
        DeniedExplicit,
        /// <summary>
        /// NotFound.
        /// </summary>
        [EnumMember(Value = "NotFound")]
        NotFound,
        /// <summary>
        /// Conflict.
        /// </summary>
        [EnumMember(Value = "Conflict")]
        Conflict
    }
}
