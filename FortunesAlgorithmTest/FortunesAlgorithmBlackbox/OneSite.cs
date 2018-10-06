using NUnit.Framework;
using System;
using FortunesAlgorithm;
using System.Collections.Generic;
using System.Linq;

namespace FortunesAlgorithmBlackbox
{
    [TestFixture]
    public class OneSite
    {
        VoronoiDiagramBordered voronoi;
        Point site;
        Rectangle border;

        [SetUp]
        public void SetUp()
        {
            site = new Point(3, 4);
            border = new Rectangle(new Point(1, 5), new Point(8, -1));
            voronoi = new VoronoiDiagramBordered(new List<Point> { site }, border);
        }

        [Test]
        public void ResultingDiagramHasOneCell()
        {
            Assert.AreEqual(1, voronoi.Cells().Count());
        }

        [Test]
        public void ResultingDiagramsCellHasCorrectSite()
        {
            Assert.AreEqual(site, voronoi.Cells().First().Site());
        }

        [Test]
        public void ResultingDiagramsCellHasFourBorders()
        {
            Assert.AreEqual(4, voronoi.Cells().First().Borders().Count());
        }
    }
}