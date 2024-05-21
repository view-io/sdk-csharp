namespace View.Sdk.Shared.Search
{
    using System;

    /// <summary>
    /// Source document statistics.
    /// </summary>
    public class SourceDocumentStatistics
    {
        #region Public-Members

        /// <summary>
        /// Source document.
        /// </summary>
        public SourceDocument SourceDocument
        {
            get
            {
                return _SourceDocument;
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(SourceDocument));
                _SourceDocument = value;
            }
        }
         
        /// <summary>
        /// Term count.
        /// </summary>
        public long TermCount
        { 
            get
            {
                return _TermCount;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(TermCount));
                _TermCount = value;
            }
        }

        /// <summary>
        /// Key-value count.
        /// </summary>
        public long KeyValueCount
        {
            get
            {
                return _KeyValueCount;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(KeyValueCount));
                _KeyValueCount = value;
            }
        }

        #endregion

        #region Private-Members

        private SourceDocument _SourceDocument = null;
        private long _TermCount = 0;
        private long _KeyValueCount = 0;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public SourceDocumentStatistics()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
