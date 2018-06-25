using System;
using Structures;
using System.Collections.Generic;

namespace FortunesAlgorithm
{
	public class EventQueue
	{

		Heap<IEventPoint> eventQueue;
		HashSet<Point> intersectionsToIgnore;

		public EventQueue ()
		{
			eventQueue = new Heap<IEventPoint> ((a, b) => a.Point().Cartesiany() > b.Point().Cartesiany());
			intersectionsToIgnore = new HashSet<Point> ();
		}

		public bool IsEmpty() {
			if (eventQueue.IsEmpty ())
				return true;
			IEventPoint next = eventQueue.Peek ();
			if (intersectionsToIgnore.Contains (next.Point())) {
                eventQueue.Pop();
                //intersectionsToIgnore.Remove(next.Point());
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
				intersectionsToIgnore.Add (eventPoint.Point());
		}

		public IEventPoint Pop() {
			IEventPoint next = eventQueue.Pop ();
			if (intersectionsToIgnore.Contains (next.Point())) {
				//intersectionsToIgnore.Remove (next.Point());
				return Pop ();
			}
			return next;
		}

	}
}

