using System;
using System.Collections;
using System.Collections.Generic;
using MagmaGraph.Core;

namespace MagmaGraph.Algorithms
{
	public class Connectivity : IGraphAlgorithm
	{
		public IGraph Graph { get; protected set; }

		protected BitArray Visited { get; set; }
		protected Queue<Vertex> VerticesToVisit { get; set; }

		public Connectivity(IGraph graph)
		{
		    this.Graph = graph ?? throw new ApplicationException("Connectivity: null graph passed to the constructor");
		}

		public bool IsConnected(string startName, string endName)
		{
			Vertex start = this.Graph.GetVertex(startName);
			Vertex end = this.Graph.GetVertex(endName);
			if (start == null || end == null)
				throw new ApplicationException("Connectivity.IsConnected: one or both of the vertices are not in this graph");

			return this.IsConnected(start, end);
		}

		public bool IsConnected(Vertex start, Vertex end)
		{
			if (start == null || end == null)
				throw new ApplicationException("Connectivity.IsConnected: one or both of the vertices are not in this graph");

			this.Visited = new BitArray(this.Graph.NumVertices);
			this.VerticesToVisit = new Queue<Vertex>(this.Graph.NumVertices);
			this.VerticesToVisit.Enqueue(start);

			// Keep popping vertices off the the "To Visit" queue until we run out of vertices or we reach the desired endpoint
			while (this.VerticesToVisit.Count > 0)
			{
				// If the popped vertex is the desired endpoint, then we have success
				Vertex vertex = this.VerticesToVisit.Dequeue();
				if (vertex == end)
					return true;

				// Don't visit this vertex again
				this.Visited[this.Graph.GetVertexIndex(vertex)] = true;

				// Get all connections to the current vertex, ignore ones that were visitd already, and put the unvisited ones on the queue
				var edges = this.Graph.GetEdges(vertex);
				foreach (var edge in edges)
				{
					// Don't consider nodes that have been visited already
					Vertex otherVertex = edge.Dest == vertex ? edge.Source : edge.Dest;
					if (this.Visited[this.Graph.GetVertexIndex(otherVertex)])
						continue;

					this.VerticesToVisit.Enqueue(otherVertex);
				}
			}

			return false;
		}
	}
}
