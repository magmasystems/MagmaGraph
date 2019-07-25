using System;

namespace MagmaGraph.Core
{
	public static class GraphFactory
	{
		public static IGraph Create(string graphName, Directionality directionality)
		{
			if (directionality == Directionality.Unknown)
				throw new ApplicationException("The directionality must be Unidirectional or Bidirectional");
			
			switch (directionality)
			{
				case Directionality.Bidirectional:
					return new BidirectionalGraph(graphName);
				case Directionality.Unidirectional:
					return new UnidirectionalGraph(graphName);
			}

			return null;
		}
	}
}
