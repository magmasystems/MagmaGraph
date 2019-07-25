using System;
using MagmaGraph.Algorithms;
using MagmaGraph.Core;
using MagmaGraph.IO;

namespace MagmaGraph.Application
{
	class Program
	{
		public IGraph Graph { get; private set; }

		static void Main()
		{
			Program app = new Program();
			app.Initialize();

			app.TestThoughtWorksRailNetwork1();
			app.TestThoughtWorksRailNetwork2();
			app.TestThoughtWorksRailNetwork3();
			app.TestThoughtWorksRailNetwork4();
			app.TestThoughtWorksRailNetwork5();
			app.TestThoughtWorksRailNetwork6();
			app.TestThoughtWorksRailNetwork7();
			app.TestThoughtWorksRailNetwork8();
			app.TestThoughtWorksRailNetwork9();
			app.TestThoughtWorksRailNetwork10();
	
			Console.WriteLine("Press ENTER to quit");
			Console.ReadLine();
		}

		public void Initialize()
		{
			// Graph: AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7

			var tr = GraphReaderFactory.Create("ThoughtWorks", "sample", Directionality.Unidirectional);
			bool rc = tr.Read("ThoughtWorksRailNetwork.txt");
			if (!rc)
			{
				Console.WriteLine("Could not read the input file");
			}
			
			this.Graph = tr.Graph;
		}


		public void TestThoughtWorksRailNetwork1()
		{
			// Cost of the route A-B-C
			int totalCost = Graph.GetCostOfRoute(new[] { "A", "B", "C" });
			Console.WriteLine((totalCost < 0) ? "NO SUCH ROUTE" : totalCost.ToString());
		}

		public void TestThoughtWorksRailNetwork2()
		{
			// Cost of the route A-D
			int totalCost = Graph.GetCostOfRoute(new[] { "A", "D" });
			Console.WriteLine((totalCost < 0) ? "NO SUCH ROUTE" : totalCost.ToString());
		}

		public void TestThoughtWorksRailNetwork3()
		{
			// Cost of the route A-D-C
			int totalCost = Graph.GetCostOfRoute(new[] { "A", "D", "C" });
			Console.WriteLine((totalCost < 0) ? "NO SUCH ROUTE" : totalCost.ToString());
		}

		public void TestThoughtWorksRailNetwork4()
		{
			// Cost of the route A-E-B-C-D
			int totalCost = Graph.GetCostOfRoute(new[] { "A", "E", "B", "C", "D" });
			Console.WriteLine((totalCost < 0) ? "NO SUCH ROUTE" : totalCost.ToString());
		}

		public void TestThoughtWorksRailNetwork5()
		{
			// Cost of the route A-E-D
			int totalCost = Graph.GetCostOfRoute(new[] { "A", "E", "D" });
			Console.WriteLine((totalCost < 0) ? "NO SUCH ROUTE" : totalCost.ToString());
		}

		public void TestThoughtWorksRailNetwork6()
		{
			// The number of trips starting at C and ending at C with a maximum of 3 stops. 
			// In the sample data below, there are two such trips: C-D-C (2 stops). and C-E-B-C (3 stops).
			PathGenerators pathGenerator = new PathGenerators(this.Graph);
			var paths = pathGenerator.GeneratePathsWithinRangeOfStops("C", "C", 1, 4);
			Console.WriteLine(paths?.Count.ToString() ?? "NO SUCH ROUTE");
		}

		public void TestThoughtWorksRailNetwork7()
		{
			// The number of trips starting at A and ending at C with exactly 4 stops. 
			// In the sample data below, there are three such trips: A to C (via B,C,D); A to C (via D,C,D); and A to C (via D,E,B).
			PathGenerators pathGenerator = new PathGenerators(this.Graph);
			var paths = pathGenerator.GeneratePathsWithinRangeOfStops("A", "C", 3, 4);
			Console.WriteLine(paths?.Count.ToString() ?? "NO SUCH ROUTE");
		}

		public void TestThoughtWorksRailNetwork8()
		{
			// The length of the shortest route (in terms of distance to travel) from A to C. (should be 9)
			DjikstraShortestPath shortestPath = new DjikstraShortestPath(Graph);
			int costOfShortestPath = shortestPath.Solve("A", "C");
			Console.WriteLine((costOfShortestPath < 0) ? "NO SUCH ROUTE" : costOfShortestPath.ToString());
		}

		public void TestThoughtWorksRailNetwork9()
		{
			// The length of the shortest route (in terms of distance to travel) from B to B. (should be 9)
			DjikstraShortestPathWithRevisiting shortestPath = new DjikstraShortestPathWithRevisiting(Graph);
			int costOfShortestPath = shortestPath.Solve("B", "B");
			Console.WriteLine((costOfShortestPath < 0) ? "NO SUCH ROUTE" : costOfShortestPath.ToString());
		}

		public void TestThoughtWorksRailNetwork10()
		{
			// The number of different routes from C to C with a distance of less than 30. 
			// In the sample data, the trips are: CDC, CEBC, CEBCDC, CDCEBC, CDEBC, CEBCEBC, CEBCEBCEBC.
			PathGenerators pathGenerator = new PathGenerators(this.Graph);
			var paths = pathGenerator.GeneratePathsWithinRangeOfCost("C", "C", 1, 30);
			Console.WriteLine(paths?.Count.ToString() ?? "NO SUCH ROUTE");
		}
	}
}
