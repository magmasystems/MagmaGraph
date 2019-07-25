using System;
using MagmaGraph.Core;

namespace MagmaGraph.Algorithms
{
	public class DjikstraShortestPathWithRevisiting : DjikstraShortestPath
	{
		public DjikstraShortestPathWithRevisiting(IGraph graph) : base(graph)
		{
		}

		/// <summary>
		/// This is a special version of the Shortest Path for the ThoughtWorks problem where we need to travel from B to B.
		/// We cannot mark B as "visited" to start with, cause this fucntion will return right away with a zero cost.
		/// </summary>
		public override int Solve(Vertex start, Vertex end)
		{
			if (start == null || end == null)
				throw new ApplicationException("DjikstraShortestPath.Solve - one of the vertices is null");

			//this.m_visited[this.m_graph.GetVertexIndex(start)] = true;

			for (Vertex vertex = start; vertex != null; )
			{
				Vertex minVertex = null;
				WeightedEdge<int> minEdge = null;
				int min = Int32.MaxValue;

				var edges = this.Graph.GetEdges(vertex);
				foreach (var edge in edges)
				{
					// Don't consider nodes that have been visited already
					Vertex otherVertex = edge.Dest == vertex ? edge.Source : edge.Dest;
					if (this.Visited[this.Graph.GetVertexIndex(otherVertex)])
						continue;

					// Disregard this edge if it is not the minimum-cost edge under consideration
					if (edge.Cost >= min)
						continue;

					// We found a new edge with a minimum cost
					minVertex = otherVertex;
					minEdge = edge;
					min = edge.Cost;
				}

				if (minEdge == null)
					break;

				this.Path.AddEdge(minEdge);
				vertex = minVertex;
				this.Visited[this.Graph.GetVertexIndex(vertex)] = true;

				if (vertex == end)
					break;
			}

			return this.Path.TotalCost;
		}
	}
}