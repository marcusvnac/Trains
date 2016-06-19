using Graph.Business;
using Graph.Models;
using System;
using System.Collections.Generic;

namespace TrainsAPI.Models
{
	/// <summary>
	/// Represents a Singleton instance of a Graph, to be used in Graph avaiable operations.
	/// </summary>
	public class GraphInstance
	{
		/// <summary>
		/// <code>Lazy</code> object to implement singleton.
		/// </summary>
		private static Lazy<GraphInstance> lazy = new Lazy<GraphInstance>(() => new GraphInstance());

		/// <summary>
		/// Instance of a <code>GraphModel</code>.
		/// </summary>
		private GraphModel graph;

		/// <summary>
		/// Initializes a Singleton instance.
		/// </summary>
		private GraphInstance()
		{   
			// Implementa um Singleton, somente instacia uma nova conexão se não estiver conectado.
			if (graph == null)
			{
				graph = new GraphModel();
			}
		}

		/// <summary>
		/// Active class instance.
		/// </summary>
		public static GraphInstance Instance
		{
			get { return lazy.Value; }
		}

		/// <summary>
		/// Builds a graph according provided informations.
		/// </summary>
		/// <param name="graphInfo">Informations about the Graph. Format 'AB9'</param>
		public void BuildGraph(string graphInfo)
		{
			graph = GraphBusiness.BuildGraph(graphInfo);
		}

		/// <summary>
		/// Verify if there is a graph builded in loaded instance.
		/// </summary>
		/// <returns><code>True</code> if there is a graph loaded, <code>False</code> otherwise.</returns>
		public bool IsGraphBuilded()
		{
			return graph.Nodes.Count > 0;
		}

		/// <summary>
		/// Computes all routes between two nodes with a maximum number of stops.
		/// </summary>
		/// <param name="startNode">Initial node of a path.</param>
		/// <param name="endNode">End node of a path.</param>
		/// <param name="maxStops">Maximum number of stops in a path.</param>
		/// <returns>List of <code>GraphRouteAlgorithmReturn</code> object that contains the paths and the weights of all routes found.</returns>
		public List<GraphRouteAlgorithmReturn> AllRoutesMaxStops(char startNode, char endNode, int maxStops)
		{
			return GraphBusiness.ComputeAllRoutesMaxStops(graph, startNode, endNode, maxStops);
		}

        /// <summary>
        /// Computes all routes between two nodes with a defined number of stops.
        /// </summary>
        /// <param name="startNode">Initial node of a path.</param>
        /// <param name="endNode">End node of a path.</param>
        /// <param name="numStops">Number of stops in a path.</param>
        /// <returns>List of <code>GraphRouteAlgorithmReturn</code> object that contains the paths and the weights of all routes found.</returns>
        public List<GraphRouteAlgorithmReturn> AllRoutesNumStops(char startNode, char endNode, int numStops)
        {
            return GraphBusiness.ComputeAllRoutesNumStops(graph, startNode, endNode, numStops);
        }

        /// <summary>
        /// Computes the number of routes between two nodes with a maximum distance.
        /// </summary>
        /// <param name="startNode">Initial node of a path.</param>
        /// <param name="endNode">End node of a path.</param>
        /// <param name="maxDistance">Maximum distance between nodes.</param>
        /// <returns>Number of routes with a maximum distance.</returns>
        public int AllRoutesMaxDistance(char startNode, char endNode, int maxDistance)
        {
            return GraphBusiness.ComputeAllRoutesMaxDistance(graph, startNode, endNode, maxDistance);
        }

		/// <summary>
		/// Computes the total weight of any route in a graph.
		/// </summary>
		/// <param name="route">Route to calculate weight. Format: 'A-B-C'</param>
		/// <returns>-1 if doesn't exists a route, total weight value if exists</returns>
		public int Distance(string route)
		{
			return GraphBusiness.RouteDistance(graph, route);
		}

		/// <summary>
		/// Computes the shortest path between two nodes.
		/// </summary>
		/// <param name="startNode">Initial node of a path.</param>
		/// <param name="endNode">End node of a path.</param>
		/// <returns>Object <code>GraphRouteAlgorithmReturn</code> that contains the path and the weight of distance computed.</returns>
		public GraphRouteAlgorithmReturn ShortestRoute(char startNode, char endNode)
		{
			return GraphBusiness.ComputeShortestRoute(graph, startNode, endNode);
		}
	}
}