namespace View.Sdk.Storage
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Part metadata for multipart uploads.
    /// </summary>
    public class PartMetadata
    {
        #region Public-Members

        /// <summary>
        /// Part number.
        /// </summary>
        public int PartNumber { get; set; } = 1;

        /// <summary>
        /// ETag of the part.
        /// </summary>
        public string ETag { get; set; } = string.Empty;

        /// <summary>
        /// Size of the part in bytes.
        /// </summary>
        public long Size { get; set; } = 0;

        /// <summary>
        /// Last modified UTC time.
        /// </summary>
        public DateTime LastModifiedUtc { get; set; } = DateTime.UtcNow;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public PartMetadata()
        {

        }

        /// <summary>
        /// Instantiate with part number.
        /// </summary>
        /// <param name="partNumber">Part number.</param>
        public PartMetadata(int partNumber)
        {
            if (partNumber < 1) throw new ArgumentOutOfRangeException(nameof(partNumber));
            PartNumber = partNumber;
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}