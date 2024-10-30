namespace View.Sdk
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Types of data supported.
    /// </summary>
    public enum DataTypeEnum
    {
        /// <summary>
        /// Object.
        /// </summary>
        [EnumMember(Value = "Object")]
        Object,
        /// <summary>
        /// Array.
        /// </summary>
        [EnumMember(Value = "Array")]
        Array,
        /// <summary>
        /// Timestamp.
        /// </summary>
        [EnumMember(Value = "Timestamp")]
        Timestamp,
        /// <summary>
        /// Integer.
        /// </summary>
        [EnumMember(Value = "Integer")]
        Integer,
        /// <summary>
        /// Long.
        /// </summary>
        [EnumMember(Value = "Long")]
        Long,
        /// <summary>
        /// Decimal.
        /// </summary>
        [EnumMember(Value = "Decimal")]
        Decimal,
        /// <summary>
        /// Double.
        /// </summary>
        [EnumMember(Value = "Double")]
        Double,
        /// <summary>
        /// Float.
        /// </summary>
        [EnumMember(Value = "Float")]
        Float,
        /// <summary>
        /// String.
        /// </summary>
        [EnumMember(Value = "String")]
        String,
        /// <summary>
        /// Boolean.
        /// </summary>
        [EnumMember(Value = "Boolean")]
        Boolean,
        /// <summary>
        /// Binary.
        /// </summary>
        [EnumMember(Value = "Binary")]
        Binary,
        /// <summary>
        /// Null.
        /// </summary>
        [EnumMember(Value = "Null")]
        Null,
        /// <summary>
        /// Unknown.
        /// </summary>
        [EnumMember(Value = "Unknown")]
        Unknown
    }
}
