namespace View.Sdk
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

    /// <summary>
    /// Embeddings document.
    /// </summary>
    public class EmbeddingsDocument
    {
        #region Public-Members

        /// <summary>
        /// Indicates if the parser was successful.
        /// </summary>
        public bool? Success { get; set; } = true;

        /// <summary>
        /// Exception, if any.
        /// </summary>
        public Exception Exception { get; set; } = null;

        /// <summary>
        /// ID.
        /// </summary>
        [JsonIgnore]
        public int? Id { get; set; } = null;

        /// <summary>
        /// GUID.
        /// </summary>
        public string GUID { get; set; } = Guid.NewGuid().ToString();

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
        /// Embeddings rule.
        /// </summary>
        public EmbeddingsRule EmbeddingsRule { get; set; } = null;

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
        /// Semantic cells.
        /// </summary>
        public List<SemanticCell> SemanticCells
        {
            get
            {
                return _SemanticCells;
            }
            set
            {
                if (value == null) value = new List<SemanticCell>();
                _SemanticCells = value;
            }
        }

        /// <summary>
        /// Creation timestamp in UTC.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        #endregion

        #region Private-Members

        private List<SemanticCell> _SemanticCells = new List<SemanticCell>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public EmbeddingsDocument()
        {

        }

        /// <summary>
        /// Instantiate from DataRow.
        /// </summary>
        /// <param name="row">DataRow.</param>
        /// <param name="serializer">Serializer.</param>
        /// <returns>EmbeddingsDocument.</returns>
        public static EmbeddingsDocument FromDataRow(DataRow row, Serializer serializer)
        {
            if (row == null) return null;
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));

            EmbeddingsDocument doc = new EmbeddingsDocument
            {
                Id = Convert.ToInt32(row["id"]),
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
                CreatedUtc = row["created_utc"] != null ? Convert.ToDateTime(row["created_utc"].ToString()) : DateTime.UtcNow
            };

            if (row.Table.Columns.Contains("score"))
                doc.Score = row["score"] != null ? Convert.ToDecimal(row["score"].ToString()) : null;

            if (row.Table.Columns.Contains("distance"))
                doc.Distance = row["distance"] != null ? Convert.ToDecimal(row["distance"].ToString()) : null;

            string cellGuid = row["cell_guid"] != null ? row["cell_guid"].ToString() : null;
            string cellType = row["cell_type"] != null ? row["cell_type"].ToString() : null;
            string cellMd5 = row["cell_md5"] != null ? row["cell_md5"].ToString() : null;
            string cellSha1 = row["cell_sha1"] != null ? row["cell_sha1"].ToString() : null;
            string cellSha256 = row["cell_sha256"] != null ? row["cell_sha256"].ToString() : null;
            int cellPosition = row["cell_position"] != null ? Convert.ToInt32(row["cell_position"]) : 0;

            string chunkGuid = row["chunk_guid"] != null ? row["chunk_guid"].ToString() : null;
            string chunkMd5 = row["chunk_md5"] != null ? row["chunk_md5"].ToString() : null;
            string chunkSha1 = row["chunk_sha1"] != null ? row["chunk_sha1"].ToString() : null;
            string chunkSha256 = row["chunk_sha256"] != null ? row["chunk_sha256"].ToString() : null;
            int chunkPosition = row["chunk_position"] != null ? Convert.ToInt32(row["chunk_position"]) : 0;
            
            string content = row["content"] != null ? row["content"].ToString() : null;
            
            string embeddingsStr = row["embedding"] != null ? row["embedding"].ToString() : null;
            List<float> embeddings = new List<float>();
            if (!String.IsNullOrEmpty(embeddingsStr))
                embeddings = serializer.DeserializeJson<List<float>>(embeddingsStr);

            SemanticCell cell = new SemanticCell
            {
                GUID = cellGuid,
                CellType =
                    (!String.IsNullOrEmpty(cellType)
                        ? (SemanticCellTypeEnum)(Enum.Parse(typeof(SemanticCellTypeEnum), cellType))
                        : SemanticCellTypeEnum.Text),
                MD5Hash = cellMd5,
                SHA1Hash = cellSha1,
                SHA256Hash = cellSha256,
                Position = cellPosition
            };

            SemanticChunk chunk = new SemanticChunk
            {
                GUID = chunkGuid,
                MD5Hash = chunkMd5,
                SHA1Hash = chunkSha1,
                SHA256Hash = chunkSha256,
                Position = chunkPosition,
                Content = content,
                Embeddings = embeddings
            };

            cell.Chunks.Add(chunk);

            doc.SemanticCells = new List<SemanticCell>();
            doc.SemanticCells.Add(cell);

            return doc;
        }

        /// <summary>
        /// Instantiate from DataTable.
        /// </summary>
        /// <param name="dt">DataTable.</param>
        /// <param name="serializer">Serializer.</param>
        /// <returns>List of EmbeddingsDocument.</returns>
        public static List<EmbeddingsDocument> FromDataTable(DataTable dt, Serializer serializer)
        {
            if (dt == null) return null;
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));

            #region Create-Raw-Records

            List<EmbeddingsDocument> raw = new List<EmbeddingsDocument>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                EmbeddingsDocument doc = FromDataRow(dt.Rows[i], serializer);
                raw.Add(doc);
            }

            #endregion

            #region Merge

            List<EmbeddingsDocument> ret = new List<EmbeddingsDocument>();

            foreach (EmbeddingsDocument doc in raw)
            {
                if (ret.Any(d => d.Id.Equals(doc.Id)))
                {
                    #region Existing-Doc

                    EmbeddingsDocument existingDoc = ret.First(d => d.Id.Equals(doc.Id));
                    foreach (SemanticCell currCell in doc.SemanticCells)
                    {
                        if (existingDoc.SemanticCells.Any(c => c.GUID.Equals(currCell.GUID)))
                        {
                            #region Existing-Cell

                            SemanticCell existingCell = existingDoc.SemanticCells.First(c => c.GUID.Equals(currCell.GUID));

                            foreach (SemanticChunk currChunk in currCell.Chunks)
                            {
                                if (existingCell.Chunks.Any(c => c.GUID.Equals(currChunk)))
                                {
                                    #region Existing-Chunk

                                    // existing doc, existing cell, existing chunk
                                    // do nothing

                                    #endregion
                                }
                                else
                                {
                                    #region New-Chunk

                                    existingDoc.SemanticCells.Remove(existingCell);
                                    existingCell.Chunks.Add(currChunk);
                                    existingDoc.SemanticCells.Add(existingCell);
                                    ret.Add(existingDoc);

                                    #endregion
                                }
                            }

                            #endregion
                        }
                        else
                        {
                            #region New-Cell

                            existingDoc.SemanticCells.Add(currCell);
                            ret.Add(existingDoc);

                            #endregion
                        }
                    }

                    #endregion
                }
                else
                {
                    #region New-Doc

                    ret.Add(doc);

                    #endregion
                }
            }

            #endregion

            return ret;
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
