using NUnit.Framework;
using System;
using FortunesAlgorithm;
using System.Collections.Generic;
using System.Linq;

namespace FortunesAlgorithmBlackbox
{
    [TestFixture]
    public class OneSiteSmoothed
    {
        VoronoiDiagramBordered voronoi;
        Point site;
        Rectangle border;

        [SetUp]
        public void SetUp()
        {
            site = new Point(3, 4);
            border = new Rectangle(new Point(1, 5), new Point(8, -1));
            voronoi = new VoronoiDiagramBordered(new List<Point> { site }, border).Smoothed().Voronoi();
        }

        [Test]
        public void ResultingDiagramHasOneCell()
        {
            Assert.AreEqual(1, voronoi.Cells().Count());
        }

        [Test]
        public void ResultingDiagramsCellHasCorrectSite()
        {
            Assert.AreEqual(new Point(4.5f, 2), voronoi.Cells().First().Site());
        }

        [Test]
        public void ResultingDiagramsCellHasFourBorders()
        {
            Assert.AreEqual(4, voronoi.Cells().First().Borders().Count());
        }

        [Test]
        public void CellTopRight()
        {
            Assert.Contains(new Point(border.Right(), border.Top()), voronoi.Cells().First().Vertices().ToList());
        }

        [Test]
        public void CellTopLeft()
        {
            Assert.Contains(border.topLeft, voronoi.Cells().First().Vertices().ToList());
        }

        [Test]
        public void CellBottomLeft()
        {
            Assert.Contains(new Point(border.Left(), border.Bottom()), voronoi.Cells().First().Vertices().ToList());
        }

        [Test]
        public void CellBottomRight()
        {
            Assert.Contains(border.bottomRight, voronoi.Cells().First().Vertices().ToList());
        }
    }
}