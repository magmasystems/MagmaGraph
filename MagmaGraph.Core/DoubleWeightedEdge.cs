namespace MagmaGraph.Core
{
	public class DoubleWeightedEdge : WeightedEdge<double>
	{
		public DoubleWeightedEdge(Vertex src, Vertex dest, double cost) : base(src, dest, cost)
		{
		}
	}
}
