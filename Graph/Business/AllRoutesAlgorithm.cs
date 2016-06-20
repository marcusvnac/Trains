using Graph.Models;
using System.Collections.Generic;
using System.Text;

namespace Graph.Business
{
    /// <summary>
    /// Implements an algorithm to find all routes from a node to another in a graph.
    /// </summary>
    public class AllRoutesAlgorithm
    {
        /// <summary>
        /// Graph object.
        /// </summary>
        private GraphModel graph;

        /// <summary>
        /// Initial node of a path.
        /// </summary>
        private char startNode;

        /// <summary>
        /// End node of a path.
        /// </summary>
        private char endNode;

        /// <summary>
        /// Number of stops in a path.
        /// </summary>
        private int stopNumber;

        /// <summary>
        /// List of routes found.
        /// </summary>
        private List<string> routes;

        /// <summary>
        /// Stack of nodes that are still not visited.
        /// </summary>
        private Stack<char> nodesToVisit;

        /// <summary>
        /// Initializes a new instance of the AllRoutesAlgorithm class with the informations provided.
        /// </summary>
        /// <param name="graph">Graph where algorithm is executed.</param>
        /// <param name="startNode">Initial node of path.</param>
        /// <param name="endNode">End node of a path.</param>
        /// <param name="stopNumber">Number of stops in a path.</param>
        public AllRoutesAlgorithm(GraphModel graph, char startNode, char endNode, int stopNumber)
        {
            this.graph = graph;
            this.startNode = startNode;
            this.endNode = endNode;
            this.stopNumber = stopNumber;

            routes = new List<string>();

            nodesToVisit = new Stack<char>();
            nodesToVisit.Push(startNode);
        }

        /// <summary>
        /// Computes all routes between two nodes in a graph, based in a stop number count
        /// <param name="maxStopNumber">If <code>true</code> indicates that the algorithm will search for a route with max number of stops,
        /// otherwise, algorothm will search for a route with exactly number of stops</param>
        /// </summary>
        private void ComputeRoutes(bool useStopCount)
        {
            StringBuilder route = new StringBuilder();
            int actualWeight = 0; 

            int count;
            if (useStopCount)
                count = -1;
            else
                count = 0;

            while (nodesToVisit.Count > 0)
            {
                char actualNode = nodesToVisit.Pop();
                Node node = graph.Nodes.Find(n => n.Label.Equals(actualNode));

                // Verify if route still valid
                if (route.Length > 0)
                {
                    char priorNodeToVerify = route[route.Length - 1];
                    Node verNode = graph.Nodes.Find(n => n.Label.Equals(priorNodeToVerify));
                    Edge verEdge = verNode.edges.Find(n => n.NodeLabel.Equals(node.Label));

                    if (verEdge == null)
                        continue;
                    else if (!useStopCount)
                        actualWeight = verEdge.Weight;
                }

                // Add a node to route
                if (route.Length == 0)
                    route.Append(node.Label);
                else
                    route.Append("-" + node.Label);

                if (useStopCount)
                    count++;
                else
                    count += actualWeight;

                // Verify if maximum stops reached
                if ((useStopCount && ((count >= stopNumber) && !(node.Label.Equals(endNode))))
                    || (!useStopCount && ((count >= stopNumber))))
                {
                    route.Remove(route.Length-2, 2);
                    if (useStopCount)
                        count--;
                    else
                        count -= actualWeight;

                    bool continueValidation = true;
                    while (continueValidation)
                    {
                        char nodeToVerify = route[route.Length - 1];
                        char priorNodeToVerify = route[route.Length - 3];

                        Node verNode = graph.Nodes.Find(n => n.Label.Equals(nodeToVerify));
                        Node priorNode = graph.Nodes.Find(n => n.Label.Equals(priorNodeToVerify));
                        if (verNode.edges.Count <= 1
                            || (priorNode.edges.Find(n => n.NodeLabel.Equals(nodeToVerify)) == null))
                        {
                            route.Remove(route.Length - 2, 2);
                            if (useStopCount)
                                count--;
                            else
                                count -= actualWeight;
                            continueValidation = (route.Length > 2);
                        }
                        else
                            continueValidation = false;
                    }
                    if ((nodesToVisit.Count == 1) && (count >= stopNumber))
                    {
                        count = 0;
                        route.Clear();
                        route.Append(startNode);
                    }
                    continue;
                }

                // Verify if found a path end 
                if (node.Label.Equals(endNode))
                {
                    if (!routes.Contains(route.ToString())
                        && route.ToString().Split('-').Length > 1)
                        routes.Add(route.ToString());

                    if (count >= stopNumber-1)
                    {                        
                        route.Clear();
                        route.Append(startNode);
                        count = 0;
                        continue;
                    }
                }
                
                foreach (Edge conn in node.edges)
                {
                    nodesToVisit.Push(conn.NodeLabel);
                }
            } 
        }

        /// <summary>
        /// Computes all routes between two nodes in a graph.
        /// <param name="maxStopNumber">If <code>true</code> indicates that the algorithm will search for a route with max number of stops,
        /// otherwise, algorithm will search for a route with exactly number of stops</param>
        /// </summary>
        /// <returns>List of <code>GraphRouteAlgorithmReturn</code> with the informations about the paths found. Empty list otherwise.</returns>

        private List<GraphRouteAlgorithmReturn> ComputeAllRoutes(bool isMaxStopNumber)
        {
            ComputeRoutes(true);

            List<GraphRouteAlgorithmReturn> result = new List<GraphRouteAlgorithmReturn>();

            foreach (string route in routes)
            {
                GraphRouteAlgorithmReturn path = new GraphRouteAlgorithmReturn();
                path.Value = route.Split('-').Length - 1;       // number of stops in path
                path.Path = route;

                if (isMaxStopNumber || (!isMaxStopNumber && path.Value == stopNumber))
                    result.Add(path);
            }

            return result;
        }


        /// <summary>
        /// Computes all routes between two nodes in a graph with a maximum number of stops.
        /// </summary>
        /// <returns>List of <code>GraphRouteAlgorithmReturn</code> with the informations about the paths found. Empty list otherwise.</returns>
        public List<GraphRouteAlgorithmReturn> ComputeAllRoutesMaxStops()
        {
            return ComputeAllRoutes(true);
        }

        /// <summary>
        /// Computes all routes between two nodes in a graph with a defined number of stops.
        /// </summary>
        /// <returns>List of <code>GraphRouteAlgorithmReturn</code> with the informations about the paths found. Empty list otherwise.</returns>
        public List<GraphRouteAlgorithmReturn> ComputeAllRoutesNumStops()
        {
            return ComputeAllRoutes(false);
        }

        /// <summary>
        /// Computes the number of routes between two nodes with a maximum distance.
        /// </summary>
        /// <returns>List of <code>GraphRouteAlgorithmReturn</code> with the informations about the paths found. Empty list otherwise.</returns>
        public int ComputeAllRoutesMaxDistance()
        {
            ComputeRoutes(false);

            return routes.Count;
        }
    }
}
