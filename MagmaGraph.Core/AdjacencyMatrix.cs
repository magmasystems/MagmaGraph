namespace MagmaGraph.Core
{
	public class AdjacencyMatrix : IAdjacencyMatrix
	{
		private int?[,] Matrix { get; }
		public int Length { get; }

		public AdjacencyMatrix(int nVertices)
		{
			this.Length = nVertices;
			this.Matrix = new int?[nVertices, nVertices];
		}

		public int? this[int n1, int n2]
		{
			get
			{
				if (n1 >= 0 && n1 < this.Length && n2 >= 0 && n2 < this.Length)
					return this.Matrix[n1, n2];
				return null;
			}
			set
			{
				if (n1 >= 0 && n1 < this.Length && n2 >= 0 && n2 < this.Length)
					this.Matrix[n1, n2] = value;
			}
		}
	}
}