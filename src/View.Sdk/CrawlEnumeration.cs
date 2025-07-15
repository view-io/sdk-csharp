namespace View.Sdk
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Crawl enumeration result.
    /// </summary>
    public class CrawlEnumeration
    {
        #region Public-Members

        /// <summary>
        /// Current enumeration items.
        /// </summary>
        [JsonPropertyName("CurrentEnumeration")]
        public List<EnumerationItem> CurrentEnumeration { get; set; } = new List<EnumerationItem>();

        /// <summary>
        /// Deleted items.
        /// </summary>
        [JsonPropertyName("Deleted")]
        public List<EnumerationItem> Deleted { get; set; } = new List<EnumerationItem>();

        /// <summary>
        /// Added items.
        /// </summary>
        [JsonPropertyName("Added")]
        public List<EnumerationItem> Added { get; set; } = new List<EnumerationItem>();

        /// <summary>
        /// Changed items.
        /// </summary>
        [JsonPropertyName("Changed")]
        public List<EnumerationItem> Changed { get; set; } = new List<EnumerationItem>();

        /// <summary>
        /// Unchanged items.
        /// </summary>
        [JsonPropertyName("Unchanged")]
        public List<EnumerationItem> Unchanged { get; set; } = new List<EnumerationItem>();

        /// <summary>
        /// Successfully processed items.
        /// </summary>
        [JsonPropertyName("Success")]
        public List<EnumerationItem> Success { get; set; } = new List<EnumerationItem>();

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public CrawlEnumeration()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
