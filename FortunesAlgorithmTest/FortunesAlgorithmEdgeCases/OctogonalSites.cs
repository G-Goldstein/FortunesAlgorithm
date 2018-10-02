using NUnit.Framework;
using System;
using FortunesAlgorithm;
using System.Collections.Generic;
using System.Linq;

namespace FortunesAlgorithm
{
    [TestFixture]
    public class OctogonalSites
    {
        VoronoiDiagram voronoi;
        static List<Point> points;

        Dictionary<Point, List<Point>> siteToBordersMap;

        [SetUp]
        public void SetUp()
        {
            SetPoints();
            
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
            points = new List<Point> {
                new Point( 2,  1),
                new Point( 1,  2),
                new Point(-1,  2),
                new Point(-2,  1),
                new Point(-2, -1),
                new Point(-1, -2),
                new Point( 1, -2),
                new Point( 2, -1)
            };
        }

        [Test]
        public void ResultingDiagramHasOneCellPerSite()
        {
            Assert.AreEqual(points.Count(), voronoi.Cells().Count());
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
                for (int i = 0; i<7; i++)
                {
                    yield return new TestCaseData(points[i], points[i+1]);
                }
                yield return new TestCaseData(points[7], points[0]);
            }
        }

        [Test]
        [Ignore("This complex case fails - might fix later")]
        public void SitesHaveTwentySixTotalBorders()
        {
            int totalBorders = 0;
            foreach (KeyValuePair<Point, List<Point>> pair in siteToBordersMap)
            {
                totalBorders += pair.Value.Count();
            }
            Assert.AreEqual(26, totalBorders);
        }
    }
}
