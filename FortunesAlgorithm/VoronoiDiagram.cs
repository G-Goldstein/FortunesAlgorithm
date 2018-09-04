using System;
using System.Collections.Generic;
using System.Linq;
using Structures;

namespace FortunesAlgorithm
{
	public class VoronoiDiagram
	{
		Dictionary<Point, VoronoiCell> cells;

		public VoronoiDiagram(IEnumerable<Point> points)
		{
            cells = new FortunesAlgorithm().WithPoints(points);
		}

        public List<VoronoiCell> Cells()
        {
            return cells.Values.ToList();
        }
	}
}

