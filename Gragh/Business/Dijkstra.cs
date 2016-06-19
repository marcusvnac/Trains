using Graph.Models;
using System;
using System.Collections.Generic;

namespace Graph.Business
{
    /// <summary>
    /// Implements Dijkstra Algorithm to find the shortest path. 
    /// Based on approach described by R.Sedgewick (Algorithms in C++)
    /// </summary>
    public class Dijkstra
    {

        /// <summary>
        /// Represents informations about predecessors of a node.
        /// </summary>
        private class PredecessorData
        {
            /// <summary>
            /// Graph node.
            /// </summary>
            public char Node { get; set; }

            /// <summary>
            /// Predecessor of node.
            /// </summary>
            public char Predecessor { get; set; }

            /// <summary>
            /// Adds information about a node and your predecessor in node's predecessors list.
            /// </summary>
            /// <param name="dataList">List that contains all predecessors of a Node.</param>
            /// <param name="node">Graph node.</param>
            /// <param name="pred">Predecessor of a node.</param>
            /// <returns>List with predecessors of a node, updated with information provided.</returns>
            internal static List<PredecessorData> AddToList(List<PredecessorData> dataList, char node, char pred)
            {
                PredecessorData data = dataList.Find(p => p.Node.Equals(node));

                if (data == null)
                {
                    dataList.Add(new PredecessorData { Node = node, Predecessor = pred });
                }
                else
                {
                    data.Predecessor = pred;
                }

                return dataList;
            }
        }

        /// <summary>
        /// Represents the weight informations about a path until a node.
        /// </summary>
        private class WeightData
        {
            /// <summary>
            /// Graph Node.
            /// </summary>
            public char Node { get; set; }

            /// <summary>
            /// Weigth of a path until a node.
            /// </summary>
            public int Weight { get; set; }
        }
        

        /// <summary>
        /// Stores graph edges that are candidates to next step of algorithm.
        /// </summary> 
        private List<char> edge;

        /// <summary>
        /// Stores graph tree whose minimun path is already known.
        /// </summary>
        private List<char> tree;

        /// <summary>
        /// Stores information about predecessors of each edge.
        /// </summary> 
        private List<PredecessorData> predecessor;

        /// <summary>
        /// Stores information about the weight of the path unil each node.
        /// </summary>
        private List<WeightData> weight;

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
        /// Initializes a new instance of the Dijkstra class with the informations provided.
        /// </summary>
        /// <param name="graph">Graph where algorithm is executed.</param>
        /// <param name="startNode">Initial node of path.</param>
        /// <param name="endNode">End node of a path.</param>
        public Dijkstra(GraphModel graph, char startNode, char endNode)
        {
            this.graph = graph;
            this.startNode = startNode;
            this.endNode = endNode;
            
            edge = new List<char>();
            edge.Add(startNode);

            tree = new List<char>();
            predecessor = new List<PredecessorData>();

            InitializeWeight();        
        }
        
        /// <summary>
        /// Initializes the Weigth list.
        /// </summary>
        private void InitializeWeight()
        {
            weight = new List<WeightData>(graph.Nodes.Count);
            int weightValue;

            foreach(Node node in graph.Nodes)
            {
                if (!node.Label.Equals(startNode))
                    weightValue = int.MaxValue;
                else
                    weightValue = 0;

                weight.Add(new WeightData {Node = node.Label, Weight = weightValue});
            }
        }

        /// <summary>
        /// Finds the node that has minimum weight calculated.
        /// </summary>
        /// <returns></returns>
        private char FindMinimumWeightNodeFromEdge()
        {
            int minWeight = int.MaxValue;
            char result = '\0';

            foreach(char node in edge)
            {
                WeightData data = weight.Find(d => d.Node.Equals(node));
                if (data != null && minWeight >= data.Weight)
                {
                    minWeight = data.Weight;
                    result = data.Node;
                }
            }
            return result;
        }

        /// <summary>
        /// Verify if the path to a node to another is less than the path until the moment.
        /// </summary>
        /// <param name="minNode">Node that has the minimun weight at the moment.</param>
        /// <param name="node">Node that has the edge to verify</param>
        /// <returns><code>True</code> if the weight between the nodes is less than actual, <code>false</code> otherwise.</returns>
        private bool IsMinimumWeight(Node minNode, Node node)
        {
            int nodeWeight = weight.Find(c => c.Node.Equals(node.Label)).Weight;
            int minNodeWeight = weight.Find(c => c.Node.Equals(minNode.Label)).Weight;
            int pathWeight = minNode.edges.Find(n => n.NodeLabel.Equals(node.Label)).Weight;

            return (nodeWeight > (minNodeWeight + pathWeight) 
                || (startNode.Equals(endNode) && (startNode.Equals(node.Label))));
        }

        /// <summary>
        /// Sets the weight of a path to a node in weight list.
        /// </summary>
        /// <param name="minNode">Node that has the minimun weight at the moment.</param>
        /// <param name="node">Node to set the weight.</param>
        private void SetWeight(Node minNode, Node node)
        {
            WeightData minChargeData = weight.Find(c => c.Node.Equals(minNode.Label));
            Edge conn = minNode.edges.Find(n => n.NodeLabel.Equals(node.Label));

            WeightData data = weight.Find(c => c.Node.Equals(node.Label));
            if (data == null)
                weight.Add(new WeightData { Node = node.Label, Weight = minChargeData.Weight + conn.Weight });
            else
                data.Weight = minChargeData.Weight + conn.Weight;
        }

        /// <summary>
        /// Computes the Shorthest Path between two nodes. It creates a Minimum spanning tree of a Graph.
        /// </summary>
        private void FindShortestPath()
        {
            char minEdgeCharge;
            bool addPredecessor;

            while (edge.Count > 0)
            {
                addPredecessor = false;
                // Gets the node that has the minimum weight
                minEdgeCharge = FindMinimumWeightNodeFromEdge();
                edge.Remove(minEdgeCharge);

                // Adds the node to spanning tree
                tree.Add(minEdgeCharge);

                // Finds and iterate on the edges of a node that has the minimum weight at the moment
                Node minNode = graph.Nodes.Find(n => n.Label.Equals(minEdgeCharge));                
                foreach(Node node in graph.Nodes)
                {                    
                    if ((minNode.edges.Find(n => n.NodeLabel.Equals(node.Label)) != null)
                        && IsMinimumWeight(minNode, node))
                    {
                        predecessor = PredecessorData.AddToList(predecessor, node.Label, minNode.Label);
                        SetWeight(minNode, node);

                        if (!startNode.Equals(node.Label))
                            edge.Add(node.Label);
                        else
                            tree.Add(node.Label);

                        addPredecessor = true;
                    }
                }

                if (!addPredecessor)
                    tree.Remove(minEdgeCharge);
            }
        }

        /// <summary>
        /// Creates the path string to return according Predecessor list.
        /// </summary>
        /// <param name="dataList">List that contains all predecessors of a Node.</param>
        /// <param name="startNode">Initial node of a path.</param>
        /// <param name="endNode">End node of a path.</param>
        /// <returns>String that contains the path.</returns>
        private static string BuildPathCharge(List<PredecessorData> dataList, char startNode, char endNode)
        {
            PredecessorData data = dataList.Find(d => d.Node.Equals(endNode));
            string result = "";
            while (data != null)
            {
                if (result.Length > 0)
                    result = Convert.ToString(data.Node) + "-" + result;
                else
                    result = Convert.ToString(data.Node);

                data = dataList.Find(d => d.Node.Equals(data.Predecessor));
                if (data != null && data.Node.Equals(startNode))
                {
                    result = Convert.ToString(data.Node) + "-" + result;
                    break;
                }
                else if (data == null)
                    result = Convert.ToString(startNode) + "-" + result;
            }

            return result;
        }

        /// <summary>
        /// Computes the shortest path between two nodes in a graph.
        /// </summary>
        /// <returns>Object <code>GraphRouteAlgorithmReturn</code> with the information about the path found. <code>null</code> otherwise.</returns>
        public GraphRouteAlgorithmReturn ComputeShortestRoute()
        {
            FindShortestPath();

            PredecessorData pred = predecessor.Find(p => p.Node.Equals(endNode));
            if (pred != null)
            {
                WeightData data = weight.Find(c => c.Node.Equals(pred.Node));
                if (data.Weight != int.MaxValue)
                {
                    GraphRouteAlgorithmReturn path = new GraphRouteAlgorithmReturn();
                    path.Value = data.Weight;
                    path.Path = BuildPathCharge(predecessor, startNode, endNode);
                    return path;
                }
                else
                    return null;
            }
            else
                return null;
        }

    }
}
