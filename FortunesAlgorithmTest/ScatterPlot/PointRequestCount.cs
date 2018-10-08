using NUnit.Framework;
using System;
using FortunesAlgorithm;
using System.Collections.Generic;
using System.Linq;

namespace ScatterPlotTest
{
    [TestFixture]
    public class NonZeroPointRequestCount
    {
        ScatterPlot sp;
        int count;
        
        [SetUp]
        public void SetUp()
        {
            count = 10;
            sp = new ScatterPlot(new Rectangle(new Point(0, 3), new Point(5, 0)), count);
        }

        [Test]
        public void NumberOfPointsMatchesRequest()
        {
            Assert.AreEqual(count, sp.points.Count);
        }
    }

    [TestFixture]
    public class ZeroPointRequestCount
    {
        ScatterPlot sp;
        int count;

        [SetUp]
        public void SetUp()
        {
            count = 0;
            sp = new ScatterPlot(new Rectangle(new Point(0, 3), new Point(5, 0)), count);
        }

        [Test]
        public void NumberOfPointsMatchesRequest()
        {
            Assert.AreEqual(count, sp.points.Count);
        }
    }
}
