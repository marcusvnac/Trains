using System.Collections.Generic;

namespace Graph.Models
{
    /// <summary>
    /// Representes a Graph
    /// </summary>
    public class GraphModel
    {
        /// <summary>
        /// Nodes list of graph
        /// </summary>
        public List<Node> Nodes { get; set; }

        /// <summary>
        /// Initializes a new instance of the Graph class that has a empty node list.
        /// </summary>
        public GraphModel()
        {
            Nodes = new List<Node>();
        }
    }
}