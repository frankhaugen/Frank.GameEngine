using System.Numerics;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Physics;

public class CollisionDetector : ICollisionHandler
{
    public void HandleCollisions(Scene scene)
    {
    }

    /// <summary>
    ///     Detects collisions between game objects in a scene.
    /// </summary>
    /// <param name="scene"></param>
    /// <returns></returns>
    public IEnumerable<Collision> DetectCollisions(Scene scene)
    {
        var polygons = scene.GameObjects.Select(gameObject => gameObject.Shape.Polygon).ToList();
        var collisions = DetectCollisions(polygons);
        var gameObjects = collisions.Select(collision => (
            scene.GameObjects.First(gameObject => gameObject.Shape.Polygon == collision.Item1),
            scene.GameObjects.First(gameObject => gameObject.Shape.Polygon == collision.Item2)));
        
        return gameObjects.Select(x => new Collision(Vector3.Zero, x.Item1, x.Item2, Vector3.Zero, Vector3.Zero));
    }

    private static IEnumerable<(Polygon, Polygon)> DetectCollisions(List<Polygon> polygons)
    {
        if (!HasVectors(polygons)) return new List<(Polygon, Polygon)>();
        var potentialCollisions = BroadPhase(polygons);
        return NarrowPhase(potentialCollisions);
    }

    private static bool HasVectors(IEnumerable<Polygon> polygons)
    {
        return polygons.Any(HasVectors);
    }

    private static bool HasVectors(Polygon polygon)
    {
        return polygon.Any();
    }

    private static List<(Polygon, Polygon)> BroadPhase(List<Polygon> polygons)
    {
        var potentialCollisions = new List<(Polygon, Polygon)>();

        // naive broad phase collision detection using bounding boxes
        for (var i = 0; i < polygons.Count; i++)
        for (var j = i + 1; j < polygons.Count; j++)
            if (DoBoundingBoxesIntersect(polygons[i], polygons[j]))
                potentialCollisions.Add((polygons[i], polygons[j]));

        return potentialCollisions;
    }

    private static List<(Polygon, Polygon)> NarrowPhase(List<(Polygon, Polygon)> potentialCollisions)
    {
        var actualCollisions = new List<(Polygon, Polygon)>();

        foreach (var (poly1, poly2) in potentialCollisions)
            if (DoPolygonsIntersect(poly1, poly2))
                actualCollisions.Add((poly1, poly2));

        return actualCollisions;
    }

    private static bool DoBoundingBoxesIntersect(Polygon poly1, Polygon poly2)
    {
        var (min1, max1) = GetBoundingBox(poly1);
        var (min2, max2) = GetBoundingBox(poly2);

        if (IsPolygon2D(poly1)) // Checking 2D bounding box intersection
            return min1.X <= max2.X && max1.X >= min2.X && min1.Y <= max2.Y && max1.Y >= min2.Y;
        // Checking 3D bounding box intersection
        return min1.X <= max2.X && max1.X >= min2.X && min1.Y <= max2.Y && max1.Y >= min2.Y && min1.Z <= max2.Z &&
               max1.Z >= min2.Z;
    }

    private static (Vector3, Vector3) GetBoundingBox(Polygon polygon)
    {
        var minX = polygon.Min(vertex => vertex.X);
        var minY = polygon.Min(vertex => vertex.Y);
        var minZ = polygon.Min(vertex => vertex.Z);

        var maxX = polygon.Max(vertex => vertex.X);
        var maxY = polygon.Max(vertex => vertex.Y);
        var maxZ = polygon.Max(vertex => vertex.Z);

        return (new Vector3(minX, minY, minZ), new Vector3(maxX, maxY, maxZ));
    }

    private static bool IsPolygon2D(Polygon polygon)
    {
        return polygon.All(vertex => vertex.Z == 0);
    }

    private static bool DoPolygonsIntersect(Polygon poly1, Polygon poly2)
    {
        return poly1.Edges.Any(edge1 => poly2.Edges.Any(edge2 => edge1.Intersect(edge2, out _)));
    }
}