using System.Collections.Concurrent;
using System.Numerics;

namespace Frank.GameEngine.Primitives;

/// <summary>
/// A factory for creating faces. A face is a triangle between three points.
/// </summary>
public static class FaceFactory
{
    /// <summary>
    /// Creates a face between three points. A face is a triangle between three points.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    public static Face Create(Vector3 a, Vector3 b, Vector3 c) => new(a, b, c);

    /// <summary>
    /// Creates faces between the points of a polygon. A face is a triangle between three points.
    /// </summary>
    /// <param name="polygon"></param>
    /// <param name="parallel"></param>
    /// <returns></returns>
    public static IEnumerable<Face> Create(Polygon polygon, bool parallel = true)
    {
        if (parallel)
        {
            var faces = new ConcurrentBag<Face>();
            Parallel.For(0, polygon.Length, i =>
            {
                var a = polygon[i];
                var b = polygon[(i + 1) % polygon.Length];
                var c = polygon.Position;
                faces.Add(new Face(a, b, c));
            });

            return faces;
        }
        else
        {
            var faces = new List<Face>();
            for (var i = 0; i < polygon.Length; i++)
            {
                var a = polygon[i];
                var b = polygon[(i + 1) % polygon.Length];
                var c = polygon.Position;
                faces.Add(new Face(a, b, c));
            }
            return faces;
        }
    }
}