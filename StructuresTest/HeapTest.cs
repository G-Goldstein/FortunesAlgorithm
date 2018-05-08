using NUnit.Framework;
using System;
using Structures;
using System.Collections.Generic;

namespace StructuresTest
{
	[TestFixture]
	public class HeapTest
	{
		Heap<int> h;

		[SetUp]
		public void SetUp() {
			h = new Heap<int> ((a, b) => a-b);
		}

		[Test]
		public void CantPopEmpty ()
		{
			Assert.Throws<InvalidOperationException> (PopEmptyHeap, "Popping an empty heap didn't throw InvalidOperationException");
		}

		[Test]
		public void NewHeapIsEmpty() {
			Assert.True (h.IsEmpty(), "IsEmpty on new heap failed");
		}

		[Test]
		public void AddAndThenNotEmpty ()
		{
			h.Add (1);
			Assert.False (h.IsEmpty(), "IsEmpty on non-empty heap failed");
		}

		[Test]
		public void AddAndPopGivesAddedResult() {
			h.Add (3);
			Assert.AreEqual (3, h.Pop());
			Assert.True (h.IsEmpty(), "IsEmpty on heap failed after adding and removing one element");
		}

		[Test]
		public void Sorting() {
			h.Add (3);
			h.Add (1);
			h.Add (4);
			h.Add (2);
			Assert.AreEqual (4, h.Pop());
			Assert.AreEqual (3, h.Pop());
			Assert.AreEqual (2, h.Pop());
			Assert.AreEqual (1, h.Pop());
			Assert.True (h.IsEmpty(), "IsEmpty on heap failed after adding and removing several elements");
		}

		[Test]
		public void LongSorting() {
			List<int> ints = new List<int> ();
			for (int j = 0; j < 10; j++) {
				for (int i = 0; i < 500; i++) {
					h.Add (i);
				}
			}
			for (int i = 499; i >= 0; i--) {
				for (int j = 0; j < 10; j++) {
					Assert.False (h.IsEmpty());
					Assert.AreEqual (i, h.Pop ());
				}
			}
			Assert.True (h.IsEmpty());
		}

		public void PopEmptyHeap() {
			h.Pop ();
		}
	}
}