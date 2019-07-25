using System;
using System.Collections.Generic;
using System.Linq;

namespace MagmaGraph.Core
{
	/// <summary>
	/// Note - it would be nice if C# supported a common type for arithmetic types, 
	/// so that we could make the Cost in an edge a generic param to IGraph
	/// </summary>
	public interface IGraph
	{
		string Name { get; set; }
		Directionality Directionality { get; }
		List<Vertex> Vertices { get; set; }
		List<WeightedEdge<int>> Edges { get; set; }
		AdjacencyMatrix AdjacencyMatrix { get; }
		int NumVertices { get; }
		int NumEdges { get; }

		Vertex GetVertex(string name);
		int GetVertexIndex(string name);
		int GetVertexIndex(Vertex vertex);
		Vertex this[int index] { get; }
	
		IEnumerable<WeightedEdge<int>> GetEdges(Vertex vertex);
		List<Vertex> GetAdjacentVertices(Vertex vertex);

		int GetCost(Vertex startVertex, Vertex endVertex);
		int GetCost(string sourceName, string destName);
		int GetCostOfRoute(IEnumerable<string> route);
	}

	public abstract class Graph : IGraph
	{
		public string Name { get; set; }
		public Directionality Directionality { get; protected set; }

		private List<Vertex> m_vertices = new List<Vertex>();
		public List<Vertex> Vertices
		{
			get => this.m_vertices;
		    set { this.m_vertices = value; this.CreateVertexDictionary(); }
		}

		private List<WeightedEdge<int>> m_edges = new List<WeightedEdge<int>>();
		public List<WeightedEdge<int>> Edges
		{
			get => this.m_edges;
		    set { this.m_edges = value; this.CreateAdjacencyMatrix(); }
		}

		protected Dictionary<string, Tuple<int, Vertex>> VertexDictionary = new Dictionary<string, Tuple<int, Vertex>>();
		public AdjacencyMatrix AdjacencyMatrix { get; protected set; }

		public int NumVertices => this.Vertices != null ? this.Vertices.Count : 0;
	    public int NumEdges => this.Edges != null ? this.Edges.Count : 0;

	    protected Graph()
		{
			this.Directionality = Directionality.Unknown;
		}

		protected Graph(string name) : this()
		{
			this.Name = name;
		}

		protected void CreateVertexDictionary()
		{
			if (this.Vertices == null)
				return;

			try
			{
				int n = 0;
				this.Vertices.ForEach(v => this.VertexDictionary.Add(v.Name, new Tuple<int, Vertex>(n++, v)));
			}
			catch (Exception)
			{
				// We will probably have a duplicate record that we are trying to insert.
				this.VertexDictionary.Clear();   // don't hold onto a bad dictionary
				
				// ReSharper disable once PossibleIntendedRethrow
				throw;
			}
		}

		protected void CreateAdjacencyMatrix()
		{
			this.AdjacencyMatrix = new AdjacencyMatrix(this.Vertices.Count);
			foreach (var edge in this.Edges)
			{
				int n1 = this.VertexDictionary[edge.Source.Name].Item1;
				int n2 = this.VertexDictionary[edge.Dest.Name].Item1;
				this.AdjacencyMatrix[n1, n2] = edge.Cost;
				if (this.Directionality == Directionality.Bidirectional)
					this.AdjacencyMatrix[n2, n1] = edge.Cost;
			}
		}

		public Vertex GetVertex(string name)
		{
			Tuple<int, Vertex> tuple;
			return this.VertexDictionary.TryGetValue(name, out tuple) ? tuple.Item2 : null;
		}

		public int GetVertexIndex(string name)
		{
			Tuple<int, Vertex> tuple;
			return this.VertexDictionary.TryGetValue(name, out tuple) ? tuple.Item1 : -1;
		}

		public int GetVertexIndex(Vertex vertex)
		{
			if (vertex == null)
				throw new ApplicationException("GetVertexIndex: the passed vertex is null");

			Tuple<int, Vertex> tuple;
			return this.VertexDictionary.TryGetValue(vertex.Name, out tuple) ? tuple.Item1 : -1;
		}

		public Vertex this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Vertices.Count)
					return null;

				return (from value in this.VertexDictionary.Values where value.Item1 == index select value.Item2).FirstOrDefault();
			}
		}

		public IEnumerable<WeightedEdge<int>> GetEdges(Vertex vertex)
		{
			if (vertex == null)
				throw new ApplicationException("GetEdges: the passed vertex is null");

			return this.Directionality == Directionality.Unidirectional 
				? this.Edges.Where(edge => edge.Source == vertex) 
				: this.Edges.Where(edge => edge.Source == vertex || edge.Dest == vertex);
		}

		/// <summary>
		/// Given a starting point, return a list of vertices that are adjacent.
		/// </summary>
		public List<Vertex> GetAdjacentVertices(Vertex vertex)
		{
			if (vertex == null)
				throw new ApplicationException("GetAdjacentVertices: the passed vertex is null");

			int row = this.GetVertexIndex(vertex);
			if (row < 0)
				throw new ApplicationException("GetAdjacentVertices: the passed vertex may not be part of this graph");

			List<Vertex> adjacentVertices = new List<Vertex>();
			for (int col = 0; col < this.AdjacencyMatrix.Length; col++)
			{
				if (col == row)  // Don't put the passed vertex into the adjacency list
					continue;

				if (this.AdjacencyMatrix[row, col].HasValue)
					adjacentVertices.Add(this[col]);
			}

			return adjacentVertices;
		}

		public int GetCost(Vertex startVertex, Vertex endVertex)
		{
			if (startVertex == null || endVertex == null)
				throw new ApplicationException("GetCost: the starting or ending vertex is null");

			return this.GetCost(startVertex.Name, endVertex.Name);
		}
	
		public int GetCost(string sourceName, string destName)
		{
			if (this.AdjacencyMatrix == null)
				throw new ApplicationException("GetCost: the adjacency matrix is null");

			int idxSource = this.GetVertexIndex(sourceName);
			int idxDest = this.GetVertexIndex(destName);
			if (idxSource < 0 || idxDest < 0)
				throw new ApplicationException("GetCost: one or both of the vertices are bad");

			return this.AdjacencyMatrix[idxSource, idxDest].HasValue ? this.AdjacencyMatrix[idxSource, idxDest].Value : -1;
		}

		/// <summary>
		/// This function traverses the points in the route, and adds up the cost along the way. We don't attempt to do any kind of 
		/// lowest-cost path here.
		/// </summary>
		/// <param name="route">An array of strings, where each string represents a name of a Vertex</param>
		/// <returns>The integer cost of the route. If there is an issue, then we return -1.</returns>
		public int GetCostOfRoute(IEnumerable<string> route)
		{
			if (route == null)
				return -1;

			int totalCost = 0;

			var enumerable = route as string[] ?? route.ToArray();
			for (int i = 0; i < enumerable.Count() - 1; i++)
			{
				int cost = this.GetCost(enumerable[i], enumerable[i + 1]);
				if (cost < 0)
					return -1;
				totalCost += cost;
			}

			return totalCost;
		}
	}
}