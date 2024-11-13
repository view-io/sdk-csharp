namespace View.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json.Serialization;
    using View.Sdk.Semantic;

    /// <summary>
    /// Source document statistics.
    /// </summary>
    public class SourceDocumentStatistics
    {
        #region Public-Members

        /// <summary>
        /// Source document.
        /// </summary>
        [JsonIgnore]
        public SourceDocument SourceDocument
        {
            get
            {
                return _SourceDocument;
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(SourceDocument));
                _SourceDocument = value;
            }
        }

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public string TenantGUID
        {
            get
            {
                return _SourceDocument.TenantGUID;
            }
        }

        /// <summary>
        /// Collection GUID.
        /// </summary>
        public string CollectionGUID
        {
            get
            {
                return _SourceDocument.CollectionGUID;
            }
        }

        /// <summary>
        /// Source document GUID.
        /// </summary>
        public string SourceDocumentGUID
        {
            get
            {
                return _SourceDocument.GUID;
            }
        }

        /// <summary>
        /// Term count.
        /// </summary>
        public int Terms
        {
            get
            {
                if (_SourceDocument.UdrDocument != null
                    && _SourceDocument.UdrDocument.Terms != null)
                {
                    return _SourceDocument.UdrDocument.Terms.Count;
                }

                return 0;
            }
        }

        /// <summary>
        /// Distinct terms.
        /// </summary>
        public int DistinctTerms
        {
            get
            {
                if (_SourceDocument.UdrDocument != null
                    && _SourceDocument.UdrDocument.Terms != null)
                {
                    return _SourceDocument.UdrDocument.Terms.Distinct().Count();
                }

                return 0;
            }
        }

        /// <summary>
        /// Key-value count.
        /// </summary>
        public int KeyValuePairs
        {
            get
            {
                if (_SourceDocument.UdrDocument != null
                    && _SourceDocument.UdrDocument.Schema != null
                    && _SourceDocument.UdrDocument.Schema.Flattened != null)
                {
                    return _SourceDocument.UdrDocument.Schema.Flattened.Count;
                }

                return 0;
            }
        }

        /// <summary>
        /// Semantic cell count.
        /// </summary>
        public int SemanticCells
        {
            get
            {
                if (_SourceDocument.UdrDocument != null
                    && _SourceDocument.UdrDocument.SemanticCells != null
                    && _SourceDocument.UdrDocument.SemanticCells.Count > 0)
                {
                    return CountSemanticCells(_SourceDocument.UdrDocument.SemanticCells);
                }

                return 0;
            }
        }

        /// <summary>
        /// Semantic cell bytes.
        /// </summary>
        public long SemanticCellBytes
        {
            get
            {
                if (_SourceDocument.UdrDocument != null
                    && _SourceDocument.UdrDocument.SemanticCells != null
                    && _SourceDocument.UdrDocument.SemanticCells.Count > 0)
                {
                    return SumSemanticCellBytes(_SourceDocument.UdrDocument.SemanticCells);
                }

                return 0;
            }
        }

        /// <summary>
        /// Semantic chunk count.
        /// </summary>
        public int SemanticChunks
        {
            get
            {
                if (_SourceDocument.UdrDocument != null
                    && _SourceDocument.UdrDocument.SemanticCells != null
                    && _SourceDocument.UdrDocument.SemanticCells.Count > 0)
                {
                    return CountSemanticChunks(_SourceDocument.UdrDocument.SemanticCells);
                }

                return 0;
            }
        }

        /// <summary>
        /// Semantic chunk bytes.
        /// </summary>
        public long SemanticChunkBytes
        {
            get
            {
                if (_SourceDocument.UdrDocument != null
                    && _SourceDocument.UdrDocument.SemanticCells != null
                    && _SourceDocument.UdrDocument.SemanticCells.Count > 0)
                {
                    return SumSemanticChunkBytes(_SourceDocument.UdrDocument.SemanticCells);
                }

                return 0;
            }
        }

        #endregion

        #region Private-Members

        private SourceDocument _SourceDocument = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public SourceDocumentStatistics()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        private int CountSemanticCells(List<SemanticCell> cells)
        {
            int count = 0;

            foreach (SemanticCell cell in cells)
            {
                count++;

                if (cell.Children != null && cell.Children.Count > 0)
                {
                    count += CountSemanticCells(cell.Children);
                }
            }

            return count;
        }

        private long SumSemanticCellBytes(List<SemanticCell> cells)
        {
            long bytes = 0;

            foreach (SemanticCell cell in cells)
            {
                if (cell.Length > 0) bytes += cell.Length;

                if (cell.Children != null && cell.Children.Count > 0)
                {
                    bytes += SumSemanticCellBytes(cell.Children);
                }
            }

            return bytes;
        }

        private int CountSemanticChunks(List<SemanticCell> cells)
        {
            int count = 0;

            foreach (SemanticCell cell in cells)
            {
                if (cell.Chunks != null && cell.Chunks.Count > 0) count += cell.Chunks.Count;

                if (cell.Children != null && cell.Children.Count > 0)
                {
                    count += CountSemanticChunks(cell.Children);
                }
            }

            return count;
        }

        private long SumSemanticChunkBytes(List<SemanticCell> cells)
        {
            long bytes = 0;

            foreach (SemanticCell cell in cells)
            {
                if (cell.Chunks != null && cell.Chunks.Count > 0)
                {
                    foreach (SemanticChunk chunk in cell.Chunks)
                    {
                        bytes += chunk.Length;
                    }
                }

                if (cell.Children != null && cell.Children.Count > 0)
                {
                    bytes += SumSemanticChunkBytes(cell.Children);
                }
            }

            return bytes;
        }

        #endregion
    }
}
