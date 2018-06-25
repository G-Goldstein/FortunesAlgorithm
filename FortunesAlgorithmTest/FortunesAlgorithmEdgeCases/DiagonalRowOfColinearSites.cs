using NUnit.Framework;
using System;
using FortunesAlgorithm;
using System.Collections.Generic;
using System.Linq;

namespace FortunesAlgorithm
{
    [TestFixture]
    public class DiagonalRowOfSites
    {
        VoronoiDiagram voronoi;
        List<Point> sites;
        int siteCount;

        Dictionary<Point, List<Point>> siteToBordersMap;

        [SetUp]
        public void SetUp()
        {
            siteCount = 6;
            sites = new List<Point>();
            for (int i = 0; i < siteCount; i++)
            {
                float x = 2 * i + 1 - siteCount;
                float y = 2 * i + 1 - siteCount;
                sites.Add(new Point(x, y));
            }
            voronoi = new VoronoiDiagram(sites);

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
            Assert.AreEqual(siteCount, voronoi.Cells().Count());
        }

        [Test]
        public void FirstCellHasOneBorder()
        {
            Assert.AreEqual(1, siteToBordersMap[sites.First()].Count());
        }

        [Test]
        public void LastCellHasOneBorder()
        {
            Assert.AreEqual(1, siteToBordersMap[sites.Last()].Count());
        }

        [Test]
        public void MiddleCellsHaveTwoBorders()
        {
            for (int i = 1; i < siteCount - 1; i++)
                Assert.AreEqual(2, siteToBordersMap[sites[i]].Count());
        }

        [Test]
        public void EachCellBordersNext()
        {
            for (int i = 0; i < siteCount - 1; i++)
                Assert.Contains(sites[i + 1], siteToBordersMap[sites[i]]);
        }

        [Test]
        public void EachCellBordersPrevious()
        {
            for (int i = 1; i < siteCount; i++)
                Assert.Contains(sites[i - 1], siteToBordersMap[sites[i]]);
        }
    }
}

