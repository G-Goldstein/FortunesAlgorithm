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

		public override int GetHashCode ()
		{
			return Hash.PairReversible (rayA, rayB);
		}

		public override bool Equals (object obj)
		{
			if (obj == null || GetType () != obj.GetType ())
				return false;

			IntersectEventPoint that = (IntersectEventPoint)obj;
			return (this.rayA.Equals(that.rayA) && this.rayB.Equals(that.rayB)
				|| this.rayA.Equals(that.rayB) && this.rayB.Equals(that.rayA));
		}
	}
}

