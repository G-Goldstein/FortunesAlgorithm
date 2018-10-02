using NUnit.Framework;
using System;
using FortunesAlgorithm;
using System.Collections.Generic;

namespace GeometryTest
{
    [TestFixture]
    class CentroidTest
    {
        [TestCaseSource("CentroidData")]
        public void NextPoints(ConvexPolygon poly, Point expectedCentroid)
        {
            Assert.AreEqual(expectedCentroid, poly.Centroid());
        }

        public static IEnumerable<ITestCaseData> CentroidData
        {
            get
            {
                yield return new TestCaseData(new ConvexPolygon(new List<Point> {
                    new Point(1, 1),
                    new Point(-1, -1),
                    new Point(1, -1),
                    new Point(-1, 1) }),
                    new Point(0, 0));
                yield return new TestCaseData(new ConvexPolygon(new List<Point> {
                    new Point(5, 4),
                    new Point(4, 5),
                    new Point(4, 3),
                    new Point(3, 4) }),
                    new Point(4, 4));
                yield return new TestCaseData(new ConvexPolygon(new List<Point> {
                    new Point(0, 0),
                    new Point(3, 0),
                    new Point(0, 3) }),
                    new Point(1, 1));
            }
        }
    }
}
