using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MagmaGraph.Core.Tests
{
	[TestClass]
	public class BinaryTreeTests
	{
		[TestMethod]
		public void InsertTest()
		{
			IntegerBinarySearchTree tree = new IntegerBinarySearchTree("sample");
			tree.Insert(new BinaryTree<int>.Node(val: 99, name: "Ninety Nine"));
			Assert.IsNotNull(tree.Find(99));
		}

		[TestMethod]
		public void InsertRangeTest()
		{
			IntegerBinarySearchTree tree = new IntegerBinarySearchTree("sample");

			tree.InsertRange(new[]
			{
				new BinaryTree<int>.Node(val:4, name:"apple"),
				new BinaryTree<int>.Node(val:6, name:"banana"),
				new BinaryTree<int>.Node(val:1, name:"strawberry"),
				new BinaryTree<int>.Node(val:3, name:"kiwi"),
				new BinaryTree<int>.Node(val:7, name:"lemon"),
				new BinaryTree<int>.Node(val:10,name:"lime"),
				new BinaryTree<int>.Node(val:6, name:"mango"),
				new BinaryTree<int>.Node(val:8, name:"pear")
			});
 
			Assert.IsNotNull(tree.Find(4));
			Assert.IsNotNull(tree.Find(6)); 
			Assert.IsNotNull(tree.Find(1)); 
			Assert.IsNotNull(tree.Find(3)); 
			Assert.IsNotNull(tree.Find(7)); 
			Assert.IsNotNull(tree.Find(10)); 
			Assert.IsNotNull(tree.Find(6)); 
			Assert.IsNotNull(tree.Find(8));
		}

		[TestMethod]
		public void DeleteTest()
		{
			var tree = this.CreateTestTree();
			BinaryTree<int>.Node node = tree.Find(10);
			Assert.IsNotNull(node);

			tree.Delete(node);
			node = tree.Find(10);
			Assert.IsNull(node);
		}

		[TestMethod]
		public void FindTest()
		{
			var tree = this.CreateTestTree();
			BinaryTree<int>.Node node = tree.Find(10);
			Assert.IsNotNull(node);
			Assert.IsTrue(node.Value == 10);
			Assert.IsTrue(node.Name == "lime");
		}

		[TestMethod]
		public void FindTestNegative()
		{
			var tree = this.CreateTestTree();
			BinaryTree<int>.Node node = tree.Find(20);
			Assert.IsNull(node);
		}

		public IntegerBinarySearchTree CreateTestTree()
		{
			IntegerBinarySearchTree tree = new IntegerBinarySearchTree("sample");

			tree.InsertRange(new[]
			{
				new BinaryTree<int>.Node(val:4, name:"apple"),
				new BinaryTree<int>.Node(val:6, name:"banana"),
				new BinaryTree<int>.Node(val:1, name:"strawberry"),
				new BinaryTree<int>.Node(val:3, name:"kiwi"),
				new BinaryTree<int>.Node(val:7, name:"lemon"),
				new BinaryTree<int>.Node(val:10,name:"lime"),
				new BinaryTree<int>.Node(val:6, name:"mango"),
				new BinaryTree<int>.Node(val:8, name:"pear")
			});

			return tree;
		}
	}
}
