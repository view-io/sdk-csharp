namespace View.Sdk
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Vector repository type.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum VectorRepositoryTypeEnum
    {
        /// <summary>
        /// MySQL.
        /// </summary>
        [EnumMember(Value = "MysqlHeatwave")]
        MysqlHeatwave,
        /// <summary>
        /// Oracle 23AI.
        /// </summary>
        [EnumMember(Value = "Oracle23AI")]
        Oracle23AI,
        /// <summary>
        /// Pgvector.
        /// </summary>
        [EnumMember(Value = "Pgvector")]
        Pgvector,
    }
}
