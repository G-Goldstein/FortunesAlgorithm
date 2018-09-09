using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FortunesAlgorithm
{
    public class Rectangle
    {
        public Point topLeft;
        public Point bottomRight;

        public Rectangle(Point topLeft, Point bottomRight)
        {
            this.topLeft = topLeft;
            this.bottomRight = bottomRight;
        }

        public float Right() { return this.bottomRight.Cartesianx(); }
        public float Top() { return this.topLeft.Cartesiany(); }
        public float Left() { return this.topLeft.Cartesianx(); }
        public float Bottom() { return this.bottomRight.Cartesiany(); }
    }
}
