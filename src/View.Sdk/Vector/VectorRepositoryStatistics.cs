namespace View.Sdk.Vector
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text.Json.Serialization;
    using View.Sdk.Serialization;

    /// <summary>
    /// Vector repository statistics.
    /// </summary>
    public class VectorRepositoryStatistics
    {
        #region Public-Members

        /// <summary>
        /// Collection.
        /// </summary>
        public VectorRepository VectorRepository
        {
            get
            {
                return _VectorRepository;
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(Collection));
                _VectorRepository = value;
            }
        }

        /// <summary>
        /// Number of documents.
        /// </summary>
        public long DocumentCount
        {
            get
            {
                return _DocumentCount;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(DocumentCount));
                _DocumentCount = value;
            }
        }

        /// <summary>
        /// Total number of bytes as recorded through semantic cells.
        /// </summary>
        public long TotalBytes
        {
            get
            {
                return _TotalBytes;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(TotalBytes));
                _TotalBytes = value;
            }
        }

        /// <summary>
        /// Semantic cell count.
        /// </summary>
        public long CellCount
        {
            get
            {
                return _CellCount;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(CellCount));
                _CellCount = value;
            }
        }

        /// <summary>
        /// Chunk count.
        /// </summary>
        public long ChunkCount
        {
            get
            {
                return _ChunkCount;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(ChunkCount));
                _ChunkCount = value;
            }
        }

        #endregion

        #region Private-Members

        private VectorRepository _VectorRepository = null;
        private long _DocumentCount = 0;
        private long _TotalBytes = 0;
        private long _CellCount = 0;
        private long _ChunkCount = 0;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public VectorRepositoryStatistics()
        {

        }

        /// <summary>
        /// Instantiate from DataRow.
        /// </summary>
        /// <param name="row">DataRow.</param>
        /// <param name="serializer">Serializer.</param>
        /// <returns>VectorRepositoryStatistics.</returns>
        public static VectorRepositoryStatistics FromDataRow(DataRow row, Serializer serializer)
        {
            if (row == null) return null;
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));

            VectorRepositoryStatistics stats = new VectorRepositoryStatistics
            {
                DocumentCount = (row["document_count"] != null ? Convert.ToInt32(row["document_count"]) : 0),
                TotalBytes = (row["total_bytes"] != null ? Convert.ToInt64(row["total_bytes"]) : 0),
                CellCount = (row["num_cells"] != null ? Convert.ToInt64(row["num_cells"]) : 0),
                ChunkCount = (row["num_chunks"] != null ? Convert.ToInt64(row["num_chunks"]) : 0)
            };

            return stats;
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
