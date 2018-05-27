using NUnit.Framework;
using System;
using FortunesAlgorithm;

namespace FortunesAlgorithmTest
{
	public static class AssertCompare {
		public static void Greater(BeachSection bs, Point p) {
			Assert.True (bs.CompareTo (p) > 0);
		}

		public static void Less(BeachSection bs, Point p) {
			Assert.True (bs.CompareTo (p) < 0);
		}

		public static void Intersecting(BeachSection bs, Point p) {
			Assert.True (bs.CompareTo (p) == 0);
		}
	}

	[TestFixture]
	public class FullBeachLineSection {

		BeachSection bs;
		Point p;

		[SetUp]
		public void SetUp() {
			bs = new BeachSection (new Point (0, 5), null, null);
			p = new Point (0, 0);
		}

		[Test]
		public void PointAlwaysIntersect() {
			AssertCompare.Intersecting(bs, p);
		}

	}

	[TestFixture]
	public class TwoLevelBeachSections
	{
		BeachSection left;
		BeachSection right;
		Point leftPoint;
		Point rightPoint;
		Point centrePoint;

		[SetUp]
		public void SetUp() {
			left = new BeachSection (new Point (-5, 5), null, new Point (5, 5));
			right = new BeachSection (new Point (5, 5), null, new Point (-5, 5));
			leftPoint = new Point (-5, 0);
			rightPoint = new Point (5, 0);
			centrePoint = new Point (0, 0);
		}

		[Test]
		public void LeftPointInLeftBeachLine() {
			AssertCompare.Intersecting (left, leftPoint);
		}

		[Test]
		public void RightBeachLineGreaterThanLeftPoint() {
			AssertCompare.Greater (right, leftPoint);
		}

		[Test]
		public void LeftBeachLineLessThanRightPoint() {
			AssertCompare.Less (left, rightPoint);
		}

		[Test]
		public void RightPointInRightBeachLine() {
			AssertCompare.Intersecting (right, rightPoint);
		}

		[Test]
		public void CentrePointInExactlyOneBeachLine() {
			int leftCompare = left.CompareTo (centrePoint);
			int rightCompare = right.CompareTo (centrePoint);
			Assert.True (leftCompare == 0 && rightCompare == 1 ||
			leftCompare == 1 && rightCompare == 0);
		}
	}

	[TestFixture]
	public class ThreeStaggeredBeachSections {
		
		[SetUp]
		public void SetUp() {
			Point topFocus = new Point (0, 8);
			Point midFocus = new Point (3, 4);
			Point lowerFocus = new Point (1, 2);
			// With these points, we get four beach sections when the directrix is at y=0:
			// Leftmost, up to x=~-2.72, where the top arc intercepts the bottom arc
			BeachSection leftMost = new BeachSection (topFocus, null, lowerFocus);
			// Leftish, from x=~-2.72 to x=3, travelling along the bottom arc from its intercept with the top arc to its intercept with the middle arc:
			BeachSection leftIsh = new BeachSection(lowerFocus, topFocus, midFocus);
			// Rightish, from x=3 to x=~13, traveling along the middle arc from its intercept with the bottom arc to its intercept with the top arc:
			BeachSection rightIsh = new BeachSection(midFocus, lowerFocus, topFocus);
			// Rightmost, from x=~13 onwards, travelling along the top arc after its intercept with the middle arc:
			BeachSection rightMost = new BeachSection(topFocus, midFocus, null);
			Point pointInExtremeLeft = new Point (-100, 0);
			Point pointInMidLeft = new Point (0, 0);
			Point pointInMidRight = new Point (8, 0);
			Point pointInExtremeRight = new Point (100, 0);
		}
	}
}

