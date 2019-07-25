using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MagmaGraph.Core.Tests
{
	[TestClass]
	public class AdjacencyMatrixTests
	{
		private AdjacencyMatrix Matrix { get; set; }
		private const int MATRIXLENGTH = 5;

		[TestInitialize]
		public void Initialize()
		{
			this.Matrix = new AdjacencyMatrix(MATRIXLENGTH);
			for (int i = 0;  i < MATRIXLENGTH;  i++)
				for (int j = 0; j < MATRIXLENGTH; j++)
					this.Matrix[i, j] = i*j;

		}

		[TestMethod]
		public void AdjacencyMatrixTest()
		{
			Assert.IsNotNull(this.Matrix, "The matrix is null");
			Assert.IsTrue(this.Matrix.Length == 5, "The matrix should be 5x5");
			Assert.IsTrue(this.Matrix[4,4] == 16, "Matrix[4,4] should be 16");

			// Out-of-bounds reference
			int? n = this.Matrix[-1, 0];
			Assert.IsNull(n, "N should be null");
		}
	}
}
