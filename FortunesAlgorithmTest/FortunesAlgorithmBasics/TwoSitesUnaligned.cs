using NUnit.Framework;
using System;
using FortunesAlgorithm;
using System.Collections.Generic;
using System.Linq;

namespace FortunesAlgorithmBasics
{
	[TestFixture]
	public class TwoSitesUnaligned
	{
		VoronoiDiagram voronoi;
		Point siteA;
		Point siteB;

		[SetUp]
		public void SetUp() {
			siteA = new Point (4, -5);
			siteB = new Point (3, 2);
			voronoi = new VoronoiDiagram(new List<Point>{siteA, siteB});
		}

		[Test]
		public void ResultingDiagramHasTwoCells ()
		{
			Assert.AreEqual (2, voronoi.UnorganisedCells ().Count ());
		}

		[Test]
		public void EachCellHasOneBorder() {
			List<VoronoiCellUnorganised> cells = voronoi.UnorganisedCells ().ToList ();
			foreach (VoronoiCellUnorganised cell in cells) {
				Assert.AreEqual (1, cell.Borders ().Count ());
			}
		}

        [Test]
        public void EachCellsBorderIsTheOtherSite()
        {
            Dictionary<Point, Point> siteToBorderMap = new Dictionary<Point, Point>();
            Dictionary<Point, Point> expectedBorderMap = new Dictionary<Point, Point>();

            expectedBorderMap[siteA] = siteB;
            expectedBorderMap[siteB] = siteA;

            List<VoronoiCellUnorganised> cells = voronoi.UnorganisedCells().ToList();
            foreach (VoronoiCellUnorganised cell in cells)
            {
                siteToBorderMap[cell.Site()] = cell.Borders().First();
            }

            Assert.AreEqual(expectedBorderMap, siteToBorderMap);
        }
	}
}

