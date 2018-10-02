using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FortunesAlgorithm
{
    public class ConvexPolygon
    {
        Point centroid;
        Dictionary<Point, Point> next;
        Dictionary<Point, Point> previous;

        public ConvexPolygon(IEnumerable<Point> points)
        {
            centroid = Centroid(points);
            List<Point> orderedPoints = OrderPoints(centroid, points);
            int count = orderedPoints.Count;
            next = new Dictionary<Point, Point>();
            previous = new Dictionary<Point, Point>();
            for (int i = 0; i < count; i++ )
            {
                if (i + 1 < count)
                    next[orderedPoints[i]] = orderedPoints[i + 1];
                else
                    next[orderedPoints[i]] = orderedPoints[0];
                if (i > 0)
                    previous[orderedPoints[i]] = orderedPoints[i - 1];
                else
                    previous[orderedPoints[i]] = orderedPoints[count - 1];
            }
        }

        public Point NextVertex(Point point)
        {
            return next[point];
        }

        public Point PreviousVertex(Point point)
        {
            return previous[point];
        }

        public Point AnyVertex()
        {
            return next.First().Value;
        }

        public IEnumerable<Point> AllPointsInOrder()
        {
            return AllPointsInOrder(AnyVertex());
        }

        public IEnumerable<Point> AllPointsInOrder(Point startPoint)
        {
            Point currentPoint = startPoint;
            yield return currentPoint;
            currentPoint = NextVertex(currentPoint);
            while (!currentPoint.Equals(startPoint))
            {
                yield return currentPoint;
                currentPoint = NextVertex(currentPoint);
            }
        }

        public void Remove(Point point)
        {
            Point successor = NextVertex(point);
            Point predecessor = PreviousVertex(point);
            next[predecessor] = successor;
            previous[successor] = predecessor;
        }

        public static Point Centroid(IEnumerable<Point> points)
        {
            float xSum = points.Aggregate(0f, (total, next) => total + next.Cartesianx());
            float ySum = points.Aggregate(0f, (total, next) => total + next.Cartesiany());
            float xMean = xSum / points.Count();
            float yMean = ySum / points.Count();
            return new Point(xMean, yMean);
        }

        public static List<Point> OrderPoints(Point centroid, IEnumerable<Point> points)
        {
            return points.OrderBy((point) => AngleFromOrigin(centroid, point)).ToList();
        }

        public static float AngleFromOrigin(Point origin, Point point)
        {
            return (float)Math.Atan2(point.Cartesiany() - origin.Cartesiany(), point.Cartesianx() - origin.Cartesianx());
        }
    }
}
