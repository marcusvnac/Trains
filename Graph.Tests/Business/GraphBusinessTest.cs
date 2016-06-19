using Graph.Business;
using Graph.Exceptions;
using Graph.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Graph.Tests.Business
{
    /// <summary>
    /// Responsible for test the Business Class of Graph.
    /// </summary>
    [TestClass]
    public class GraphBusinessTest
    {
        [TestMethod]
        public void CreateGraphTest()
        {            
            // arrange
            string graphInfo = "AB6, AE4, BA6, BC2, BD4, CB3, CD1, CE7, DB8, EB5, ED7";

            // act
            GraphModel graph = GraphBusiness.BuildGraph(graphInfo);

            // assert
            Assert.IsTrue(graph.Nodes.Count == 5);
            Assert.IsTrue(graph.Nodes.Find(n => n.Label.Equals('A')).edges.Count == 2);
            Assert.IsTrue(graph.Nodes.Find(n => n.Label.Equals('B')).edges.Count == 3);
            Assert.IsTrue(graph.Nodes.Find(n => n.Label.Equals('C')).edges.Count == 3);
            Assert.IsTrue(graph.Nodes.Find(n => n.Label.Equals('D')).edges.Count == 1);
            Assert.IsTrue(graph.Nodes.Find(n => n.Label.Equals('E')).edges.Count == 2);
        }
    
        [TestMethod]
        public void CreateGraphNodeInfoIncorrectExceptionTest()
        {            
            // arrange
            string graphInfo = "AB6, AB, A4, BA6, B2, BD4, CB3, CD1, CE7, DB8, EB5, ED7";

            // act
            bool correctExcetion = false;
            try
            {
                GraphModel graph = GraphBusiness.BuildGraph(graphInfo);
            }
            catch (NodeInfoIncorrectException)
            {
                correctExcetion = true;
            }

            // assert
            Assert.IsTrue(correctExcetion);
        }

        [TestMethod]
        public void CreateGraphDuplicatedNodeConnectionExceptionTest()
        {
            // arrange
            string graphInfo = "AB6, AB6, A4, BA6, B2, BD4, CB3, CD1, CE7, DB8, EB5, ED7";

            // act
            bool correctExcetion = false;
            try
            {
                GraphModel graph = GraphBusiness.BuildGraph(graphInfo);
            }
            catch (DuplicatedNodeConnectionException)
            {
                correctExcetion = true;
            }

            // assert
            Assert.IsTrue(correctExcetion);
        }

        [TestMethod]
        public void RouteDistanceTest()
        {
            // arrange
            string graphInfo = "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7";
            
            // act
            GraphModel graph = GraphBusiness.BuildGraph(graphInfo);

            // assert
            Assert.AreEqual(9, GraphBusiness.RouteDistance(graph, "A-B-C"));
            Assert.AreEqual(5, GraphBusiness.RouteDistance(graph, "A-D"));
            Assert.AreEqual(13, GraphBusiness.RouteDistance(graph, "A-D-C"));
            Assert.AreEqual(22, GraphBusiness.RouteDistance(graph, "A-E-B-C-D"));
            Assert.AreEqual(-1, GraphBusiness.RouteDistance(graph, "A-E-D"));
        }


        [TestMethod]
        public void ComputeShortestRouteTest()
        {
            // arrange
            string graphInfo = "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7";

            // act
            GraphModel graph = GraphBusiness.BuildGraph(graphInfo);

            // assert
            GraphRouteAlgorithmReturn result = GraphBusiness.ComputeShortestRoute(graph, 'A', 'C');
            Assert.AreEqual(9, result.Value);
            Assert.AreEqual("A-B-C", result.Path);

            result = GraphBusiness.ComputeShortestRoute(graph, 'B', 'B');
            Assert.AreEqual(9, result.Value);
            Assert.AreEqual("B-C-E-B", result.Path);
        }

        [TestMethod]
        public void ComputeAllRoutesTest()
        {
            // arrange
            string graphInfo = "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7";

            // act
            GraphModel graph = GraphBusiness.BuildGraph(graphInfo);

            // assert            
            List<GraphRouteAlgorithmReturn> result = GraphBusiness.ComputeAllRoutesMaxStops(graph, 'C', 'C', 3);
            Assert.AreEqual(2, result.Count);
            Assert.IsNotNull(result.Find(f => f.Path.Equals("C-D-C")));
            Assert.AreEqual(2, result.Find(f => f.Path.Equals("C-D-C")).Value);

            Assert.IsNotNull(result.Find(f => f.Path.Equals("C-E-B-C")));
            Assert.AreEqual(3, result.Find(f => f.Path.Equals("C-E-B-C")).Value);
            
            result = GraphBusiness.ComputeAllRoutesMaxStops(graph, 'A', 'C', 4);
            //Assert.AreEqual(6, result.Count);
            Assert.AreEqual(4, result.Count);

            Assert.IsNotNull(result.Find(f => f.Path.Equals("A-B-C")));
            Assert.AreEqual(2, result.Find(f => f.Path.Equals("A-B-C")).Value);

            Assert.IsNotNull(result.Find(f => f.Path.Equals("A-D-E-B-C")));
            Assert.AreEqual(4, result.Find(f => f.Path.Equals("A-D-E-B-C")).Value);

            Assert.IsNotNull(result.Find(f => f.Path.Equals("A-E-B-C")));
            Assert.AreEqual(3, result.Find(f => f.Path.Equals("A-E-B-C")).Value);

            Assert.IsNotNull(result.Find(f => f.Path.Equals("A-B-C-D-C")));
            Assert.AreEqual(4, result.Find(f => f.Path.Equals("A-B-C-D-C")).Value);

            /*
            Assert.IsNotNull(result.Find(f => f.Path.Equals("A-D-C")));
            Assert.AreEqual(2, result.Find(f => f.Path.Equals("A-D-C")).Value);
            
            Assert.IsNotNull(result.Find(f => f.Path.Equals("A-D-C-D-C")));
            Assert.AreEqual(4, result.Find(f => f.Path.Equals("A-D-C-D-C")).Value);
            */
        }
    }
}
