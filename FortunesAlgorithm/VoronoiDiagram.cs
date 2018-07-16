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
			if (points.Count () == 0)
				throw new System.ArgumentException ("No points provided");

			cells = new Dictionary<Point, VoronoiCell> ();

			// And this is where we do Fortune's algorithm. We'll start at the top of the field and work down.
			HashSet<Point> distinctPoints = new HashSet<Point>(points);
			List<Point> highestPoints = FindHighestPoints (distinctPoints).OrderBy (p => p.Cartesianx ()).ToList();
			float mostY = highestPoints.First ().Cartesiany();
			IEnumerable<Point> otherPoints = distinctPoints.Where (p => p.Cartesiany() < mostY);
            
            List<BeachSection> initialBeachSections = new List<BeachSection>();

			// Initialise the beachline with all points having most Y coordinate.
			// There'll often be only one of these, but calculating their interactions 
			// with no background beachline is more difficult so it helps to include them all at once.

			for (int i = 0; i < highestPoints.Count; i++) {
				Point left = null;
				Point right = null;
				Point focus = highestPoints [i];
                cells[focus] = new VoronoiCell(focus);
                if (i > 0)
                {
                    left = highestPoints [i - 1];
                    cells[focus].AddBorder(left);
                }

                if (i + 1 < highestPoints.Count)
                {
                    right = highestPoints [i + 1];
                    cells[focus].AddBorder(right);
                }

                BeachSection bs = new BeachSection (focus, left, right);
                initialBeachSections.Add (bs);
			}

            BeachLine beachLine = new BeachLine(initialBeachSections, otherPoints);

			while (beachLine.HasMoreEvents()) { // This loop needs tidying up. There's lots of repetition and similar ideas within it, and they could do with factoring out.

				IEventPoint eventPoint = beachLine.PopEvent ();

				if (eventPoint.EventType () == "Site") {

                    Point site = eventPoint.Point ();

                    BeachSection containingBeachSection = beachLine.BeachSectionContainingPoint(site);

                    VoronoiCell cell = new VoronoiCell (site);
					cells[site] = cell;
                    cells[site].AddBorder(containingBeachSection.focus);
                    cells[containingBeachSection.focus].AddBorder(site);
                    
                    beachLine.SplitBeachSection(site);
                    
				} else { // EventType = "Intersect"

                    IntersectEventPoint intersectEventPoint = (IntersectEventPoint)eventPoint;

					BeachSection consumedBeachSection = intersectEventPoint.consumedBeachSection;

                    cells[consumedBeachSection.leftBoundary].AddBorder(consumedBeachSection.rightBoundary);
                    cells[consumedBeachSection.rightBoundary].AddBorder(consumedBeachSection.leftBoundary);

                    beachLine.ConsumeBeachSection(intersectEventPoint);
                    
				}
			}
		}

		HashSet<Point> FindHighestPoints(HashSet<Point> points) {
			HashSet<Point> highestPoints = new HashSet<Point> ();
			float mostY = points.First ().Cartesiany();
			foreach (Point point in points) {
				if (point.Cartesiany() == mostY)
					highestPoints.Add (point);
				else if (point.Cartesiany() > mostY) {
					highestPoints = new HashSet<Point> {point};
					mostY = point.Cartesiany();
				}
			}
			return highestPoints;
		}

		public IEnumerable<VoronoiCell> Cells() {
			return cells.Values;
		}

	}
}

