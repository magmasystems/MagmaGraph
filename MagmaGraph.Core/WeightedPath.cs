using System.Collections.Generic;
using System.Text;

namespace MagmaGraph.Core
{
	public class WeightedPath : List<WeightedEdge<int>>
	{
		public int TotalCost { get; set; }
		
		public void AddEdge(WeightedEdge<int> edge)
		{
			if (edge == null)
				return;

			this.Add(edge);
			this.TotalCost += edge.Cost;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			foreach (var edge in this)
			{
				sb.AppendLine(edge.ToString());
			}
			return sb.ToString();
		}
	}
}
