using System;

namespace MagmaGraph.Core
{
	public abstract class BinarySearchTree<TValue> : BinaryTree<TValue> where TValue : IComparable<TValue>, IEquatable<TValue>
	{
		protected BinarySearchTree()
		{
		}
		
		protected BinarySearchTree(string name) : base(name)
		{
		}

		public override IBinaryTree<TValue> Insert(Node node)
		{
			if (node == null)
			{
				return this;
			}

			if (this.Root == null)
			{
				this.Root = node;
				return this;
			}

			Node visitedNode = this.Root;
			while (visitedNode != null)
			{
				if (visitedNode.Value.Equals(node.Value)) // no dupes
					return this;

				if (visitedNode.Value .CompareTo(node.Value) < 0)
				{
					if (visitedNode.Left == null)
					{
						visitedNode.Left = node;
						return this;
					}
					visitedNode = visitedNode.Left;
				}
				else
				{
					if (visitedNode.Right == null)
					{
						visitedNode.Right = node;
						return this;
					}
					visitedNode = visitedNode.Right;
				}
			}

			return this;
		}

		public override IBinaryTree<TValue> Delete(Node node)
		{
			if (node == null)
				return this;

			if (this.Root == null)
				return this;

			// The node can have several possibilities.
			// 1) It can be a leaf node. We Don't need to worry about any children. The parent's left or right can be set to null.
			// 2) It can have a left child but no right child.
			// 3) It can have a right child but no left child.
			// 4) It can have both a left and right child.
			// 5) In addition to the above cases, it can be the root.


			Node parent = this.FindParent(node);

			// Is the node a leaf node?
			if (node.IsLeaf)
			{
				if (parent == null)		// deleting the root, and the root is the only node
					this.Root = null;
				else if (parent.Left == node)
					parent.Left = null;
				else
					parent.Right = null;
			}

				// The case where the node is not a leaf, but has only one subtree
			else if (node.Left == null || node.Right == null)
			{
				// Deleting the root and the root has only one subtree?
				if (parent == null)
				{
					this.Root = node.Left ?? node.Right;
				}
				
					// The node has only 1 subtree, so point the parent to that subtree
				else if (node.Right == null)
				{
					if (parent.Left == node)
						parent.Left = node.Left;
					else
						parent.Right = node.Left;
				}
				else
				{
					if (parent.Left == node)
						parent.Left = node.Right;
					else
						parent.Right = node.Right;					
				}
			}

			// Now we know that we are deleting a node with two subtrees. The general rule-of-thumb is:
			//   Replace the value in the node with the largest value in its left subtree and then delete the node with the largest value from its left subtree.
			Node largestNode = this.FindLargestNode(node.Left);
			if (largestNode != null)
			{
				node.Value = largestNode.Value;
				this.Delete(largestNode);
			}

			return this;
		}

		public abstract Node FindLargestNode(Node node);

		#region Searching
		/// <summary>
		/// Locates the node that contains a certain value
		/// </summary>
		/// <param name="nodeValue">The value to search for</param>
		/// <returns>The found node, or null if not found</returns>
		public virtual Node Find(TValue nodeValue)
		{
			if (this.Root == null)
			{
				return null;
			}

			Node visitedNode = this.Root;
			while (visitedNode != null)
			{
				if (visitedNode.Value.Equals(nodeValue))
					return visitedNode;

				visitedNode = visitedNode.Value.CompareTo(nodeValue) < 0 ? visitedNode.Left : visitedNode.Right;
			}

			return null;
		}

		/// <summary>
		/// Locates the parent of the passed node
		/// </summary>
		/// <param name="node">The node whose parent you want to locate</param>
		/// <returns>The parent node, or null if no parent (or the root)</returns>
		public Node FindParent(Node node)
		{
			if (node == null || this.Root == null || node == this.Root)
				return null;

			Node visitedNode = this.Root;
			Node prevVisitedNode = null;

			while (visitedNode != null)
			{
				if (visitedNode == node)
					return prevVisitedNode;

				prevVisitedNode = visitedNode;
				visitedNode = visitedNode.Value.CompareTo(node.Value) < 0 ? visitedNode.Left : visitedNode.Right;
			}

			return prevVisitedNode;
		}
		#endregion
	}
}