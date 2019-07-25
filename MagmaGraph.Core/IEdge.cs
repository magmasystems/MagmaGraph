namespace MagmaGraph.Core
{
	public interface IEdge
	{
		Vertex Source { get; set; }
		Vertex Dest { get; set; }
	}
}