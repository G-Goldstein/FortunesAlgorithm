using NUnit.Framework;
using System;
using FortunesAlgorithm;
using System.Collections.Generic;
using System.Linq;

namespace FortunesAlgorithmBlackbox
{
    [TestFixture]
    public class RandomSites
    {
        VoronoiDiagramBordered voronoi;
        int points;
        Rectangle border;
        ScatterPlot scatterPlot;

        [Test]
        public void VoronoiWithZeroSitesThrowsArgumentException()
        {
            points = 0;
            border = new Rectangle(new Point(0, 10), new Point(10, 0));
            scatterPlot = new ScatterPlot(border, points);
            Assert.Throws<ArgumentException>(() => scatterPlot.Voronoi());
        }

        [TestCaseSource("PointCounts")]
        public void BothCellVertices(int pointCount)
        {
            border = new Rectangle(new Point(0, 10), new Point(10, 0));
            scatterPlot = new ScatterPlot(border, pointCount);
            voronoi = scatterPlot.Voronoi();
            Assert.AreEqual(pointCount, voronoi.Cells().Count());
        }

        public static IEnumerable<ITestCaseData> PointCounts
        {
            get
            {
                yield return new TestCaseData(1);
                yield return new TestCaseData(2);
                yield return new TestCaseData(3);
                yield return new TestCaseData(4);
                yield return new TestCaseData(5);
                yield return new TestCaseData(10);
                yield return new TestCaseData(100);
            }
        }
    }
}