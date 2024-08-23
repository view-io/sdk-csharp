namespace View.Sdk
{
    using System;

    /// <summary>
    /// Ingest queue statistics.
    /// </summary>
    public class IngestQueueStatistics
    {
        #region Public-Members

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

        #endregion

        #region Private-Members

        private long _DocumentCount = 0;
        private long _TotalBytes = 0;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public IngestQueueStatistics()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
