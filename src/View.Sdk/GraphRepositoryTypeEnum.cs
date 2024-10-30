namespace View.Sdk
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Graph repository type.
    /// </summary>
    public enum GraphRepositoryTypeEnum
    {
        /// <summary>
        /// LiteGraph.
        /// </summary>
        [EnumMember(Value = "LiteGraph")]
        LiteGraph,
    }
}
