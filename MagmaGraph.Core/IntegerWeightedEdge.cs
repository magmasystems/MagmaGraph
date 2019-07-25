namespace MagmaGraph.Core
{
	public class IntegerWeightedEdge : WeightedEdge<int>
	{
		public IntegerWeightedEdge(Vertex src, Vertex dest, int cost) : base(src, dest, cost)
		{
		}
	}
}
