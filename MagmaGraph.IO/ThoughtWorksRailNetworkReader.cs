using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using MagmaGraph.Core;

namespace MagmaGraph.IO
{
	public class ThoughtWorksRailNetworkReader : GraphReader
	{
		public ThoughtWorksRailNetworkReader(string graphName, Directionality directionality) : base(graphName, directionality)
		{
		}

		protected override bool Parse(TextReader reader)
		{
			if (reader == null)
				return false;

			Dictionary<string, Vertex> vertices = new Dictionary<string, Vertex>();
			List<WeightedEdge<int>> edges = new List<WeightedEdge<int>>();

			// Each line of the file will have the form LetterLetterInteger, 
			// where the first letter is the Start, the second letter is the Dest, and the number is the distance
			Regex regex = new Regex("([A-Za-z])([A-Za-z])(\\d+)");

			string line;
			int idxLine = 0;
			while ((line = reader.ReadLine()) != null)
			{
				// A blank line will terminate the processing
				line = line.Trim();
				if (string.IsNullOrEmpty(line))
					return true;

				idxLine++;

				// Make sure that we match the regex
				var match = regex.Match(line);
				if (!match.Success || match.Groups.Count != 4)
					throw new ApplicationException("Bad input in the graph file at line " + idxLine);

				// Extract the start, end, and cost
				string sourceName = match.Groups[1].Value;
				string destName = match.Groups[2].Value;
				int cost = Convert.ToInt32(match.Groups[3].Value);

				// Add the new vertices and edge to the graph.
				// Note - we do not check for cycles in the graph.
				Vertex source;
				Vertex dest;
				if (!vertices.TryGetValue(sourceName, out source))
				{
					source = new Vertex(sourceName);
					vertices.Add(sourceName, source);
				}
				if (!vertices.TryGetValue(destName, out dest))
				{
					dest = new Vertex(destName);
					vertices.Add(destName, dest);
				}
				edges.Add(new WeightedEdge<int>(source, dest, cost));
			}

			// Add the list of vertices and edges to the graph
			this.Graph.Vertices = vertices.Values.ToList();
			this.Graph.Edges = edges;
	
			return true;
		}
	}
}
