namespace MagmaGraph.Core
{
	public class Vertex : IVertex
	{
		public string Name { get; set; }

		public Vertex()
		{
		}

		public Vertex(string name) : this()
		{
			this.Name = name;
		}

		public override string ToString()
		{
			return this.Name;
		}
	}
}