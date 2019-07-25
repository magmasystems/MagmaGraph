using System;

namespace MagmaGraph.Core
{
	public class IntegerBinarySearchTree : BinarySearchTree<int>
	{
		public IntegerBinarySearchTree()
		{
		}
		
		public IntegerBinarySearchTree(string name) : base(name)
		{
		}

		public override Node FindLargestNode(Node node)
		{
			int max = Int32.MinValue;
			Node largestNode = null;
			
			this.VisitInOrder(node, visitedNode =>
			{
				if (visitedNode.Value > max)
				{
					max = visitedNode.Value;
					largestNode = visitedNode;
				}
				return true;
			});

			return largestNode;
		}
	}
}