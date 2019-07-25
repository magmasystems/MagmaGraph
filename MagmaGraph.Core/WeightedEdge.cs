namespace MagmaGraph.Core
{
	public class WeightedEdge<T> : Edge
	{
		public T Cost { get; set; }

		public WeightedEdge(Vertex src, Vertex dest, T cost) : base(src, dest)
		{
			this.Cost = cost;
		}

		public override string ToString()
		{
			return base.ToString() + "/Cost " + this.Cost;
		}
	}
}