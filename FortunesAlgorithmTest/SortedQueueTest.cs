using NUnit.Framework;
using System;
using FortunesAlgorithm;
using System.Collections.Generic;

namespace FortunesAlgorithmTest
{
	[TestFixture ()]
	public class SortedQueueTest
	{
		SortedQueue<int, string> sortedQueue;

		int Length(string s) {
			return s.Length;
		}

		class IntComparer : Comparer<int> {
			public override int Compare(int a, int b) {
				return a - b;
			}
		}

		[SetUp]
		public void SetUp() {
			IntComparer intComparer = new IntComparer ();
			sortedQueue = new SortedQueue<int, string> (Length, intComparer);
		}

		[Test]
		public void SimpleQueuing ()
		{
			Assert.True (sortedQueue.IsEmpty ());
			sortedQueue.Enqueue ("Is");
			Assert.False (sortedQueue.IsEmpty ());
			sortedQueue.Enqueue ("A");
			Assert.False (sortedQueue.IsEmpty ());
			sortedQueue.Enqueue ("The");

			Assert.False (sortedQueue.IsEmpty ());

			Assert.AreEqual (sortedQueue.Dequeue (), "A");
			Assert.False (sortedQueue.IsEmpty ());
			Assert.AreEqual (sortedQueue.Dequeue (), "Is");
			Assert.False (sortedQueue.IsEmpty ());
			Assert.AreEqual (sortedQueue.Dequeue (), "The");
			Assert.True (sortedQueue.IsEmpty ());
		}

		[Test]
		public void QueueWithDuplicates() {
			Assert.True (sortedQueue.IsEmpty ());
			sortedQueue.Enqueue ("One");
			Assert.False (sortedQueue.IsEmpty ());
			sortedQueue.Enqueue ("Two");
			Assert.False (sortedQueue.IsEmpty ());
			sortedQueue.Enqueue ("Three");
			Assert.False (sortedQueue.IsEmpty ());
			sortedQueue.Enqueue ("Four");
			Assert.False (sortedQueue.IsEmpty ());
			sortedQueue.Enqueue ("Five");
			Assert.False (sortedQueue.IsEmpty ());
			sortedQueue.Enqueue ("Six");

			Assert.False (sortedQueue.IsEmpty ());

			List<string> threeLetterWords = new List<string> ();
			threeLetterWords.Add (sortedQueue.Dequeue ());
			Assert.False (sortedQueue.IsEmpty ());
			threeLetterWords.Add (sortedQueue.Dequeue ());
			Assert.False (sortedQueue.IsEmpty ());
			threeLetterWords.Add (sortedQueue.Dequeue ());
			Assert.False (sortedQueue.IsEmpty ());

			Assert.Contains ("One", threeLetterWords);
			Assert.Contains ("Two", threeLetterWords);
			Assert.Contains ("Six", threeLetterWords);

			List<string> fourLetterWords = new List<string> ();
			fourLetterWords.Add (sortedQueue.Dequeue ());
			Assert.False (sortedQueue.IsEmpty ());
			fourLetterWords.Add (sortedQueue.Dequeue ());
			Assert.False (sortedQueue.IsEmpty ());

			Assert.Contains ("Four", fourLetterWords);
			Assert.Contains ("Five", fourLetterWords);

			Assert.AreEqual (sortedQueue.Dequeue (), "Three");
			Assert.True (sortedQueue.IsEmpty ());
		}

		public void DequeueEmptyQueue() {
			sortedQueue.Dequeue ();
		}

		[Test]
		public void ErrorFromDequeuingEmptyQueue() {
			Assert.Throws<System.InvalidOperationException> (DequeueEmptyQueue);
		}
	}
}

