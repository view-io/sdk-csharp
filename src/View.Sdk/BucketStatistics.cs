namespace View.Sdk
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Bucket statistics.
    /// </summary>
    public class BucketStatistics
    {
        #region Public-Members

        /// <summary>
        /// Bucket.
        /// </summary>
        public BucketMetadata Bucket
        {
            get
            {
                return _Bucket;
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(Bucket));
                _Bucket = value;
            }
        }

        /// <summary>
        /// Number of objects.
        /// </summary>
        public long ObjectCount
        {
            get
            {
                return _ObjectCount;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(ObjectCount));
                _ObjectCount = value;
            }
        }

        /// <summary>
        /// Number of versions.
        /// </summary>
        public long VersionCount
        {
            get
            {
                return _VersionCount;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(VersionCount));
                _VersionCount = value;
            }
        }

        /// <summary>
        /// Number of delete markers.
        /// </summary>
        public long DeleteMarkers
        {
            get
            {
                return _DeleteMarkers;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(DeleteMarkers));
                _DeleteMarkers = value;
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

        private BucketMetadata _Bucket = null;
        private long _ObjectCount = 0;
        private long _VersionCount = 0;
        private long _DeleteMarkers = 0;
        private long _TotalBytes = 0;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public BucketStatistics()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
