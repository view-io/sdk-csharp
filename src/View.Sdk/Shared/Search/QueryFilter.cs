namespace View.Sdk.Shared.Search
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A filter for the query.
    /// </summary>
    public class QueryFilter
    {
        #region Public-Members

        /// <summary>
        /// Timestamp, in UTC time, after which an entry must have been created.
        /// </summary>
        public DateTime? CreatedAfter { get; set; } = null;

        /// <summary>
        /// Timestamp, in UTC time, before which an entry must have been created.
        /// </summary>
        public DateTime? CreatedBefore { get; set; } = null;

        /// <summary>
        /// List of terms upon which to match.
        /// </summary>
        public List<string> Terms
        {
            get
            {
                return _Terms;
            }
            set
            {
                if (value == null) _Terms = new List<string>();
                else _Terms = value;
            }
        }

        /// <summary>
        /// List of content-types on which to match.
        /// </summary>
        public List<string> MimeTypes
        {
            get
            {
                return _MimeTypes;
            }
            set
            {
                if (value == null) _MimeTypes = new List<string>();
                else _MimeTypes = value;
            }
        }

        /// <summary>
        /// List of key prefix values on which to match.
        /// </summary>
        public List<string> Prefixes
        {
            get
            {
                return _Prefixes;
            }
            set
            {
                if (value == null) _Prefixes = new List<string>();
                else _Prefixes = value;
            }
        }

        /// <summary>
        /// List of key suffix values on which to match.
        /// </summary>
        public List<string> Suffixes
        {
            get
            {
                return _Suffixes;
            }
            set
            {
                if (value == null) _Suffixes = new List<string>();
                else _Suffixes = value;
            }
        }

        /// <summary>
        /// List of schema filters upon which to match.
        /// </summary>
        [JsonPropertyOrder(2)]
        public List<SchemaFilter> SchemaFilters
        {
            get
            {
                return _SchemaFilters;
            }
            set
            {
                if (value == null) _SchemaFilters = new List<SchemaFilter>();
                else _SchemaFilters = value;
            }
        }

        #endregion

        #region Private-Members

        private List<string> _Terms = new List<string>();
        private List<string> _MimeTypes = new List<string>();
        private List<string> _Prefixes = new List<string>();
        private List<string> _Suffixes = new List<string>();
        private List<SchemaFilter> _SchemaFilters = new List<SchemaFilter>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public QueryFilter()
        {
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
