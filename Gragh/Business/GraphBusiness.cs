using Graph.Exceptions;
using Graph.Models;
using System;
using System.Collections.Generic;

namespace Graph.Business
{
    /// <summary>
    /// Responsible for manipulate a <code>GraphModel</code> object.
    /// </summary>
    public static class GraphBusiness
    {
        /// <summary>
        /// Creates a Graph according the information provided.
        /// </summary>
        /// <param name="graphInfo">Informations about the Graph. Format 'AB9'.</param>
        /// <exception cref="Graph.Exceptions.DuplicatedNodeConnectionException">Thrown when graph contains a duplicated node definition.</exception>
        /// <exception cref="Graph.Exceptions.NodeInfoIncorrectException">Thrown when informations provided are not in correct format.</exception>
        /// <returns>A <code>GraphModel</code> object created according <paramref name="graphInfo"/></returns>
        public static GraphModel BuildGraph(string graphInfo)
        {
            GraphModel graph = new GraphModel();
            // Clean and get information graph information
            graphInfo = graphInfo.Replace(" ", "").ToUpper().Trim();
            string[] nodeInfos = graphInfo.Split(',');

            foreach (string nodeInfo in nodeInfos)
            {
                // All nodes has starting and ending and a weight
                if (nodeInfo.Length == 3)
                {
                    // Build a Node and Edge Information
                    char nodeName = nodeInfo[0];
                    Edge nodeConn = new Edge();
                    nodeConn.NodeLabel = nodeInfo[1];
                    nodeConn.Weight = int.Parse(nodeInfo[2].ToString());

                    // Searches for a node in Graph                    
                    Node node = graph.Nodes.Find(n => n.Label.Equals(nodeName));

                    // Add a new node or new edge in a existing node
                    if (node != null)
                    {
                        if (node.edges.Find(e => e.NodeLabel.Equals(nodeConn.NodeLabel)) == null)
                            node.edges.Add(nodeConn);
                        else
                            throw new DuplicatedNodeConnectionException(String.Format("Node: {0}, Weight: {1}", nodeConn.NodeLabel, nodeConn.Weight));
                    }
                    else
                    {
                        Node newNode = new Node();
                        newNode.Label = nodeName;
                        newNode.edges.Add(nodeConn);

                        graph.Nodes.Add(newNode);
                    }
                }
                else
                    throw new NodeInfoIncorrectException(nodeInfo);                    
            }
            return graph;
        }

        /// <summary>
        /// Computes the total weight of any route in a graph.
        /// </summary>
        /// <param name="graph">Graph informations.</param>
        /// <param name="route">Route to calculate weight. Format: 'A-B-C'</param>
        /// <returns>-1 if doesn't exists a route, total weight value if exists</returns>
        public static int RouteDistance(GraphModel graph, string route)
        {
            // Clean and get route information
            route = route.Replace(" ", "").ToUpper().Trim();
            string[] routeInfos = route.Split('-');

            // Only have a route if exists 2 places at least
            if (routeInfos.Length < 2)
                return -1;

            Node node = null;
            int result = -1;
            for (int i = 0; i < routeInfos.Length; i++)
            {
                char nodeName = Convert.ToChar(routeInfos[i]);
                if (i > 0)
                {
                    // Searchs for a conection between two nodes in a graph
                    Edge conn = node.edges.Find(n => n.NodeLabel.Equals(nodeName));

                    if (conn != null)
                    {
                        result += conn.Weight;                        
                        // Set actual node as last node analysed in route
                        node = graph.Nodes.Find(n => n.Label.Equals(nodeName));
                    }
                    else
                        return -1;
                }
                else
                {
                    // First place of a route, searches for a node in Graph                    
                    node = graph.Nodes.Find(n => n.Label.Equals(nodeName));

                    if (node != null)
                        result = 0;
                    else
                        return -1;
                }
            }
            return result;
        }

        /// <summary>
        /// Computes the shortest path between two nodes.
        /// </summary>
        /// <param name="graph">Graph informations.</param>
        /// <param name="startNode">Initial node of a path.</param>
        /// <param name="endNode">End node of a path.</param>
        /// <returns>Object <code>GraphRouteAlgorithmReturn</code> that contains the path and the weight of distance computed.</returns>
        public static GraphRouteAlgorithmReturn ComputeShortestRoute(GraphModel graph, char startNode, char endNode)
        {
            Dijkstra algorithm = new Dijkstra(graph, startNode, endNode);

            return algorithm.ComputeShortestRoute();
        }

        /// <summary>
        /// Computes all routes between two nodes with a maximum number of stops.
        /// </summary>
        /// <param name="graph">Graph informations.</param>
        /// <param name="startNode">Initial node of a path.</param>
        /// <param name="endNode">End node of a path.</param>
        /// <param name="maxStops">Maximum number of stops in a path.</param>
        /// <returns>List of <code>GraphRouteAlgorithmReturn</code> object that contains the paths and the weights of all routes found.</returns>
        public static List<GraphRouteAlgorithmReturn> ComputeAllRoutesMaxStops(GraphModel graph, char startNode, char endNode, int maxStops)
        {
            AllRoutesAlgorithm algorithm = new AllRoutesAlgorithm(graph, startNode, endNode, maxStops);

            return algorithm.ComputeAllRoutesMaxStops();
        }

        /// <summary>
        /// Computes all routes between two nodes with a defined number of stops.
        /// </summary>
        /// <param name="graph">Graph informations.</param>
        /// <param name="startNode">Initial node of a path.</param>
        /// <param name="endNode">End node of a path.</param>
        /// <param name="numStops">Maximum number of stops in a path.</param>
        /// <returns>List of <code>GraphRouteAlgorithmReturn</code> object that contains the paths and the weights of all routes found.</returns>
        public static List<GraphRouteAlgorithmReturn> ComputeAllRoutesNumStops(GraphModel graph, char startNode, char endNode, int numStops)
        {
            AllRoutesAlgorithm algorithm = new AllRoutesAlgorithm(graph, startNode, endNode, numStops);

            return algorithm.ComputeAllRoutesNumStops();
        }

        /// <summary>
        /// Computes the number of routes between two nodes with a maximum distance.
        /// </summary>
        /// <param name="graph">Graph informations.</param>
        /// <param name="startNode">Initial node of a path.</param>
        /// <param name="endNode">End node of a path.</param>
        /// <param name="maxDistance">Maximum distance between nodes.</param>
        /// <returns>Number of routes with a maximum distance.</returns>
        public static int ComputeAllRoutesMaxDistance(GraphModel graph, char startNode, char endNode, int maxDistance)
        {
            AllRoutesAlgorithm algorithm = new AllRoutesAlgorithm(graph, startNode, endNode, maxDistance);

            return algorithm.ComputeAllRoutesMaxDistance();
        }
    }
}