using NUnit.Framework;
using System;
using FortunesAlgorithm;
using System.Collections.Generic;
using System.Linq;

namespace FortunesAlgorithm
{
	[TestFixture]
	public class HorizontalRowOfSites
	{
		VoronoiDiagram voronoi;
        Point leftSite;
        Point middleSite;
        Point rightSite;

        Dictionary<Point, List<Point>> siteToBordersMap;

        [SetUp]
		public void SetUp() {
            leftSite = new Point (-2, 0);
			middleSite = new Point (0, 0);
            rightSite = new Point (2, 0);
			voronoi = new VoronoiDiagram(new List<Point>{ leftSite, middleSite, rightSite });

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
        public void LeftCellHasOneBorder()
        {
            Assert.AreEqual(1, siteToBordersMap[leftSite].Count());
        }

        [Test]
        public void RightCellHasOneBorder()
        {
            Assert.AreEqual(1, siteToBordersMap[rightSite].Count());
        }

        [Test]
        public void MiddleCellHasTwoBorders()
        {
            Assert.AreEqual(2, siteToBordersMap[middleSite].Count());
        }

        [Test]
        public void LeftCellBordersMiddleCell()
        {
            Assert.Contains(middleSite, siteToBordersMap[leftSite]);
        }

        [Test]
        public void RightCellBordersMiddleCell()
        {
            Assert.Contains(middleSite, siteToBordersMap[rightSite]);
        }

        [Test]
        public void MiddleCellBordersLeftCell()
        {
            Assert.Contains(leftSite, siteToBordersMap[middleSite]);
        }

        [Test]
        public void MiddleCellBordersRightCell()
        {
            Assert.Contains(rightSite, siteToBordersMap[middleSite]);
        }
    }
}

