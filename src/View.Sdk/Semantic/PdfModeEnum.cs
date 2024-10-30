namespace View.Sdk.Semantic
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// PDF mode.
    /// </summary>
    public enum PdfModeEnum
    {
        /// <summary>
        /// FlatTextExtraction.
        /// </summary>
        [EnumMember(Value = "FlatTextExtraction")]
        FlatTextExtraction,
        /// <summary>
        /// BoundingBoxExtraction.
        /// </summary>
        [EnumMember(Value = "BoundingBoxExtraction")]
        BoundingBoxExtraction
    }
}
