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
    using View.Sdk.Helpers;
    using View.Sdk.Semantic;

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
        public Guid GUID { get; set; } = Guid.NewGuid();

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
        /// Data repository GUID.
        /// </summary>
        public Guid? DataRepositoryGUID { get; set; } = null;

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
                GUID = DataTableHelper.GetGuidValue(row, "guid"),
                DocumentGUID = DataTableHelper.GetGuidValue(row, "document_guid"),
                TenantGUID = DataTableHelper.GetGuidValue(row, "tenant_guid"),
                CollectionGUID = DataTableHelper.GetNullableGuidValue(row, "collection_guid"),
                SourceDocumentGUID = DataTableHelper.GetNullableGuidValue(row, "source_document_guid"),
                DataRepositoryGUID = DataTableHelper.GetNullableGuidValue(row, "data_repository_guid"),
                VectorRepositoryGUID = DataTableHelper.GetNullableGuidValue(row, "vector_repository_guid"),
                GraphRepositoryGUID = DataTableHelper.GetNullableGuidValue(row, "graph_repository_guid"),
                GraphNodeIdentifier = DataTableHelper.GetStringValue(row, "graph_node_identifier"),
                BucketGUID = DataTableHelper.GetNullableGuidValue(row, "bucket_guid"),
                ObjectGUID = DataTableHelper.GetNullableGuidValue(row, "object_guid"),
                ObjectKey = DataTableHelper.GetStringValue(row, "object_key"),
                ObjectVersion = DataTableHelper.GetStringValue(row, "object_version"),
                Model = DataTableHelper.GetStringValue(row, "model"),
                CreatedUtc = DataTableHelper.GetDateTimeValue(row, "created_utc")
            };

            if (row.Table.Columns.Contains("score")) doc.Score = DataTableHelper.GetNullableDecimalValue(row, "score");
            if (row.Table.Columns.Contains("distance")) doc.Distance = DataTableHelper.GetNullableDecimalValue(row, "distance");

            SemanticCell cell = SemanticCell.FromDataRow(row);
            SemanticChunk chunk = SemanticChunk.FromDataRow(row);

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
                if (ret.Any(d => d.DocumentGUID.Equals(doc.DocumentGUID)))
                {
                    #region Existing-Doc

                    EmbeddingsDocument existingDoc = ret.First(d => d.DocumentGUID.Equals(doc.DocumentGUID));
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
