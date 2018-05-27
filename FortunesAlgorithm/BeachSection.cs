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

		public override string ToString ()
		{
			return String.Format("BS({0}, {1}, {2})", leftBoundary, focus, rightBoundary);
		}

		public int CompareTo(Point site) {
			return focus.Cartesianx ().CompareTo (site.Cartesianx ());
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
			if (this.focus.Cartesiany () == that.focus.Cartesiany ()) {
				return this.focus.Cartesianx ().CompareTo (that.focus.Cartesianx ());
			}
			// Both sections have a left and a right edge and they're not the same. 
			// Use a separate function for the details, which can be tail recursive.
			return CompareProperBeachSectionToProperBeachSection (that);
		}

		int CompareProperBeachSectionToProperBeachSection(BeachSection that) {
			if (this.focus.Cartesiany() < that.focus.Cartesiany())
				return -that.CompareProperBeachSectionToProperBeachSection (this);
			if (this.focus.Cartesianx () > that.focus.Cartesianx ()) // We want to assume that 'this' focus is to the left of 'that' focus, so if that's not true then flip the situation.
				return -FlippedHorizontally (this).CompareProperBeachSectionToProperBeachSection (FlippedHorizontally (that));
			if (this.leftBoundary.Cartesiany () >= this.focus.Cartesiany()) // If our edge point is above both foci
				return -1;
			if (this.leftBoundary.Cartesiany () < that.focus.Cartesiany ()) // If our edge point is below both foci
				return this.leftBoundary.Cartesianx().CompareTo(that.focus.Cartesianx());
			if (this.leftBoundary.Cartesianx() >= that.focus.Cartesianx ()) // If our left edge is to the right of the lower focus
				return 1;
			// Final case, if our perpendicular bisector of the two foci crosses the vertical line through our lower focus at a lower point than
			// our perpendicular bisector of our 'left' edge and our lower focus does...
			Line twoFociBisector = this.focus.PerpendicularBisector(that.focus);
			Line edgeAndLowerFocusBisector = this.leftBoundary.PerpendicularBisector (that.focus);
			Line verticalLineThroughLowerFocus = new Line (1, 0, -that.focus.Cartesianx());
			Point twoFociBisectorVerticalIntersect = twoFociBisector.Intersect (verticalLineThroughLowerFocus);
			Point edgeAndLowerFocusBisectorVerticalIntersect = edgeAndLowerFocusBisector.Intersect (verticalLineThroughLowerFocus);
			return twoFociBisectorVerticalIntersect.Cartesiany ().CompareTo (edgeAndLowerFocusBisectorVerticalIntersect.Cartesiany ());
			throw new ApplicationException (String.Format ("Couldn't resolve comparison of beach sections {0} and {1}", this, that));
		}

		BeachSection FlippedHorizontally(BeachSection bs) { // Just completely mirror all the x points through the line x=0, to conform to our 'higher focus is to the left' convention.
			Point focus = bs.focus;
			Point leftBoundary = bs.leftBoundary;
			Point rightBoundary = bs.rightBoundary;
			Point flippedFocus = new Point (-focus.Cartesianx (), focus.Cartesiany());
			Point flippedLeftBoundary = new Point(-rightBoundary.Cartesianx(), rightBoundary.Cartesiany());
			Point flippedRightBoundary = new Point(-leftBoundary.Cartesianx(), leftBoundary.Cartesiany());
			return new BeachSection (flippedFocus, flippedLeftBoundary, flippedRightBoundary);
		}
	}
}

