namespace View.Sdk
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Enumeration item.
    /// </summary>
    public class EnumerationItem
    {
        #region Public-Members

        /// <summary>
        /// Key.
        /// </summary>
        [JsonPropertyName("Key")]
        public string Key { get; set; } = null;

        /// <summary>
        /// Indicates if the item is a folder.
        /// </summary>
        [JsonPropertyName("IsFolder")]
        public bool IsFolder { get; set; } = false;

        /// <summary>
        /// Content length in bytes.
        /// </summary>
        [JsonPropertyName("ContentLength")]
        public long ContentLength { get; set; } = 0;

        /// <summary>
        /// ETag.
        /// </summary>
        [JsonPropertyName("ETag")]
        public string ETag { get; set; } = null;

        /// <summary>
        /// MD5 hash.
        /// </summary>
        [JsonPropertyName("MD5Hash")]
        public string MD5Hash { get; set; } = null;

        /// <summary>
        /// SHA1 hash.
        /// </summary>
        [JsonPropertyName("SHA1Hash")]
        public string SHA1Hash { get; set; } = null;

        /// <summary>
        /// SHA256 hash.
        /// </summary>
        [JsonPropertyName("SHA256Hash")]
        public string SHA256Hash { get; set; } = null;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public EnumerationItem()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
