namespace View.Sdk.Graph
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using ExpressionTree;

    /// <summary>
    /// Graph database driver interface.
    /// </summary>
    public interface IGraphDriver : IDisposable
    {
        #region General

        /// <summary>
        /// Validate connectivity to the repository.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if connected.</returns>
        public Task<bool> ValidateConnectivity(CancellationToken token = default);

        #endregion

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
        /// <param name="guid">GUID.</param>
        /// <param name="name">Name.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph.</returns>
        public Task<Graph> CreateGraph(Guid guid, string name, CancellationToken token = default);

        /// <summary>
        /// Read a graph.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph.</returns>
        public Task<Graph> ReadGraph(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read a graph.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph.</returns>
        public Task<List<Graph>> ReadGraphs(CancellationToken token = default);

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
        /// <param name="force">Force deletion of subordinate edges and nodes.</param>
        /// <param name="token">Cancellation token.</param>
        public Task DeleteGraph(Guid guid, bool force = false, CancellationToken token = default);

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
        public Task<List<GraphNode>> ReadNodes(Guid graphGuid, CancellationToken token = default);

        /// <summary>
        /// Read nodes.
        /// </summary>
        /// <param name="graphGuid">Graph GUID.</param>
        /// <param name="expr">Expression.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Nodes.</returns>
        public Task<List<GraphNode>> SearchNodes(Guid graphGuid, Expr expr, CancellationToken token = default);

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
        public Task<GraphEdge> CreateEdge(GraphEdge edge, CancellationToken token = default);

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
        public Task<List<GraphEdge>> ReadEdges(Guid graphGuid, CancellationToken token = default);

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
        public Task<List<GraphEdge>> EdgesFromNode(Guid graphGuid, Guid nodeGuid, CancellationToken token = default);

        /// <summary>
        /// Retrieve edges to node.
        /// </summary>
        /// <param name="graphGuid">Graph GUID.</param>
        /// <param name="nodeGuid">Node GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Edges.</returns>
        public Task<List<GraphEdge>> EdgesToNode(Guid graphGuid, Guid nodeGuid, CancellationToken token = default);

        /// <summary>
        /// Retrieve edges to node.
        /// </summary>
        /// <param name="graphGuid">Graph GUID.</param>
        /// <param name="fromNodeGuid">From node GUID.</param>
        /// <param name="toNodeGuid">To node GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Edges.</returns>
        public Task<List<GraphEdge>> EdgesBetweenNodes(
            Guid graphGuid, 
            Guid fromNodeGuid, 
            Guid toNodeGuid, 
            CancellationToken token = default);

        /// <summary>
        /// Retrieve all edges associated with a node.
        /// </summary>
        /// <param name="graphGuid">Graph GUID.</param>
        /// <param name="nodeGuid">Node GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Edges.</returns>
        public Task<List<GraphEdge>> AllNodeEdges(Guid graphGuid, Guid nodeGuid, CancellationToken token = default);

        /// <summary>
        /// Retrieve parent nodes for a given node, i.e. those nodes that have edges to the given node.
        /// </summary>
        /// <param name="graphGuid">Graph GUID.</param>
        /// <param name="nodeGuid">Node GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Nodes.</returns>
        public Task<List<GraphNode>> GetNodeParents(Guid graphGuid, Guid nodeGuid, CancellationToken token = default);

        /// <summary>
        /// Retrieve child nodes for a given node, i.e. those nodes to which the given node has edges.
        /// </summary>
        /// <param name="graphGuid">Graph GUID.</param>
        /// <param name="nodeGuid">Node GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Nodes.</returns>
        public Task<List<GraphNode>> GetNodeChildren(Guid graphGuid, Guid nodeGuid, CancellationToken token = default);

        /// <summary>
        /// Retrieve neighboring nodes, i.e. those nodes to which the given node has an edge either to or from.
        /// </summary>
        /// <param name="graphGuid">Graph GUID.</param>
        /// <param name="nodeGuid">Node GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Nodes.</returns>
        public Task<List<GraphNode>> GetNodeNeighbors(Guid graphGuid, Guid nodeGuid, CancellationToken token = default);

        #endregion
    }
}
