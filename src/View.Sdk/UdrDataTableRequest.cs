namespace View.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// UDR data table processing request.
    /// </summary>
    public class UdrDataTableRequest
    {
        #region Public-Members

        /// <summary>
        /// GUID.
        /// </summary>
        public Guid GUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Database type.
        /// </summary>
        public string DatabaseType
        {
            get
            {
                return _DatabaseType;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(DatabaseType));
                if (!_ValidDatabaseTypes.Contains(value)) throw new ArgumentException("Unknown database type '" + value + "'.");
                _DatabaseType = value;
            }
        }

        /// <summary>
        /// Hostname.
        /// </summary>
        public string Hostname { get; set; } = null;

        /// <summary>
        /// Port.
        /// </summary>
        public int Port
        {
            get
            {
                return _Port;
            }
            set
            {
                if (value < 0 || value > 65535) throw new ArgumentOutOfRangeException(nameof(Port));
                _Port = value;
            }
        }

        /// <summary>
        /// Username.
        /// </summary>
        public string Username { get; set; } = null;

        /// <summary>
        /// Password.
        /// </summary>
        public string Password { get; set; } = null;

        /// <summary>
        /// Database name.
        /// </summary>
        public string DatabaseName { get; set; } = null;

        /// <summary>
        /// Query.
        /// </summary>
        public string Query { get; set; } = null;

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
        /// Sqlite file data.
        /// </summary>
        public byte[] SqliteFileData
        {
            get
            {
                return _SqliteFileData;
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(SqliteFileData));
                _SqliteFileData = value;
            }
        }

        #endregion

        #region Private-Members

        private int _TopTerms = 10;
        private int _Port = 0;
        private Dictionary<string, object> _Metadata = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
        private byte[] _SqliteFileData = Array.Empty<byte>();
        private string _DatabaseType = "Sqlite";

        private List<string> _ValidDatabaseTypes = new List<string>
        {
            "Mysql",
            "Postgresql",
            "SqlServer",
            "Sqlite"
        };

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public UdrDataTableRequest()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
