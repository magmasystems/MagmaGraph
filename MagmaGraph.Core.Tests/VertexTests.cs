using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MagmaGraph.Core.Tests
{
	[TestClass]
	public class VertexTests
	{
		[TestMethod]
		public void VertexTest()
		{
			Vertex v = new Vertex();
			Assert.IsTrue(string.IsNullOrEmpty(v.Name));
		}

		[TestMethod]
		public void NamedVertexTest()
		{
			Vertex v = new Vertex("vertex");
			Assert.IsTrue(v.Name == "vertex");
		}

		[TestMethod]
		public void ToStringTest()
		{
			Vertex v = new Vertex("vertex");
			Assert.IsTrue(v.ToString() == "vertex");
		}
	}
}
