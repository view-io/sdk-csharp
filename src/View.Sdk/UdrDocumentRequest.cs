﻿namespace View.Sdk
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// UDR document request.
    /// </summary>
    public class UdrDocumentRequest
    {
        #region Public-Members

        /// <summary>
        /// GUID.
        /// </summary>
        public Guid GUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Document type.
        /// </summary>
        public DocumentTypeEnum Type { get; set; } = DocumentTypeEnum.Unknown;

        /// <summary>
        /// Key.
        /// </summary>
        public string Key { get; set; } = null;

        /// <summary>
        /// Content-type.
        /// </summary>
        public string ContentType { get; set; } = null;

        /// <summary>
        /// Character on which to split semantic cells.
        /// </summary>
        public string SemanticCellSplitCharacter
        {
            get
            {
                return _SemanticCellSplitCharacter;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(SemanticCellSplitCharacter));
                _SemanticCellSplitCharacter = value; 
            }
        }

        /// <summary>
        /// Maximum chunk content length.  Minimum is 128 and maximum is 2048.
        /// </summary>
        public int MaxChunkContentLength
        {
            get
            {
                return _MaxChunkContentLength;
            }
            set
            {
                if (value < 128 || value > 2048) throw new ArgumentOutOfRangeException(nameof(MaxChunkContentLength));
                _MaxChunkContentLength = value;
            }
        }

        /// <summary>
        /// True to include a flattened representation of the source document.
        /// </summary>
        public bool IncludeFlattened { get; set; } = true;

        /// <summary>
        /// True to enable case insensitive processing.
        /// </summary>
        public bool CaseInsensitive { get; set; } = true;

        /// <summary>
        /// Number of top terms to include.
        /// </summary>
        public int TopTerms
        {
            get
            {
                return _TopTerms;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(TopTerms));
                _TopTerms = value;
            }
        }

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
        /// Data.
        /// </summary>
        public byte[] Data
        {
            get
            {
                return _Data;
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(Data));
                _Data = value;
            }
        }

        /// <summary>
        /// Metadata rule.
        /// </summary>
        public MetadataRule MetadataRule { get; set; } = null;

        #endregion

        #region Private-Members

        private int _TopTerms = 10;
        private string _SemanticCellSplitCharacter = "\r\n";
        private int _MaxChunkContentLength = 512;

        private Dictionary<string, object> _Metadata = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
        private byte[] _Data = Array.Empty<byte>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public UdrDocumentRequest()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
