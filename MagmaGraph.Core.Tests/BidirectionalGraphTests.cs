using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MagmaGraph.Core.Tests
{
	[TestClass]
	public class BidirectionalGraphTests
	{
		[TestMethod]
		public void BidirectionalGraphTest()
		{
			BidirectionalGraph graph = new BidirectionalGraph();
			Assert.IsTrue(string.IsNullOrEmpty(graph.Name));
			GraphTestHelpers.CommonInitialize(graph);

			Assert.IsNotNull(graph.Edges);
			Assert.IsTrue(graph.Edges.Count > 0);
			Assert.IsNotNull(graph.Vertices);
			Assert.IsNotNull(graph.Vertices.Count == 6);
		}

		[TestMethod]
		public void NamedBidirectionalGraphTest()
		{
			BidirectionalGraph graph = new BidirectionalGraph("testBi");
			Assert.IsTrue(graph.Name == "testBi");
			GraphTestHelpers.CommonInitialize(graph);

			Assert.IsNotNull(graph.Edges);
			Assert.IsTrue(graph.Edges.Count > 0);
			Assert.IsNotNull(graph.Vertices);
			Assert.IsNotNull(graph.Vertices.Count == 6);
		}
	}
}
