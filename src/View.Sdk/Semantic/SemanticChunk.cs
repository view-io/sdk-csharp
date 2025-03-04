namespace View.Sdk.Semantic
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using View.Sdk.Helpers;
    using View.Sdk.Serialization;

    /// <summary>
    /// Semantic chunk.
    /// </summary>
    public class SemanticChunk
    {
        #region Public-Members

        /// <summary>
        /// GUID.
        /// </summary>
        public Guid GUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// MD5.
        /// </summary>
        public string MD5Hash { get; set; } = null;

        /// <summary>
        /// SHA1.
        /// </summary>
        public string SHA1Hash { get; set; } = null;

        /// <summary>
        /// SHA256.
        /// </summary>
        public string SHA256Hash { get; set; } = null;

        /// <summary>
        /// Position.
        /// </summary>
        public int Position
        {
            get
            {
                return _Position;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(Position));
                _Position = value;
            }
        }

        /// <summary>
        /// Start position.
        /// </summary>
        public int Start
        {
            get
            {
                return _Start;
            }
            set
            {
                if (value < 0) throw new ArgumentException("Start must be zero or greater.");
                _Start = value;
            }
        }

        /// <summary>
        /// End position.
        /// </summary>
        public int End
        {
            get
            {
                return _End;
            }
            set
            {
                if (value < 0) throw new ArgumentException("End must be zero or greater.");
                _End = value;
            }
        }

        /// <summary>
        /// Length.
        /// </summary>
        public int Length
        {
            get
            {
                return _Length;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(Length));
                _Length = value;
            }
        }

        /// <summary>
        /// Binary data.
        /// </summary>
        public byte[] Binary
        {
            get
            {
                return _Binary;
            }
            set
            {
                if (value != null) _Length = value.Length;
                _Binary = value;
            }
        }

        /// <summary>
        /// Content.
        /// </summary>
        public string Content
        {
            get
            {
                return _Content;
            }
            set
            {
                if (!String.IsNullOrEmpty(value)) _Length = value.Length;
                _Content = value;
            }
        }

        /// <summary>
        /// Embeddings.
        /// </summary>
        public List<float> Embeddings
        {
            get
            {
                return _Embeddings;
            }
            set
            {
                if (value == null) value = new List<float>();
                else _Embeddings = value;
            }
        }

        #endregion

        #region Private-Members

        private int _Position = 0;
        private int _Start = 0;
        private int _End = 0;
        private int _Length = 0;
        private List<float> _Embeddings = new List<float>();
        private byte[] _Binary = null;
        private string _Content = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public SemanticChunk()
        {

        }

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="position">Position.</param>
        /// <param name="start">Start.</param>
        /// <param name="end">End.</param>
        /// <param name="content">Text content.</param>
        /// <param name="binary">Binary content.</param>
        /// <param name="embeddings">Embeddings.</param>
        public SemanticChunk(
            int position,
            int start,
            int end,
            string content,
            byte[] binary,
            List<float> embeddings = null)
        {
            Position = position;
            Start = start;
            End = end;
            Content = content;
            Binary = binary;
            Embeddings = embeddings;

            if (!String.IsNullOrEmpty(Content))
            {
                Length = Content.Length;
                MD5Hash = Convert.ToHexString(Helpers.HashHelper.MD5Hash(Encoding.UTF8.GetBytes(Content)));
                SHA1Hash = Convert.ToHexString(Helpers.HashHelper.SHA1Hash(Encoding.UTF8.GetBytes(Content)));
                SHA256Hash = Convert.ToHexString(Helpers.HashHelper.SHA256Hash(Encoding.UTF8.GetBytes(Content)));
            }
            else if (Binary != null && Binary.Length > 0)
            {
                Length = Binary.Length;
                MD5Hash = Convert.ToHexString(Helpers.HashHelper.MD5Hash(Binary));
                SHA1Hash = Convert.ToHexString(Helpers.HashHelper.SHA1Hash(Binary));
                SHA256Hash = Convert.ToHexString(Helpers.HashHelper.SHA256Hash(Binary));
            }
        }

        /// <summary>
        /// Instantiate from DataRow.
        /// </summary>
        /// <param name="row">DataRow.</param>
        /// <returns>SemanticChunk.</returns>
        public static SemanticChunk FromDataRow(DataRow row)
        {
            if (row == null) return null;

            Serializer serializer = new Serializer();

            string embeddingsStr = DataTableHelper.GetStringValue(row, "embedding");
            List<float> embeddings = new List<float>();
            if (!String.IsNullOrEmpty(embeddingsStr))
                embeddings = serializer.DeserializeJson<List<float>>(embeddingsStr);

            SemanticChunk chunk = new SemanticChunk
            {
                GUID = DataTableHelper.GetGuidValue(row, "chunk_guid"),
                MD5Hash = DataTableHelper.GetStringValue(row, "chunk_md5"),
                SHA1Hash = DataTableHelper.GetStringValue(row, "chunk_sha1"),
                SHA256Hash = DataTableHelper.GetStringValue(row, "chunk_sha256"),
                Position = DataTableHelper.GetInt32Value(row, "chunk_position"),
                Length = DataTableHelper.GetInt32Value(row, "chunk_length"),
                Content = DataTableHelper.GetStringValue(row, "content"),
                Binary = DataTableHelper.GetNullableBinaryValue(row, "binary_content"),
                Embeddings = embeddings
            };

            return chunk;
        }

        /// <summary>
        /// Instantiate from DataTable.
        /// </summary>
        /// <param name="dt">DataTable.</param>
        /// <returns>List of SemanticChunk.</returns>
        public static List<SemanticChunk> FromDataTable(DataTable dt)
        {
            if (dt == null) return null;

            List<SemanticChunk> ret = new List<SemanticChunk>();
            foreach (DataRow row in dt.Rows)
                ret.Add(FromDataRow(row));

            return ret;
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
