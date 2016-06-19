
namespace Graph.Models
{
    /// <summary>
    /// Represents de execution result of an route algorithm in a Graph.
    /// </summary>
    public class GraphRouteAlgorithmReturn
    {
        /// <summary>
        /// Route path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Represents a weigh of a path in Short Path Algorithm or total stops in All Routes Algorithm.
        /// </summary>
        public int Value { get; set; }

    }
}
