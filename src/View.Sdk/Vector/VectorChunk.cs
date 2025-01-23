namespace View.Sdk.Vector
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.Text.Json.Serialization;
    using Timestamps;
    using View.Sdk.Serialization;
    using View.Sdk;
    using Microsoft.VisualBasic;
    using View.Sdk.Semantic;

    /// <summary>
    /// Vector chunk.
    /// </summary>
    public class VectorChunk
    {
        #region Public-Members

        /// <summary>
        /// Document GUID.
        /// </summary>
        public string DocumentGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public string TenantGUID { get; set; } = null;

        /// <summary>
        /// Collection GUID.
        /// </summary>
        public string CollectionGUID { get; set; } = null;

        /// <summary>
        /// Source document GUID.
        /// </summary>
        public string SourceDocumentGUID { get; set; } = null;

        /// <summary>
        /// Bucket GUID.
        /// </summary>
        public string BucketGUID { get; set; } = null;

        /// <summary>
        /// Vector repository GUID.
        /// </summary>
        public string VectorRepositoryGUID { get; set; } = null;

        /// <summary>
        /// Graph repository GUID.
        /// </summary>
        public string GraphRepositoryGUID { get; set; } = null;

        /// <summary>
        /// Graph node identifier.
        /// </summary>
        public string GraphNodeIdentifier { get; set; } = null;

        /// <summary>
        /// Object GUID.
        /// </summary>
        public string ObjectGUID { get; set; } = null;

        /// <summary>
        /// Object key.
        /// </summary>
        public string ObjectKey { get; set; } = null;

        /// <summary>
        /// Object version.
        /// </summary>
        public string ObjectVersion { get; set; } = null;

        /// <summary>
        /// Model.
        /// </summary>
        public string Model { get; set; } = null;

        /// <summary>
        /// GUID.
        /// </summary>
        public string CellGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Semantic cell type.
        /// </summary>
        public SemanticCellTypeEnum CellType { get; set; } = SemanticCellTypeEnum.Text;

        /// <summary>
        /// Cell MD5.
        /// </summary>
        public string CellMD5Hash { get; set; } = string.Empty;

        /// <summary>
        /// Cell SHA1.
        /// </summary>
        public string CellSHA1Hash { get; set; } = null;

        /// <summary>
        /// Cell SHA256.
        /// </summary>
        public string CellSHA256Hash { get; set; } = null;

        /// <summary>
        /// Cell position.
        /// </summary>
        public int CellPosition
        {
            get
            {
                return _CellPosition;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(CellPosition));
                _CellPosition = value;
            }
        }

        /// <summary>
        /// Chunk GUID.
        /// </summary>
        public string ChunkGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Chunk MD5.
        /// </summary>
        public string ChunkMD5Hash { get; set; } = null;

        /// <summary>
        /// Chunk SHA1.
        /// </summary>
        public string ChunkSHA1Hash { get; set; } = null;

        /// <summary>
        /// Chunk SHA256.
        /// </summary>
        public string ChunkSHA256Hash { get; set; } = null;

        /// <summary>
        /// Position.
        /// </summary>
        public int ChunkPosition
        {
            get
            {
                return _ChunkPosition;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(ChunkPosition));
                _ChunkPosition = value;
            }
        }

        /// <summary>
        /// Chunk length.
        /// </summary>
        public int ChunkLength
        {
            get
            {
                return _ChunkLength;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(ChunkLength));
                _ChunkLength = value;
            }
        }

        /// <summary>
        /// Content.
        /// </summary>
        public string Content { get; set; } = null;

        /// <summary>
        /// Score.
        /// </summary>
        public decimal? Score { get; set; } = null;

        /// <summary>
        /// Distance.
        /// </summary>
        public decimal? Distance { get; set; } = null;

        /// <summary>
        /// Creation timestamp in UTC.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

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

        private int _CellPosition = 0;
        private int _ChunkPosition = 0;
        private int _ChunkLength = 0;
        private List<float> _Embeddings = new List<float>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public VectorChunk()
        {

        }

        /// <summary>
        /// Instantiate from DataRow.
        /// </summary>
        /// <param name="row">DataRow.</param>
        /// <param name="serializer">Serializer.</param>
        /// <returns>EmbeddingsDocument.</returns>
        public static VectorChunk FromDataRow(DataRow row, Serializer serializer)
        {
            if (row == null) return null;
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));

            VectorChunk ret = new VectorChunk
            {
                DocumentGUID = row["document_guid"] != null ? row["document_guid"].ToString() : null,
                TenantGUID = row["tenant_guid"] != null ? row["tenant_guid"].ToString() : null,
                CollectionGUID = row["collection_guid"] != null ? row["collection_guid"].ToString() : null,
                SourceDocumentGUID = row["source_document_guid"] != null ? row["source_document_guid"].ToString() : null,
                VectorRepositoryGUID = row["vector_repository_guid"] != null ? row["vector_repository_guid"].ToString() : null,
                GraphRepositoryGUID = row["graph_repository_guid"] != null ? row["graph_repository_guid"].ToString() : null,
                GraphNodeIdentifier = row["graph_node_identifier"] != null ? row["graph_node_identifier"].ToString() : null,
                BucketGUID = row["bucket_guid"] != null ? row["bucket_guid"].ToString() : null,
                ObjectGUID = row["object_guid"] != null ? row["object_guid"].ToString() : null,
                ObjectKey = row["object_key"] != null ? row["object_key"].ToString() : null,
                ObjectVersion = row["object_version"] != null ? row["object_version"].ToString() : null,
                Model = row["model"] != null ? row["model"].ToString() : null,
                
                CellGUID = row["cell_guid"] != null ? row["cell_guid"].ToString() : null,
                CellType = row["cell_type"] != null ? (SemanticCellTypeEnum)Enum.Parse(typeof(SemanticCellTypeEnum), row["cell_type"].ToString()) : SemanticCellTypeEnum.Text,
                CellMD5Hash = row["cell_md5"] != null ? row["cell_md5"].ToString() : null,
                CellSHA1Hash = row["cell_sha1"] != null ? row["cell_sha1"].ToString() : null,
                CellSHA256Hash = row["cell_sha256"] != null ? row["cell_sha256"].ToString() : null,
                CellPosition = row["cell_position"] != null ? Convert.ToInt32(row["cell_position"]) : 0,

                ChunkGUID = row["chunk_guid"] != null ? row["chunk_guid"].ToString() : null,
                ChunkMD5Hash = row["chunk_md5"] != null ? row["chunk_md5"].ToString() : null,
                ChunkSHA1Hash = row["chunk_sha1"] != null ? row["chunk_sha1"].ToString() : null,
                ChunkSHA256Hash = row["chunk_sha256"] != null ? row["chunk_sha256"].ToString() : null,
                ChunkPosition = row["chunk_position"] != null ? Convert.ToInt32(row["chunk_position"]) : 0,
                ChunkLength = row["chunk_length"] != null ? Convert.ToInt32(row["chunk_length"]) : 0,
                Content = row["content"] != null ? row["content"].ToString() : null,
                
                Score = row.Table.Columns.Contains("score") && row["score"] != null ? Convert.ToDecimal(row["score"]) : 0,
                Distance = row.Table.Columns.Contains("distance") && row["distance"] != null ? Convert.ToDecimal(row["distance"]) : 0,

                CreatedUtc = row["created_utc"] != null ? Convert.ToDateTime(row["created_utc"].ToString()) : DateTime.UtcNow,
            };

            string embeddingsStr = row["embedding"] != null ? row["embedding"].ToString() : null;
            List<float> embeddings = new List<float>();
            if (!String.IsNullOrEmpty(embeddingsStr))
                ret.Embeddings = serializer.DeserializeJson<List<float>>(embeddingsStr);

            return ret;
        }

        /// <summary>
        /// Instantiate from DataTable.
        /// </summary>
        /// <param name="dt">DataTable.</param>
        /// <param name="serializer">Serializer.</param>
        /// <returns>List of EmbeddingsDocument.</returns>
        public static List<VectorChunk> FromDataTable(DataTable dt, Serializer serializer)
        {
            if (dt == null) return null;
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));

            List<VectorChunk> ret = new List<VectorChunk>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                VectorChunk chunk = FromDataRow(dt.Rows[i], serializer);
                ret.Add(chunk);
            }

            return ret;
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
