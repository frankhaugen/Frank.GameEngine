using System.Collections.Concurrent;
using System.Numerics;

namespace Frank.GameEngine.Primitives;

public static class PolygonExtensions
{
    /// <summary>
    ///     Moves the polygon the specified amount in the specified direction.
    /// </summary>
    /// <param name="polygon"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public static Polygon Translate(this Polygon polygon, Vector3 position)
    {
        var vertices = new Vector3[polygon.Length];
        for (var i = 0; i < polygon.Length; i++) vertices[i] = polygon[i] += position;

        return new Polygon(vertices);
    }

    /// <summary>
    ///     Determines if the polygon intersects with another polygon.
    /// </summary>
    /// <param name="polygon"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static bool Intersect(this Polygon polygon, Polygon other)
    {
        var edges = polygon.Edges;
        var otherEdges = other.Edges;
        var isColliding = false;


        Parallel.ForEach(edges, edge =>
        {
            if (edge.Intersect(otherEdges)) isColliding = true;
        });

        return isColliding;
    }

    /// <summary>
    ///     Determines if the polygon intersects with another polygon.
    /// </summary>
    /// <param name="polygon"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static IEnumerable<Vector3> GetIntersectionPoints(this Polygon polygon, Polygon other)
    {
        var edges = polygon.Edges;
        var otherEdges = other.Edges;
        var intersectionPoints = new ConcurrentBag<Vector3>();

        Parallel.ForEach(edges, edge =>
        {
            var edgeIntersectionPoints = edge.GetIntersectionPoints(otherEdges);
            foreach (var edgeIntersectionPoint in edgeIntersectionPoints)
            {
                if (intersectionPoints.Contains(edgeIntersectionPoint)) continue;
                intersectionPoints.Add(edgeIntersectionPoint);
            }
        });

        return intersectionPoints;
    }

    /// <summary>
    ///     Rotates the polygon the specified amount.
    /// </summary>
    /// <param name="polygon"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public static Polygon Rotate(this Polygon polygon, Quaternion rotation)
    {
        var vertices = new Vector3[polygon.Length];
        for (var i = 0; i < polygon.Length; i++) vertices[i] = Vector3.Transform(polygon[i], rotation);
        return new Polygon(vertices);
    }

    /// <summary>
    ///     Scales the polygon the specified amount.
    /// </summary>
    /// <param name="polygon"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static Polygon Scale(this Polygon polygon, float scale)
    {
        var vertices = new Vector3[polygon.Length];
        for (var i = 0; i < polygon.Length; i++) vertices[i] = polygon[i] * scale;
        return new Polygon(vertices);
    }

    /// <summary>
    ///     Gets a copy of the polygon with the same vertices.
    /// </summary>
    /// <param name="polygon"></param>
    /// <returns></returns>
    public static Polygon GetCopy(this Polygon polygon)
    {
        var vertices = new Vector3[polygon.Length];
        for (var i = 0; i < polygon.Length; i++) vertices[i] = polygon[i];
        return new Polygon(vertices);
    }

    /// <summary>
    ///     Gets the center of the polygon.
    /// </summary>
    /// <param name="polygon"></param>
    /// <returns></returns>
    public static Vector3 GetCenter(this Polygon polygon)
    {
        var x = polygon.Average(v => v.X);
        var y = polygon.Average(v => v.Y);
        var z = polygon.Average(v => v.Z);
        return new Vector3(x, y, z);
    }
}