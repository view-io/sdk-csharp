namespace View.Sdk
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Enumeration order.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EnumerationOrderEnum
    {
        /// <summary>
        /// CreatedAscending.
        /// </summary>
        [EnumMember(Value = "CreatedAscending")]
        CreatedAscending,
        /// <summary>
        /// CreatedDescending.
        /// </summary>
        [EnumMember(Value = "CreatedDescending")]
        CreatedDescending,
        /// <summary>
        /// LastAccessAscending.
        /// </summary>
        [EnumMember(Value = "LastAccessAscending")]
        LastAccessAscending,
        /// <summary>
        /// LastAccessDescending.
        /// </summary>
        [EnumMember(Value = "LastAccessDescending")]
        LastAccessDescending,
        /// <summary>
        /// KeyAscending.
        /// </summary>
        [EnumMember(Value = "KeyAscending")]
        KeyAscending,
        /// <summary>
        /// KeyDescending.
        /// </summary>
        [EnumMember(Value = "KeyDescending")]
        KeyDescending
    }
}
