using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MagmaGraph.Core.Tests
{
	[TestClass]
	public class UnidirectionalGraphTests
	{
		[TestMethod]
		public void UnidirectionalGraphTest()
		{
			UnidirectionalGraph graph = new UnidirectionalGraph();
			Assert.IsTrue(string.IsNullOrEmpty(graph.Name));
			GraphTestHelpers.CommonInitialize(graph);

			Assert.IsNotNull(graph.Edges);
			Assert.IsTrue(graph.Edges.Count > 0);
			Assert.IsNotNull(graph.Vertices);
			Assert.IsNotNull(graph.Vertices.Count == 6);
		}

		[TestMethod]
		public void NamedUnidirectionalGraphTest()
		{
			UnidirectionalGraph graph = new UnidirectionalGraph("testUni");
			Assert.IsTrue(graph.Name == "testUni");
			GraphTestHelpers.CommonInitialize(graph);

			Assert.IsNotNull(graph.Edges);
			Assert.IsTrue(graph.Edges.Count > 0);
			Assert.IsNotNull(graph.Vertices);
			Assert.IsNotNull(graph.Vertices.Count == 6);
		}
	}
}
