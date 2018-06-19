using NUnit.Framework;
using System;
using FortunesAlgorithm;
using System.Collections.Generic;
using System.Linq;

namespace FortunesAlgorithm
{
	[TestFixture]
	public class HorizontalRowOfSitesWithLeader
	{
		VoronoiDiagram voronoi;
        List<Point> linearSites;
        Point leadingPoint;
        int linearSiteCount;

        Dictionary<Point, List<Point>> siteToBordersMap;

        [SetUp]
		public void SetUp() {
            linearSiteCount = 6;
            linearSites = new List<Point>();
            List<Point> sites = new List<Point>();
            for (int i = 0; i < linearSiteCount; i++)
            {
                float x = 2*i + 1 - linearSiteCount;
                linearSites.Add(new Point(x, 0));
                sites.Add(new Point(x, 0));
            }

            // Leading point, to make sure that our linear sites are not added as leaders before the algorithm can get into full swing.
            leadingPoint = new Point(0, 3);
            sites.Add(leadingPoint);

			voronoi = new VoronoiDiagram(sites);

            siteToBordersMap = new Dictionary<Point, List<Point>>();

            List<VoronoiCell> cells = voronoi.Cells().ToList();
            foreach (VoronoiCell cell in cells)
            {
                siteToBordersMap[cell.Site()] = cell.Borders().ToList();
            }
        }

		[Test]
		public void ResultingDiagramHasOneCellPerSite ()
		{
			Assert.AreEqual (linearSiteCount + 1, voronoi.Cells ().Count ());
		}

        [Test]
        public void FirstLinearCellHasTwoBorders()
        {
            Assert.AreEqual(2, siteToBordersMap[linearSites.First()].Count());
        }

        [Test]
        public void LastLinearCellHasTwoBorders()
        {
            Assert.AreEqual(2, siteToBordersMap[linearSites.Last()].Count());
        }

        [Test]
        public void MiddleLinearCellsHaveThreeBorders([Range(1, 4)] int i)
        {
            Assert.AreEqual(3, siteToBordersMap[linearSites[i]].Count());
        }

        [Test]
        public void EachLinearCellBordersNext([Range(0, 4)] int i)
        {
            Assert.Contains(linearSites[i + 1], siteToBordersMap[linearSites[i]]);
        }

        [Test]
        public void EachLinearCellBordersPrevious([Range(1, 5)] int i)
        {
            Assert.Contains(linearSites[i - 1], siteToBordersMap[linearSites[i]]);
        }

        [Test]
        public void EachLinearCellBordersLeader([Range(0, 5)] int i)
        {
            Assert.Contains(leadingPoint, siteToBordersMap[linearSites[i]]);
        }

        [Test]
        public void LeaderBordersEachLinearCell([Range(0, 5)] int i)
        {
            Assert.Contains(linearSites[i], siteToBordersMap[leadingPoint]);
        }
    }
}

