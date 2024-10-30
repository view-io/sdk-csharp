namespace View.Sdk.Vector
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Vector search type.
    /// </summary>
    public enum VectorSearchTypeEnum
    {
        // https://docs.timescale.com/ai/latest/key-vector-database-concepts-for-understanding-pgvector/

        /// <summary>
        /// InnerProduct.
        /// </summary>
        [EnumMember(Value = "InnerProduct")]
        InnerProduct,
        /// <summary>
        /// CosineDistance.
        /// </summary>
        [EnumMember(Value = "CosineDistance")]
        CosineDistance,
        /// <summary>
        /// L2Distance.
        /// </summary>
        [EnumMember(Value = "L2Distance")]
        L2Distance,
    }
}
