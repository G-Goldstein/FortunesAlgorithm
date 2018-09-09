using NUnit.Framework;
using System;
using FortunesAlgorithm;
using System.Collections.Generic;
using System.Linq;

namespace GeometryTest
{
    [TestFixture]
    public class RectangleEnclosesPoint
    {
        Rectangle rect;

        [SetUp]
        public void SetUp()
        {
            rect = new Rectangle(new Point(-1, 1), new Point(1, -1));
        }

        [TestCaseSource("OutsidePoints")]
        [TestCaseSource("BorderPoints")]
        public void OutsidePointsAreOutside(Point p)
        {
            Assert.False(Geometry.RectangleEnclosesPoint(rect, p));
        }

        [TestCaseSource("InsidePoints")]
        public void InsidePointsAreInside(Point p)
        {
            Assert.True(Geometry.RectangleEnclosesPoint(rect, p));
        }

        public static IEnumerable<ITestCaseData> OutsidePoints
        {
            get
            {
                yield return new TestCaseData(new Point(-2, 2));
                yield return new TestCaseData(new Point(0, 2));
                yield return new TestCaseData(new Point(2, 2));
                yield return new TestCaseData(new Point(-2, 0));
                yield return new TestCaseData(new Point(2, 0));
                yield return new TestCaseData(new Point(-2, -2));
                yield return new TestCaseData(new Point(0, -2));
                yield return new TestCaseData(new Point(2, -2));
            }
        }

        public static IEnumerable<ITestCaseData> BorderPoints
        {
            get
            {
                yield return new TestCaseData(new Point(-1, 1));
                yield return new TestCaseData(new Point(0, 1));
                yield return new TestCaseData(new Point(1, 1));
                yield return new TestCaseData(new Point(-1, 0));
                yield return new TestCaseData(new Point(1, 0));
                yield return new TestCaseData(new Point(-1, -1));
                yield return new TestCaseData(new Point(0, -1));
                yield return new TestCaseData(new Point(1, -1));
            }
        }

        public static IEnumerable<ITestCaseData> InsidePoints
        {
            get
            {
                yield return new TestCaseData(new Point(0, 0));
                yield return new TestCaseData(new Point(0, 0.5f));
                yield return new TestCaseData(new Point(0.9f, 0.9f));
                yield return new TestCaseData(new Point(-0.9f, 0.4f));
            }
        }
    }

    [TestFixture]
    public class MirrorPointsForRectangle
    {
        Rectangle rect;

        [SetUp]
        public void SetUp()
        {
            rect = new Rectangle(new Point(-10, 10), new Point(10, -10));
        }

        [Test]
        public void MirrorPointsAreCorrect()
        {
            Point site = new Point(3, -7);
            HashSet<Point> expectedPoints = new HashSet<Point> { new Point(17, -7), new Point(3, 27), new Point(-23, -7), new Point(3, -13) };
            Assert.AreEqual(expectedPoints, new HashSet<Point>(Geometry.MirrorPointsFormingRectangle(site, rect)));
        }
    }
}
