
namespace Graph.Models
{
    /// <summary>
    /// Represents the edges of a graph.
    /// </summary>
    public class Edge
    {
        /// <summary>
        /// Label of node on edge end
        /// </summary>
        public char NodeLabel { get; set; }

        /// <summary>
        /// Weight of edge
        /// </summary>
        public int Weight { get; set; }
    }
}