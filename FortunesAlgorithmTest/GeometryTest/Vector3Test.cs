using NUnit.Framework;
using System;
using FortunesAlgorithm;
using System.Collections.Generic;

namespace GeometryTest
{
	[TestFixture]
	public class Vector3Tests
	{
		[Test]
		public void Colinear ()
		{
			List<Vector3> vectors = new List<Vector3> {
				new Vector3 (3, 2, 1),
				new Vector3 (9, 6, 3),
				new Vector3 (-3, -2, -1),
				new Vector3 (4.5f, 3, 1.5f)
			};
			foreach (Vector3 a in vectors) {
				foreach (Vector3 b in vectors) {
					Assert.True (a.Colinear (b));
				}
			}
		}

		[Test]
		public void NotColinear() {
			List<Vector3> vectors = new List<Vector3> {
				new Vector3 (3, 2, 1),
				new Vector3 (3, 1, 2),
				new Vector3 (2, 3, 1),
				new Vector3 (2, 1, 3),
				new Vector3 (1, 3, 2),
				new Vector3 (1, 2, 3)
			};
			foreach (Vector3 a in vectors) {
				foreach (Vector3 b in vectors) {
					if (a != b)
						Assert.False (a.Colinear (b), string.Format ("{0} should not be colinear with {1}", a, b));
				}
			}
		}
	}
}

