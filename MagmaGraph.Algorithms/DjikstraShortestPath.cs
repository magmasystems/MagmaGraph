using System;
using System.Collections;
using MagmaGraph.Core;

namespace MagmaGraph.Algorithms
{
	public class DjikstraShortestPath : IGraphAlgorithm
	{
		public IGraph Graph { get; protected set; }
		public readonly WeightedPath Path = new WeightedPath();
		protected BitArray Visited { get; set; }

		public DjikstraShortestPath(IGraph graph)
		{
			this.Graph = graph;
			this.Visited = new BitArray(graph.NumVertices);
		}

		public int Solve(string startName, string endName)
		{
			Vertex start = this.Graph.GetVertex(startName);
			Vertex end = this.Graph.GetVertex(endName);
			return this.Solve(start, end);
		}

		public virtual int Solve(Vertex start, Vertex end)
		{
			if (start == null || end == null)
				throw new ApplicationException("DjikstraShortestPath.Solve - one of the vertices is null");
	
			// Mark the origin as visited. We don't have to consider that node anymore.
			this.Visited[this.Graph.GetVertexIndex(start)] = true;

			// Starting at the origin, traverse edges until we get to the destination
			for (Vertex vertex = start; vertex != end && vertex != null; )
			{
				// These three variables hold the information about the min-cost edge as we check all edges from the current node
				Vertex minVertex = null;
				WeightedEdge<int> minEdge = null;
				int min = Int32.MaxValue;

				// Find all vertices that are connected to the current node. Go through all of the edges, and don't consider vertices that have been visited already. 
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

				// Add the lowest-cost edge to the path, and make the 'current' vertex the endpoint of the min edge
				this.Path.AddEdge(minEdge);
				vertex = minVertex;
				this.Visited[this.Graph.GetVertexIndex(vertex)] = true;
			}

			return this.Path.TotalCost;
		}
	}
}