namespace View.Sdk
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Data catalog type.
    /// </summary>
    public enum DataCatalogTypeEnum
    {
        /// <summary>
        /// Lexi.
        /// </summary>
        [EnumMember(Value = "Lexi")]
        Lexi,
    }
}
