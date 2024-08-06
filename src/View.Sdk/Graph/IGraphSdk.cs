namespace View.Sdk.Graph
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Graph interface.
    /// </summary>
    public interface IGraphSdk
    {
        #region Graph-Interface

        /// <summary>
        /// Check if a graph exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> GraphExists(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Create a graph.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="data">Data.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph.</returns>
        public Task<Graph> CreateGraph(string name, object data = null, CancellationToken token = default);

        /// <summary>
        /// Read a graph.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph.</returns>
        public Task<Graph> ReadGraph(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Update a graph.
        /// </summary>
        /// <param name="graph">Graph.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph.</returns>
        public Task<Graph> UpdateGraph(Graph graph, CancellationToken token = default);

        /// <summary>
        /// Delete a graph.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        public Task DeleteGraph(Guid guid, CancellationToken token = default);

        #endregion

        #region Node-Interface

        /// <summary>
        /// Check if a node exists.
        /// </summary>
        /// <param name="graphGuid">Graph GUID.</param>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> NodeExists(Guid graphGuid, Guid guid, CancellationToken token = default);

        /// <summary>
        /// Create a node.
        /// </summary>
        /// <param name="node">Node.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Node.</returns>
        public Task<GraphNode> CreateNode(GraphNode node, CancellationToken token = default);

        /// <summary>
        /// Read a node.
        /// </summary>
        /// <param name="graphGuid">Graph GUID.</param>
        /// <param name="nodeGuid">Node GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Node.</returns>
        public Task<GraphNode> ReadNode(Guid graphGuid, Guid nodeGuid, CancellationToken token = default);

        /// <summary>
        /// Read nodes.
        /// </summary>
        /// <param name="graphGuid">Graph GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Nodes.</returns>
        public Task<IEnumerable<GraphNode>> ReadNodes(Guid graphGuid, CancellationToken token = default);

        /// <summary>
        /// Delete a node.
        /// </summary>
        /// <param name="graphGuid">Graph GUID.</param>
        /// <param name="nodeGuid">Node GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public Task DeleteNode(Guid graphGuid, Guid nodeGuid, CancellationToken token = default);

        #endregion

        #region Edge-Interface

        /// <summary>
        /// Check if an edge exists.
        /// </summary>
        /// <param name="graphGuid">Graph GUID.</param>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> EdgeExists(Guid graphGuid, Guid guid, CancellationToken token = default);

        /// <summary>
        /// Create an edge.
        /// </summary>
        /// <param name="edge">Edge.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Edge.</returns>
        public Task<GraphNode> CreateEdge(GraphEdge edge, CancellationToken token = default);

        /// <summary>
        /// Read an edge.
        /// </summary>
        /// <param name="graphGuid">Graph GUID.</param>
        /// <param name="edgeGuid">Edge GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Edge.</returns>
        public Task<GraphEdge> ReadEdge(Guid graphGuid, Guid edgeGuid, CancellationToken token = default);

        /// <summary>
        /// Read edges.
        /// </summary>
        /// <param name="graphGuid">Graph GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Edges.</returns>
        public Task<IEnumerable<GraphEdge>> ReadEdges(Guid graphGuid, CancellationToken token = default);

        /// <summary>
        /// Delete an edge.
        /// </summary>
        /// <param name="graphGuid">Graph GUID.</param>
        /// <param name="edgeGuid">Edge GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public Task DeleteEdge(Guid graphGuid, Guid edgeGuid, CancellationToken token = default);

        #endregion

        #region Routes-and-Traversal

        /// <summary>
        /// Retrieve edges from node.
        /// </summary>
        /// <param name="graphGuid">Graph GUID.</param>
        /// <param name="nodeGuid">Node GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Edges.</returns>
        public Task<IEnumerable<GraphEdge>> EdgesFromNode(Guid graphGuid, Guid nodeGuid, CancellationToken token = default);

        /// <summary>
        /// Retrieve edges to node.
        /// </summary>
        /// <param name="graphGuid">Graph GUID.</param>
        /// <param name="nodeGuid">Node GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Edges.</returns>
        public Task<IEnumerable<GraphEdge>> EdgesToNode(Guid graphGuid, Guid nodeGuid, CancellationToken token = default);

        /// <summary>
        /// Retrieve all edges associated with a node.
        /// </summary>
        /// <param name="graphGuid">Graph GUID.</param>
        /// <param name="nodeGuid">Node GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Edges.</returns>
        public Task<IEnumerable<GraphEdge>> AllNodeEdges(Guid graphGuid, Guid nodeGuid, CancellationToken token = default);

        /// <summary>
        /// Retrieve parent nodes for a given node, i.e. those nodes that have edges to the given node.
        /// </summary>
        /// <param name="graphGuid">Graph GUID.</param>
        /// <param name="nodeGuid">Node GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Nodes.</returns>
        public Task<IEnumerable<GraphNode>> GetNodeParents(Guid graphGuid, Guid nodeGuid, CancellationToken token = default);

        /// <summary>
        /// Retrieve child nodes for a given node, i.e. those nodes to which the given node has edges.
        /// </summary>
        /// <param name="graphGuid">Graph GUID.</param>
        /// <param name="nodeGuid">Node GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Nodes.</returns>
        public Task<IEnumerable<GraphNode>> GetNodeChildren(Guid graphGuid, Guid nodeGuid, CancellationToken token = default);

        /// <summary>
        /// Retrieve neighboring nodes, i.e. those nodes to which the given node has an edge either to or from.
        /// </summary>
        /// <param name="graphGuid">Graph GUID.</param>
        /// <param name="nodeGuid">Node GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Nodes.</returns>
        public Task<IEnumerable<GraphNode>> GetNodeNeighbors(Guid graphGuid, Guid nodeGuid, CancellationToken token = default);

        #endregion
    }
}
