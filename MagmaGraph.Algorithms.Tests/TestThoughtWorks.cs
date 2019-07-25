using System;
using MagmaGraph.Core;
using MagmaGraph.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MagmaGraph.Algorithms.Test
{
	[TestClass]
	public class TestThoughtWorks
	{
		public IGraph Graph { get; private set; }
	
		[TestInitialize]
		public void Initialize()
		{
			// Graph: AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7

			var tr = GraphReaderFactory.Create("ThoughtWorks", "sample", Directionality.Unidirectional);
			bool rc = tr.Read("ThoughtWorksRailNetwork.txt");
			Assert.IsTrue(rc);

			this.Graph = tr.Graph;
			Assert.IsNotNull(Graph);
		}

		[TestMethod]
		[ExpectedException(typeof(ApplicationException))]
		public void TestCreatingBidirectionalGraph()
		{
			var tr = GraphReaderFactory.Create("ThoughtWorks", "sample", Directionality.Bidirectional);
			this.Graph = tr.Graph;
			Assert.IsNotNull(Graph);

			// ReSharper disable once UnusedVariable
			PathGenerators pathGenerator = new PathGenerators(this.Graph);
		}

		[TestMethod]
		public void TestThoughtWorksRailNetwork1()
		{
			// Cost of the route A-B-C
			int totalCost = Graph.GetCostOfRoute(new[] {"A", "B", "C"});
			if (totalCost < 0)
				Assert.Fail("There is no route between all points in the route A-B-C");
			Assert.IsTrue(totalCost == 9, "The distance of A-B-C is not 9. The calculcation returned " + totalCost);
		}

		[TestMethod]
		public void TestThoughtWorksRailNetwork2()
		{
			// Cost of the route A-D
			int totalCost = Graph.GetCostOfRoute(new[] {"A", "D"});
			if (totalCost < 0)
				Assert.Fail("There is no route between all points in the route A-D");
			Assert.IsTrue(totalCost == 5, "The distance of A-D is not 5. The calculcation returned " + totalCost);
		}

		[TestMethod]
		public void TestThoughtWorksRailNetwork3()
		{
			// Cost of the route A-D-C
			int totalCost = Graph.GetCostOfRoute(new[] { "A", "D", "C" });
			if (totalCost < 0)
				Assert.Fail("There is no route between all points in the route A-D-C");
			Assert.IsTrue(totalCost == 13, "The distance of A-D-C is not 13. The calculcation returned " + totalCost);
		}

		[TestMethod]
		public void TestThoughtWorksRailNetwork4()
		{
			// Cost of the route A-E-B-C-D
			int totalCost = Graph.GetCostOfRoute(new[] {"A", "E", "B", "C", "D"});
			if (totalCost < 0)
				Assert.Fail("There is no route between all points in the route A-E-B-C-D");
			Assert.IsTrue(totalCost == 22, "The distance of A-E-B-C-D is not 22. The calculcation returned " + totalCost);
		}

		[TestMethod]
		public void TestThoughtWorksRailNetwork5()
		{
			// Cost of the route A-E-D
			int totalCost = Graph.GetCostOfRoute(new[] {"A", "E", "D"});
			if (totalCost >= 0)
				Assert.Fail("There should not be a route between points A-E-D");
		}

		[TestMethod]
		public void TestThoughtWorksRailNetwork6()
		{
			// The number of trips starting at C and ending at C with a maximum of 3 stops. 
			// In the sample data below, there are two such trips: C-D-C (2 stops). and C-E-B-C (3 stops).
			PathGenerators pathGenerator = new PathGenerators(this.Graph);
			var paths = pathGenerator.GeneratePathsWithinRangeOfStops("C", "C", 1, 4);
			Assert.IsNotNull(paths, "There should be paths");
			Assert.IsTrue(paths.Count == 2, "The number of paths from C to C of at most 3 stops should be 2");
		}

		[TestMethod]
		public void TestThoughtWorksRailNetwork7()
		{
			// The number of trips starting at A and ending at C with exactly 4 stops. 
			// In the sample data below, there are three such trips: A to C (via B,C,D); A to C (via D,C,D); and A to C (via D,E,B).
			PathGenerators pathGenerator = new PathGenerators(this.Graph);
			var paths = pathGenerator.GeneratePathsWithinRangeOfStops("A", "C", 3, 4);
			Assert.IsNotNull(paths, "There should be paths");
			Assert.IsTrue(paths.Count == 3, "The number of paths from A to C with exactly 4 stops should be 3");
		}

		[TestMethod]
		public void TestThoughtWorksRailNetwork8()
		{
			// The length of the shortest route (in terms of distance to travel) from A to C. (should be 9)
			DjikstraShortestPath shortestPath = new DjikstraShortestPath(Graph);
			int costOfShortestPath = shortestPath.Solve("A", "C");
			Assert.IsTrue(costOfShortestPath == 9, "The shortest distance between A and C is not 9. The calculcation returned " + costOfShortestPath);
		}

		[TestMethod]
		public void TestThoughtWorksRailNetwork9()
		{
			// The length of the shortest route (in terms of distance to travel) from B to B. (should be 9)
			DjikstraShortestPathWithRevisiting shortestPath = new DjikstraShortestPathWithRevisiting(Graph);
			int costOfShortestPath = shortestPath.Solve("B", "B");
			Assert.IsTrue(costOfShortestPath == 9, "The shortest distance between B and B is not 9. The calculcation returned " + costOfShortestPath);

		}

		[TestMethod]
		public void TestThoughtWorksRailNetwork10()
		{
			// The number of different routes from C to C with a distance of less than 30. 
			// In the sample data, the trips are: CDC, CEBC, CEBCDC, CDCEBC, CDEBC, CEBCEBC, CEBCEBCEBC.
			PathGenerators pathGenerator = new PathGenerators(this.Graph);
			var paths = pathGenerator.GeneratePathsWithinRangeOfCost("C", "C", 1, 30);
			Assert.IsNotNull(paths, "There should be paths");
			Assert.IsTrue(paths.Count == 7, "The number of paths from C to C with a distance of under 30 should be 7");
		}
	}
}
