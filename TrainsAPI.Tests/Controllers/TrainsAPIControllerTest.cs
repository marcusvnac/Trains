using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainsAPI.Controllers;
using System.Collections.Generic;
using Graph.Models;
using System.Linq;

namespace TrainsAPI.Tests.Controllers
{
    [TestClass]
    public class TrainsAPIControllerTest
    {

        [TestMethod]
        public void GetDistanceTest()
        {
            // Arrange
            TrainsAPIController controller = new TrainsAPIController();

            // Act
            int resp1 = controller.GetRouteDistance("A-B-C");
            int resp2 = controller.GetRouteDistance("A-D");
            int resp3 = controller.GetRouteDistance("A-D-C");
            int resp4 = controller.GetRouteDistance("A-E-B-C-D");
            int resp5 = controller.GetRouteDistance("A-E-D");

            // Assert
            Assert.AreEqual(9, resp1);
            Assert.AreEqual(5, resp2);
            Assert.AreEqual(13, resp3);
            Assert.AreEqual(22, resp4);
            Assert.AreEqual(-1, resp5);
        }
        
        [TestMethod]
        public void GetAllRoutesGetAllRoutesMaxStopsTest()
        {
            // Arrange
            TrainsAPIController controller = new TrainsAPIController();

            // Act
            var resp = controller.GetAllRoutesMaxStops('C', 'C', 3);

            // Assert
            Assert.AreEqual(2, resp.ToList().Count);
        }

        [TestMethod]
        public void GetAllRoutesNumStopsTest()
        {
            // Arrange
            TrainsAPIController controller = new TrainsAPIController();

            // Act
            var resp = controller.GetAllRoutesNumStops('A', 'C', 4);           

            // Assert
            Assert.AreEqual(3, resp.ToList().Count);
        }

        [TestMethod]
        public void GetShortestRouteTest()
        {
            // Arrange
            TrainsAPIController controller = new TrainsAPIController();

            // Act
            var resp1 = controller.GetShortestRoute('A', 'C');
            var resp2 = controller.GetShortestRoute('B', 'B');

            // Assert
            Assert.AreEqual(9, resp1.Value);
            Assert.AreEqual(9, resp2.Value);
        }

        [TestMethod]
        public void GetAllRoutesMaxDistanceTest()
        {
            // Arrange
            TrainsAPIController controller = new TrainsAPIController();

            // Act
            int resp = controller.GetAllRoutesMaxDistance('C', 'C', 30);

            // Assert
            Assert.AreEqual(7, resp);
        }
    }
}
