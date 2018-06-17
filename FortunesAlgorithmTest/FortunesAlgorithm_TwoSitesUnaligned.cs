using NUnit.Framework;
using System;
using FortunesAlgorithm;
using System.Collections.Generic;
using System.Linq;

namespace FortunesAlgorithm
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
		public void ResultingDiagramHasTwoSites ()
		{
			Assert.AreEqual (2, voronoi.Cells ().Count ());
		}

		[Test]
		public void EachCellHasOneBorder() {
			List<VoronoiCell> cells = voronoi.Cells ().ToList ();
			foreach (VoronoiCell cell in cells) {
				Assert.AreEqual (1, cell.Borders ().Count ());
			}
		}
	}
}

