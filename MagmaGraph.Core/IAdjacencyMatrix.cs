namespace MagmaGraph.Core
{
	public interface IAdjacencyMatrix
	{
		int Length { get; }
		int? this[int n1, int n2] { get; set; }
	}
}