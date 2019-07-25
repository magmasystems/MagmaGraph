using System;
using System.Collections.Generic;
using System.Text;

namespace MagmaGraph.Core
{
	public abstract class BinaryTree<TValue> : IBinaryTree<TValue> where TValue : IComparable<TValue>, IEquatable<TValue>
	{
		#region Variables
		public string Name { get; set; }
		public Node Root { get; set; }
		#endregion

		#region Node class
		public class Node
		{
			public string Name { get; set; }
			public TValue Value { get; set; }

			public Node Left { get; set; }
			public Node Right { get; set; }

			public Node()
			{
			}

			public Node(string name = null, TValue val = default(TValue)) : this()
			{
				this.Name = name;
				this.Value = val;
			}

			public bool IsLeaf => this.Left == null && this.Right == null;
		}
		#endregion

		#region Constructors
		protected BinaryTree(string name = null)
		{
			this.Name = name;
		}
		#endregion

		#region Printing
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			this.VisitPreOrder(this.Root, node =>
			{
				sb.Append(node.Name);
				sb.Append(", ");
				return true;
			});

			return sb.ToString();
		}

		public string Print(TraversalMethod traversalMethod = TraversalMethod.PreOrder)
		{
			StringBuilder sb = new StringBuilder();

			switch (traversalMethod)
			{
				case TraversalMethod.InOrder:
					this.VisitInOrder(this.Root, node =>
					{
						sb.Append(node.Name);
						sb.Append(", ");
						return true;
					});
					break;
				case TraversalMethod.PreOrder:
					this.VisitPreOrder(this.Root, node =>
					{
						sb.Append(node.Name);
						sb.Append(", ");
						return true;
					});
					break;
				case TraversalMethod.PostOrder:
					this.VisitPostOrder(this.Root, node =>
					{
						sb.Append(node.Name);
						sb.Append(", ");
						return true;
					});
					break;
			}

			return sb.ToString();
		}
		#endregion

		#region Abstract Methods
		/// <summary>
		/// Inserts a node into the proper place in the tree
		/// </summary>
		/// <param name="node">The node to insert</param>
		/// <returns>The tree itself (fluent API)</returns>
		public abstract IBinaryTree<TValue> Insert(Node node);

		/// <summary>
		/// Deletes a node from the tree
		/// </summary>
		/// <param name="node">The node to delete</param>
		/// <returns>The tree itself (fluent API)</returns>
		public abstract IBinaryTree<TValue> Delete(Node node);
		#endregion

		#region Concrete Methods
		/// <summary>
		/// Inserts a range of nodes into the proper place in the tree
		/// </summary>
		/// <param name="nodes">The nodes to insert</param>
		/// <returns>The tree itself (fluent API)</returns>
		public virtual IBinaryTree<TValue> InsertRange(IEnumerable<Node> nodes)
		{
			foreach (var node in nodes)
				this.Insert(node);
			return this;
		}
		#endregion

		#region Traversal Methods
		public virtual bool VisitPreOrder(Node node, Func<Node, bool> onVisit = null)
		{
			if (node == null)
				return true;

			if (onVisit == null) 
				return true;

			if (!onVisit(node))
				return false;
			if (!this.VisitPreOrder(node.Left))
				return false;
			if (!this.VisitPreOrder(node.Right))
				return false;

			return true;
		}

		public virtual bool VisitPostOrder(Node node, Func<Node, bool> onVisit = null)
		{
			if (node == null)
				return true;

			if (onVisit == null)
				return true;

			if (!this.VisitPostOrder(node.Left))
				return false;
			if (!this.VisitPostOrder(node.Right))
				return false;
			if (!onVisit(node))
				return false;

			return true;
		}
		
		public virtual bool VisitInOrder(Node node, Func<Node, bool> onVisit = null)
		{
			if (node == null)
				return true;

			if (onVisit == null)
				return true;

			if (!this.VisitInOrder(node.Left))
				return false;
			if (!onVisit(node))
				return false;
			if (!this.VisitInOrder(node.Right))
				return false;

			return true;
		}
		#endregion

	}
}
