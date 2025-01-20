namespace View.Sdk.Graph
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Graph node.
    /// </summary>
    public class GraphNode
    {
        #region Public-Members

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public Guid TenantGUID { get; set; } = Guid.NewGuid();

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
        /// Object data.
        /// </summary>
        public GraphData Data { get; set; } = null;

        /// <summary>
        /// Timestamp from creation, in UTC.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.Now.ToUniversalTime();

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public GraphNode()
        { 
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
