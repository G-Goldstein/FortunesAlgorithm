using NUnit.Framework;
using System;
using FortunesAlgorithm;
using System.Collections.Generic;
using System.Linq;

namespace FortunesAlgorithmBasics
{
	[TestFixture]
	public class OneSite
	{
		VoronoiDiagram voronoi;
		Point site;

		[SetUp]
		public void SetUp() {
			site = new Point (4, -5);
			voronoi = new VoronoiDiagram(new List<Point>{site});
		}

		[Test]
		public void ResultingDiagramHasOneCell ()
		{
			Assert.AreEqual (1, voronoi.UnorganisedCells ().Count ());
		}

		[Test]
		public void ResultingDiagramsCellIsCentredOnSite() {
			Assert.AreEqual (site, voronoi.UnorganisedCells ().First ().Site ());
		}

		[Test]
		public void ResultingDiagramsCellHasNoBorders() {
			Assert.AreEqual (0, voronoi.UnorganisedCells ().First ().Borders ().Count ());
		}
	}
}

