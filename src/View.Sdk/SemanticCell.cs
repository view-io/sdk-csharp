namespace View.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using View.Sdk.Semantic;

    /// <summary>
    /// Semantic cell.
    /// </summary>
    public class SemanticCell
    {
        #region Public-Members

        /// <summary>
        /// GUID.
        /// </summary>
        public string GUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// MD5.
        /// </summary>
        public string MD5Hash { get; set; } = string.Empty;

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
        /// Chunks.
        /// </summary>
        public List<SemanticChunk> Chunks
        {
            get
            {
                return _Chunks;
            }
            set
            {
                if (value == null) value = new List<SemanticChunk>();
                _Chunks = value;
            }
        }

        /// <summary>
        /// Children.
        /// </summary>
        public List<SemanticCell> Children
        {
            get
            {
                return _Children;
            }
            set
            {
                if (value == null) value = new List<SemanticCell>();
                _Children = value;
            }
        }

        #endregion

        #region Private-Members

        private int _Position = 0;
        private int _Length = 0;
        private List<SemanticChunk> _Chunks = new List<SemanticChunk>();
        private List<SemanticCell> _Children = new List<SemanticCell>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public SemanticCell()
        {

        }

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="children">Child semantic cells.</param>
        /// <param name="chunks">Semantic chunks.</param>
        public SemanticCell(List<SemanticCell> children = null, List<SemanticChunk> chunks = null)
        {
            Children = children;
            Chunks = chunks;
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// Count the number of embeddings in a list of semantic cells.
        /// </summary>
        /// <param name="cells">Semantic cells.</param>
        /// <returns>Number of embeddings.</returns>
        public static int CountEmbeddings(List<SemanticCell> cells)
        {
            int ret = 0;

            if (cells != null && cells.Count > 0)
            {
                foreach (SemanticCell cell in cells)
                {
                    ret += cell.CountEmbeddings();
                }
            }

            return ret;
        }

        /// <summary>
        /// Count the number of semantic cells in a list of semantic cells.
        /// </summary>
        /// <param name="cells">Semantic cells.</param>
        /// <returns>Number of semantic cells.</returns>
        public static int CountSemanticCells(List<SemanticCell> cells)
        {
            int ret = 0;

            if (cells != null && cells.Count > 0)
            {
                foreach (SemanticCell cell in cells)
                {
                    ret += 1;

                    if (cell.Children != null)
                    {
                        ret += CountSemanticCells(cell.Children);
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// Count the number of semantic chunks in a list of semantic cells.
        /// </summary>
        /// <param name="cells">Semantic cells.</param>
        /// <returns>Number of chunks.</returns>
        public static int CountSemanticChunks(List<SemanticCell> cells)
        {
            int ret = 0;

            if (cells != null && cells.Count > 0)
            {
                foreach (SemanticCell cell in cells)
                {
                    if (cell.Chunks != null) ret += cell.Chunks.Count;

                    if (cell.Children != null)
                    {
                        int childChunks = CountSemanticChunks(cell.Children);
                        ret += childChunks;
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// Count the number of bytes in chunks within a list of semantic cells.
        /// </summary>
        /// <param name="cells">Semantic cells.</param>
        /// <returns>Number of bytes.</returns>
        public static int CountBytes(List<SemanticCell> cells)
        {
            int ret = 0;

            if (cells != null && cells.Count > 0)
            {
                foreach (SemanticCell cell in cells)
                {
                    ret += cell.CountBytes();
                }
            }

            return ret;
        }

        /// <summary>
        /// Count the number of semantic cells in this semantic cell.
        /// </summary>
        /// <returns>Number of semantic cells.</returns>
        public int CountSemanticCells()
        {
            int ret = 1;

            if (Children != null && Children.Count > 0)
            {
                foreach (SemanticCell child in Children)
                {
                    ret += child.CountSemanticCells();
                }
            }

            return ret;
        }

        /// <summary>
        /// Count the number of embeddings contained within the semantic cell.
        /// </summary>
        /// <returns></returns>
        public int CountEmbeddings()
        {
            int ret = 0;

            if (Chunks != null && Chunks.Count > 0)
            {
                foreach (SemanticChunk chunk in Chunks)
                {
                    ret += chunk.Embeddings.Count;
                }
            }

            if (Children != null && Children.Count > 0)
            {
                foreach (SemanticCell childCell in Children)
                {
                    int childEmbeddings = childCell.CountEmbeddings();
                    ret += childEmbeddings;
                }
            }

            return ret;
        }

        /// <summary>
        /// Count the number of bytes contained within chunks within the semantic cell.
        /// </summary>
        /// <returns>Number of bytes.</returns>
        public int CountBytes()
        {
            int ret = 0;

            if (Chunks != null && Chunks.Count > 0)
            {
                foreach (SemanticChunk chunk in Chunks)
                {
                    if (!String.IsNullOrEmpty(chunk.Content)) ret += chunk.Content.Length;
                }
            }

            if (Children != null && Children.Count > 0)
            {
                foreach (SemanticCell child in Children)
                {
                    ret += child.CountBytes();
                }
            }

            return ret;
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}
