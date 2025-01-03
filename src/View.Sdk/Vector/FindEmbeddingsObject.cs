namespace View.Sdk.Vector
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using View.Sdk.Serialization;

    /// <summary>
    /// Find embeddings object.
    /// </summary>
    public class FindEmbeddingsObject
    {
        #region Public-Members

        /// <summary>
        /// SHA256 on which to match.
        /// </summary>
        public string SHA256Hash
        {
            get
            {
                return _SHA256Hash;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(SHA256Hash));
                _SHA256Hash = value;
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

        private string _SHA256Hash = null;
        private List<float> _Embeddings = new List<float>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public FindEmbeddingsObject()
        {

        }

        /// <summary>
        /// Instantiate from DataRow.
        /// </summary>
        /// <param name="row">DataRow.</param>
        /// <param name="serializer">Serializer.</param>
        /// <returns>FindEmbeddingsObject.</returns>
        public static FindEmbeddingsObject FromDataRow(DataRow row, Serializer serializer)
        {
            if (row == null) return null;
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));

            string sha256Hash = row["sha256"] != null ? row["sha256"].ToString() : null;
            string embeddingsStr = row["embedding"] != null ? row["embedding"].ToString() : null;
            List<float> embeddings = new List<float>();
            if (!String.IsNullOrEmpty(embeddingsStr))
                embeddings = serializer.DeserializeJson<List<float>>(embeddingsStr);

            return new FindEmbeddingsObject
            {
                SHA256Hash = sha256Hash,
                Embeddings = embeddings
            };
        }

        /// <summary>
        /// Instantiate from DataTable.
        /// </summary>
        /// <param name="dt">DataTable.</param>
        /// <param name="serializer">Serializer.</param>
        /// <returns>List of FindEmbeddingsObject.</returns>
        public static List<FindEmbeddingsObject> FromDataTable(DataTable dt, Serializer serializer)
        {
            if (dt == null) return null;
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));

            List<FindEmbeddingsObject> ret = new List<FindEmbeddingsObject>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FindEmbeddingsObject doc = FromDataRow(dt.Rows[i], serializer);
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
