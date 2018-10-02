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

        public IEnumerable<Point> Points()
        {
            return Cells().Select(c => c.Site());
        }
	}

    public class VoronoiDiagramBordered : VoronoiDiagram
    {
        Rectangle enclosingRegion;

        public VoronoiDiagramBordered(IEnumerable<Point> points, Rectangle enclosingRegion) : base(points.Where(p => Geometry.RectangleEnclosesPoint(enclosingRegion, p)))
        {
            this.enclosingRegion = enclosingRegion;
            foreach (VoronoiCellUnorganised cell in cells.Values)
                foreach (Point p in Geometry.MirrorPointsFormingRectangle(cell.Site(), this.enclosingRegion))
                    cell.AddBorder(p);
        }

        public VoronoiDiagramBordered Smoothed()
        {
            IEnumerable<Point> newPoints = cells.Values.Select(c => c.Organised().Centroid());
            return new VoronoiDiagramBordered(newPoints, this.enclosingRegion);
        }
    }
}

