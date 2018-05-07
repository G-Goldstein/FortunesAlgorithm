using System;

namespace FortunesAlgorithm
{
	public class IntersectEventPoint : IEventPoint {

		public BoundaryRay rayA;
		public BoundaryRay rayB;

		public IntersectEventPoint(BoundaryRay a, BoundaryRay b) {
			rayA = a;
			rayB = b;
		}

		public Point Point ()
		{
			return rayA.Line().Intersect(rayB.Line());
		}

		public string EventType ()
		{
			return "Intersect";
		}
	}
}

