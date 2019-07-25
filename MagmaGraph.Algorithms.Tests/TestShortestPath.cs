using System;
using System.Collections.Generic;
using System.Linq;
using MagmaGraph.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MagmaGraph.Algorithms.Test
{
	[TestClass]
	public class TestShortestPath
	{
		[TestMethod]
		public void TestYouTubeSample()
		{
			// https://www.youtube.com/watch?v=WN3Rb9wVYDY

			var vertices = new Dictionary<string, Vertex>
			{
				{ "a", new Vertex("a") },
				{ "b", new Vertex("b") },
				{ "c", new Vertex("c") },
				{ "d", new Vertex("d") },
				{ "e", new Vertex("e") },
				{ "z", new Vertex("z") },
			};

			var edges = new List<WeightedEdge<int>>
			{
				new WeightedEdge<int>(vertices["a"], vertices["b"], 4),
				new WeightedEdge<int>(vertices["a"], vertices["c"], 2),
				new WeightedEdge<int>(vertices["b"], vertices["c"], 1),
				new WeightedEdge<int>(vertices["b"], vertices["d"], 5),
				new WeightedEdge<int>(vertices["c"], vertices["d"], 8),
				new WeightedEdge<int>(vertices["c"], vertices["e"], 10),
				new WeightedEdge<int>(vertices["d"], vertices["e"], 2),
				new WeightedEdge<int>(vertices["d"], vertices["z"], 6),
				new WeightedEdge<int>(vertices["e"], vertices["z"], 3),
			};

			BidirectionalGraph graph = new BidirectionalGraph("sample") { Vertices = vertices.Values.ToList(), Edges = edges };
			DjikstraShortestPath shortestPath = new DjikstraShortestPath(graph);

			int n = shortestPath.Solve(graph.GetVertex("a"), graph.GetVertex("z"));
			Console.WriteLine("The least cost from a to z is {0}", n);
			Console.WriteLine(shortestPath.Path);

			Assert.IsTrue(n == 13, "The shortest path should be 13");
			Assert.IsTrue(shortestPath.Path[0].Source.Name == "a" && shortestPath.Path[shortestPath.Path.Count-1].Dest.Name == "z");
		}
	}
}
