using System;
using System.Collections.Generic;
using System.Linq;

namespace FortunesAlgorithm
{
	public class VoronoiCell
	{
		Point site;
		List<Point> borderSites;
		public VoronoiCell (Point site)
		{
			this.site = site;
			this.borderSites = new List<Point> ();
		}

		public void AddBorder(Point border) {
			borderSites.Add (border);
		}
	}
}

