using NUnit.Framework;
using System;
using FortunesAlgorithm;
using System.Collections.Generic;
using System.Linq;

namespace ScatterPlotTest
{
    [TestFixture]
    public class InteriorPointsIncluded
    {
        ScatterPlot sp;
        static List<Point> inPoints;
        static List<Point> outPoints;
        static List<Point> borderPoints;

        [SetUp]
        public void SetUp()
        {
            SetUpPoints();
            sp = new ScatterPlot(new Rectangle(new Point(0, 3), new Point(5, 0)), inPoints.Union(outPoints).Union(borderPoints));
        }

        public static void SetUpPoints()
        {
            inPoints = new List<Point> { new Point(2, 1), new Point(4.9f, 2.9f), new Point(2, 2) };
            outPoints = new List<Point> { new Point(6, 1), new Point(5.1f, 2.9f), new Point(2, 4) };
            borderPoints = new List<Point> { new Point(5, 1), new Point(0, 0), new Point(2, 3), new Point(5, 3), new Point(0, 1), new Point(1, 0) };
        }

        [TestCaseSource("InPointsData")]
        public void InPoints(Point p)
        {
            Assert.Contains(p, sp.points);
        }

        [TestCaseSource("NotInPointsData")]
        public void NotInPoints(Point p)
        {
            CollectionAssert.DoesNotContain(sp.points, p);
        }

        public static IEnumerable<ITestCaseData> InPointsData
        {
            get
            {
                SetUpPoints();
                foreach (Point p in inPoints)
                    yield return new TestCaseData(p);
            }
        }

        public static IEnumerable<ITestCaseData> NotInPointsData
        {
            get
            {
                SetUpPoints();
                foreach (Point p in outPoints)
                    yield return new TestCaseData(p);
                foreach (Point p in borderPoints)
                    yield return new TestCaseData(p);
            }
        }
    }
}
