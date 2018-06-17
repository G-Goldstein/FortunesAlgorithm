using System;
using Structures;
using System.Collections.Generic;

namespace FortunesAlgorithm
{
	public class EventQueue
	{

		Heap<IEventPoint> eventQueue;
		HashSet<IEventPoint> intersectionsToIgnore;

		public EventQueue ()
		{
			eventQueue = new Heap<IEventPoint> ((a, b) => a.Point().Cartesiany() > b.Point().Cartesiany());
			intersectionsToIgnore = new HashSet<IEventPoint> ();
		}

		public bool IsEmpty() {
			if (eventQueue.IsEmpty ())
				return true;
			IEventPoint next = eventQueue.Peek ();
			if (intersectionsToIgnore.Contains (next)) {
				Pop ();
				intersectionsToIgnore.Remove (next);
                return IsEmpty();
            }
            return false;
		}

		public void Add(IEventPoint eventPoint) {
			eventQueue.Add (eventPoint);
		}

		public void Add(IEventPoint eventPoint, float sweepLineY) {
			if (eventPoint.Point ().Cartesiany () <= sweepLineY)
				Add (eventPoint);
		}

		public void Remove(IEventPoint eventPoint, float sweepLineY) {
			if (eventPoint.Point ().Cartesiany () <= sweepLineY)
				intersectionsToIgnore.Add (eventPoint);
		}

		public IEventPoint Pop() {
			IEventPoint next = eventQueue.Pop ();
			if (intersectionsToIgnore.Contains (next)) {
				intersectionsToIgnore.Remove (next);
				return Pop ();
			}
			return next;
		}

	}
}

