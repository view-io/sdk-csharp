namespace View.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using Timestamps;
    using View.Sdk;

    /// <summary>
    /// Object returned as the result of an enumeration against a bucket.
    /// </summary>
    public class BucketEnumerationResult
    {
        #region Public-Members

        /// <summary>
        /// Indicates if the statistics operation was successful.
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// Start and end timestamps.
        /// </summary>
        public Timestamp Timestamp { get; set; } = new Timestamp();

        /// <summary>
        /// Maximum number of results to retrieve.
        /// </summary>
        public int MaxResults
        {
            get
            {
                return _MaxResults;
            }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(MaxResults));
                _MaxResults = value;
            }
        }

        /// <summary>
        /// Iterations required.
        /// </summary>
        public int IterationsRequired
        {
            get
            {
                return _IterationsRequired;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(IterationsRequired));
                _IterationsRequired = value;
            }
        }

        /// <summary>
        /// Object statistics.
        /// </summary>
        public ObjectStatistics Statistics { get; set; } = null;

        /// <summary>
        /// Continuation token.
        /// </summary>
        public string ContinuationToken { get; set; } = null;

        /// <summary>
        /// Shared prefixes.
        /// </summary>
        public List<string> SharedPrefixes { get; set; } = null;

        /// <summary>
        /// Object metadata.
        /// </summary>
        public List<ObjectMetadata> Objects { get; set; } = null;

        /// <summary>
        /// Delete markers.
        /// </summary>
        public List<ObjectMetadata> DeleteMarkers { get; set; } = null;

        /// <summary>
        /// Boolean indicating end of results.
        /// </summary>
        public bool EndOfResults { get; set; } = true;

        /// <summary>
        /// Number of candidate records remaining in the enumeration.
        /// </summary>
        public long RecordsRemaining
        {
            get
            {
                return _RecordsRemaining;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(RecordsRemaining));
                _RecordsRemaining = value;
            }
        }

        #endregion

        #region Private-Members

        private int _MaxResults = 1000;
        private int _IterationsRequired = 0;
        private long _RecordsRemaining = 0;
        private ObjectStatistics _Statistics = new ObjectStatistics();
        private List<ObjectMetadata> _Objects = new List<ObjectMetadata>();
        private List<ObjectMetadata> _DeleteMarkers = new List<ObjectMetadata>();
        private List<string> _SharedPrefixes = new List<string>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiates the object.
        /// </summary>
        public BucketEnumerationResult()
        {
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion 
    }
}
