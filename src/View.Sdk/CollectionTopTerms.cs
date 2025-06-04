namespace View.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Collection top terms.
    /// </summary>
    public class CollectionTopTerms
    {
        #region Public-Members

        /// <summary>
        /// Dictionary of terms and their frequencies.
        /// </summary>
        [JsonExtensionData]
        public Dictionary<string, object> Terms { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Helper method to get term frequency as integer
        /// </summary>
        /// <param name="term">The term to get frequency for</param>
        /// <returns>The frequency as integer, or 0 if not found</returns>
        public int GetTermFrequency(string term)
        {
            if (Terms.TryGetValue(term, out object value))
            {
                if (value is JsonElement element)
                {
                    return element.ValueKind == System.Text.Json.JsonValueKind.Number ? element.GetInt32() : 0;
                }
                else if (value is int intValue)
                {
                    return intValue;
                }
            }
            return 0;
        }

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public CollectionTopTerms()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}