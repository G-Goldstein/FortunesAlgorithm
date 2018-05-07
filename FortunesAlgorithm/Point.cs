using System;

namespace FortunesAlgorithm
{
	public class Point
	{
		float x;
		float y;
		float z;

		public Point (float x, float y) : this(new Vector3(x, y, 1)) {
		}

		public Point(Vector3 v) {
			if (v.z == 0)
				throw new ArgumentOutOfRangeException("z cannot be 0 for a point defined as Vector3.");
			x = v.x;
			y = v.y;
			z = v.z;
		}

		public Vector3 Vector() {
			return new Vector3 (x, y, z);
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
			return (x / z).GetHashCode () + (y / z).GetHashCode ();
		}

		public override bool Equals (object obj)
		{
			if (obj == null || GetType () != obj.GetType ())
				return false;

			Point that = (Point)obj;
			return (this.Vector ().Colinear (that.Vector ()));
		}

		public float Cartesianx() {
			return x / z;
		}

		public float Cartesiany() {
			return y / z;
		}

		public Point MidpointWith(Point that) {
			Vector3 av = this.Vector ();
			Vector3 bv = that.Vector ();
			return new Point (new Vector3(av.x * bv.z + bv.x * av.z, av.y * bv.z + bv.y * av.z, 2 * av.z * bv.z));
		}
	}
}
