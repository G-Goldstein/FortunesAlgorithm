using NUnit.Framework;
using System;
using FortunesAlgorithm;
using System.Collections.Generic;
using System.Linq;

namespace FortunesAlgorithm
{
	[TestFixture]
	public class VerticalRowOfSites
	{
		VoronoiDiagram voronoi;
        Point bottomSite;
        Point middleSite;
        Point topSite;

        Dictionary<Point, List<Point>> siteToBordersMap;

        [SetUp]
		public void SetUp() {
            bottomSite = new Point (0, -2);
			middleSite = new Point (0, 0);
            topSite = new Point (0, 2);
			voronoi = new VoronoiDiagram(new List<Point>{ bottomSite, middleSite, topSite });

            siteToBordersMap = new Dictionary<Point, List<Point>>();

            List<VoronoiCell> cells = voronoi.Cells().ToList();
            foreach (VoronoiCell cell in cells)
            {
                siteToBordersMap[cell.Site()] = cell.Borders().ToList();
            }
        }

		[Test]
		public void ResultingDiagramHasThreeCells ()
		{
			Assert.AreEqual (3, voronoi.Cells ().Count ());
		}

        [Test]
        public void BottomCellHasOneBorder()
        {
            Assert.AreEqual(1, siteToBordersMap[bottomSite].Count());
        }

        [Test]
        public void TopCellHasOneBorder()
        {
            Assert.AreEqual(1, siteToBordersMap[topSite].Count());
        }

        [Test]
        public void MiddleCellHasTwoBorders()
        {
            Assert.AreEqual(2, siteToBordersMap[middleSite].Count());
        }

        [Test]
        public void BottomCellBordersMiddleCell()
        {
            Assert.Contains(middleSite, siteToBordersMap[bottomSite]);
        }

        [Test]
        public void TopCellBordersMiddleCell()
        {
            Assert.Contains(middleSite, siteToBordersMap[topSite]);
        }

        [Test]
        public void MiddleCellBordersBottomCell()
        {
            Assert.Contains(bottomSite, siteToBordersMap[middleSite]);
        }

        [Test]
        public void MiddleCellBordersTopCell()
        {
            Assert.Contains(topSite, siteToBordersMap[middleSite]);
        }
    }
}

