namespace View.Sdk.Graph
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using LiteGraph.Sdk;

    /// <summary>
    /// View LiteGraph SDK.
    /// </summary>
    public class ViewLitegraphSdk : IGraphSdk
    {
        #region Public-Members

        /// <summary>
        /// Endpoint.
        /// </summary>
        public string Endpoint
        {
            get
            {
                return _Endpoint;
            }
        }

        #endregion

        #region Private-Members

        private LiteGraphSdk _Sdk = null;
        private string _Endpoint = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        public ViewLitegraphSdk(string endpoint = "http://localhost:8701/")
        {
            if (String.IsNullOrEmpty(endpoint)) throw new ArgumentNullException(nameof(endpoint));

            _Endpoint = endpoint;
            _Sdk = new LiteGraphSdk(_Endpoint);
        }

        #endregion

        #region Public-Graph-Methods

        /// <inheritdoc />
        public async Task<bool> GraphExists(Guid guid, CancellationToken token = default)
        {
            return await _Sdk.GraphExists(guid, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Graph> CreateGraph(string name, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            LiteGraph.Sdk.Graph graph = await _Sdk.CreateGraph(name, token).ConfigureAwait(false);
            if (graph != null) return GraphConverters.LgGraphToGraph(graph);
            return null;
        }

        /// <inheritdoc />
        public async Task<Graph> ReadGraph(Guid guid, CancellationToken token = default)
        {
            LiteGraph.Sdk.Graph graph = await _Sdk.ReadGraph(guid, token).ConfigureAwait(false);
            if (graph != null) return GraphConverters.LgGraphToGraph(graph);
            return null;
        }

        /// <inheritdoc />
        public async Task<List<Graph>> ReadGraphs(CancellationToken token = default)
        {
            IEnumerable<LiteGraph.Sdk.Graph> graphs = await _Sdk.ReadGraphs(token).ConfigureAwait(false);
            if (graphs != null) return GraphConverters.LgGraphListToGraphList(graphs.ToList());
            return null;
        }

        /// <inheritdoc />
        public async Task<Graph> UpdateGraph(Graph graph, CancellationToken token = default)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));
            LiteGraph.Sdk.Graph updated = await _Sdk.UpdateGraph(
                GraphConverters.GraphToLgGraph(graph), token).ConfigureAwait(false);
            if (updated != null) return GraphConverters.LgGraphToGraph(updated);
            return null;
        }

        /// <inheritdoc />
        public async Task DeleteGraph(Guid guid, bool force = false,CancellationToken token = default)
        {
            await _Sdk.DeleteGraph(guid, force, token).ConfigureAwait(false);
        }

        #endregion

        #region Public-Node-Methods

        /// <inheritdoc />
        public async Task<bool> NodeExists(Guid graphGuid, Guid guid, CancellationToken token = default)
        {
            return await _Sdk.NodeExists(graphGuid, guid, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<GraphNode> CreateNode(GraphNode node, CancellationToken token = default)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            Node created = await _Sdk.CreateNode(
                GraphConverters.GraphNodeToLgNode(node), token).ConfigureAwait(false);
            if (created != null) return GraphConverters.LgNodeToGraphNode(created);
            return null;
        }

        /// <inheritdoc />
        public async Task<GraphNode> ReadNode(Guid graphGuid, Guid nodeGuid, CancellationToken token = default)
        {
            Node node = await _Sdk.ReadNode(graphGuid, nodeGuid, token).ConfigureAwait(false);
            if (node != null) return GraphConverters.LgNodeToGraphNode(node);
            return null;
        }

        /// <inheritdoc />
        public async Task<List<GraphNode>> ReadNodes(Guid graphGuid, CancellationToken token = default)
        {
            IEnumerable<Node> nodes = await _Sdk.ReadNodes(graphGuid, token).ConfigureAwait(false);
            if (nodes != null) return GraphConverters.LgNodeListToGraphNodeList(nodes.ToList());
            return null;            
        }

        /// <inheritdoc />
        public async Task DeleteNode(Guid graphGuid, Guid nodeGuid, CancellationToken token = default)
        {
            await _Sdk.DeleteNode(graphGuid, nodeGuid, token).ConfigureAwait(false);
        }

        #endregion

        #region Public-Edge-Methods

        /// <inheritdoc />
        public async Task<bool> EdgeExists(Guid graphGuid, Guid guid, CancellationToken token = default)
        {
            return await _Sdk.EdgeExists(graphGuid, guid, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<GraphEdge> CreateEdge(GraphEdge edge, CancellationToken token = default)
        {
            LiteGraph.Sdk.Edge created = await _Sdk.CreateEdge(
                GraphConverters.GraphEdgeToLgEdge(edge), token).ConfigureAwait(false);
            if (created != null) return GraphConverters.LgEdgeToGraphEdge(created);
            return null;
        }

        /// <inheritdoc />
        public async Task<GraphEdge> ReadEdge(Guid graphGuid, Guid edgeGuid, CancellationToken token = default)
        {
            LiteGraph.Sdk.Edge edge = await _Sdk.ReadEdge(graphGuid, edgeGuid, token).ConfigureAwait(false);
            if (edge != null) return GraphConverters.LgEdgeToGraphEdge(edge);
            return null;
        }

        /// <inheritdoc />
        public async Task<List<GraphEdge>> ReadEdges(Guid graphGuid, CancellationToken token = default)
        {
            IEnumerable<Edge> edges = await _Sdk.ReadEdges(graphGuid, token).ConfigureAwait(false);
            if (edges != null) return GraphConverters.LgEdgeListToGraphEdgeList(edges.ToList());
            return null;
        }

        /// <inheritdoc />
        public async Task DeleteEdge(Guid graphGuid, Guid edgeGuid, CancellationToken token = default)
        {
            await _Sdk.DeleteEdge(graphGuid, edgeGuid, token).ConfigureAwait(false);
        }

        #endregion

        #region Public-Traversal-Methods

        /// <inheritdoc />
        public async Task<List<GraphEdge>> EdgesFromNode(Guid graphGuid, Guid nodeGuid, CancellationToken token = default)
        {
            IEnumerable<LiteGraph.Sdk.Edge> edges = await _Sdk.GetEdgesFromNode(graphGuid, nodeGuid, token).ConfigureAwait(false);
            if (edges != null) return GraphConverters.LgEdgeListToGraphEdgeList(edges.ToList());
            return null;

        }

        /// <inheritdoc />
        public async Task<List<GraphEdge>> EdgesToNode(Guid graphGuid, Guid nodeGuid, CancellationToken token = default)
        {
            IEnumerable<LiteGraph.Sdk.Edge> edges = await _Sdk.GetEdgesToNode(graphGuid, nodeGuid, token).ConfigureAwait(false);
            if (edges != null) return GraphConverters.LgEdgeListToGraphEdgeList(edges.ToList());
            return null;
        }

        /// <inheritdoc />
        public async Task<List<GraphEdge>> AllNodeEdges(Guid graphGuid, Guid nodeGuid, CancellationToken token = default)
        {
            IEnumerable<LiteGraph.Sdk.Edge> edges = await _Sdk.GetAllNodeEdges(graphGuid, nodeGuid, token).ConfigureAwait(false);
            if (edges != null) return GraphConverters.LgEdgeListToGraphEdgeList(edges.ToList());
            return null;
        }

        /// <inheritdoc />
        public async Task<List<GraphNode>> GetNodeParents(Guid graphGuid, Guid nodeGuid, CancellationToken token = default)
        {
            IEnumerable<LiteGraph.Sdk.Node> nodes = await _Sdk.GetParentsFromNode(graphGuid, nodeGuid, token).ConfigureAwait(false);
            if (nodes != null) return GraphConverters.LgNodeListToGraphNodeList(nodes.ToList());
            return null;
        }

        /// <inheritdoc />
        public async Task<List<GraphNode>> GetNodeChildren(Guid graphGuid, Guid nodeGuid, CancellationToken token = default)
        {
            IEnumerable<LiteGraph.Sdk.Node> nodes = await _Sdk.GetChildrenFromNode(graphGuid, nodeGuid, token).ConfigureAwait(false);
            if (nodes != null) return GraphConverters.LgNodeListToGraphNodeList(nodes.ToList());
            return null;
        }

        /// <inheritdoc />
        public async Task<List<GraphNode>> GetNodeNeighbors(Guid graphGuid, Guid nodeGuid, CancellationToken token = default)
        {
            IEnumerable<LiteGraph.Sdk.Node> nodes = await _Sdk.GetNodeNeighbors(graphGuid, nodeGuid, token).ConfigureAwait(false);
            if (nodes != null) return GraphConverters.LgNodeListToGraphNodeList(nodes.ToList());
            return null;
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}
