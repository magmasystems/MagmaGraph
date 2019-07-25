using System;

namespace MagmaGraph.Core
{
	public interface IBinarySearchTree<TValue> : IBinaryTree<TValue> where TValue : IComparable<TValue>, IEquatable<TValue>
	{
		/// <summary>
		/// Locates the node that contains a certain value
		/// </summary>
		/// <param name="nodeValue">The value to search for</param>
		/// <returns>The found node, or null if not found</returns>
		BinaryTree<TValue>.Node Find(TValue nodeValue);

		/// <summary>
		/// Locates the parent of the passed node
		/// </summary>
		/// <param name="node">The node whose parent you want to locate</param>
		/// <returns>The parent node, or null if no parent (or the root)</returns>
		BinaryTree<TValue>.Node FindParent(BinaryTree<TValue>.Node node);
	}
}