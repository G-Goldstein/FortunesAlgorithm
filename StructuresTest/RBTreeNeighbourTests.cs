using NUnit.Framework;
using System;
using Structures;
using System.Collections.Generic;

namespace RBTreeNeighbourTests
{
	[TestFixture]
	public class EmptyTree
	{
		RBTree<int> emptyTree;
		int absentElement;

		[SetUp]
		public void SetUp() {
			emptyTree = new RBTree<int> ();
			absentElement = 3;
		}

		[Test]
		public void AskingForPredecessorThrowsError ()
		{
			Assert.Throws<ArgumentException>(AskForPredecessor);
		}

		public void AskForPredecessor() {
			emptyTree.Predecessor (absentElement);
		}

		[Test]
		public void AskingForSuccessorThrowsError ()
		{
			Assert.Throws<ArgumentException>(AskForSuccessor);
		}

		public void AskForSuccessor() {
			emptyTree.Successor (absentElement);
		}
	}

	[TestFixture]
	public class TwoElementTree
	{
		RBTree<int> twoElementTree;
		int firstElement;
		int secondElement;
		int absentElement;

		[SetUp]
		public void SetUp() {
			twoElementTree = new RBTree<int> ();
			firstElement = 1;
			secondElement = 2;
			absentElement = 3;
			twoElementTree.Add (firstElement);
			twoElementTree.Add (secondElement);
		}

		[Test]
		public void FirstElementHasNoPredecessor() {
			Assert.Throws<ArgumentOutOfRangeException>(GetFirstElementPredecessor);
		}

		public void GetFirstElementPredecessor() {
			twoElementTree.Predecessor (firstElement);
		}

		[Test]
		public void SecondElementHasNoSuccessor() {
			Assert.Throws<ArgumentOutOfRangeException>(GetSecondElementSuccessor);
		}

		public void GetSecondElementSuccessor() {
			twoElementTree.Successor (secondElement);
		}

		[Test]
		public void FirstElementSuccessorIsSecondElement() {
			Assert.AreEqual(secondElement, twoElementTree.Successor(firstElement));
		}

		[Test]
		public void SecondElementSuccessorIsFirstElement() {
			Assert.AreEqual (firstElement, twoElementTree.Predecessor (secondElement));
		}

		[Test]
		public void AbsentElementSuccessorThrowsArgumentException() {
			Assert.Throws<ArgumentException>(GetAbsentElementSuccessor);
		}

		public void GetAbsentElementSuccessor() {
			twoElementTree.Successor (absentElement);
		}

		[Test]
		public void AbsentElementPredecessorThrowsArgumentException() {
			Assert.Throws<ArgumentException>(GetAbsentElementPredecessor);
		}

		public void GetAbsentElementPredecessor() {
			twoElementTree.Predecessor (absentElement);
		}
	}

	[TestFixture]
	public class ManyElements {

		RBTree<int> tree;

		[SetUp]
		public void SetUp() {
			tree = new RBTree<int> ();
			tree.Add (0);
			tree.Add (1);
			tree.Add (2);
			tree.Add (8);
			tree.Add (7);
			tree.Add (6);
			tree.Add (10);
			tree.Add (11);
			tree.Add (9);
			tree.Add (5);
			tree.Add (3);
			tree.Add (4);
		}

		[Test]
		public void LeastElementHasNoPredecessor() {
			Assert.Throws<ArgumentOutOfRangeException> (GetLeastElementPredecessor);
		}

		public void GetLeastElementPredecessor() {
			tree.Predecessor (0);
		}

		[Test]
		public void GreatestElementHasNoSuccessor() {
			Assert.Throws<ArgumentOutOfRangeException> (GetGreatestElementSuccessor);
		}

		public void GetGreatestElementSuccessor() {
			tree.Successor (11);
		}

		[Test]
		public void LeastElementSuccessor() {
			Assert.AreEqual (1, tree.Successor (0));
		}

		[Test]
		public void GreatestElementPredecessor() {
			Assert.AreEqual (10, tree.Predecessor (11));
		}

		[Test]
		public void IntermediateElementSuccessors() {
			for (int i = 1; i < 11; i++) {
				Assert.AreEqual (i + 1, tree.Successor (i));
			}
		}

		[Test]
		public void IntermediateElementPredecessors() {
			for (int i = 1; i < 11; i++) {
				Assert.AreEqual (i - 1, tree.Predecessor (i));
			}
		}
	}
}

