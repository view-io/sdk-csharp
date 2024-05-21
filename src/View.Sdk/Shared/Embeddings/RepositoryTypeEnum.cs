namespace View.Sdk.Shared.Embeddings
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
        /// Pgvector.
        /// </summary>
        [EnumMember(Value = "Pgvector")]
        Pgvector
    }
}
