using NUnit.Framework;
using System;
using FortunesAlgorithm;

namespace FortunesAlgorithmTest
{
	[TestFixture]
	public class BeachSectionTest
	{
		Random rng;

		[SetUp]
		public void SetUp() {
			rng = new Random ();
		}

		void AssertGreater (BeachSection a, BeachSection b) {
			Assert.Greater (a, b);
			Assert.Less (b, a);
		}
		
		[Test]
		public void LeftmostBeachSectionIsLesser ()
		{
			BeachSection leftBeach = new BeachSection (new Point (0, 0), null, new Point (0, 1));

			for (int i = 0; i < 100; i++) {
				AssertGreater (RandomBeachSection (), leftBeach);
			}
		}

		[Test]
		public void RightmostBeachSectionIsGreater ()
		{
			BeachSection rightBeach = new BeachSection (new Point (0, 0), new Point(2, 2), null);

			for (int i = 0; i < 100; i++) {
				AssertGreater (rightBeach, RandomBeachSection());
			}
		}

		[Test]
		public void FullBeachLineCannotBeCompared() {
			Assert.Throws<InvalidOperationException> (CompareFullBeachlineToOtherBeachline);
			Assert.Throws<InvalidOperationException> (CompareOtherBeachlineToFullBeachline);
		}

		[Test]
		public void BeachSectionsAtSameHeightAreComparedByHorizontalPosition() {
			for (int i = 0; i < 100; i++) {
				BeachSection beachA = new BeachSection (new Point (1, 0), RandomPoint (), RandomPoint ());
				BeachSection beachB = new BeachSection (new Point (-1, 0), RandomPoint (), RandomPoint ());
				AssertGreater (beachA, beachB);
			}
		}

		void CompareFullBeachlineToOtherBeachline() {
			BeachSection fullBeach = new BeachSection (new Point (0, 0), null, null);
			fullBeach.CompareTo (RandomBeachSection ());
		}

		void CompareOtherBeachlineToFullBeachline() {
			BeachSection fullBeach = new BeachSection (new Point (0, 0), null, null);
			RandomBeachSection().CompareTo (fullBeach);
		}

		Point RandomPoint() {
			return new Point (rng.Next (), rng.Next ());
		}

		BeachSection RandomBeachSection() {
			return new BeachSection (RandomPoint (), RandomPoint (), RandomPoint ());
		}
	}
}

