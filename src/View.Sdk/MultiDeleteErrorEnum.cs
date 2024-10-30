namespace View.Sdk
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Multi delete error codes.
    /// </summary>
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
