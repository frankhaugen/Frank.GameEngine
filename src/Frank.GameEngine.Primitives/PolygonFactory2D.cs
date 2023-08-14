using System.Numerics;

namespace Frank.GameEngine.Primitives;

/// <summary>
///     Factory for creating polygons of various known shapes.
/// </summary>
public static partial class PolygonFactory
{
    /// <summary>
    ///     Creates a polygon of the specified number of sides.
    /// </summary>
    /// <param name="sides">The number of sides the polygon should have.</param>
    /// <param name="radius">The radius of the polygon.</param>
    /// <param name="center">The center of the polygon.</param>
    /// <returns></returns>
    public static Polygon CreateCircle(int sides, float radius, Vector3 center)
    {
        var points = new List<Vector3>();
        var angle = 360f / sides;
        for (var i = 0; i < sides; i++)
        {
            var x = radius * MathF.Cos(DegreesToRadians(angle * i));
            var y = radius * MathF.Sin(DegreesToRadians(angle * i));
            points.Add(new Vector3(x, y, 0) + center);
        }

        return new Polygon(points);
    }

    public static Polygon CreateRectangle(float width, float height, Vector3 center)
    {
        var points = new List<Vector3>();
        points.Add(new Vector3(-width / 2, -height / 2, 0) + center);
        points.Add(new Vector3(width / 2, -height / 2, 0) + center);
        points.Add(new Vector3(width / 2, height / 2, 0) + center);
        points.Add(new Vector3(-width / 2, height / 2, 0) + center);
        return new Polygon(points);
    }

    /// <summary>
    ///     Creates a polygon that is a rectangle with the specified width and height.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public static Polygon CreateRectangle(float width, float height)
    {
        var polygon = new Vector3[4];
        polygon[0] = new Vector3(0, 0, 0);
        polygon[1] = new Vector3(width, 0, 0);
        polygon[2] = new Vector3(width, height, 0);
        polygon[3] = new Vector3(0, height, 0);
        return new Polygon(polygon);
    }

    /// <summary>
    ///     Creates a polygon that is a circle with the specified radius and number of sides.
    /// </summary>
    /// <param name="radius"></param>
    /// <param name="sides"></param>
    /// <returns></returns>
    public static Polygon CreateCircle(float radius, int sides)
    {
        var polygon = new Vector3[sides];
        var step = (float)(Math.PI * 2 / sides);
        for (var i = 0; i < sides; i++)
        {
            var angle = step * i;
            polygon[i] = new Vector3((float)Math.Cos(angle) * radius, (float)Math.Sin(angle) * radius, 0);
        }

        return new Polygon(polygon);
    }

    /// <summary>
    ///     Creates a polygon that is a triangle with the specified width and height.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public static Polygon CreateTriangle(float width, float height)
    {
        var polygon = new Vector3[3];
        polygon[0] = new Vector3(0, 0, 0);
        polygon[1] = new Vector3(width, 0, 0);
        polygon[2] = new Vector3(width / 2, height, 0);
        return new Polygon(polygon);
    }

    /// <summary>
    ///     Creates a polygon that is a hexagon with the specified radius.
    /// </summary>
    /// <param name="radius"></param>
    /// <returns></returns>
    public static Polygon CreateHexagon(float radius)
    {
        var polygon = new Vector3[6];
        const float step = (float)(Math.PI * 2 / 6);
        for (var i = 0; i < 6; i++)
        {
            var angle = step * i;
            polygon[i] = new Vector3((float)Math.Cos(angle) * radius, (float)Math.Sin(angle) * radius, 0);
        }

        return new Polygon(polygon);
    }

    private static float DegreesToRadians(this float degrees)
    {
        return degrees * MathF.PI / 180f;
    }
}