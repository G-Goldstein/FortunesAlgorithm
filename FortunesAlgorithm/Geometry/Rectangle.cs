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

        public float Right() { return this.bottomRight.x; }
        public float Top() { return this.topLeft.y; }
        public float Left() { return this.topLeft.x; }
        public float Bottom() { return this.bottomRight.y; }
    }
}
