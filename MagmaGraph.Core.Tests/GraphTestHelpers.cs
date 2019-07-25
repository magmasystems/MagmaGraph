using System.Collections.Generic;
using System.Linq;

namespace MagmaGraph.Core.Tests
{
	public class GraphTestHelpers
	{
		public static void CommonInitialize(Graph g)
		{
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

			g.Vertices = vertices.Values.ToList();
			g.Edges = edges;
		}
	}
}
