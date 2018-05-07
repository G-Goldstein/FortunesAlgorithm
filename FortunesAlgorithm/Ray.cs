using System;

namespace FortunesAlgorithm
{
	public class Ray : IDirectional
	{
		public Point origin;
		public Vector direction;

		public Ray (Point origin, Vector direction)
		{
			this.origin = origin;
			this.direction = direction.Normalised ();
		}

		public Vector Direction() {
			return direction;
		}

		public Ray Negative() {
			return new Ray(origin, direction.Negative());
		}

		public override int GetHashCode ()
		{
			return origin.GetHashCode() ^ direction.GetHashCode();
		}

		public override bool Equals (object obj)
		{
			if (obj == null || GetType () != obj.GetType ())
				return false;

			Ray that = (Ray)obj;
			return origin.Equals(that.origin) && Directional.Parallel(this, that);
		}
	}
}

