namespace View.Sdk.Graph
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using LiteGraph.Sdk;
    using View.Sdk.Serialization;

    using Serializer = View.Sdk.Serialization.Serializer;

    internal static class GraphConverters
    {
        #region LiteGraph

        internal static LiteGraph.Sdk.Graph GraphToLgGraph(Graph graph)
        {
            if (graph == null) return null;

            return new LiteGraph.Sdk.Graph
            {
                GUID = graph.GUID,
                Name = graph.Name,
                CreatedUtc = graph.CreatedUtc
            };
        }

        internal static Graph LgGraphToGraph(LiteGraph.Sdk.Graph graph)
        {
            if (graph == null) return null;

            return new Graph
            {
                GUID = graph.GUID,
                Name = graph.Name,
                CreatedUtc = graph.CreatedUtc
            };
        }

        internal static List<Graph> LgGraphListToGraphList(List<LiteGraph.Sdk.Graph> graphs)
        {
            if (graphs == null) return null;

            List<Graph> ret = new List<Graph>();

            foreach (LiteGraph.Sdk.Graph graph in graphs)
            {
                ret.Add(LgGraphToGraph(graph));
            }

            return ret;
        }

        internal static LiteGraph.Sdk.Node GraphNodeToLgNode(GraphNode node)
        {
            if (node == null) return null;

            return new LiteGraph.Sdk.Node
            {
                GUID = node.GUID,
                GraphGUID = node.GraphGUID,
                Name = node.Name,
                Data = node.Data,
                CreatedUtc = node.CreatedUtc
            };
        }

        internal static GraphNode LgNodeToGraphNode(LiteGraph.Sdk.Node node, Serializer serializer)
        {
            if (node == null) return null;

            GraphData data = null;
            if (node.Data != null && node.Data.GetType() == typeof(JsonElement))
                data = new Serializer().DeserializeJson<GraphData>(((JsonElement)(node.Data)).GetRawText());

            return new GraphNode
            {
                GUID = node.GUID,
                GraphGUID = node.GraphGUID,
                Name = node.Name,
                Data = data,
                CreatedUtc = node.CreatedUtc
            };
        }

        internal static List<GraphNode> LgNodeListToGraphNodeList(List<LiteGraph.Sdk.Node> nodes, Serializer serializer)
        {
            if (nodes == null) return null;

            List<GraphNode> ret = new List<GraphNode>();

            foreach (LiteGraph.Sdk.Node node in nodes)
            {
                ret.Add(LgNodeToGraphNode(node, serializer));
            }

            return ret;
        }

        internal static LiteGraph.Sdk.Edge GraphEdgeToLgEdge(GraphEdge edge)
        {
            if (edge == null) return null;

            return new LiteGraph.Sdk.Edge
            {
                GUID = edge.GUID,
                GraphGUID = edge.GraphGUID,
                Name = edge.Name,
                From = edge.From,
                To = edge.To,
                Cost = edge.Cost,
                CreatedUtc = edge.CreatedUtc
            };
        }

        internal static GraphEdge LgEdgeToGraphEdge(LiteGraph.Sdk.Edge edge)
        {
            if (edge == null) throw new ArgumentNullException(nameof(edge));

            return new GraphEdge
            {
                GUID = edge.GUID,
                GraphGUID = edge.GraphGUID,
                Name = edge.Name,
                From = edge.From,
                To = edge.From,
                Cost = edge.Cost,
                CreatedUtc = edge.CreatedUtc
            };
        }

        internal static List<GraphEdge> LgEdgeListToGraphEdgeList(List<LiteGraph.Sdk.Edge> edges)
        {
            if (edges == null) return null;

            List<GraphEdge> ret = new List<GraphEdge>();

            foreach (LiteGraph.Sdk.Edge edge in edges)
            {
                ret.Add(LgEdgeToGraphEdge(edge));
            }

            return ret;
        }

        #endregion
    }
}
