using MagmaGraph.Core;

namespace MagmaGraph.IO
{
	public interface IGraphReader
	{
		IGraph Graph { get; }
		bool Read(string filename);
	}
}