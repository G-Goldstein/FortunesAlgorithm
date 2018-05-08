using NUnit.Framework;
using System;
using FortunesAlgorithm;

namespace FortunesAlgorithmTest
{
	[TestFixture ()]
	public class PointTest
	{
		[Test ()]
		public void Equals()
		{
			Point a = new Point (2, 1);
			Point b = new Point (2, 1);
			Assert.That (a, Is.EqualTo (b));
		}

		[Test ()]
		public void NotEqual() {
			Point a = new Point (2, 1);
			Point b = new Point (-2, -1);
			Point c = new Point (1, 2);
			Assert.That (a, Is.Not.EqualTo (b));
			Assert.That (a, Is.Not.EqualTo (c));
			Assert.That (b, Is.Not.EqualTo (c));
		}

		[Test()]
		public void LineWith() {
			Point bottomLeft = new Point (0, 0);
			Point topLeft = new Point (0, 1);
			Point bottomRight = new Point (1, 0);
			Point topRight = new Point (1, 1);

			Line top = new Line (0, 1, -1);
			Line bottom = new Line (0, 1, 0);
			Line left = new Line (1, 0, 0);
			Line right = new Line (1, 0, -1);
			Line topLeftToBottomRight = new Line (1, 1, -1);
			Line bottomLeftToTopRight = new Line (1, -1, 0);

			Assert.AreEqual (bottomLeft.LineWith (topLeft), left);
			Assert.AreEqual (bottomLeft.LineWith (bottomRight), bottom);
			Assert.AreEqual (bottomLeft.LineWith (topRight), bottomLeftToTopRight);
			Assert.AreEqual (topLeft.LineWith (bottomRight), topLeftToBottomRight);
			Assert.AreEqual (topLeft.LineWith (topRight), top);
			Assert.AreEqual (bottomRight.LineWith (topRight), right);
		}

		[Test]
		public void MidPointWith ()
		{
			Point a = new Point (0, 0);
			Point b = new Point (0, 4);
			Point ab = new Point (0, 2);

			Assert.AreEqual (ab, a.MidpointWith(b));

			a = new Point (3, -5);
			b = new Point (2, -7);
			ab = new Point (2.5f, -6);

			Assert.AreEqual (ab, a.MidpointWith (b));
		}

		[Test]
		public void StressTest() {
			// With floating point precision, do the maths break down after many repeated calls?
			// We'll use a rectangle of points to construct a line and two perpendiculars,
			// and keep calling for the intersections of the lines made from the line
			// passing through the previous iteration's intersections.
			Point topLeft = new Point(3, 7);
			Point bottomLeft = new Point(2, 3);
			Point bottomRight = new Point(6, 2);
			Point topRight = new Point(7, 6);

			Line left = topLeft.LineWith (bottomLeft);
			Line bottom = bottomLeft.LineWith (bottomRight);
			Line right = bottomRight.LineWith (topRight);
			Line top = topRight.LineWith (topLeft);

			Point cumulativeLeftPoint = topLeft;
			Point cumulativeRightPoint = topRight;
			Line cumulativeHorizontal;

			for (int i = 0; i < 1000; i++) {
				cumulativeHorizontal = cumulativeLeftPoint.LineWith (cumulativeRightPoint);
				Assert.AreEqual (cumulativeHorizontal, cumulativeLeftPoint.LineWith (cumulativeRightPoint));
				cumulativeLeftPoint = left.Intersect (cumulativeHorizontal);
				cumulativeRightPoint = right.Intersect (cumulativeHorizontal);
				Assert.AreEqual (cumulativeLeftPoint, topLeft);
				Assert.AreEqual (cumulativeRightPoint, topRight);
			}
		}
	}
}