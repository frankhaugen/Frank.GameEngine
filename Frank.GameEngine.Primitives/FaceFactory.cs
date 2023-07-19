using System.Collections.Concurrent;
using System.Numerics;

namespace Frank.GameEngine.Primitives;

public static class FaceFactory
{
    public static Face Create(Vector3 a, Vector3 b, Vector3 c) => new(a, b, c);

    public static IEnumerable<Face> CreateNormal(Polygon polygon, bool parallel = true)
    {
        return parallel
            ? CreateParallel(polygon)
            : CreateNormal(polygon);
    }

    public static IEnumerable<Face> CreateNormal(Polygon polygon)
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

    public static IEnumerable<Face> CreateParallel(Polygon polygon)
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
}