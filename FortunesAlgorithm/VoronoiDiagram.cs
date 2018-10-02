using System;
using System.Collections.Generic;
using System.Linq;
using Structures;

namespace FortunesAlgorithm
{
	public class VoronoiDiagram
	{
		protected Dictionary<Point, VoronoiCellUnorganised> cells;

		public VoronoiDiagram(IEnumerable<Point> points)
		{
            cells = new FortunesAlgorithm().WithPoints(points);
		}
        
        public List<VoronoiCellUnorganised> Cells()
        {
            return cells.Values.ToList();
        }
	}

    public class VoronoiDiagramBordered : VoronoiDiagram
    {
        public VoronoiDiagramBordered(IEnumerable<Point> points, Rectangle enclosingRegion) : base(points.Where(p => Geometry.RectangleEnclosesPoint(enclosingRegion, p)))
        {
            foreach (VoronoiCellUnorganised cell in cells.Values)
                foreach (Point p in Geometry.MirrorPointsFormingRectangle(cell.Site(), enclosingRegion))
                    cell.AddBorder(p);
        }
    }
}

