using System;
using System.Collections.Generic;

namespace MagmaGraph.Core
{
	public interface IBinaryTree<TValue> where TValue : IComparable<TValue>, IEquatable<TValue>
	{		
		BinaryTree<TValue>.Node Root { get; }
		
		/// <summary>
		/// Inserts a node into the proper place in the tree
		/// </summary>
		/// <param name="node">The node to insert</param>
		/// <returns>The tree itself (fluent API)</returns>
		IBinaryTree<TValue> Insert(BinaryTree<TValue>.Node node);

		/// <summary>
		/// Inserts a range of nodes into the proper place in the tree
		/// </summary>
		/// <param name="nodes">The nodes to insert</param>
		/// <returns>The tree itself (fluent API)</returns>
		IBinaryTree<TValue> InsertRange(IEnumerable<BinaryTree<TValue>.Node> nodes);

		/// <summary>
		/// Deletes a node from the tree
		/// </summary>
		/// <param name="node">The node to delete</param>
		/// <returns>The tree itself (fluent API)</returns>
		IBinaryTree<TValue> Delete(BinaryTree<TValue>.Node node);

		bool VisitPreOrder(BinaryTree<TValue>.Node node, Func<BinaryTree<TValue>.Node, bool> onVisit = null);
		bool VisitPostOrder(BinaryTree<TValue>.Node node, Func<BinaryTree<TValue>.Node, bool> onVisit = null);
		bool VisitInOrder(BinaryTree<TValue>.Node node, Func<BinaryTree<TValue>.Node, bool> onVisit = null);
	}
}