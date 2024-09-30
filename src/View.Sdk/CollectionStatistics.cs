namespace View.Sdk
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Collection statistics.
    /// </summary>
    public class CollectionStatistics
    {
        #region Public-Members

        /// <summary>
        /// Collection.
        /// </summary>
        public Collection Collection
        {
            get
            {
                return _Collection;
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(Collection));
                _Collection = value;
            }
        }

        /// <summary>
        /// Number of source documents.
        /// </summary>
        public long DocumentCount
        {
            get
            {
                return _DocumentCount;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(DocumentCount));
                _DocumentCount = value;
            }
        }

        /// <summary>
        /// Total number of bytes.
        /// </summary>
        public long TotalBytes
        {
            get
            {
                return _TotalBytes;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(TotalBytes));
                _TotalBytes = value;
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

        private Collection _Collection = null;
        private long _DocumentCount = 0;
        private long _TotalBytes = 0;
        private long _TermCount = 0;
        private long _KeyValueCount = 0;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public CollectionStatistics()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
