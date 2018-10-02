using NUnit.Framework;
using System;
using FortunesAlgorithm;
using System.Collections.Generic;
using System.Linq;

namespace FortunesAlgorithm
{
	[TestFixture]
	public class ThreeSitesUnaligned
	{
		VoronoiDiagram voronoi;
		Point siteA;
		Point siteB;
        Point siteC;

        Dictionary<Point, List<Point>> siteToBordersMap;

        [SetUp]
		public void SetUp() {
			siteA = new Point (4, -5);
			siteB = new Point (3, 2);
            siteC = new Point (-1, -1);
			voronoi = new VoronoiDiagram(new List<Point>{siteA, siteB, siteC});

            siteToBordersMap = new Dictionary<Point, List<Point>>();

            List<VoronoiCellUnorganised> cells = voronoi.Cells().ToList();
            foreach (VoronoiCellUnorganised cell in cells)
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
        public void CellAHasTwoBorders()
        {
            Assert.AreEqual(2, siteToBordersMap[siteA].Count());
        }

        [Test]
        public void CellBHasTwoBorders()
        {
            Assert.AreEqual(2, siteToBordersMap[siteB].Count());
        }

        [Test]
        public void CellCHasTwoBorders()
        {
            Assert.AreEqual(2, siteToBordersMap[siteC].Count());
        }

        [Test]
        public void CellABordersB()
        {
            Assert.Contains(siteB, siteToBordersMap[siteA]);
        }

        [Test]
        public void CellABordersC()
        {
            Assert.Contains(siteC, siteToBordersMap[siteA]);
        }

        [Test]
        public void CellBBordersA()
        {
            Assert.Contains(siteA, siteToBordersMap[siteB]);
        }

        [Test]
        public void CellBBordersC()
        {
            Assert.Contains(siteC, siteToBordersMap[siteB]);
        }

        [Test]
        public void CellCBordersA()
        {
            Assert.Contains(siteA, siteToBordersMap[siteC]);
        }

        [Test]
        public void CellCBordersB()
        {
            Assert.Contains(siteB, siteToBordersMap[siteC]);
        }
	}
}

