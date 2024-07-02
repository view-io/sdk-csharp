namespace View.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Schema result.
    /// </summary>
    public class SchemaResult
    {
        #region Public-Members

        /// <summary>
        /// Data type.
        /// </summary>
        public DocumentTypeEnum Type { get; set; } = DocumentTypeEnum.Unknown;

        /// <summary>
        /// Number of rows.
        /// </summary>
        public int? Rows
        {
            get
            {
                return _Rows;
            }
            set
            {
                if (value != null && value.Value < 0) throw new ArgumentOutOfRangeException(nameof(Rows));
                _Rows = value;
            }
        }

        /// <summary>
        /// Number of columns.
        /// </summary>
        public int? Columns
        {
            get
            {
                return _Columns;
            }
            set
            {
                if (value != null && value.Value < 0) throw new ArgumentOutOfRangeException(nameof(Columns));
                _Columns = value;
            }
        }

        /// <summary>
        /// Flag to indicate if the geometry of the object is irregular.
        /// </summary>
        public bool? Irregular { get; set; } = null;

        /// <summary>
        /// Maximum depth observed in the object.
        /// </summary>
        public int? MaxDepth
        {
            get
            {
                return _MaxDepth;
            }
            set
            {
                if (value != null && value.Value < 0) throw new ArgumentOutOfRangeException(nameof(MaxDepth));
                _MaxDepth = value;
            }
        }

        /// <summary>
        /// Number of objects.
        /// </summary>
        public int? NumObjects
        {
            get
            {
                return _NumObjects;
            }
            set
            {
                if (value != null && value.Value < 0) throw new ArgumentOutOfRangeException(nameof(NumObjects));
                _NumObjects = value;
            }
        }

        /// <summary>
        /// Number of arrays.
        /// </summary>
        public int? NumArrays
        {
            get
            {
                return _NumArrays;
            }
            set
            {
                if (value != null && value.Value < 0) throw new ArgumentOutOfRangeException(nameof(NumArrays));
                _NumArrays = value;
            }
        }

        /// <summary>
        /// Number of key values.
        /// </summary>
        public int? NumKeyValues
        {
            get
            {
                return _NumKeyValues;
            }
            set
            {
                if (value != null && value.Value < 0) throw new ArgumentOutOfRangeException(nameof(NumKeyValues));
                _NumKeyValues = value;
            }
        }

        /// <summary>
        /// Schema of the document.
        /// </summary>
        [JsonPropertyOrder(10)]
        public Dictionary<string, DataTypeEnum> Schema
        {
            get
            {
                return _Schema;
            }
            set
            {
                if (value == null) _Schema = new Dictionary<string, DataTypeEnum>(StringComparer.InvariantCultureIgnoreCase);
                else _Schema = value;
            }
        }

        /// <summary>
        /// Metadata.
        /// </summary>
        [JsonPropertyOrder(11)]
        public Dictionary<string, object> Metadata
        {
            get
            {
                return _Metadata;
            }
            set
            {
                if (value == null) _Metadata = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
                else _Metadata = value;
            }
        }

        /// <summary>
        /// Flattened representation of the object.
        /// </summary>
        [JsonPropertyOrder(12)]
        public List<DataNode> Flattened
        {
            get
            {
                return _Flattened;
            }
            set
            {
                if (value == null) _Flattened = new List<DataNode>();
                else _Flattened = value;
            }
        }

        #endregion

        #region Private-Members

        private int? _Rows = null;
        private int? _Columns = null;
        private int? _MaxDepth = null;
        private int? _NumObjects = null;
        private int? _NumArrays = null;
        private int? _NumKeyValues = null;
        private Dictionary<string, object> _Metadata = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
        private Dictionary<string, DataTypeEnum> _Schema = new Dictionary<string, DataTypeEnum>(StringComparer.InvariantCultureIgnoreCase);
        private List<DataNode> _Flattened = new List<DataNode>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public SchemaResult()
        {
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
