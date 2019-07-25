namespace MagmaGraph.Core
{
	public class BidirectionalGraph : Graph
	{
		public BidirectionalGraph()
		{
			this.Directionality = Directionality.Bidirectional;
		}

		public BidirectionalGraph(string name) : base(name)
		{
			this.Directionality = Directionality.Bidirectional;
		}
	}
}