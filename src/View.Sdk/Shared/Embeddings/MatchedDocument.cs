namespace View.Sdk.Shared.Embeddings
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text.Json.Serialization;
    using Timestamps;

    /// <summary>
    /// Matched document.
    /// </summary>
    public class MatchedDocument
    {
        #region Public-Members

        /// <summary>
        /// Database row ID.
        /// </summary>
        [JsonIgnore]
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(Id));
                _Id = value;
            }
        }

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public string TenantGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Bucket GUID.
        /// </summary>
        public string BucketGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Object GUID.
        /// </summary>
        public string ObjectGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Object key.
        /// </summary>
        public string ObjectKey { get; set; } = null;

        /// <summary>
        /// Object version.
        /// </summary>
        public string ObjectVersion { get; set; } = null;

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
                if (value == null) _Embeddings = new List<float>();
                else _Embeddings = value;
            }
        }

        /// <summary>
        /// Creation timestamp.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        #endregion

        #region Private-Members

        private int _Id = 0;
        private List<float> _Embeddings = new List<float>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public MatchedDocument()
        {

        }

        /// <summary>
        /// Instantiate from DataRow.
        /// </summary>
        /// <param name="row">DataRow.</param>
        /// <returns>MatchedDocument.</returns>
        public static MatchedDocument FromDataRow(DataRow row)
        {
            if (row == null) return null;

            MatchedDocument doc = new MatchedDocument
            {
                Id = Convert.ToInt32(row["id"]),
                TenantGUID = row["tenant_guid"] != null ? row["tenant_guid"].ToString() : null,
                BucketGUID = row["bucket_guid"] != null ? row["bucket_guid"].ToString() : null,
                ObjectGUID = row["object_guid"] != null ? row["object_guid"].ToString() : null,
                ObjectKey = row["object_key"] != null ? row["object_key"].ToString() : null,
                ObjectVersion = row["object_version"] != null ? row["object_version"].ToString() : null,
                CreatedUtc = row["created_utc"] != null ? Convert.ToDateTime(row["created_utc"].ToString()) : DateTime.UtcNow
            };

            object embeddingsColumn = row["embedding"];
            string embeddingsStr = embeddingsColumn.ToString().Replace("[", "").Replace("]", "");
            List<string> embeddingsSplit = embeddingsStr.Split(',').ToList();
            doc.Embeddings = embeddingsSplit.Select(float.Parse).ToList();
            return doc;
        }

        /// <summary>
        /// Instantiate from DataTable.
        /// </summary>
        /// <param name="dt">DataTable.</param>
        /// <returns>List of MatchedDocument.</returns>
        public static List<MatchedDocument> FromDataTable(DataTable dt)
        {
            if (dt == null) return null;

            List<MatchedDocument> ret = new List<MatchedDocument>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MatchedDocument doc = MatchedDocument.FromDataRow(dt.Rows[i]);
                ret.Add(doc);
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
