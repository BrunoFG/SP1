using System;
using System.Collections.Generic;

public class ConvexHull
{
    // Helper function to compute the cross product of vectors p0p1 and p0p2
    private int CrossProduct(Point p0, Point p1, Point p2)
    {
        return (p1.X - p0.X) * (p2.Y - p0.Y) - (p1.Y - p0.Y) * (p2.X - p0.X);
    }

    // Function to find the point with the lowest y-coordinate (and leftmost in case of tie)
    private Point FindLowestPoint(List<Point> points)
    {
        Point lowest = points[0];
        foreach (var point in points)
        {
            if (point.Y < lowest.Y || (point.Y == lowest.Y && point.X < lowest.X))
            {
                lowest = point;
            }
        }
        return lowest;
    }

    // Function to sort points based on polar angle from the lowest point
    private List<Point> SortPointsByPolarAngle(List<Point> points, Point lowest)
    {
        points.Sort((p1, p2) =>
        {
            int cross = CrossProduct(lowest, p1, p2);
            if (cross == 0) // If collinear, choose the one closer to lowest point
            {
                return SquareDistance(lowest, p1).CompareTo(SquareDistance(lowest, p2));
            }
            else
            {
                return cross.CompareTo(0); // Sort by cross product sign
            }
        });

        return points;
    }

    // Helper function to compute the square of the distance between two points
    private int SquareDistance(Point p1, Point p2)
    {
        return (p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y);
    }

    // Function to compute the convex hull using Andrew's monotone chain algorithm
    public List<Point> ComputeConvexHull(int[,] matrix)
    {
        List<Point> points = new List<Point>();

        // Convert matrix into points
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] == 1) // Considering non-zero values represent points of interest
                {
                    points.Add(new Point(j, i)); // j -> x, i -> y (invert for matrix to point mapping)
                }
            }
        }

        // Step 1: Find the point with the lowest y-coordinate (and leftmost in case of tie)
        Point lowest = FindLowestPoint(points);

        // Step 2: Sort points based on polar angle from the lowest point
        points = SortPointsByPolarAngle(points, lowest);

        // Step 3: Build the convex hull using Andrew's monotone chain algorithm
        List<Point> hull = new List<Point>();

        // Lower hull
        foreach (var point in points)
        {
            while (hull.Count >= 2 && CrossProduct(hull[hull.Count - 2], hull[hull.Count - 1], point) <= 0)
                hull.RemoveAt(hull.Count - 1);
            hull.Add(point);
        }

        // Upper hull
        int t = hull.Count + 1;
        for (int i = points.Count - 1; i >= 0; i--)
        {
            Point point = points[i];
            while (hull.Count >= t && CrossProduct(hull[hull.Count - 2], hull[hull.Count - 1], point) <= 0)
                hull.RemoveAt(hull.Count - 1);
            hull.Add(point);
        }

        // Remove last point because it's the same as the first point in the lower hull
        hull.RemoveAt(hull.Count - 1);

        return hull;
    }

}