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

		// First, we'll get out of the way those cases where one beach section is leftmost or rightmost, or both.
		// These are trivial, because the leftmost beach section is always on the left, and so on.
		// The cases that will remain are those where both beach sections in the comparison have both a left and right edge.

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

		// Many of the tests here involve three points: An original focus, a lower left focus, and the point that makes the left edge of a beach line with the original focus.
		// Or, alternatively, if the lower focus is on the right, we'll look at the point that makes the right edge of a beach line with the original focus.
		// The tests will involve checking whether the beach section made from the original focus and the 'left' edge is to the left or right of a beach section focused on
		// the lower focus.
		// By swapping the two foci and mirroring the situation through a vertical line, we can generate most situations involving the comparison of two beach lines.
		// The remainder occur when two or more of these points align horizontally or vertically. These cases are trivial and can be covered separately.

		// The language of these tests refers to the primary beach section arc as 'this beach section', the focus of that arc as 'this focus',
		// and the beach section it's being compared against as 'that'. 'Edge point' is always the left/right point of this beach section, as appropriate.

		// It's notable that not all generated scenarios are possible, because sometimes parabolas will overlap too closely. 
		// While it would be nice to omit these scenarios, it's easier to just accept that the valid scenarios give the
		// correct result, while the invalid ones give an 'appropriate' result that doesn't matter in practice.

		[Test]
		public void When_ThisFocusLeftOfThat_And_EdgePointAboveThisFocus_Then_ThisIsLeftOfThat() {
			for (int i = 0; i < 100; i++) {
				float thisx = SomeValue (10);
				float thisy = SomeValue (10);
				float thatx = thisx + SomePositiveValue (10);
				float thaty = thisy - SomePositiveValue (10);
				float edgex = thisx + SomeNonZeroValue (10);
				float edgey = thisy + SomePositiveValue (10);
				Point thisFocus = new Point (thisx, thisy);
				Point thatFocus = new Point (thatx, thaty);
				Point edge = new Point (edgex, edgey);
				BeachSection thisSection = new BeachSection (thisFocus, edge, RandomPoint ());
				BeachSection thatSection = new BeachSection (thatFocus, RandomPoint (), RandomPoint());
				AssertGreater (thatSection, thisSection);
			}
		}

		[Test]
		public void When_ThisFocusRightOfThat_And_EdgePointAboveThisFocus_Then_ThisIsRightOfThat() {
			for (int i = 0; i < 100; i++) {
				float thisx = SomeValue (10);
				float thisy = SomeValue (10);
				float thatx = thisx - SomePositiveValue (10);
				float thaty = thisy - SomePositiveValue (10);
				float edgex = thisx + SomeNonZeroValue (10);
				float edgey = thisy + SomePositiveValue (10);
				Point thisFocus = new Point (thisx, thisy);
				Point thatFocus = new Point (thatx, thaty);
				Point edge = new Point (edgex, edgey);
				BeachSection thisSection = new BeachSection (thisFocus, RandomPoint(), edge);
				BeachSection thatSection = new BeachSection (thatFocus, RandomPoint (), RandomPoint());
				AssertGreater (thisSection, thatSection);
			}
		}

		[Test]
		public void When_ThisFocusLeftOfThat_And_EdgePointBelowThatFocus_And_EdgePointLeftOfThatFocus_Then_ThisIsLeftOfThat() {
			for (int i = 0; i < 100; i++) {
				float thisx = SomeValue (10);
				float thisy = SomeValue (10);
				float thatx = thisx + SomePositiveValue (10);
				float thaty = thisy - SomePositiveValue (10);
				float edgex = thatx - SomePositiveValue (10);
				float edgey = thaty - SomePositiveValue (10);
				Point thisFocus = new Point (thisx, thisy);
				Point thatFocus = new Point (thatx, thaty);
				Point edge = new Point (edgex, edgey);
				BeachSection thisSection = new BeachSection (thisFocus, edge, RandomPoint ());
				BeachSection thatSection = new BeachSection (thatFocus, RandomPoint (), RandomPoint());
				AssertGreater (thatSection, thisSection);
			}
		}

		[Test]
		public void When_ThisFocusRightOfThat_And_EdgePointBelowThatFocus_And_EdgePointRightOfThatFocus_Then_ThisIsRightOfThat() {
			for (int i = 0; i < 100; i++) {
				float thisx = SomeValue (10);
				float thisy = SomeValue (10);
				float thatx = thisx - SomePositiveValue (10);
				float thaty = thisy - SomePositiveValue (10);
				float edgex = thatx + SomePositiveValue (10);
				float edgey = thaty - SomePositiveValue (10);
				Point thisFocus = new Point (thisx, thisy);
				Point thatFocus = new Point (thatx, thaty);
				Point edge = new Point (edgex, edgey);
				BeachSection thisSection = new BeachSection (thisFocus, RandomPoint (), edge);
				BeachSection thatSection = new BeachSection (thatFocus, RandomPoint (), RandomPoint());
				AssertGreater (thatSection, thisSection);
			}
		}

		[Test]
		public void When_ThisFocusLeftOfThat_And_EdgePointBelowThatFocus_And_EdgePointRightOfThatFocus_Then_ThisIsRightOfThat() {
			for (int i = 0; i < 100; i++) {
				float thisx = SomeValue (10);
				float thisy = SomeValue (10);
				float thatx = thisx + SomePositiveValue (10);
				float thaty = thisy - SomePositiveValue (10);
				float edgex = thatx + SomePositiveValue (10);
				float edgey = thaty - SomePositiveValue (10);
				Point thisFocus = new Point (thisx, thisy);
				Point thatFocus = new Point (thatx, thaty);
				Point edge = new Point (edgex, edgey);
				BeachSection thisSection = new BeachSection (thisFocus, edge, RandomPoint ());
				BeachSection thatSection = new BeachSection (thatFocus, RandomPoint (), RandomPoint());
				AssertGreater (thisSection, thatSection);
			}
		}

		[Test]
		public void When_ThisFocusRightOfThat_And_EdgePointBelowThatFocus_And_EdgePointLeftOfThatFocus_Then_ThisIsLeftOfThat() {
			for (int i = 0; i < 100; i++) {
				float thisx = SomeValue (10);
				float thisy = SomeValue (10);
				float thatx = thisx - SomePositiveValue (10);
				float thaty = thisy - SomePositiveValue (10);
				float edgex = thatx - SomePositiveValue (10);
				float edgey = thaty - SomePositiveValue (10);
				Point thisFocus = new Point (thisx, thisy);
				Point thatFocus = new Point (thatx, thaty);
				Point edge = new Point (edgex, edgey);
				BeachSection thisSection = new BeachSection (thisFocus, RandomPoint (), edge);
				BeachSection thatSection = new BeachSection (thatFocus, RandomPoint (), RandomPoint());
				AssertGreater (thisSection, thatSection);
			}
		}

		// Remaining tests concern situations where two or more of the relevant points are directly horizontal or vertical.

		[Test]
		public void BeachSectionsAtSameHeightAreComparedByHorizontalPosition() {
			for (int i = 0; i < 100; i++) {
				float y = SomeValue (10);
				float ax = SomeValue (10);
				float bx = SomeValue (10);
				while (ax == bx)
					bx = SomeValue (10);
				BeachSection beachA = new BeachSection (new Point (ax, y), RandomPoint (), RandomPoint ());
				BeachSection beachB = new BeachSection (new Point (bx, y), RandomPoint (), RandomPoint ());
				if (ax > bx) 
					AssertGreater (beachA, beachB);
				else
					AssertGreater (beachB, beachA);
			}
		}

		// Helper functions

		void AssertGreater (BeachSection a, BeachSection b) {
			Assert.Greater (a, b);
			Assert.Less (b, a);
		}

		float SomeNonNegativeValue(float max) {
			return (float)rng.NextDouble () * max;
		}

		float SomeValue(float max) {
			if (rng.NextDouble() > 0.5d) {
				return SomeNonNegativeValue (max);
			}
			return -SomeNonNegativeValue(max);
		}

		float SomePositiveValue(float max) {
			float result;
			result = SomeNonNegativeValue(max);
			while (result <= 0) {
				result = SomeNonNegativeValue(max);
			}
			return result * max;
		}

		float SomeNonZeroValue(float max) {
			if (rng.NextDouble() > 0.5d) {
				return SomePositiveValue (max);
			}
			return -SomePositiveValue(max);
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
			return new Point (SomeValue(10), SomeValue(10));
		}

		BeachSection RandomBeachSection() {
			return new BeachSection (RandomPoint (), RandomPoint (), RandomPoint ());
		}
	}
}

