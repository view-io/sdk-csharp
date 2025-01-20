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
    using View.Sdk;
    using View.Sdk.Helpers;
    using View.Sdk.Serialization;
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
        public Guid DocumentGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public Guid TenantGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Collection GUID.
        /// </summary>
        public Guid? CollectionGUID { get; set; } = null;

        /// <summary>
        /// Source document GUID.
        /// </summary>
        public Guid? SourceDocumentGUID { get; set; } = null;

        /// <summary>
        /// Bucket GUID.
        /// </summary>
        public Guid? BucketGUID { get; set; } = null;

        /// <summary>
        /// Vector repository GUID.
        /// </summary>
        public Guid? VectorRepositoryGUID { get; set; } = null;

        /// <summary>
        /// Graph repository GUID.
        /// </summary>
        public Guid? GraphRepositoryGUID { get; set; } = null;

        /// <summary>
        /// Graph node identifier.
        /// </summary>
        public string GraphNodeIdentifier { get; set; } = null;

        /// <summary>
        /// Object GUID.
        /// </summary>
        public Guid? ObjectGUID { get; set; } = null;

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
        public Guid CellGUID { get; set; } = Guid.NewGuid();

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
        public Guid ChunkGUID { get; set; } = Guid.NewGuid();

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
                DocumentGUID = DataTableHelper.GetGuidValue(row, "document_guid"),
                TenantGUID = DataTableHelper.GetGuidValue(row, "tenant_guid"),
                CollectionGUID = DataTableHelper.GetNullableGuidValue(row, "collection_guid"),
                SourceDocumentGUID = DataTableHelper.GetNullableGuidValue(row, "source_document_guid"),
                VectorRepositoryGUID = DataTableHelper.GetNullableGuidValue(row, "vector_repository_guid"),
                GraphRepositoryGUID = DataTableHelper.GetNullableGuidValue(row, "graph_repository_guid"),
                GraphNodeIdentifier = DataTableHelper.GetStringValue(row, "graph_node_identifier"),
                BucketGUID = DataTableHelper.GetNullableGuidValue(row, "bucket_guid"),
                ObjectGUID = DataTableHelper.GetNullableGuidValue(row, "object_guid"),
                ObjectKey = DataTableHelper.GetStringValue(row, "object_key"),
                ObjectVersion = DataTableHelper.GetStringValue(row, "object_version"),
                Model = DataTableHelper.GetStringValue(row, "model"),

                CellGUID = DataTableHelper.GetGuidValue(row, "cell_guid"),
                CellType = DataTableHelper.GetEnumValue<SemanticCellTypeEnum>(row, "cell_type"),
                CellMD5Hash = DataTableHelper.GetStringValue(row, "cell_md5"),
                CellSHA1Hash = DataTableHelper.GetStringValue(row, "cell_sha1"),
                CellSHA256Hash = DataTableHelper.GetStringValue(row, "cell_sha256"),
                CellPosition = DataTableHelper.GetInt32Value(row, "cell_position"),

                ChunkGUID = DataTableHelper.GetGuidValue(row, "chunk_guid"),
                ChunkMD5Hash = DataTableHelper.GetStringValue(row, "chunk_md5"),
                ChunkSHA1Hash = DataTableHelper.GetStringValue(row, "chunk_sha1"),
                ChunkSHA256Hash = DataTableHelper.GetStringValue(row, "chunk_sha256"),
                ChunkPosition = DataTableHelper.GetInt32Value(row, "chunk_position"),
                ChunkLength = DataTableHelper.GetInt32Value(row, "chunk_length"),
                Content = DataTableHelper.GetStringValue(row, "content"),

                Score = row.Table.Columns.Contains("score") && row["score"] != null ? DataTableHelper.GetNullableDecimalValue(row, "score") : 0,
                Distance = row.Table.Columns.Contains("distance") && row["distance"] != null ? DataTableHelper.GetNullableDecimalValue(row, "distance") : 0,

                CreatedUtc = DataTableHelper.GetDateTimeValue(row, "created_utc")
            };

            string embeddingsStr = DataTableHelper.GetStringValue(row, "embedding");
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
