namespace MagmaGraph.Core
{
	public class Edge : IEdge
	{
		public Vertex Source { get; set; }
		public Vertex Dest   { get; set; }

		public Edge(Vertex src, Vertex dest)
		{
			this.Source = src;
			this.Dest = dest;
		}

		public override string ToString()
		{
			return string.Format("Src {0}/Dest {1}", this.Source.Name, this.Dest.Name);
		}
	}
}