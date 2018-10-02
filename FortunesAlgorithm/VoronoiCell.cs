using System;
using System.Collections.Generic;
using System.Linq;

namespace FortunesAlgorithm
{
    public abstract class VoronoiCell
    {
        protected Point site;

        public VoronoiCell(Point site)
        {
            this.site = site;
        }

        public Point Site()
        {
            return site;
        }

        internal abstract IEnumerable<Point> Borders();
    }

	internal class VoronoiCellUnorganised : VoronoiCell
    {

        protected List<Point> borderSites;

        public VoronoiCellUnorganised (Point site) : base(site)
		{
			this.borderSites = new List<Point> ();
		}

		public void AddBorder(Point border) {
			borderSites.Add (border);
		}

        public VoronoiCellOrganised Organised()
        {
            return new VoronoiCellOrganised(site, borderSites);
        }

        internal override IEnumerable<Point> Borders()
        {
            return borderSites;
        }
}

    public class VoronoiCellOrganised : VoronoiCell
    {
        ConvexPolygon borderOrdering;

        public VoronoiCellOrganised(Point site, IEnumerable<Point> borderSites) : base(site)
        {
            if (borderSites.Count() == 0)
                throw new System.ArgumentException("No borders provided");
            borderOrdering = new ConvexPolygon(borderSites);
            RemoveEclipsedBorders();
        }

        private void RemoveEclipsedBorders()
        {
            Point candidateBorder = borderOrdering.AnyVertex();
            int initialBorders = Borders().Count();
            int excludedBorders = 0;
            int streak = 0;
            while (streak + excludedBorders + 1 < initialBorders)
            {
                if (EclipsedByNeighbours(candidateBorder))
                {
                    Point nextCandidate = borderOrdering.PreviousVertex(candidateBorder);
                    borderOrdering.Remove(candidateBorder);
                    streak = Math.Max(streak - 1, 0);
                    excludedBorders++;
                } else
                {
                    Point nextCandidate = borderOrdering.NextVertex(candidateBorder);
                    streak++;
                }
            }
            
        }

        private bool EclipsedByNeighbours(Point border)
        {
            Point neighbourIntersect = Geometry.CircleCentre(border, borderOrdering.NextVertex(border), borderOrdering.PreviousVertex(border));
            return border.DistanceFrom(neighbourIntersect) >= site.DistanceFrom(neighbourIntersect);
        }

        internal override IEnumerable<Point> Borders()
        {
            return borderOrdering.AllPointsInOrder();
        }

        IEnumerable<Point> Vertices()
        {
            List<Line> borders = borderOrdering.AllPointsInOrder().Select(p => p.PerpendicularBisector(site)).ToList();
            for (int i = 0; i < borders.Count(); i++ )
            {
                if (i + 1 < borders.Count())
                    yield return borders[i].Intersect(borders[i + 1]);
                else
                    yield return borders[i].Intersect(borders[0]);
            }
        }

        ConvexPolygon Polygon()
        {
            return new ConvexPolygon(Vertices());
        }

        public Point Centroid()
        {
            return Polygon().Centroid();
        }
        
    }
}

