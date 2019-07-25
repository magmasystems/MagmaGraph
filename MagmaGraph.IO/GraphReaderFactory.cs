using System;
using MagmaGraph.Core;

namespace MagmaGraph.IO
{
	public static class GraphReaderFactory
	{
		public static IGraphReader Create(string driver, string graphName, Directionality dir)
		{
			if (string.IsNullOrEmpty(driver))
				throw new ApplicationException("A null or empty driver name was passed into GraphReaderFactory.Create");

			switch (driver.Trim().ToLower())
			{
				case "thoughtworks":
					return new ThoughtWorksRailNetworkReader(graphName, dir);
				default:
					throw new ApplicationException("An unknown driver name was passed into GraphReaderFactory.Create");
			}
		}
	}
}
