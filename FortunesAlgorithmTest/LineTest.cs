using NUnit.Framework;
using System;
using FortunesAlgorithm;
using System.Collections.Generic;

namespace Lines
{
	[TestFixture ()]
	public class LineTest
	{

		Line aLine1;
		Line aLine2;
		Line aLine3;
		Line bLine;
		Line cLine;
		Line dLine;
		Line eLine;
		Line fLine;

		List<Line> aLines;
		List<Line> otherLines;

		[SetUp]
		public void SetupLines() {
			// All of these lines are equivalent:
			aLine1 = new Line (new Point(0, 1), new Point(2, 2));
			aLine2 = new Line (new Point (2, 2), new Point(0, 1));
			aLine3 = new Line (new Point (-2, 0), new Point(2, 2));

			// These lines are not equivalent to those above, but they may have things in common:
			bLine = new Line (new Point(0, 1), new Point(2, 1));
			cLine = new Line (new Point (1, 1), new Point(2, 2));
			dLine = new Line (new Point (0, -1), new Point (2, 0));
			eLine = new Line (new Point (0, 0), new Point (1, 0));
			fLine = new Line (new Point (0, 0), new Point (0, 1));

			aLines = new List<Line> { aLine1, aLine2, aLine3 };
			otherLines = new List<Line> { bLine, cLine, dLine, eLine, fLine };
		}

		[Test ()]
		public void CompareEqualLines ()
		{
			foreach (Line line1 in aLines) {
				foreach (Line line2 in aLines) {
					LinesEqual (line1, line2);
				}
			}
		}

		[Test()]
		public void CompareDifferentLines()
		{
			foreach (Line line1 in aLines) {
				foreach (Line line2 in otherLines) {
					LinesNotEqual (line1, line2);
				}
			}
		}

		[Test]
		public void Intersections() {
			Line up = new Line (1, 0, -1);
			Line across = new Line (0, 1, -1);
			Line diag = new Line (1, 1, 0);
			Assert.AreEqual (new Point (1, 1), up.Intersect (across));
			Assert.AreEqual (new Point (1, 1), across.Intersect (up));
			Assert.AreEqual (new Point (-1, 1), diag.Intersect (across));
			Assert.AreEqual (new Point (-1, 1), across.Intersect (diag));
			Assert.AreEqual (new Point (1, -1), diag.Intersect (up));
			Assert.AreEqual (new Point (1, -1), up.Intersect (diag));
		}

		[Test]
		public void PerpendicularThroughPoint() {
			Line up = new Line (1, 0, -1);
			Point noughtThree = new Point (0, 3);
			Point oneThree = new Point (1, 3);
			Point threeThree = new Point (3, 3);
			Line perpendicular = new Line (0, 1, -3);
			Assert.AreEqual (perpendicular, up.PerpendicularThroughPoint (noughtThree));
			Assert.AreEqual (perpendicular, up.PerpendicularThroughPoint (oneThree));
			Assert.AreEqual (perpendicular, up.PerpendicularThroughPoint (threeThree));
		}

		[Test]
		public void StressTest() {
			// With floating point precision, do the maths break down after many repeated calls?
			// We'll use a rectangle of points, and keep calling for perpendicular lines along its edges.
			Point topLeft = new Point(3, 7);
			Point bottomLeft = new Point(2, 3);
			Point bottomRight = new Point(6, 2);
			Point topRight = new Point(7, 6);

			Line left = topLeft.LineWith (bottomLeft);
			Line bottom = bottomLeft.LineWith (bottomRight);
			Line right = bottomRight.LineWith (topRight);
			Line top = topRight.LineWith (topLeft);

			Line cumulativeLine = left;

			for (int i = 0; i < 1000; i++) {
				cumulativeLine = cumulativeLine.PerpendicularThroughPoint (bottomLeft);
				Assert.AreEqual (bottom, cumulativeLine);
				cumulativeLine = cumulativeLine.PerpendicularThroughPoint (bottomRight);
				Assert.AreEqual (right, cumulativeLine);
				cumulativeLine = cumulativeLine.PerpendicularThroughPoint (topRight);
				Assert.AreEqual (top, cumulativeLine);
				cumulativeLine = cumulativeLine.PerpendicularThroughPoint (topLeft);
				Assert.AreEqual (left, cumulativeLine);
			}
		}



		// Helpers

		public void LinesEqual (Line a, Line b) 
		{
			Assert.True (a.GetHashCode () == b.GetHashCode (), "Lines " + a + " and " + b + " should have the same hashcode");
			Assert.True (a.Equals (b), "Lines " + a + " and " + b + " should be equal.");
			Assert.True (b.Equals (a), "Lines " + b + " and " + a + " should be equal.");
		}

		public void LinesNotEqual(Line a, Line b)
		{
			Assert.False (a.Equals (b), "Lines " + a + " and " + b + " should not be equal.");
			Assert.False (b.Equals (a), "Lines " + b + " and " + a + " should not be equal.");
		}
			
	}
}

