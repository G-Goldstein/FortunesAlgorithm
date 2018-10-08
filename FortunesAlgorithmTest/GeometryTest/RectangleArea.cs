using NUnit.Framework;
using System;
using FortunesAlgorithm;
using System.Collections.Generic;
using System.Linq;

namespace GeometryTest
{
    [TestFixture]
    public class RectangleArea
    {
        Rectangle r;

        [SetUp]
        public void SetUp()
        {
            r = new Rectangle(new Point(-3, 6), new Point(8, -1));
        }

        [Test]
        public void NumberOfPointsMatchesRequest()
        {
            Assert.AreEqual(77, r.Area());
        }
    }
}
