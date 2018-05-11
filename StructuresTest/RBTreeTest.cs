using NUnit.Framework;
using System;
using Structures;
using System.Collections.Generic;

namespace StructuresTest
{
	[TestFixture]
	public class RBTreeTest
	{
		RBTree<int> intTree;
		RBTree<float> floatTree;
		Random rng;

		[SetUp]
		public void SetUp() {
			rng = new Random ();
			intTree = new RBTree<int> ();
			floatTree = new RBTree<float> ();
		}

		[Test]
		public void EmptyTreeSatisfiesProperties() {
			AssertRBTreeProperties(intTree);
		}

		[Test]
		public void AddLinearlyAndCheckProperties()
		{
			for (int i = 0; i < 1000; i++) {
				AddAndCheck (intTree, i);
			}
		}

		[Test]
		public void AddRandomlyAndCheckProperties() {
			for (int i = 0; i < 1000; i++) {
				AddAndCheck (floatTree, rng.Next());
			}
		}

		void AddAndCheck<T>(RBTree<T> tree, T value) where T : IComparable {
			tree.Add (value);
			AssertRBTreeProperties (tree);
		}

		void AssertRBTreeProperties<T>(RBTree<T> tree) where T : IComparable {
			AssertRootIsBlack (tree);
			AssertAllLeavesBlack (tree);
			AssertRedNodesHaveBlackChildren (tree);
			EveryDescendantPathHasTheSameNumberOfBlackNodes (tree);
		}

		void AssertRootIsBlack<T>(RBTree<T> tree) where T : IComparable {
			Assert.False (tree.root.red, "Root is not black");
		}

		void AssertAllLeavesBlack<T>(RBTree<T> tree) where T : IComparable {
			AssertAllLeavesBlack<T> (tree.root);
		}

		void AssertAllLeavesBlack<T>(RBNode<T> node) where T : IComparable {
			if (node is RBBranch<T>) {
				RBBranch<T> branch = (RBBranch<T>)node;
				AssertAllLeavesBlack (branch.left);
				AssertAllLeavesBlack (branch.right);
			}
			else
				Assert.False (node.red);
		}

		void AssertRedNodesHaveBlackChildren<T>(RBTree<T> tree) where T : IComparable {
			AssertRedNodesHaveBlackChildren<T> (tree.root);
		}

		void AssertRedNodesHaveBlackChildren<T>(RBNode<T> node) where T : IComparable {
			if (node is RBLeaf<T>)
				return;
			RBBranch<T> branch = (RBBranch<T>)node;
			Assert.True (!branch.red || (!branch.left.red && !branch.right.red));
			AssertRedNodesHaveBlackChildren (branch.left);
			AssertRedNodesHaveBlackChildren (branch.right);
		}

		void EveryDescendantPathHasTheSameNumberOfBlackNodes<T>(RBTree<T> tree) where T : IComparable {
			EveryDescendantPathHasTheSameNumberOfBlackNodes<T> (tree.root);
		}

		int EveryDescendantPathHasTheSameNumberOfBlackNodes<T>(RBNode<T> node) where T : IComparable {
			if (node is RBLeaf<T>)
				return 1;
			RBBranch<T> branch = (RBBranch<T>)node;
			int leftBlacks = EveryDescendantPathHasTheSameNumberOfBlackNodes<T> (branch.left);
			int rightBlacks = EveryDescendantPathHasTheSameNumberOfBlackNodes<T> (branch.right);
			Assert.AreEqual (leftBlacks, rightBlacks);
			if (branch.red)
				return leftBlacks;
			return leftBlacks + 1;
		}

		void DrawTreeColour<T>(RBTree<T> tree, int maxDepth) where T : IComparable {
			for (int i = 0; i < maxDepth; i++ ) {
				DrawTreeRow<T>(tree.root, i);
				Console.WriteLine ();
			}
		}

		void DrawTreeRow<T>(RBNode<T> node, int depth) where T : IComparable {
			if (depth == 0 && node is RBBranch<T>) {
				if (node.red)
					Console.Write ("r");
				else
					Console.Write ("b");
			} else if (depth > 0 && node is RBBranch<T>) {
				RBBranch<T> branch = (RBBranch<T>)node;
				DrawTreeRow (branch.left, depth-1);
				DrawTreeRow (branch.right, depth-1);
			}	
		}
	}
}

