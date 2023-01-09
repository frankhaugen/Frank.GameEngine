using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core.Extensions;

public static class ShapeExtensions
{
    public static float CalculateArea(this IShape shape)
    {
        // Calculate area of shape using its vertices
        var area = 0f;
        for (int i = 0; i < shape.Polygon.Vertices.Length; i++)
        {
            var j = (i + 1) % shape.Polygon.Vertices.Length;
            area += Vector3.Cross(shape.Polygon.Vertices[i], shape.Polygon.Vertices[j]).Length() * 0.5f;
        }
        return area;
    }

    public static float CalculateVolume(this IShape shape)
    {
        // Calculate volume of shape using its vertices and surface normal
        var volume = 0f;
        var normal = Vector3.Cross(shape.Polygon.Vertices[1] - shape.Polygon.Vertices[0], shape.Polygon.Vertices[2] - shape.Polygon.Vertices[0]);
        for (int i = 0; i < shape.Polygon.Vertices.Length; i++)
        {
            var j = (i + 1) % shape.Polygon.Vertices.Length;
            volume += Vector3.Dot(normal, shape.Polygon.Vertices[i]) * (shape.Polygon.Vertices[j].X - shape.Polygon.Vertices[i].X);
        }
        return volume / 6;
    }

    public static Vector3 CalculateSurfaceNormal(this IShape shape)
    {
        // Calculate surface normal of shape using its vertices
        var normal = Vector3.Zero;
        for (int i = 0; i < shape.Polygon.Vertices.Length; i++)
        {
            var j = (i + 1) % shape.Polygon.Vertices.Length;
            normal += Vector3.Cross(shape.Polygon.Vertices[i], shape.Polygon.Vertices[j]);
        }
        return Vector3.Normalize(normal);
    }
}