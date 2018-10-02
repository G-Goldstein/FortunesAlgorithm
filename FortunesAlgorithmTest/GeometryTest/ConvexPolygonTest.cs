using NUnit.Framework;
using System;
using FortunesAlgorithm;
using System.Collections.Generic;
using System.Linq;

namespace GeometryTest
{
    [TestFixture]
    public class ConvexSquare
    {
        List<Point> vertices;
        ConvexPolygon poly;
        static Point topLeft;
        static Point topRight;
        static Point bottomLeft;
        static Point bottomRight;

        [SetUp]
        public void SetUp()
        { 
            SetPoints();
            vertices = new List<Point> { topLeft, bottomRight, bottomLeft, topRight };
            poly = new ConvexPolygon(vertices);
        }

        static void SetPoints()
        {
            topLeft = new Point(-1, 1);
            topRight = new Point(1, 1);
            bottomLeft = new Point(-1, -1);
            bottomRight = new Point(1, -1);
        }

        [TestCaseSource("NextPointsData")]
        public void NextPoints(Point a, Point b)
        {
            Assert.AreEqual(b, poly.NextVertex(a));
        }

        [TestCaseSource("PreviousPointsData")]
        public void PreviousPoints(Point a, Point b)
        {
            Assert.AreEqual(b, poly.PreviousVertex(a));
        }

        [TestCaseSource("OrderedPointsData")]
        public void AllPoints(Point start, List<Point> ordering)
        {
            IEnumerable<Point> actual = poly.AllPointsInOrder(start);
            Assert.AreEqual(ordering, actual);
        }

        public static IEnumerable<ITestCaseData> NextPointsData
        {
            get
            {
                SetPoints();
                yield return new TestCaseData(topRight, topLeft);
                yield return new TestCaseData(topLeft, bottomLeft);
                yield return new TestCaseData(bottomLeft, bottomRight);
                yield return new TestCaseData(bottomRight, topRight);
            }
        }

        public static IEnumerable<ITestCaseData> PreviousPointsData
        {
            get
            {
                SetPoints();
                yield return new TestCaseData(topRight, bottomRight);
                yield return new TestCaseData(topLeft, topRight);
                yield return new TestCaseData(bottomLeft, topLeft);
                yield return new TestCaseData(bottomRight, bottomLeft);
            }
        }

        public static IEnumerable<ITestCaseData> OrderedPointsData
        {
            get
            {
                SetPoints();
                yield return new TestCaseData(topRight, new List<Point> { topRight, topLeft, bottomLeft, bottomRight });
                yield return new TestCaseData(topLeft, new List<Point> { topLeft, bottomLeft, bottomRight, topRight });
                yield return new TestCaseData(bottomLeft, new List<Point> { bottomLeft, bottomRight, topRight, topLeft });
                yield return new TestCaseData(bottomRight, new List<Point> { bottomRight, topRight, topLeft, bottomLeft });
            }
        }
    }
}