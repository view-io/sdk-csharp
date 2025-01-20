namespace View.Sdk.Graph
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using ExpressionTree;
    using LiteGraph.Sdk;
    using View.Sdk.Serialization;

    using Serializer = View.Sdk.Serialization.Serializer;

    /// <summary>
    /// LiteGraph driver.
    /// </summary>
    public class LiteGraphDriver : IGraphDriver, IDisposable
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

        /// <summary>
        /// Timeout, in milliseconds.
        /// </summary>
        public int TimeoutMs
        {
            get
            {
                return _TimeoutMs;
            }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(TimeoutMs));
                _TimeoutMs = value;
            }
        }

        #endregion

        #region Private-Members

        private string _Endpoint = null;
        private string _AccessKey = null;
        private LiteGraphSdk _Sdk = null;
        private Serializer _Serializer = new Serializer();
        private int _TimeoutMs = 300000;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="accessKey">Access key.</param>
        public LiteGraphDriver(string endpoint, string accessKey)
        {
            if (String.IsNullOrEmpty(endpoint)) throw new ArgumentNullException(nameof(endpoint));
            if (String.IsNullOrEmpty(accessKey)) throw new ArgumentNullException(nameof(accessKey));

            _Endpoint = endpoint;
            _AccessKey = accessKey;
            _Sdk = new LiteGraphSdk(_Endpoint, _AccessKey);
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            if (_Sdk != null) _Sdk.Dispose();

            _Serializer = null;
        }

        #endregion

        #region Public-Interface-Methods

        #region Public-General-Methods

        /// <inheritdoc />
        public async Task<bool> ValidateConnectivity(CancellationToken token = default)
        {
            return await _Sdk.ValidateConnectivity(token).ConfigureAwait(false);
        }

        #endregion

        #region Public-Graph-Methods

        /// <inheritdoc />
        public async Task<bool> GraphExists(Guid tenantGuid, Guid guid, CancellationToken token = default)
        {
            return await _Sdk.GraphExists(tenantGuid, guid, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Graph> CreateGraph(Guid tenantGuid, Guid guid, string name, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            LiteGraph.Sdk.Graph graph = await _Sdk.CreateGraph(tenantGuid, guid, name, null, null, null, null, token).ConfigureAwait(false);
            if (graph != null) return GraphConverters.LgGraphToGraph(graph);
            return null;
        }

        /// <inheritdoc />
        public async Task<Graph> ReadGraph(Guid tenantGuid, Guid guid, CancellationToken token = default)
        {
            LiteGraph.Sdk.Graph graph = await _Sdk.ReadGraph(tenantGuid, guid, token).ConfigureAwait(false);
            if (graph != null) return GraphConverters.LgGraphToGraph(graph);
            return null;
        }

        /// <inheritdoc />
        public async Task<List<Graph>> ReadGraphs(Guid tenantGuid, CancellationToken token = default)
        {
            IEnumerable<LiteGraph.Sdk.Graph> graphs = await _Sdk.ReadGraphs(tenantGuid, token).ConfigureAwait(false);
            if (graphs != null) return GraphConverters.LgGraphListToGraphList(graphs.ToList());
            return null;
        }

        /// <inheritdoc />
        public async Task<Graph> UpdateGraph(Guid tenantGuid, Graph graph, CancellationToken token = default)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));
            LiteGraph.Sdk.Graph updated = await _Sdk.UpdateGraph(
                tenantGuid,
                GraphConverters.GraphToLgGraph(graph), token).ConfigureAwait(false);
            if (updated != null) return GraphConverters.LgGraphToGraph(updated);
            return null;
        }

        /// <inheritdoc />
        public async Task DeleteGraph(Guid tenantGuid, Guid guid, bool force = false,CancellationToken token = default)
        {
            await _Sdk.DeleteGraph(tenantGuid, guid, force, token).ConfigureAwait(false);
        }

        #endregion

        #region Public-Node-Methods

        /// <inheritdoc />
        public async Task<bool> NodeExists(Guid tenantGuid, Guid graphGuid, Guid guid, CancellationToken token = default)
        {
            return await _Sdk.NodeExists(tenantGuid, graphGuid, guid, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<GraphNode> CreateNode(Guid tenantGuid, GraphNode node, CancellationToken token = default)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            Node created = await _Sdk.CreateNode(
                tenantGuid,
                node.GraphGUID,
                GraphConverters.GraphNodeToLgNode(node), token).ConfigureAwait(false);
            if (created != null) return GraphConverters.LgNodeToGraphNode(created, _Serializer);
            return null;
        }

        /// <inheritdoc />
        public async Task<GraphNode> ReadNode(Guid tenantGuid, Guid graphGuid, Guid nodeGuid, CancellationToken token = default)
        {
            Node node = await _Sdk.ReadNode(tenantGuid,graphGuid, nodeGuid, token).ConfigureAwait(false);
            if (node != null) return GraphConverters.LgNodeToGraphNode(node, _Serializer);
            return null;
        }

        /// <inheritdoc />
        public async Task<List<GraphNode>> ReadNodes(Guid tenantGuid, Guid graphGuid, CancellationToken token = default)
        {
            IEnumerable<Node> nodes = await _Sdk.ReadNodes(tenantGuid,graphGuid, token).ConfigureAwait(false);
            if (nodes != null) return GraphConverters.LgNodeListToGraphNodeList(nodes.ToList(), _Serializer);
            return null;            
        }

        /// <inheritdoc/>
        public async Task<List<GraphNode>> SearchNodes(Guid tenantGuid, Guid graphGuid, Expr expr, CancellationToken token = default)
        {
            SearchRequest req = new SearchRequest
            {
                TenantGUID = tenantGuid,
                GraphGUID = graphGuid,
                Expr = expr
            };

            SearchResult result = await _Sdk.SearchNodes(tenantGuid, graphGuid, req, token).ConfigureAwait(false);
            if (result != null && result.Nodes != null) 
                return GraphConverters.LgNodeListToGraphNodeList(result.Nodes.ToList(), _Serializer);
            return null;
        }

        /// <inheritdoc />
        public async Task DeleteNode(Guid tenantGuid, Guid graphGuid, Guid nodeGuid, CancellationToken token = default)
        {
            await _Sdk.DeleteNode(tenantGuid, graphGuid, nodeGuid, token).ConfigureAwait(false);
        }

        #endregion

        #region Public-Edge-Methods

        /// <inheritdoc />
        public async Task<bool> EdgeExists(Guid tenantGuid, Guid graphGuid, Guid guid, CancellationToken token = default)
        {
            return await _Sdk.EdgeExists(tenantGuid,graphGuid, guid, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<GraphEdge> CreateEdge(Guid tenantGuid, GraphEdge edge, CancellationToken token = default)
        {
            LiteGraph.Sdk.Edge created = await _Sdk.CreateEdge(
                tenantGuid,
                edge.GraphGUID,
                GraphConverters.GraphEdgeToLgEdge(edge), token).ConfigureAwait(false);
            if (created != null) return GraphConverters.LgEdgeToGraphEdge(created);
            return null;
        }

        /// <inheritdoc />
        public async Task<GraphEdge> ReadEdge(Guid tenantGuid, Guid graphGuid, Guid edgeGuid, CancellationToken token = default)
        {
            LiteGraph.Sdk.Edge edge = await _Sdk.ReadEdge(tenantGuid, graphGuid, edgeGuid, token).ConfigureAwait(false);
            if (edge != null) return GraphConverters.LgEdgeToGraphEdge(edge);
            return null;
        }

        /// <inheritdoc />
        public async Task<List<GraphEdge>> ReadEdges(Guid tenantGuid, Guid graphGuid, CancellationToken token = default)
        {
            IEnumerable<Edge> edges = await _Sdk.ReadEdges(tenantGuid,graphGuid, token).ConfigureAwait(false);
            if (edges != null) return GraphConverters.LgEdgeListToGraphEdgeList(edges.ToList());
            return null;
        }

        /// <inheritdoc />
        public async Task DeleteEdge(Guid tenantGuid, Guid graphGuid, Guid edgeGuid, CancellationToken token = default)
        {
            await _Sdk.DeleteEdge(tenantGuid, graphGuid, edgeGuid, token).ConfigureAwait(false);
        }

        #endregion

        #region Public-Traversal-Methods

        /// <inheritdoc />
        public async Task<List<GraphEdge>> EdgesFromNode(Guid tenantGuid, Guid graphGuid, Guid nodeGuid, CancellationToken token = default)
        {
            IEnumerable<LiteGraph.Sdk.Edge> edges = await _Sdk.GetEdgesFromNode(tenantGuid, graphGuid, nodeGuid, token).ConfigureAwait(false);
            if (edges != null) return GraphConverters.LgEdgeListToGraphEdgeList(edges.ToList());
            return null;

        }

        /// <inheritdoc />
        public async Task<List<GraphEdge>> EdgesToNode(Guid tenantGuid, Guid graphGuid, Guid nodeGuid, CancellationToken token = default)
        {
            IEnumerable<LiteGraph.Sdk.Edge> edges = await _Sdk.GetEdgesToNode(tenantGuid, graphGuid, nodeGuid, token).ConfigureAwait(false);
            if (edges != null) return GraphConverters.LgEdgeListToGraphEdgeList(edges.ToList());
            return null;
        }

        /// <inheritdoc />
        public async Task<List<GraphEdge>> EdgesBetweenNodes(
            Guid tenantGuid,
            Guid graphGuid, 
            Guid fromNodeGuid, 
            Guid toNodeGuid, 
            CancellationToken token = default)
        {
            IEnumerable<LiteGraph.Sdk.Edge> edges = await _Sdk.GetEdgesBetween(tenantGuid, graphGuid, fromNodeGuid, toNodeGuid, token).ConfigureAwait(false);
            if (edges != null) return GraphConverters.LgEdgeListToGraphEdgeList(edges.ToList());
            return null;
        }

        /// <inheritdoc />
        public async Task<List<GraphEdge>> AllNodeEdges(Guid tenantGuid, Guid graphGuid, Guid nodeGuid, CancellationToken token = default)
        {
            IEnumerable<LiteGraph.Sdk.Edge> edges = await _Sdk.GetAllNodeEdges(tenantGuid, graphGuid, nodeGuid, token).ConfigureAwait(false);
            if (edges != null) return GraphConverters.LgEdgeListToGraphEdgeList(edges.ToList());
            return null;
        }

        /// <inheritdoc />
        public async Task<List<GraphNode>> GetNodeParents(Guid tenantGuid, Guid graphGuid, Guid nodeGuid, CancellationToken token = default)
        {
            IEnumerable<LiteGraph.Sdk.Node> nodes = await _Sdk.GetParentsFromNode(tenantGuid, graphGuid, nodeGuid, token).ConfigureAwait(false);
            if (nodes != null) return GraphConverters.LgNodeListToGraphNodeList(nodes.ToList(), _Serializer);
            return null;
        }

        /// <inheritdoc />
        public async Task<List<GraphNode>> GetNodeChildren(Guid tenantGuid, Guid graphGuid, Guid nodeGuid, CancellationToken token = default)
        {
            IEnumerable<LiteGraph.Sdk.Node> nodes = await _Sdk.GetChildrenFromNode(tenantGuid, graphGuid, nodeGuid, token).ConfigureAwait(false);
            if (nodes != null) return GraphConverters.LgNodeListToGraphNodeList(nodes.ToList(), _Serializer);
            return null;
        }

        /// <inheritdoc />
        public async Task<List<GraphNode>> GetNodeNeighbors(Guid tenantGuid, Guid graphGuid, Guid nodeGuid, CancellationToken token = default)
        {
            IEnumerable<LiteGraph.Sdk.Node> nodes = await _Sdk.GetNodeNeighbors(tenantGuid, graphGuid, nodeGuid, token).ConfigureAwait(false);
            if (nodes != null) return GraphConverters.LgNodeListToGraphNodeList(nodes.ToList(), _Serializer);
            return null;
        }

        #endregion

        #endregion

        #region Private-Methods

        #endregion
    }
}
