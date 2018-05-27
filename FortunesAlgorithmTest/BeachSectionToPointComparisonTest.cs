using NUnit.Framework;
using System;
using FortunesAlgorithm;

namespace BeachSectionsAndPoints
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
		Point p1;
		Point p2;
		Point p3;

		[SetUp]
		public void SetUp() {
			bs = new BeachSection (new Point (0, 5), null, null);
			p1 = new Point (-5, 0);
			p2 = new Point (0, 0);
			p3 = new Point (5, 0);
		}

		[Test]
		public void LeftPointIntersects() {
			AssertCompare.Intersecting(bs, p1);
		}

		[Test]
		public void CentralPointIntersects() {
			AssertCompare.Intersecting(bs, p2);
		}

		[Test]
		public void RightPointIntersects() {
			AssertCompare.Intersecting(bs, p3);
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
			right = new BeachSection (new Point (5, 5), new Point (-5, 5), null);
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
			Assert.True (leftCompare == 0 || rightCompare == 0);
			Assert.True (leftCompare == -1 || rightCompare == 1);
		}
	}

	[TestFixture]
	public class ThreeStaggeredBeachSections {

		BeachSection leftMost;
		BeachSection leftIsh;
		BeachSection rightIsh;
		BeachSection rightMost;
		Point pointInExtremeLeft;
		Point pointInMidLeft;
		Point pointInMidRight;
		Point pointInExtremeRight;
		Point pointOnEdgeOfLeftishAndRightish;

		[SetUp]
		public void SetUp() {
			Point topFocus = new Point (0, 8);
			Point midFocus = new Point (3, 4);
			Point lowerFocus = new Point (1, 2);
			// With these points, we get four beach sections when the directrix is at y=0:
			// Leftmost, up to x=~-2.72, where the top arc intercepts the bottom arc
			leftMost = new BeachSection (topFocus, null, lowerFocus);
			// Leftish, from x=~-2.72 to x=3, travelling along the bottom arc from its intercept with the top arc to its intercept with the middle arc:
			leftIsh = new BeachSection(lowerFocus, topFocus, midFocus);
			// Rightish, from x=3 to x=~13, traveling along the middle arc from its intercept with the bottom arc to its intercept with the top arc:
			rightIsh = new BeachSection(midFocus, lowerFocus, topFocus);
			// Rightmost, from x=~13 onwards, travelling along the top arc after its intercept with the middle arc:
			rightMost = new BeachSection(topFocus, midFocus, null);
			pointInExtremeLeft = new Point (-100, 0);
			pointInMidLeft = new Point (0, 0);
			pointInMidRight = new Point (8, 0);
			pointInExtremeRight = new Point (100, 0);
			pointOnEdgeOfLeftishAndRightish = new Point (3, 0);
		}

		[Test]
		public void PointInExtremeLeftIsInLeftmostSection() {
			AssertCompare.Intersecting (leftMost, pointInExtremeLeft);
		}

		[Test]
		public void PointInExtremeLeftIsLeftOfAllOtherSections() {
			AssertCompare.Greater (leftIsh, pointInExtremeLeft);
			AssertCompare.Greater (rightIsh, pointInExtremeLeft);
			AssertCompare.Greater (rightMost, pointInExtremeLeft);
		}

		[Test]
		public void PointInMidLeftIsRightOfLeftmostSection() {
			AssertCompare.Less (leftMost, pointInMidLeft);
		}

		[Test]
		public void PointInMidLeftIsInLeftishSection() {
			AssertCompare.Intersecting (leftIsh, pointInMidLeft);
		}

		[Test]
		public void PointInMidLeftIsLeftOfTwoRightmostSections() {
			AssertCompare.Greater (rightIsh, pointInMidLeft);
			AssertCompare.Greater (rightMost, pointInMidLeft);
		}

		[Test]
		public void PointInMidRightIsRightOfTwoLeftmostSections() {
			AssertCompare.Less (leftMost, pointInMidRight);
			AssertCompare.Less (leftIsh, pointInMidRight);
		}

		[Test]
		public void PointInMidRightIsInRightishSection() {
			AssertCompare.Intersecting (rightIsh, pointInMidRight);
		}

		[Test]
		public void PointInMidRightIsLeftOfRightmostSection() {
			AssertCompare.Greater (rightMost, pointInMidRight);
		}

		[Test]
		public void PointInExtremeRightIsInRightmostSection() {
			AssertCompare.Intersecting (rightMost, pointInExtremeRight);
		}

		[Test]
		public void PointInExtremeRightIsRightOfAllOtherSections() {
			AssertCompare.Less (leftMost, pointInExtremeRight);
			AssertCompare.Less (leftIsh, pointInExtremeRight);
			AssertCompare.Less (rightIsh, pointInExtremeRight);
		}

		[Test]
		public void PointOnEdgeOfLeftishAndRightishSectionsIsInExactlyOneOfThem() {
			int leftCompare = leftIsh.CompareTo (pointOnEdgeOfLeftishAndRightish);
			int rightCompare = rightIsh.CompareTo (pointOnEdgeOfLeftishAndRightish);
			Assert.True (leftCompare == 0 || rightCompare == 0);
			Assert.True (leftCompare == -1 || rightCompare == 1);
		}
	}

	[TestFixture]
	public class VerticallyStackedFoci {

		BeachSection leftBeach;
		BeachSection midBeach;
		BeachSection rightBeach;
		Point leftPoint;
		Point midPoint;
		Point rightPoint;

		[SetUp]
		public void SetUp() {
			Point upperFocus = new Point (0, 8);
			Point lowerFocus = new Point (0, 4);

			leftBeach = new BeachSection (upperFocus, null, lowerFocus);
			midBeach = new BeachSection (lowerFocus, upperFocus, upperFocus);
			rightBeach = new BeachSection (upperFocus, lowerFocus, null);

			leftPoint = new Point (-100, 1);
			midPoint = new Point (0, 1);
			rightPoint = new Point (100, 1);
		}

		[Test]
		public void LeftPointIsInLeftBeachSection() {
			AssertCompare.Intersecting (leftBeach, leftPoint);
		}

		[Test]
		public void MidPointIsInMidBeachSection() {
			AssertCompare.Intersecting (midBeach, midPoint);
		}

		[Test]
		public void RightPointIsInRightBeachSection() {
			AssertCompare.Intersecting (rightBeach, rightPoint);
		}

		[Test]
		public void LeftPointIsLeftOfOtherBeachSections() {
			AssertCompare.Greater (midBeach, leftPoint);
			AssertCompare.Greater (rightBeach, leftPoint);
		}

		[Test]
		public void RightPointIsRightOfOtherBeachSections() {
			AssertCompare.Less (leftBeach, rightPoint);
			AssertCompare.Less (midBeach, rightPoint);
		}

		[Test]
		public void MidPointIsRightOfLeftBeachSection() {
			AssertCompare.Less (leftBeach, midPoint);
		}

		[Test]
		public void MidPointIsLeftOfRightBeachSection() {
			AssertCompare.Greater (rightBeach, midPoint);
		}
	}
}

