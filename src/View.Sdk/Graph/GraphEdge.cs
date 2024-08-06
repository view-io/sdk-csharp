namespace View.Sdk.Graph
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Graph edge.
    /// </summary>
    public class GraphEdge
    {
        #region Public-Members

        /// <summary>
        /// Globally-unique identifier.
        /// </summary>
        public Guid GUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Globally-unique identifier for the graph.
        /// </summary>
        public Guid GraphGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; } = null;

        /// <summary>
        /// Globally-unique identifier of the from node.
        /// </summary>
        public Guid From { get; set; } = default(Guid);

        /// <summary>
        /// From node.  This property is only populated when retrieving routes.
        /// </summary>
        public GraphNode FromNode { get; set; } = null;

        /// <summary>
        /// Globally-unique identifier of the to node.
        /// </summary>
        public Guid To { get; set; } = default(Guid);

        /// <summary>
        /// To node.  This property is only populated when retrieving routes.
        /// </summary>
        public GraphNode ToNode { get; set; } = null;

        /// <summary>
        /// Cost.
        /// </summary>
        public int Cost { get; set; } = 0;

        /// <summary>
        /// Timestamp from creation, in UTC.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public GraphEdge()
        { 
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
