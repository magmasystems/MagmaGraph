namespace MagmaGraph.Core
{
	public class UnidirectionalGraph : Graph
	{
		public UnidirectionalGraph()
		{
			this.Directionality = Directionality.Unidirectional;
		}

		public UnidirectionalGraph(string name) : base(name)
		{
			this.Directionality = Directionality.Unidirectional;
		}
	}
}