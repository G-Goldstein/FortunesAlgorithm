using NUnit.Framework;
using System;
using FortunesAlgorithm;
using System.Collections.Generic;
using System.Linq;

namespace ScatterPlotTest
{
    [TestFixture]
    public class MeanRadiusRequest
    {
        ScatterPlot sp;
        float requestedRadius;
        int expectedPointCount;
        
        [SetUp]
        public void SetUp()
        {
            requestedRadius = 0.5f;
            sp = new ScatterPlot(new Rectangle(new Point(0, 3), new Point(5, 0)), requestedRadius);
            float requestedArea = (float)Math.PI * requestedRadius * requestedRadius;
            expectedPointCount = (int)(sp.rectangle.Area() / requestedArea);
        }

        [Test]
        public void NumberOfPointsMatchesRequest()
        {
            Assert.AreEqual(expectedPointCount, sp.points.Count);
        }
    }

    [TestFixture]
    public class MeanRadiusRequestSmall
    {
        ScatterPlot sp;
        float requestedRadius;
        int expectedPointCount;

        [SetUp]
        public void SetUp()
        {
            requestedRadius = 0.05f;
            sp = new ScatterPlot(new Rectangle(new Point(0, 0.3f), new Point(0.5f, 0)), requestedRadius);
            float requestedArea = (float)Math.PI * requestedRadius * requestedRadius;
            expectedPointCount = (int)(sp.rectangle.Area() / requestedArea);
        }

        [Test]
        public void NumberOfPointsMatchesRequest()
        {
            Assert.AreEqual(expectedPointCount, sp.points.Count);
        }
    }
}
