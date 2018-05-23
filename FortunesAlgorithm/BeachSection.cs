using System;

namespace FortunesAlgorithm
{
	public class BeachSection : IComparable
	{
		Point focus;
		Point leftBoundary;
		Point rightBoundary;

		public BeachSection (Point focus, Point leftBoundary, Point rightBoundary)
		{
			this.focus = focus;
			this.leftBoundary = leftBoundary;
			this.rightBoundary = rightBoundary;
		}

		public static BeachSection FirstSection(Point focus) {
			return new BeachSection(focus, null, null);
		}
		public static BeachSection LeftSection(Point focus, Point rightBoundary) {
			return new BeachSection (focus, null, rightBoundary);
		}
		public static BeachSection RightSection(Point focus, Point leftBoundary) {
			return new BeachSection (focus, leftBoundary, null);
		}

		public bool IsLeftmost() {
			return leftBoundary == null;
		}

		public bool IsRightmost() {
			return rightBoundary == null;
		}

		public bool IsFullBeachLine() {
			return IsLeftmost () && IsRightmost ();
		}

		public int CompareTo(Object obj) {
			if (obj is BeachSection) {
				BeachSection that = (BeachSection)obj;
				return CompareTo (that);
			} else if (obj is Point) {
				Point that = (Point)obj;
				return CompareTo (that);
			}
			throw new ArgumentException (String.Format ("Can't compare objects of type BeachSection and {0}", obj.GetType()));
		}

		public override bool Equals(Object obj) {
			if (obj == null || GetType () != obj.GetType ())
				return false;

			BeachSection that = (BeachSection)obj;
			return this.focus == that.focus && this.leftBoundary == that.leftBoundary && this.rightBoundary == that.rightBoundary;
		}

		public override int GetHashCode() {
			int hashCode = focus.GetHashCode ();
			if (leftBoundary != null)
				hashCode += leftBoundary.GetHashCode ();
			if (rightBoundary != null)
				hashCode += rightBoundary.GetHashCode ();
			return hashCode;
		}

		public int CompareTo(Point site) {
			return 0; // This needs writing and testing.
		}

		public int CompareTo(BeachSection that) {
			if (this.Equals(that))
				return 0;
			// Eliminate all cases with null boundaries
			if (this.IsFullBeachLine () || that.IsFullBeachLine())
				throw new InvalidOperationException ("Can't compare full beach line to another beach section");
			if (this.IsLeftmost () || that.IsRightmost ())
				return -1;
			if (this.IsRightmost () || that.IsLeftmost ())
				return 1;
			return 0; // This needs finishing

		}
	}
}

