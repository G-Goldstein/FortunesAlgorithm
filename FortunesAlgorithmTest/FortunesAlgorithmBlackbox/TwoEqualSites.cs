using NUnit.Framework;
using System;
using FortunesAlgorithm;
using System.Collections.Generic;
using System.Linq;

namespace FortunesAlgorithmBlackbox
{
    [TestFixture]
    public class TwoEqualSites
    {
        VoronoiDiagramBordered voronoi;
        List<Point> sites;
        Rectangle border;

        [SetUp]
        public void SetUp()
        {
            sites = new List<Point> { new Point(1, 1), new Point(3, 1) };
            border = new Rectangle(new Point(0, 2), new Point(4, 0));
            voronoi = new VoronoiDiagramBordered(sites, border);
        }

        [Test]
        public void ResultingDiagramHasTwoCells()
        {
            Assert.AreEqual(2, voronoi.Cells().Count());
        }

        [Test]
        public void CellsHaveCorrectSites()
        {
            Assert.AreEqual(sites, voronoi.Cells().Select(c => c.Site()));
        }

        [Test]
        public void FirstCellHasFourBorders()
        {
            Assert.AreEqual(4, voronoi.Cells().First().Borders().Count());
        }

        [Test]
        public void SecondCellHasFourBorders()
        {
            Assert.AreEqual(4, voronoi.Cells().Last().Borders().Count());
        }

        [TestCaseSource("SingleCellVerticesData")]
        public void SingleCellVertices(Point v)
        {
            List<Point> firstVertices = voronoi.Cells().First().Vertices().ToList();
            List<Point> lastVertices = voronoi.Cells().Last().Vertices().ToList();
            Assert.IsTrue(firstVertices.Contains(v) ^
                          lastVertices.Contains(v),
                          string.Format("{0} should be in exactly one of {1} or {2}", v,
                          string.Join(", ", firstVertices.Select(p => p.ToString()).ToArray()),
                          string.Join(", ", lastVertices.Select(p => p.ToString()).ToArray())));
        }

        [TestCaseSource("BothCellVerticesData")]
        public void BothCellVertices(Point v)
        {
            List<Point> firstVertices = voronoi.Cells().First().Vertices().ToList();
            List<Point> lastVertices = voronoi.Cells().Last().Vertices().ToList();
            Assert.IsTrue(firstVertices.Contains(v) &&
                          lastVertices. Contains(v),
                          string.Format("{0} should be in both of {1} and {2}", v,
                          string.Join(", ", firstVertices.Select(p => p.ToString()).ToArray()),
                          string.Join(", ", lastVertices.Select(p => p.ToString()).ToArray())));
        }

        public static IEnumerable<ITestCaseData> SingleCellVerticesData
        {
            get
            {
                yield return new TestCaseData(new Point(4, 2));
                yield return new TestCaseData(new Point(0, 2));
                yield return new TestCaseData(new Point(0, 0));
                yield return new TestCaseData(new Point(4, 0));
            }
        }

        public static IEnumerable<ITestCaseData> BothCellVerticesData
        {
            get
            {
                yield return new TestCaseData(new Point(2, 2));
                yield return new TestCaseData(new Point(2, 0));
            }
        }
    }
}