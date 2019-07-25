using MagmaGraph.Core;
using MagmaGraph.Core.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MagmaGraph.Algorithms.Test
{
	[TestClass]
	public class TestConnectivity
	{
		[TestMethod]
		public void YouTubeExampleConnectivityTest()
		{
			Graph graph = new UnidirectionalGraph();
			GraphTestHelpers.CommonInitialize(graph);

			Connectivity connectivity = new Connectivity(graph);

			bool isConnected = connectivity.IsConnected("a", "z");
			Assert.IsTrue(isConnected, "A should be connected to Z");
			isConnected = connectivity.IsConnected(graph.GetVertex("z"), graph.GetVertex("a"));
			Assert.IsFalse(isConnected, "Z should not be connected to A");
		}
	}
}
