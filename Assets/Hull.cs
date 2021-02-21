using System;
using System.Collections.Generic;
using System.Linq;

public sealed class Hull
{

	// Returns a new list of points representing the convex hull of
	// the given set of points. The convex hull excludes collinear points.
	// This algorithm runs in O(n log n) time.
	public static IList<Point> MakeHull(IList<Point> points)
	{
		List<Point> newPoints = new List<Point>(points);
		newPoints.Sort();
		return MakeHullPresorted(newPoints);
	}


	// Returns the convex hull, assuming that each points[i] <= points[i + 1]. Runs in O(n) time.
	public static IList<Point> MakeHullPresorted(IList<Point> points)
	{
		if (points.Count <= 1)
			return new List<Point>(points);

		// Andrew's monotone chain algorithm. Positive y coordinates correspond to "up"
		// as per the mathematical convention, instead of "down" as per the computer
		// graphics convention. This doesn't affect the correctness of the result.

		List<Point> upperHull = new List<Point>();
		foreach (Point p in points)
		{
			while (upperHull.Count >= 2)
			{
				Point q = upperHull[upperHull.Count - 1];
				Point r = upperHull[upperHull.Count - 2];
				if ((q.x - r.x) * (p.y - r.y) >= (q.y - r.y) * (p.x - r.x))
					upperHull.RemoveAt(upperHull.Count - 1);
				else
					break;
			}
			upperHull.Add(p);
		}
		upperHull.RemoveAt(upperHull.Count - 1);

		IList<Point> lowerHull = new List<Point>();
		for (int i = points.Count - 1; i >= 0; i--)
		{
			Point p = points[i];
			while (lowerHull.Count >= 2)
			{
				Point q = lowerHull[lowerHull.Count - 1];
				Point r = lowerHull[lowerHull.Count - 2];
				if ((q.x - r.x) * (p.y - r.y) >= (q.y - r.y) * (p.x - r.x))
					lowerHull.RemoveAt(lowerHull.Count - 1);
				else
					break;
			}
			lowerHull.Add(p);
		}
		lowerHull.RemoveAt(lowerHull.Count - 1);

		if (!(upperHull.Count == 1 && Enumerable.SequenceEqual(upperHull, lowerHull)))
			upperHull.AddRange(lowerHull);
		return upperHull;
	}

}



public struct Point : IComparable<Point>
{

	public double x;
	public double y;


	public Point(double x, double y)
	{
		this.x = x;
		this.y = y;
	}


	public int CompareTo(Point other)
	{
		if (x < other.x)
			return -1;
		else if (x > other.x)
			return +1;
		else if (y < other.y)
			return -1;
		else if (y > other.y)
			return +1;
		else
			return 0;
	}

}