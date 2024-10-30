namespace View.Sdk
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Specify whether the object key or GUID is used to write to the storage pool as the object identifier.
    /// </summary>
    public enum ObjectWriteModeEnum
    {
        /// <summary>
        /// Object key.
        /// </summary>
        [EnumMember(Value = "KEY")]
        KEY,
        /// <summary>
        /// Object GUID.
        /// </summary>
        [EnumMember(Value = "GUID")]
        GUID
    }
}