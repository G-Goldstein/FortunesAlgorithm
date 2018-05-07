using System;
using System.Collections.Generic;
using System.Linq;

namespace FortunesAlgorithm
{
	public class VoronoiDiagram
	{
		HashSet<VoronoiCell> cells;
		
		public VoronoiDiagram (HashSet<VoronoiCell> cells)
		{
			this.cells = cells;
		}

		public VoronoiDiagram(IEnumerable<Point> points)
		{
			if (points.Count () == 0)
				throw new System.ArgumentException ("No points provided");
			
			// And this is where we do Fortune's algorithm. We'll start at the bottom of the field and work upwards.
			HashSet<Point> distinctPoints = new HashSet<Point>(points);
			IEnumerable<Point> lowestPoints = FindLowestPoints (distinctPoints);
			float leastY = lowestPoints.First ().Cartesiany();
			IEnumerable<Point> otherPoints = distinctPoints.Where (p => p.Cartesiany() > leastY);



		}

		HashSet<Point> FindLowestPoints(HashSet<Point> points) {
			HashSet<Point> lowestPoints = new HashSet<Point> ();
			float leastY = lowestPoints.First ().Cartesiany();
			foreach (Point point in points) {
				if (point.Cartesiany() == leastY)
					lowestPoints.Add (point);
				else if (point.Cartesiany() < leastY) {
					lowestPoints = new HashSet<Point> {point};
					leastY = point.Cartesiany();
				}
			}
			return lowestPoints;
		}
	}
}

