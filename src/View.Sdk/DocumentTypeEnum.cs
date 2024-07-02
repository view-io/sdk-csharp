namespace View.Sdk
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Data type associated with an input object or file.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DocumentTypeEnum
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        [EnumMember(Value = "Unknown")]
        Unknown,
        /// <summary>
        /// CSV.
        /// </summary>
        [EnumMember(Value = "Csv")]
        Csv,
        /// <summary>
        /// DataTable.
        /// </summary>
        [EnumMember(Value = "DataTable")]
        DataTable,
        /// <summary>
        /// DOCX, Word document.
        /// </summary>
        [EnumMember(Value = "Docx")]
        Docx,
        /// <summary>
        /// HTML.
        /// </summary>
        [EnumMember(Value = "Html")]
        Html,
        /// <summary>
        /// JSON.
        /// </summary>
        [EnumMember(Value = "Json")]
        Json,
        /// <summary>
        /// Parquet.
        /// </summary>
        [EnumMember(Value = "Parquet")]
        Parquet,
        /// <summary>
        /// PDF.
        /// </summary>
        [EnumMember(Value = "Pdf")]
        Pdf,
        /// <summary>
        /// PPTX, PowerPoint presentation.
        /// </summary>
        [EnumMember(Value = "Pptx")]
        Pptx,
        /// <summary>
        /// Sqlite database file.
        /// </summary>
        [EnumMember(Value = "Sqlite")]
        Sqlite,
        /// <summary>
        /// Text.
        /// </summary>
        [EnumMember(Value = "Text")]
        Text,
        /// <summary>
        /// XLSX, Excel spreadsheet.
        /// </summary> 
        [EnumMember(Value = "Xlsx")]
        Xlsx,
        /// <summary>
        /// XML.
        /// </summary>
        [EnumMember(Value = "Xml")]
        Xml
    }
}
