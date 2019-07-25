using System.IO;
using MagmaGraph.Core;

namespace MagmaGraph.IO
{
	public abstract class GraphReader : IGraphReader
	{
		public IGraph Graph { get; }
	
		protected GraphReader(string graphName, Directionality directionality)
		{
			this.Graph = GraphFactory.Create(graphName, directionality);
		}

		public bool Read(string filename)
		{
			if (string.IsNullOrEmpty(filename))
			{
				throw new FileNotFoundException("A null or empty filename was passed into GraphReader.Read");
			}

			if (!File.Exists(filename))
			{
				throw new FileNotFoundException($"GraphReader.Read: The file {filename} does not exist");
			}

			using (var stream = new FileStream(filename, FileMode.Open))
			{
				using (TextReader reader = new StreamReader(stream))
				{
					this.Parse(reader);
				}
			}

			return true;
		}

		protected abstract bool Parse(TextReader reader);
	}
}