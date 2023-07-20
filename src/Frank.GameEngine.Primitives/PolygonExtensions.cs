using System.Numerics;

namespace Frank.GameEngine.Primitives;

public static class PolygonExtensions
{
    /// <summary>
    /// Moves the polygon the specified amount in the specified direction.
    /// </summary>
    /// <param name="polygon"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public static Polygon Translate(this Polygon polygon, Vector3 position)
    {
        var vertices = new Vector3[polygon.Length];
        for (var i = 0; i < polygon.Length; i++)
        {
            vertices[i] = (polygon[i] += position);
        }

        return new Polygon(vertices);
    }

    /// <summary>
    /// Rotates the polygon the specified amount.
    /// </summary>
    /// <param name="polygon"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public static Polygon Rotate(this Polygon polygon, Quaternion rotation)
    {
        var vertices = new Vector3[polygon.Length];
        for (var i = 0; i < polygon.Length; i++)
        {
            vertices[i] = Vector3.Transform(polygon[i], rotation);
        }
        return new Polygon(vertices);
    }

    /// <summary>
    /// Scales the polygon the specified amount.
    /// </summary>
    /// <param name="polygon"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static Polygon Scale(this Polygon polygon, float scale)
    {
        var vertices = new Vector3[polygon.Length];
        for (var i = 0; i < polygon.Length; i++)
        {
            vertices[i] = polygon[i] * scale;
        }
        return new Polygon(vertices);
    }
    
    /// <summary>
    /// Gets a copy of the polygon with the same vertices.
    /// </summary>
    /// <param name="polygon"></param>
    /// <returns></returns>
    public static Polygon GetCopy(this Polygon polygon)
    {
        var vertices = new Vector3[polygon.Length];
        for (var i = 0; i < polygon.Length; i++)
        {
            vertices[i] = polygon[i];
        }
        return new Polygon(vertices);
    }
}