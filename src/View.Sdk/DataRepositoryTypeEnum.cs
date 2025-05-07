namespace View.Sdk
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Data repository type.
    /// </summary>
    public enum DataRepositoryTypeEnum
    {
        /// <summary>
        /// Other.
        /// </summary>
        [EnumMember(Value = "Other")]
        Other,
        /// <summary>
        /// File.
        /// </summary>
        [EnumMember(Value = "File")]
        File,
        /// <summary>
        /// CIFS.
        /// </summary>
        [EnumMember(Value = "CIFS")]
        CIFS,
        /// <summary>
        /// NFS.
        /// </summary>
        [EnumMember(Value = "NFS")]
        NFS,
        /// <summary>
        /// Amazon S3.
        /// </summary>
        [EnumMember(Value = "AmazonS3")]
        AmazonS3,
        /// <summary>
        /// Azure BLOB.
        /// </summary>
        [EnumMember(Value = "AzureBlob")]
        AzureBlob,
        /// <summary>
        /// Web.
        /// </summary>
        [EnumMember(Value = "Web")]
        Web,
        /// <summary>
        /// Printer.
        /// </summary>
        [EnumMember(Value = "Printer")]
        Printer
    }
}
