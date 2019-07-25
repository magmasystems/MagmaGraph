using System;
using System.IO;
using MagmaGraph.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MagmaGraph.IO.Tests
{
	[TestClass]
	public class ThoughtWorksRailNetworkReaderTests
	{
		[TestMethod]
		public void ThoughtWorksRailNetworkReaderTest()
		{
			var tr = GraphReaderFactory.Create("ThoughtWorks", "sample", Directionality.Unidirectional);
			bool rc = tr.Read("ThoughtWorksRailNetwork.txt");
			Assert.IsTrue(rc, "There was a problem with reading the graph file");

			Assert.IsNotNull(tr.Graph);
			Assert.IsTrue(tr.Graph.NumVertices == 5);
			Assert.IsTrue(tr.Graph.NumEdges == 9);
		}

		[TestMethod]
		[ExpectedException(typeof(ApplicationException))]
		public void NullDriverThoughtWorksRailNetworkReaderTest()
		{
			var tr = GraphReaderFactory.Create(null, "sample", Directionality.Unidirectional);
			bool rc = tr.Read("ThoughtWorksRailNetwork.txt");
			Assert.IsTrue(rc, "There was a problem with reading the graph file");
		}

		[TestMethod]
		[ExpectedException(typeof(ApplicationException))]
		public void BadDriverThoughtWorksRailNetworkReaderTest()
		{
			var tr = GraphReaderFactory.Create("FooBaz", "sample", Directionality.Unidirectional);
			bool rc = tr.Read("ThoughtWorksRailNetwork.txt");
			Assert.IsTrue(rc, "There was a problem with reading the graph file");
		}

		[TestMethod]
		[ExpectedException(typeof(FileNotFoundException))]
		public void BadFileNameThoughtWorksRailNetworkReaderTest()
		{
			var tr = GraphReaderFactory.Create("ThoughtWorks", "sample", Directionality.Unidirectional);
			bool rc = tr.Read("XXXThoughtWorksRailNetwork.txt");
			Assert.IsTrue(rc, "There was a problem with reading the graph file");
		}

		[TestMethod]
		[ExpectedException(typeof(FileNotFoundException))]
		public void NullFileNameThoughtWorksRailNetworkReaderTest()
		{
			var tr = GraphReaderFactory.Create("ThoughtWorks", "sample", Directionality.Unidirectional);
			bool rc = tr.Read(null);
			Assert.IsTrue(rc, "There was a problem with reading the graph file");
		}
	}
}
