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
}

