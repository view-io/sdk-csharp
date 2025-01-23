namespace View.Sdk
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Bucket category.
    /// </summary>
    public enum BucketCategoryEnum
    {
        /// <summary>
        /// Data.
        /// </summary>
        [EnumMember(Value = "Data")]
        Data,
        /// <summary>
        /// Metadata.
        /// </summary>
        [EnumMember(Value = "Metadata")]
        Metadata,
        /// <summary>
        /// Embeddings.
        /// </summary>
        [EnumMember(Value = "Embeddings")]
        Embeddings
    }
}