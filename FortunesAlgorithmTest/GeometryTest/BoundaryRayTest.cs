using NUnit.Framework;
using System;
using FortunesAlgorithm;
using System.Collections.Generic;

namespace GeometryTest
{
	[TestFixture]
	public class PerpendicularBisectors
	{
		[Test]
		public void PerpendicularBisectorsAreGeneratedCorrectly ()
		{
			PerpendicularBisector aBR = new PerpendicularBisector (new Point (0, 0), new Point (1, 1));
			Line aL = new Line (1, 1, -1);
			Assert.AreEqual (aL, aBR.Line ());

			aBR = new PerpendicularBisector (new Point (3, 4), new Point (4, 5));
			aL = new Line (1, 1, -8);
			Assert.AreEqual (aL, aBR.Line ());
		}
	}
}