using System;
using System.Collections.Generic;
using MagmaGraph.Core;

namespace MagmaGraph.Algorithms
{
	/// <summary>
	/// This is a class that serves as an umbrella for different algorithms that generate paths that satisfy some constraint. 
	/// The constraints that we have so far are:
	/// 1) Given a min and max number of stops, generate all paths that go from point A to point B and have a range of [min|max] number of stops in between.
	/// 2) Given a min and max distance, generate all paths that go from point A to point B and stay within the range of distance.
	/// 
	/// An important part about these algorithms is that they allow for cycles. If you had to go from New York to Chicago and back to New York, this
	/// is allowed.
	/// </summary>
	public class PathGenerators : IGraphAlgorithm
	{
		public bool Debug { get; set; }
		public IGraph Graph { get; protected set; }

		public PathGenerators(IGraph graph)
		{
			this.Graph = graph;

			if (graph == null)
				throw new ApplicationException("PathGenerators: a null graph was passed in");

			if (graph.Directionality != Directionality.Unidirectional)
				throw new ApplicationException("PathGenerators: you must use a Unidirectional graph");
		}
		
		public List<string> GeneratePathsWithinRangeOfStops(string startName, string endName, int minStops, int maxStops)
		{
			// Sanity checks
			if (this.Graph == null)
				return null;
			if (minStops <= 0 || maxStops <= 0 || minStops > maxStops)
				throw new ApplicationException("The range of stops is bad");

			// Make sure that the starting and ending points are in this graph
			Vertex startVertex = this.Graph.GetVertex(startName);
			Vertex endVertex = this.Graph.GetVertex(endName);

			if (startVertex == null || endVertex == null)
				throw new ApplicationException("One of the verticies is null");

			// Invoke the algorithm
			var resultsPath = new List<string>();
			return this.ExplorePath(startVertex, endVertex, resultsPath, startVertex.Name + "|", 0, minStops, maxStops);
		}

		public List<string> GeneratePathsWithinRangeOfCost(string startName, string endName, int minCost, int maxCost)
		{
			// Sanity checks
			if (this.Graph == null)
				return null;
			if (minCost <= 0 || maxCost <= 0 || minCost > maxCost)
				throw new ApplicationException("The range of cost is bad");

			// Make sure that the starting and ending points are in this graph
			Vertex startVertex = this.Graph.GetVertex(startName);
			Vertex endVertex = this.Graph.GetVertex(endName);

			if (startVertex == null || endVertex == null)
				throw new ApplicationException("One of the verticies is null");

			// Invoke the algorithm
			var resultsPath = new List<string>();
			return this.ExplorePathWithCosts(startVertex, endVertex, resultsPath, startVertex.Name + "|", 0, minCost, maxCost);
		}

		private List<string> ExplorePath(Vertex startVertex, Vertex endVertex, List<string> resultsPath, string path, int numStops, int minStops, int maxStops)
		{
			// If we have already gone over the max number of stops, then halt the recursion
			if (numStops >= maxStops)
				return resultsPath;

			// Get a list of vertices that are adjacent to the current vertex
			List<Vertex> queue = this.Graph.GetAdjacentVertices(startVertex);
			if (queue == null)
				return resultsPath;

			// Debug logging
			if (this.Debug)
			{
				Console.WriteLine();
				Console.WriteLine("Path: {0}, level: {1}, Queue:", path, numStops);
				foreach (var v in queue)
					Console.Write(" " + v.Name);
				Console.WriteLine();
			}

			foreach (var vertex in queue)
			{
				// Have we reached the end point? If so, and the number of stops is within the range, then add the path to the list of finished paths
				if (vertex == endVertex)
				{
					if (numStops >= minStops && numStops < maxStops)
					{
						resultsPath.Add(path + vertex.Name + "|");
						return resultsPath;
					}
				}

				// Recurse, using this vertex as the starting point
				this.ExplorePath(vertex, endVertex, resultsPath, path + vertex.Name + "|", numStops + 1, minStops, maxStops);
			}

			return resultsPath;
		}

		private List<string> ExplorePathWithCosts(Vertex startVertex, Vertex endVertex, List<string> resultsPath, string path, int totalCost, int minCost, int maxCost)
		{
			// If we have already gone over the max cost, then halt the recursion
			if (totalCost >= maxCost)
				return resultsPath;

			// Get a list of vertices that are adjacent to the current vertex
			List<Vertex> queue = this.Graph.GetAdjacentVertices(startVertex);
			if (queue == null)
				return resultsPath;

			// Debug logging
			if (this.Debug)
			{
				Console.WriteLine();
				Console.WriteLine("Path: {0}, cost: {1}, Queue:", path, totalCost);
				foreach (var v in queue)
					Console.Write(" " + v.Name);
				Console.WriteLine();
			}

			foreach (var vertex in queue)
			{
				int edgeCost = this.Graph.GetCost(startVertex, vertex);
				if (totalCost + edgeCost >= maxCost)
					return resultsPath;

				// Have we reached the end point? If so, then add the path to the list of finished paths
				if (vertex == endVertex)
				{
					resultsPath.Add(path + vertex.Name + "|");
				}

				// Recurse, using this vertex as the starting point
				this.ExplorePathWithCosts(vertex, endVertex, resultsPath, path + vertex.Name + "|", totalCost + edgeCost, minCost, maxCost);
			}

			return resultsPath;
		}
	}
}
