namespace View.Sdk.Shared.Udr
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Timestamps;

    /// <summary>
    /// UDR document.
    /// </summary>
    public class UdrDocument
    {
        #region Public-Members

        /// <summary>
        /// GUID.
        /// </summary>
        public string GUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Indicates if the parser was successful.
        /// </summary>
        public bool Success { get; set; } = false;

        /// <summary>
        /// Exception, if any.
        /// </summary>
        public Exception Exception { get; set; } = null;

        /// <summary>
        /// Start and end timestamps.
        /// </summary>
        public Timestamp Timestamp { get; set; } = new Timestamp();

        /// <summary>
        /// Additional data, not used by the service.
        /// </summary>
        public string AdditionalData { get; set; } = null;

        /// <summary>
        /// Metadata, attached to the result.
        /// </summary>
        public Dictionary<string, object> Metadata
        {
            get
            {
                return _Metadata;
            }
            set
            {
                if (value == null) value = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
                _Metadata = value;
            }
        }

        /// <summary>
        /// Key.
        /// </summary>
        public string Key { get; set; } = null;

        /// <summary>
        /// Document type.
        /// </summary>
        public TypeResult TypeResult { get; set; }

        /// <summary>
        /// Terms identified through text extraction.
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
        /// Top terms and their count.
        /// </summary>
        public Dictionary<string, int> TopTerms
        {
            get
            {
                return GetTopTerms();
            }
        }

        /// <summary>
        /// Postings.
        /// </summary>
        public List<Posting> Postings
        {
            get
            {
                return _Postings;
            }
            set
            {
                if (value == null) _Postings = new List<Posting>();
                else _Postings = value;
            }
        }

        /// <summary>
        /// Schema.
        /// </summary>
        public SchemaResult Schema { get; set; } = null;

        #endregion

        #region Private-Members

        private Dictionary<string, object> _Metadata = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
        private List<string> _Terms = new List<string>();
        private List<Posting> _Postings = new List<Posting>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public UdrDocument()
        {

        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// Retrieve top terms.
        /// </summary>
        /// <param name="count">Number of top terms to retrieve.</param>
        /// <returns>Dictionary containing terms and their counts.</returns>
        public Dictionary<string, int> GetTopTerms(int count = 10)
        {
            if (count < 1) throw new ArgumentOutOfRangeException(nameof(count));

            return Terms
                .GroupBy(s => s)
                .Select(s => new
                {
                    Term = s.Key,
                    Count = s.Count()
                })
                .Where(s => !String.IsNullOrEmpty(s.Term))
                .OrderByDescending(g => g.Count)
                .Take(count)
                .ToDictionary(g => g.Term, g => g.Count);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}
