﻿namespace View.Sdk.Semantic
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Semantic cell request.
    /// </summary>
    public class SemanticCellRequest
    {
        #region Public-Members

        /// <summary>
        /// Document type.
        /// </summary>
        public DocumentTypeEnum DocumentType { get; set; } = DocumentTypeEnum.Unknown;

        /// <summary>
        /// Minimum chunk content length.  Minimum is 1.
        /// </summary>
        public int MinChunkContentLength
        {
            get
            {
                return _MinChunkContentLength;
            }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(MinChunkContentLength));
                _MinChunkContentLength = value;
            }
        }

        /// <summary>
        /// Maximum chunk content length.  Minimum is 256 and maximum is 16384.
        /// </summary>
        public int MaxChunkContentLength
        {
            get
            {
                return _MaxChunkContentLength;
            }
            set
            {
                if (value < 256 || value > 16384) throw new ArgumentOutOfRangeException(nameof(MaxChunkContentLength));
                _MaxChunkContentLength = value;
            }
        }

        /// <summary>
        /// Maximum number of tokens per chunk.
        /// </summary>
        public int MaxTokensPerChunk
        {
            get
            {
                return _MaxTokensPerChunk;
            }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(MaxTokensPerChunk));
                _MaxTokensPerChunk = value;
            }
        }

        /// <summary>
        /// Shift size, used to determine overlap amongst neighboring chunks.
        /// When set to the same value as the maximum chunk content length, no overlap will exist amongst neighboring chunks.
        /// When set to a smaller amount than the maximum chunk content length, overlap will exist amongst neighboring chunks.
        /// This value must be equal to or less than the maximum chunk content length.
        /// </summary>
        public int ShiftSize
        {
            get
            {
                return _ShiftSize;
            }
            set
            {
                if (value > _MaxChunkContentLength) throw new ArgumentException("ShiftSize must be equal to or less than MaxChunkContentLength.");
                _ShiftSize = value;
            }
        }

        /// <summary>
        /// PDF options.
        /// </summary>
        public PdfOptions Pdf
        {
            get
            {
                return _Pdf;
            }
            set
            {
                if (value == null) value = new PdfOptions();
                _Pdf = value;
            }
        }

        /// <summary>
        /// Metadata rule.
        /// </summary>
        public MetadataRule MetadataRule { get; set; } = null;

        /// <summary>
        /// Data.
        /// When serializing, convert to a base64-encoded string.
        /// </summary>
        public byte[] Data { get; set; } = null;

        #endregion

        #region Private-Members

        private int _MinChunkContentLength = 2;
        private int _MaxChunkContentLength = 512;
        private int _MaxTokensPerChunk = 256;
        private int _ShiftSize = 512;
        private PdfOptions _Pdf = new PdfOptions();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public SemanticCellRequest()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
