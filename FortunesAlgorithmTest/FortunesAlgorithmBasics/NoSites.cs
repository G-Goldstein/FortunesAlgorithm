﻿using NUnit.Framework;
using System;
using FortunesAlgorithm;
using System.Collections.Generic;
using System.Linq;

namespace FortunesAlgorithmBasics
{
	[TestFixture]
	public class NoSites
	{
		VoronoiDiagram voronoi;

		public void CreateVoronoiDiagramFromNoPoints() {
			voronoi = new VoronoiDiagram(new List<Point>());
		}

		[Test]
		public void ZeroPointVoronoiDiagramIsInvalid ()
		{
			Assert.Throws<ArgumentException> (CreateVoronoiDiagramFromNoPoints);
		}
	}
}

