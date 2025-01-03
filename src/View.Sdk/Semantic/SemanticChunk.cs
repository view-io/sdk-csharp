namespace View.Sdk.Semantic
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
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
        public string GUID { get; set; } = Guid.NewGuid().ToString();

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
        /// <param name="content">Content.</param>
        /// <param name="embeddings">Embeddings.</param>
        public SemanticChunk(
            int position,
            int start,
            int end,
            string content,
            List<float> embeddings = null)
        {
            Position = position;
            Start = start;
            End = end;
            Content = content;
            Embeddings = embeddings;

            if (!String.IsNullOrEmpty(content))
            {
                Length = content.Length;
                MD5Hash = Convert.ToHexString(Helpers.HashHelper.MD5Hash(Encoding.UTF8.GetBytes(content)));
                SHA1Hash = Convert.ToHexString(Helpers.HashHelper.SHA1Hash(Encoding.UTF8.GetBytes(content)));
                SHA256Hash = Convert.ToHexString(Helpers.HashHelper.SHA256Hash(Encoding.UTF8.GetBytes(content)));
            }
        }

        /// <summary>
        /// Instantiate from DataRow.
        /// </summary>
        /// <param name="row">DataRow.</param>
        /// <param name="serializer">Serializer.</param>
        /// <returns>SemanticChunk.</returns>
        public static SemanticChunk FromDataRow(DataRow row, Serializer serializer)
        {
            if (row == null) return null;
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));

            string chunkGuid = row["chunk_guid"] != null ? row["chunk_guid"].ToString() : null;
            string chunkMd5 = row["chunk_md5"] != null ? row["chunk_md5"].ToString() : null;
            string chunkSha1 = row["chunk_sha1"] != null ? row["chunk_sha1"].ToString() : null;
            string chunkSha256 = row["chunk_sha256"] != null ? row["chunk_sha256"].ToString() : null;
            int chunkPosition = row["chunk_position"] != null ? Convert.ToInt32(row["chunk_position"]) : 0;
            int chunkLength = row["chunk_length"] != null ? Convert.ToInt32(row["chunk_length"]) : 0;
            string content = row["content"] != null ? row["content"].ToString() : null;

            string embeddingsStr = row["embedding"] != null ? row["embedding"].ToString() : null;
            List<float> embeddings = new List<float>();
            if (!String.IsNullOrEmpty(embeddingsStr))
                embeddings = serializer.DeserializeJson<List<float>>(embeddingsStr);

            SemanticChunk chunk = new SemanticChunk
            {
                GUID = chunkGuid,
                MD5Hash = chunkMd5,
                SHA1Hash = chunkSha1,
                SHA256Hash = chunkSha256,
                Position = chunkPosition,
                Length = chunkLength,
                Content = content,
                Embeddings = embeddings
            };

            return chunk;
        }

        /// <summary>
        /// Instantiate from DataTable.
        /// </summary>
        /// <param name="dt">DataTable.</param>
        /// <param name="serializer">Serializer.</param>
        /// <returns>List of SemanticChunk.</returns>
        public static List<SemanticChunk> FromDataTable(DataTable dt, Serializer serializer)
        {
            if (dt == null) return null;
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));

            List<SemanticChunk> ret = new List<SemanticChunk>();
            foreach (DataRow row in dt.Rows)
                ret.Add(FromDataRow(row, serializer));

            return ret;
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
