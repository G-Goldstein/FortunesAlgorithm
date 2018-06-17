using System;
using System.Collections.Generic;

namespace FortunesAlgorithm
{
	public class IntersectEventPoint : IEventPoint {

		public BeachSection consumedBeachSection;
		Point a;
		Point b;
		Point c;

		IntersectEventPoint(BeachSection consumedBeachSection) {
			this.consumedBeachSection = consumedBeachSection;
			this.a = consumedBeachSection.leftBoundary;
			this.b = consumedBeachSection.focus;
			this.c = consumedBeachSection.rightBoundary;
		}

		public Point Point ()
		{
			// This point isn't the centre of the circle formed by the two rays, but instead the point on
			// the circle's perimeter with the least y. The 'bottom' of the circle.
			Point centre = Centre();
			return new Point (centre.Cartesianx (), centre.Cartesiany () - Radius ());
		}

		Point Centre() {
			return new PerpendicularBisector (a, b).Line ().Intersect (new PerpendicularBisector (a, c).Line ());
		}

		float Radius() {
			return a.DistanceFrom (Centre ());
		}

		public string EventType ()
		{
			return "Intersect";
		}

		public override int GetHashCode ()
		{
			return Hash.TripleSet (a, b, c);
		}

		public override bool Equals (object obj)
		{
			if (obj == null || GetType () != obj.GetType ())
				return false;

			IntersectEventPoint that = (IntersectEventPoint)obj;
			List<Point> thisPoints = new List<Point> { a, b, c };
			return thisPoints.Contains (that.a) && thisPoints.Contains (that.b) && thisPoints.Contains (that.c);
		}

        public static HashSet<IntersectEventPoint> FromBeachSections(IEnumerable<BeachSection> beachSections)
        {
            HashSet<IntersectEventPoint> set = new HashSet<IntersectEventPoint>();
            foreach (BeachSection bs in beachSections)
            {
                if (!bs.IsLeftmost() && !bs.IsRightmost())
                {
                    Line lineA = bs.leftBoundary.LineWith(bs.rightBoundary);
                    Line lineB = bs.leftBoundary.LineWith(bs.focus);
                    if (!lineA.Colinear(lineB))
                        set.Add(new IntersectEventPoint(bs));
                }
            }
            return set;
        }
	}
}

