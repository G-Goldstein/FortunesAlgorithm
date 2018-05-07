using System;
using System.Collections.Generic;

namespace FortunesAlgorithm
{
	public class BoundaryRay
	{
		// The line that bisects points a and b.

		Point a;
		Point b;

		public BoundaryRay (Point a, Point b)
		{
			this.a = a;
			this.b = b;
		}

		public Line Line() {
			return Intersection ().PerpendicularThroughPoint (Midpoint ());
		}

		public override int GetHashCode ()
		{
			return a.GetHashCode() + b.GetHashCode();
		}

		public override bool Equals (object obj)
		{
			if (obj == null || GetType () != obj.GetType ())
				return false;

			BoundaryRay that = (BoundaryRay)obj;
			return Line ().Equals (that.Line ());
		}

		Point Midpoint() {
			return a.MidpointWith (b);
		}

		Line Intersection() {
			return new Line (a, b);
		}
	}
}

