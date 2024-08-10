using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timestamps;

namespace View.Sdk.Graph
{
    /// <summary>
    /// Graph result.
    /// </summary>
    public class GraphResult
    {
        #region Public-Members

        /// <summary>
        /// Success or failure.
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// Timestamp.
        /// </summary>
        public Timestamp Timestamp
        {
            get
            {
                return _Timestamp;
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(Timestamp));
                _Timestamp = value;
            }
        }

        /// <summary>
        /// Graph.
        /// </summary>
        public Graph Graph { get; set; } = null;

        /// <summary>
        /// Tenant.
        /// </summary>
        public GraphNode Tenant { get; set; } = null;

        /// <summary>
        /// Storage pool.
        /// </summary>
        public GraphNode StoragePool { get; set; } = null;

        /// <summary>
        /// Bucket.
        /// </summary>
        public GraphNode Bucket { get; set; } = null;

        /// <summary>
        /// Object.
        /// </summary>
        public GraphNode Object { get; set; } = null;

        /// <summary>
        /// Collection.
        /// </summary>
        public GraphNode Collection { get; set; } = null;

        /// <summary>
        /// Source document.
        /// </summary>
        public GraphNode SourceDocument { get; set; } = null;

        /// <summary>
        /// Data repository.
        /// </summary>
        public GraphNode DataRepository { get; set; } = null;

        /// <summary>
        /// Semantic cells.
        /// </summary>
        public List<GraphNode> SemanticCells { get; set; } = null;

        /// <summary>
        /// Semantic chunks.
        /// </summary>
        public List<GraphNode> SemanticChunks { get; set; } = null;

        /// <summary>
        /// Edges.
        /// </summary>
        public List<GraphEdge> Edges
        {
            get
            {
                return _Edges;
            }
            set
            {
                if (value == null) value = new List<GraphEdge>();
                _Edges = value;
            }
        }

        #endregion

        #region Private-Members

        private Timestamp _Timestamp = new Timestamp();
        private List<GraphEdge> _Edges = new List<GraphEdge>();

        #endregion

        #region Constructors-and-Factories

        #endregion

        #region Public-Methods

        /// <summary>
        /// Instantiate.
        /// </summary>
        public GraphResult()
        {

        }

        #endregion

        #region Private-Methods

        #endregion
    }
}
