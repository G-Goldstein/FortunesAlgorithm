using System;
using System.Collections.Generic;
using System.Linq;

namespace FortunesAlgorithm
{
	public class VoronoiCell
	{
		Point site;
		List<Point> vertices;
		public VoronoiCell (Point site, IEnumerable<Point> vertices)
		{
			this.site = site;
			this.vertices = vertices.ToList ();
		}
	}
}

