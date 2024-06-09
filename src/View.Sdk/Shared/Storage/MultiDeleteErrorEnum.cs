namespace View.Sdk.Storage
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Multi delete error codes.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MultiDeleteErrorEnum
    {
        /// <summary>
        /// ObjectNotFound.
        /// </summary>
        [EnumMember(Value = "ObjectNotFound")]
        ObjectNotFound,
        /// <summary>
        /// PoolNotFound.
        /// </summary>
        [EnumMember(Value = "PoolNotFound")]
        PoolNotFound,
        /// <summary>
        /// BucketNotFound.
        /// </summary>
        [EnumMember(Value = "BucketNotFound")]
        BucketNotFound,
        /// <summary>
        /// InternalError.
        /// </summary>
        [EnumMember(Value = "InternalError")]
        InternalError
    }
}
