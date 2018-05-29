using System;

namespace FortunesAlgorithm
{
	public class Point
	{
		float x;
		float y;

		public Point (float x, float y) : this(new Vector3(x, y, 1)) {
		}

		public Point(Vector3 v) {
			if (v.z == 0)
				throw new ArgumentOutOfRangeException("z cannot be 0 for a point defined as Vector3.");
			x = v.x/v.z;
			y = v.y/v.z;
		}

		public Vector3 Vector() {
			return new Vector3 (x, y, 1);
		}

		public Line LineWith(Point that) {
			return new Line (this.Vector ().CrossProduct (that.Vector()));
		}

		public Line LineThroughWithPerpendicular(Line that) {
			return that.PerpendicularThroughPoint (this);
		}

		public override string ToString ()
		{
			return string.Format ("Point({0})", Vector());
		}

		public override int GetHashCode ()
		{
			return x.GetHashCode () + y.GetHashCode ();
		}

		public override bool Equals (object obj)
		{
			if (obj == null || GetType () != obj.GetType ())
				return false;

			Point that = (Point)obj;
			return (this.Vector ().Colinear (that.Vector ()));
		}

		public float Cartesianx() {
			return x;
		}

		public float Cartesiany() {
			return y;
		}

		public Point MidpointWith(Point that) {
			Vector3 av = this.Vector ();
			Vector3 bv = that.Vector ();
			return new Point (new Vector3(av.x * bv.z + bv.x * av.z, av.y * bv.z + bv.y * av.z, 2 * av.z * bv.z));
		}

		public Line PerpendicularBisector(Point that) {
			return this.LineWith (that).PerpendicularThroughPoint (this.MidpointWith (that));
		}

		public static Point CircleCentre(Point a, Point b, Point c) {
			return a.PerpendicularBisector(b).Intersect(a.PerpendicularBisector(c));
		}
	}
}
