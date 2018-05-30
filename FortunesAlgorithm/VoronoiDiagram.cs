using System;
using System.Collections.Generic;
using System.Linq;
using Structures;

namespace FortunesAlgorithm
{
	public class VoronoiDiagram
	{
		HashSet<VoronoiCell> cells;

		public VoronoiDiagram(IEnumerable<Point> points)
		{
			if (points.Count () == 0)
				throw new System.ArgumentException ("No points provided");
			
			// And this is where we do Fortune's algorithm. We'll start at the top of the field and work down.
			HashSet<Point> distinctPoints = new HashSet<Point>(points);
			List<Point> highestPoints = FindHighestPoints (distinctPoints).OrderBy (p => p.Cartesianx ()).ToList();
			float mostY = highestPoints.First ().Cartesiany();
			IEnumerable<Point> otherPoints = distinctPoints.Where (p => p.Cartesiany() < mostY);

			RBTree<BeachSection> beachLine = new RBTree<BeachSection> ();
			Heap<IEventPoint> eventQueue = new Heap<IEventPoint> ((a, b) => a.Point().Cartesiany() > b.Point().Cartesiany());

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
			}

			foreach (Point point in otherPoints)
				eventQueue.Add (new SiteEventPoint (point));

			while (!eventQueue.IsEmpty()) {
				IEventPoint eventPoint = eventQueue.Pop ();

				if (eventPoint.EventType () == "Site") {
					Point site = eventPoint.Point ();
					BeachSection containingBeachSection = BeachSectionContainingPoint (beachLine, site);
				}
			}
		}

		HashSet<Point> FindHighestPoints(HashSet<Point> points) {
			HashSet<Point> highestPoints = new HashSet<Point> ();
			float mostY = highestPoints.First ().Cartesiany();
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

	}
}

