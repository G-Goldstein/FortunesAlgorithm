using NUnit.Framework;
using System;
using FortunesAlgorithm;
using System.Collections.Generic;
using System.Linq;

namespace FortunesAlgorithm
{
    [TestFixture]
    public class PerfectSquare
    {
        VoronoiDiagram voronoi;
        Point topLeft;
        Point topRight;
        Point bottomLeft;
        Point bottomRight;
        List<Point> points;

        Dictionary<Point, List<Point>> siteToBordersMap;

        [SetUp]
        public void SetUp()
        {
            topLeft = new Point(-1, 1);
            topRight = new Point(1, 1);
            bottomLeft = new Point(-1, -1);
            bottomRight = new Point(1, -1);

            points = new List<Point> { topLeft, topRight, bottomLeft, bottomRight };

            voronoi = new VoronoiDiagram(points);

            siteToBordersMap = new Dictionary<Point, List<Point>>();

            List<VoronoiCell> cells = voronoi.Cells().ToList();
            foreach (VoronoiCell cell in cells)
            {
                siteToBordersMap[cell.Site()] = cell.Borders().ToList();
            }
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
                PerfectSquare ps = new PerfectSquare();
                ps.SetUp();
                yield return new TestCaseData(ps.topLeft, ps.topRight);
                yield return new TestCaseData(ps.topRight, ps.topLeft);
                yield return new TestCaseData(ps.topRight, ps.bottomRight);
                yield return new TestCaseData(ps.bottomRight, ps.topRight);
                yield return new TestCaseData(ps.bottomLeft, ps.bottomRight);
                yield return new TestCaseData(ps.bottomRight, ps.bottomLeft);
                yield return new TestCaseData(ps.topLeft, ps.bottomLeft);
                yield return new TestCaseData(ps.bottomLeft, ps.topLeft);
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
    }
}
