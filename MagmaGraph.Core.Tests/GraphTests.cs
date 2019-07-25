using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MagmaGraph.Core.Tests
{
	[TestClass]
	public class GraphTests
	{
		[TestMethod]
		public void GetVertexTest()
		{
			Graph graph = new UnidirectionalGraph();
			Assert.IsTrue(graph.Directionality == Directionality.Unidirectional);
			
			GraphTestHelpers.CommonInitialize(graph);
			Assert.IsTrue(graph.NumVertices == 6);

			Vertex v = graph.GetVertex("a");
			Assert.IsNotNull(v);
		}

		[TestMethod]
		public void GetVertexIndexTest()
		{
			Graph graph = new UnidirectionalGraph();
			Assert.IsTrue(graph.Directionality == Directionality.Unidirectional);

			GraphTestHelpers.CommonInitialize(graph);
			Assert.IsTrue(graph.NumVertices == 6);

			int index = graph.GetVertexIndex("b");
			Assert.IsTrue(index >= 0);
		}

		[TestMethod]
		public void GetVertexIndexTest2()
		{
			Graph graph = new UnidirectionalGraph();
			Assert.IsTrue(graph.Directionality == Directionality.Unidirectional);

			GraphTestHelpers.CommonInitialize(graph);
			Vertex v = graph.GetVertex("b");
			Assert.IsNotNull(v);

			int index = graph.GetVertexIndex(v);
			Assert.IsTrue(index >= 0);

		}

		[TestMethod]
		public void GetEdgesTest()
		{
			Graph graph = new UnidirectionalGraph();
			Assert.IsTrue(graph.Directionality == Directionality.Unidirectional);

			GraphTestHelpers.CommonInitialize(graph);
			Vertex v = graph.GetVertex("a");
			Assert.IsNotNull(v);

			IEnumerable<WeightedEdge<int>> edges = graph.GetEdges(v);
			Assert.IsNotNull(edges);
			Assert.IsTrue(edges.Any());
		}

		[TestMethod]
		public void GetAdjacentVerticesTest()
		{
			Graph graph = new UnidirectionalGraph();
			Assert.IsTrue(graph.Directionality == Directionality.Unidirectional);

			GraphTestHelpers.CommonInitialize(graph);
			Vertex v = graph.GetVertex("a");
			Assert.IsNotNull(v);

			List<Vertex> adjacentVertices = graph.GetAdjacentVertices(v);
			Assert.IsNotNull(adjacentVertices);
			Assert.IsTrue(adjacentVertices.Any());

			// Vertex B and C should be the only vertices that are adjacent to A
			Assert.IsTrue(adjacentVertices.Count == 2);
			Assert.IsTrue(adjacentVertices[0].Name == "b");
			Assert.IsTrue(adjacentVertices[1].Name == "c");
		}

		[TestMethod]
		public void GetAdjacentVerticesNoConnectionsTest()
		{
			Graph graph = new UnidirectionalGraph();
			Assert.IsTrue(graph.Directionality == Directionality.Unidirectional);

			GraphTestHelpers.CommonInitialize(graph);
			Vertex v = graph.GetVertex("z");
			Assert.IsNotNull(v);

			List<Vertex> adjacentVertices = graph.GetAdjacentVertices(v);
			Assert.IsNotNull(adjacentVertices);
			Assert.IsFalse(adjacentVertices.Any());
		}
	}
}
