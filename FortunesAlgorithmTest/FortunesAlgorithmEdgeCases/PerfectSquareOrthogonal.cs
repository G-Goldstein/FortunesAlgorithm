using NUnit.Framework;
using System;
using FortunesAlgorithm;
using System.Collections.Generic;
using System.Linq;

namespace FortunesAlgorithm
{
    [TestFixture]
    public class PerfectSquareOrthogonal
    {
        VoronoiDiagram voronoi;
        static Point topLeft;
        static Point topRight;
        static Point bottomLeft;
        static Point bottomRight;
        List<Point> points;

        Dictionary<Point, List<Point>> siteToBordersMap;

        [SetUp]
        public void SetUp()
        {
            SetPoints();

            points = new List<Point> { topLeft, topRight, bottomLeft, bottomRight };

            voronoi = new VoronoiDiagram(points);

            siteToBordersMap = new Dictionary<Point, List<Point>>();

            List<VoronoiCellUnorganised> cells = voronoi.Cells().ToList();
            foreach (VoronoiCellUnorganised cell in cells)
            {
                siteToBordersMap[cell.Site()] = cell.Borders().ToList();
            }
        }

        static void SetPoints()
        {
            topLeft = new Point(-1, 1);
            topRight = new Point(1, 1);
            bottomLeft = new Point(-1, -1);
            bottomRight = new Point(1, -1);
        }

        [Test]
        public void ResultingDiagramHasOneCellPerSite()
        {
            Assert.AreEqual(4, voronoi.Cells().Count());
        }

        [TestCaseSource("AdjacentNeighbours")]
        public void AdjacentPointsAreNeighbours(Point a, Point b)
        {
            Assert.Contains(a, siteToBordersMap[b]);
        }

        public static IEnumerable<ITestCaseData> AdjacentNeighbours
        {
            get
            {
                SetPoints();
                yield return new TestCaseData(topLeft, topRight);
                yield return new TestCaseData(topRight, topLeft);
                yield return new TestCaseData(topRight, bottomRight);
                yield return new TestCaseData(bottomRight, topRight);
                yield return new TestCaseData(bottomLeft, bottomRight);
                yield return new TestCaseData(bottomRight, bottomLeft);
                yield return new TestCaseData(topLeft, bottomLeft);
                yield return new TestCaseData(bottomLeft, topLeft);
            }
        }

        [Test]
        public void ExactlyOneLeftPointBordersItsDiagonal()
        {
            if (siteToBordersMap[bottomRight].Contains(topLeft))
                CollectionAssert.DoesNotContain(siteToBordersMap[topRight], bottomLeft);
            else
                CollectionAssert.Contains(siteToBordersMap[topRight], bottomLeft);
        }

        [Test]
        public void ExactlyOneRightPointBordersItsDiagonal()
        {
            if (siteToBordersMap[bottomLeft].Contains(topRight))
                CollectionAssert.DoesNotContain(siteToBordersMap[topLeft], bottomRight);
            else
                CollectionAssert.Contains(siteToBordersMap[topLeft], bottomRight);
        }

        [Test]
        public void SitesHaveTenTotalBorders()
        {
            int totalBorders = 0;
            foreach (KeyValuePair<Point, List<Point>> pair in siteToBordersMap)
            {
                totalBorders += pair.Value.Count();
            }
            Assert.AreEqual(10, totalBorders);
        }
    }
}
