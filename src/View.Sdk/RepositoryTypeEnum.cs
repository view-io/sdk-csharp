namespace View.Sdk
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Repository type.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RepositoryTypeEnum
    {
        /// <summary>
        /// Mysql.
        /// </summary>
        [EnumMember(Value = "Mysql")]
        Mysql,
        /// <summary>
        /// Pgvector.
        /// </summary>
        [EnumMember(Value = "Pgvector")]
        Pgvector
    }
}
