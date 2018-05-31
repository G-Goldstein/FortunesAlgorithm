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
		HashSet<float> floatSet;

		[SetUp]
		public void SetUp() {
			intTree = new RBTree<int> ();
			floatTree = new RBTree<float> ();
			rng = new Random ();
			floatSet = new HashSet<float> ();
			for (int i = 0; i < 1000; i++) {
				floatSet.Add(rng.Next());
			}
		}

		[Test]
		public void NewTreeIsEmpty() {
			Assert.True(EmptyTree(intTree));
			Assert.True(EmptyTree(floatTree));
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
			foreach (float f in floatSet) {
				AddAndCheck (floatTree, f);
			}
		}

		[Test]
		public void TreeWithElementIsNotEmpty() {
			AddTwoToIntTree ();
			Assert.False(EmptyTree(intTree));
		}

		[Test]
		public void AddDuplicateValueShouldError() {
			AddTwoToIntTree ();
			Assert.Throws<ArgumentException>(AddTwoToIntTree);
		}

		[Test]
		public void RemoveAbsentValueShouldError() {
			Assert.Throws<ArgumentException>(RemoveTwoFromIntTree);
		}

		[Test]
		public void AddAndRemoveOneElementSatisfiesConditionsAndEmptiness() {
			AddAndCheck (intTree, 5);
			RemoveAndCheck (intTree, 5);
			Assert.True(EmptyTree(intTree));
		}

		[Test]
		public void RemoveLinearlyAndCheckProperties() {
			for (int i = 0; i < 1000; i++) {
				intTree.Add (i);
			}
			for (int i = 0; i < 1000; i++) {
				RemoveAndCheck (intTree, i);
			}
		}

		[Test]
		public void RemoveLinearlyAndCheckPropertiesDescending() {
			for (int i = 0; i < 1000; i++) {
				intTree.Add (i);
			}
			for (int i = 999; i >= 0; i--) {
				RemoveAndCheck (intTree, i);
			}
		}

		[Test]
		public void RemoveLinearlyAndCheckPropertiesFromMiddle() {
			for (int i = 0; i < 1000; i++) {
				intTree.Add (i);
			}
			for (int i = 500; i < 1000; i++) {
				RemoveAndCheck (intTree, i);
			}
		}

		[Test]
		public void RemoveRandomlyAndCheckProperties() {
			foreach (float f in floatSet) {
				floatTree.Add (f);
			}
			AssertRBTreeProperties (floatTree);
			foreach (float f in floatSet) {
				RemoveAndCheck (floatTree, f);
			}
		}

		void AddTwoToIntTree() {
			intTree.Add (2);
		}

		void RemoveTwoFromIntTree() {
			intTree.Remove (2);
		}

		bool EmptyTree<T>(RBTree<T> tree) where T : IComparable {
			return tree.root is RBLeaf<T>;
		}

		void AddAndCheck<T>(RBTree<T> tree, T value) where T : IComparable {
			tree.Add (value);
			AssertRBTreeProperties (tree);
		}

		void RemoveAndCheck<T>(RBTree<T> tree, T value) where T : IComparable {
			tree.Remove (value);
			AssertRBTreeProperties (tree);
		}

		void AssertRBTreeProperties<T>(RBTree<T> tree) where T : IComparable {
			AssertTreeProperties<T> (tree);
			AssertRootIsBlack (tree);
			AssertAllLeavesBlack (tree);
			AssertRedNodesHaveBlackChildren (tree);
			EveryDescendantPathHasTheSameNumberOfBlackNodes (tree);
		}

		void AssertTreeProperties<T>(RBTree<T> tree) where T : IComparable {
			if (tree.root is RBLeaf<T>)
				return;
			RBBranch<T> branch = (RBBranch<T>)tree.root;
			AssertValuesLessThan (branch.left, branch.value);
			AssertValuesGreaterThan (branch.right, branch.value);
		}

		void AssertValuesLessThan<T>(RBNode<T> node, T max) where T : IComparable {
			if (node is RBLeaf<T>)
				return;
			RBBranch<T> branch = (RBBranch<T>)node;
			Assert.Less (branch.value, max);
			AssertValuesLessThan (branch.left, branch.value);
			AssertValuesInRange (branch.right, branch.value, max);
		}

		void AssertValuesGreaterThan<T>(RBNode<T> node, T min) where T : IComparable {
			if (node is RBLeaf<T>)
				return;
			RBBranch<T> branch = (RBBranch<T>)node;
			Assert.Greater (branch.value, min);
			AssertValuesInRange (branch.left, min, branch.value);
			AssertValuesGreaterThan (branch.right, branch.value);
		}

		void AssertValuesInRange<T>(RBNode<T> node, T min, T max) where T : IComparable {
			if (node is RBLeaf<T>)
				return;
			RBBranch<T> branch = (RBBranch<T>)node;
			Assert.Greater (branch.value, min);
			Assert.Less (branch.value, max);
			AssertValuesInRange (branch.left, min, branch.value);
			AssertValuesInRange (branch.right, branch.value, max);
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
	}
}

