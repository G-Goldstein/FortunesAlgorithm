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

			RBTree<BeachSection> beachLine = new RBTree<BeachSection> ();
			EventQueue eventQueue = new EventQueue();

			// Initialise the beachline with all points having most Y coordinate.
			// There'll often be only one of these, but calculating their interactions 
			// with no background beachline is more difficult so it helps to include them all at once.

			for (int i = 0; i < highestPoints.Count; i++) {
				Point left = null;
				Point right = null;
				Point focus = highestPoints [i];
				if (i > 0)
					left = highestPoints [i - 1];
				if (i + 1 < highestPoints.Count)
					right = highestPoints [i + 1];
				BeachSection bs = new BeachSection (focus, left, right);
				beachLine.Add (bs);
				cells [focus] = new VoronoiCell(focus);
			}

			foreach (Point point in otherPoints)
				eventQueue.Add (new SiteEventPoint (point));

			while (!eventQueue.IsEmpty()) { // This loop needs tidying up. There's lots of repetition and similar ideas within it, and they could do with factoring out. It may also create degenerate circle events at the moment.
				IEventPoint eventPoint = eventQueue.Pop ();
				float sweepLineY = eventPoint.Point ().Cartesiany ();
				if (eventPoint.EventType () == "Site") {
					Point site = eventPoint.Point ();
					VoronoiCell cell = new VoronoiCell (site);
					cells [site] = cell;

					BeachSection containingBeachSection = BeachSectionContainingPoint (beachLine, site);
					BeachSection newBeachSectionLeft = new BeachSection (containingBeachSection.focus, containingBeachSection.leftBoundary, site);
					BeachSection newBeachSectionCentre = new BeachSection (site, containingBeachSection.focus, containingBeachSection.focus);
					BeachSection newBeachSectionRight = new BeachSection (containingBeachSection.focus, site, containingBeachSection.rightBoundary);
					beachLine.Remove (containingBeachSection);
					beachLine.Add (newBeachSectionLeft);
					beachLine.Add (newBeachSectionCentre);
					beachLine.Add (newBeachSectionRight);

					IntersectEventPoint leftIntersect = new IntersectEventPoint (newBeachSectionLeft);
					IntersectEventPoint rightIntersect = new IntersectEventPoint (newBeachSectionRight);
					eventQueue.Remove (new IntersectEventPoint(containingBeachSection), sweepLineY);
					eventQueue.Add(leftIntersect, sweepLineY);
					if (!leftIntersect.Equals(rightIntersect))
						eventQueue.Add(rightIntersect, sweepLineY);
					
					cells [site].AddBorder (containingBeachSection.focus);
					cells [containingBeachSection.focus].AddBorder (site);

				} else { // EventType = "Intersect"
					IntersectEventPoint intersectEventPoint = (IntersectEventPoint)eventPoint;

					BeachSection consumedBeachSection = intersectEventPoint.consumedBeachSection;
					BeachSection leftBeachSection = beachLine.Predecessor (consumedBeachSection);
					BeachSection rightBeachSection = beachLine.Successor (consumedBeachSection);
					BeachSection newLeftBeachSection = new BeachSection (leftBeachSection.focus, leftBeachSection.leftBoundary, rightBeachSection.focus);
					BeachSection newRightBeachSection = new BeachSection (rightBeachSection.focus, leftBeachSection.focus, rightBeachSection.rightBoundary);
					beachLine.Remove (consumedBeachSection);
					beachLine.Remove (leftBeachSection);
					beachLine.Remove (rightBeachSection);
					beachLine.Add (newLeftBeachSection);
					beachLine.Add (newRightBeachSection);

					eventQueue.Remove (new IntersectEventPoint (leftBeachSection), sweepLineY);
					if (!leftBeachSection.Equals(rightBeachSection))
						eventQueue.Remove (new IntersectEventPoint (rightBeachSection), sweepLineY);
					eventQueue.Add (new IntersectEventPoint (newLeftBeachSection), sweepLineY);
					if (!newLeftBeachSection.Equals(newRightBeachSection))
						eventQueue.Add (new IntersectEventPoint (newRightBeachSection), sweepLineY);

					cells [newLeftBeachSection.focus].AddBorder (newRightBeachSection.focus);
					cells [newRightBeachSection.focus].AddBorder (newLeftBeachSection.focus);
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

		BeachSection BeachSectionContainingPoint(RBTree<BeachSection> beachLine, Point point) {
			RBBranch<BeachSection> candidate = (RBBranch<BeachSection>)beachLine.root;
			while (true) {
				int compareResult = candidate.value.CompareTo (point);
				if (compareResult == 0)
					return candidate.value;
				if (compareResult > 0)
					candidate = (RBBranch<BeachSection>)candidate.left;
				if (compareResult < 0)
					candidate = (RBBranch<BeachSection>)candidate.right;
			}
		}

		public IEnumerable<VoronoiCell> Cells() {
			return cells.Values;
		}

	}
}

