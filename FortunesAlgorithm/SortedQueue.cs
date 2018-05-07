using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace FortunesAlgorithm
{
	public class SortedQueue<Key, Value>
	{
		/* This thing probably offers O(n) complexity, whereas a proper red-black tree implementation such as would be available in .NET 4.0 could
		 * bring this down to O(log(n)). It might be nice to sort this out one day. */

		SortedList<Key, Queue<Value>> sortedList;
		Func<Value, Key> keyOf;
		IComparer<Key> comparer;

		public SortedQueue (Func<Value, Key> keyOf, IComparer<Key> comparer)
		{
			sortedList = new SortedList<Key, Queue<Value>> ();
			this.keyOf = keyOf;
			this.comparer = comparer;
		}

		public void Enqueue(Value value) {
			Key key = keyOf (value);
			Queue<Value> queue;
			if (sortedList.TryGetValue (key, out queue)) {
				queue.Enqueue (value);
			} else {
				queue = new Queue<Value>();
				queue.Enqueue (value);
				sortedList.Add (key, queue);
			}
		}

		public Value Dequeue() {
			Queue<Value> queue;
			try {
				queue = sortedList.First().Value;
			} catch {
				throw new InvalidOperationException ("Queue empty.");
			}
			Value value = queue.Dequeue ();
			if (queue.Count == 0) {
				sortedList.RemoveAt (0);
			}
			return value;
		}

		public bool IsEmpty() {
			return sortedList.Count == 0;
		}
	}
}

