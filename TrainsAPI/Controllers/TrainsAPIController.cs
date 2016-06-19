using Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrainsAPI.Models;

namespace TrainsAPI.Controllers
{

    /// <summary>
    /// Responsible to provide a RESTfull services to Commuter Railroad Services
    /// </summary>
    [RoutePrefix("api/TrainsAPI")]
    public class TrainsAPIController : ApiController
    {

        /// <summary>
        /// Creates a new instance of a Graph to be used in avaiable operations.
        /// </summary>
         private static void BuildGraphInstance()
         {
            string text = System.IO.File.ReadAllText(@"C:\VS-Workspaces\Projects\TW\Trains\TrainsAPI\CityConnections.txt");             

            GraphInstance graph = GraphInstance.Instance;
            graph.BuildGraph(text);
        }

         /// <summary>
         /// Computes the total weight of any route in a graph.
         /// </summary>
         /// <param name="route">Route to calculate weight. Format: 'A-B-C'</param>
         /// <returns>-1 if doesn't exists a route, total weight value if exists</returns>
         // GET Distance/A-B-C
         [Route("RouteDistance/{route}")]

         public int GetRouteDistance(string route)
         {
             GraphInstance graph = GraphInstance.Instance;
             if (!graph.IsGraphBuilded())
                 BuildGraphInstance();

             return graph.Distance(route);
         }


        /// <summary>
        /// Computes all routes between two nodes with maximum number of stops.
        /// </summary>
        /// <param name="startNode">Initial node of a path.</param>
        /// <param name="endNode">End node of a path.</param>
        /// <param name="maxStops">Maximum number of stops in a path.</param>
        /// <returns>List of <code>GraphRouteAlgorithmReturn</code> object in JSON or XML format, that contains the paths and the weights of all routes found.</returns>
        // GET AvailableRoutesMaxStops/C/C/3
        [Route("AvailableRoutesMaxStops/{startNode:length(1)}/{endNode:length(1)}/{maxStops:int}")]

        public IEnumerable<GraphRouteAlgorithmReturn> GetAllRoutesMaxStops(char startNode, char endNode, int maxStops)
        {
            GraphInstance graph = GraphInstance.Instance;
            if (!graph.IsGraphBuilded())
                BuildGraphInstance();

            return graph.AllRoutesMaxStops(startNode, endNode, maxStops);
        }


        /// <summary>
        /// Computes all routes between two nodes with a defined number of stops.
        /// </summary>
        /// <param name="startNode">Initial node of a path.</param>
        /// <param name="endNode">End node of a path.</param>
        /// <param name="numStops">Number of stops in a path.</param>
        /// <returns>List of <code>GraphRouteAlgorithmReturn</code> object in JSON or XML format, that contains the paths and the weights of all routes found.</returns>
        // GET AvailableRoutesNumStops/A/C/4
        [Route("AvailableRoutesNumStops/{startNode:length(1)}/{endNode:length(1)}/{numStops:int}")]

        public IEnumerable<GraphRouteAlgorithmReturn> GetAllRoutesNumStops(char startNode, char endNode, int numStops)
        {
            GraphInstance graph = GraphInstance.Instance;
            if (!graph.IsGraphBuilded())
                BuildGraphInstance();

            return graph.AllRoutesNumStops(startNode, endNode, numStops);
        }

        /// <summary>
        /// Computes the number of routes between two nodes with a maximum distance.
        /// </summary>
        /// <param name="startNode">Initial node of a path.</param>
        /// <param name="endNode">End node of a path.</param>
        /// <param name="maxDistance">Maximum distance between nodes.</param>
        /// <returns>Number of routes with a maximum distance.</returns>
        // GET AvailableRouteNumStops/A/C/4
        [Route("AvailableRoutesMaxDistance/{startNode:length(1)}/{endNode:length(1)}/{maxDistance:int}")]

        public int GetAllRoutesMaxDistance(char startNode, char endNode, int maxDistance)
        {
            GraphInstance graph = GraphInstance.Instance;
            if (!graph.IsGraphBuilded())
                BuildGraphInstance();

            return graph.AllRoutesMaxDistance(startNode, endNode, maxDistance);
        }

        /// <summary>
        /// Computes the shortest path between two nodes.
        /// </summary>
        /// <param name="startNode">Initial node of a path.</param>
        /// <param name="endNode">End node of a path.</param>
        /// <returns>Object <code>GraphRouteAlgorithmReturn</code> in JSON or XML format, that contains the path and the weight of distance computed.</returns>
        // GET ShortestRoute/A/C"
        [Route("ShortestRoute/{startNode:length(1)}/{endNode:length(1)}")]

        public GraphRouteAlgorithmReturn GetShortestRoute(char startNode, char endNode)
        {
            GraphInstance graph = GraphInstance.Instance;
            if (!graph.IsGraphBuilded())
                BuildGraphInstance();

            return graph.ShortestRoute(startNode, endNode);
        }

    }
}
