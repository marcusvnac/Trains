using System.Collections.Generic;

namespace Graph.Models
{
    /// <summary>
    /// Represents a graph node
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Label of a node
        /// </summary>
        public char Label { get; set; }

        /// <summary>
        /// Represents the edges of connected on Node.
        /// </summary>
        public List<Edge> edges { get; set; }

        /// <summary>
        /// Initializes a new instance of the Node class that has a empty edge list.
        /// </summary>
        public Node()
        {
            edges = new List<Edge>();
        }
    }
}