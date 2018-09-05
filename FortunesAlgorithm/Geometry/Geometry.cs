using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FortunesAlgorithm
{
    public static class Geometry
    {
        public static VoronoiCell PointInRectangleAsVoronoiCell(Point point, Rectangle rectangle)
        {
            if (!RectangleEnclosesPoint(rectangle, point))
                throw new ArgumentOutOfRangeException("Rectangle doesn't contain point");
            VoronoiCell cell = new VoronoiCell(point);
            return cell;
        }
        
        public static bool RectangleEnclosesPoint(Rectangle rectangle, Point point)
        {
            return point.Cartesianx() > rectangle.topLeft.Cartesianx()
                 && point.Cartesianx() < rectangle.bottomRight.Cartesianx()
                 && point.Cartesiany() < rectangle.topLeft.Cartesiany()
                 && point.Cartesiany() > rectangle.bottomRight.Cartesiany();
        }
    }
}
