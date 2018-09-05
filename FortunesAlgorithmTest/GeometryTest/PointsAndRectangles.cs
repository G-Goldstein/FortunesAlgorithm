using NUnit.Framework;
using System;
using FortunesAlgorithm;
using System.Collections.Generic;

namespace GeometryTest
{
    [TestFixture]
    public class PointsAndRectangles
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
}
