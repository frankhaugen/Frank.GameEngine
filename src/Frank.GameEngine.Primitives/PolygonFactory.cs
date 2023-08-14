using System.Numerics;

namespace Frank.GameEngine.Primitives;

public static partial class PolygonFactory
{
    /// <summary>
    ///     Creates a polygon that is a cube with the specified size.
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    public static Polygon CreateCube(float size)
    {
        return CreateCube(size, size, size);
    }

    /// <summary>
    ///     Creates a polygon that is a cube with the specified width, height, and depth.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="depth"></param>
    /// <returns></returns>
    public static Polygon CreateCube(float width, float height, float depth)
    {
        var polygon = new Vector3[8];
        polygon[0] = new Vector3(0, 0, 0);
        polygon[1] = new Vector3(width, 0, 0);
        polygon[2] = new Vector3(width, height, 0);
        polygon[3] = new Vector3(0, height, 0);
        polygon[4] = new Vector3(0, 0, depth);
        polygon[5] = new Vector3(width, 0, depth);
        polygon[6] = new Vector3(width, height, depth);
        polygon[7] = new Vector3(0, height, depth);
        return new Polygon(polygon);
    }

    /// <summary>
    ///     Creates a polygon that is a sphere with the specified radius and number of segments.
    /// </summary>
    /// <param name="radius"></param>
    /// <param name="segments"></param>
    /// <returns></returns>
    public static Polygon CreateSphere(float radius, int segments)
    {
        var polygon = new Vector3[segments * segments];
        var step = (float)(Math.PI * 2 / segments);
        for (var i = 0; i < segments; i++)
        {
            var angle = step * i;
            for (var j = 0; j < segments; j++)
            {
                var angle2 = step * j;
                polygon[i * segments + j] = new Vector3((float)Math.Cos(angle) * (float)Math.Cos(angle2) * radius,
                    (float)Math.Sin(angle) * (float)Math.Cos(angle2) * radius, (float)Math.Sin(angle2) * radius);
            }
        }

        return new Polygon(polygon);
    }

    /// <summary>
    ///     Creates a polygon that is a cylinder with the specified radius, height, and number of segments.
    /// </summary>
    /// <param name="radius"></param>
    /// <param name="height"></param>
    /// <param name="segments"></param>
    /// <returns></returns>
    public static Polygon CreateCylinder(float radius, float height, int segments)
    {
        var polygon = new Vector3[segments * 2];
        var step = (float)(Math.PI * 2 / segments);
        for (var i = 0; i < segments; i++)
        {
            var angle = step * i;
            polygon[i] = new Vector3((float)Math.Cos(angle) * radius, (float)Math.Sin(angle) * radius, 0);
            polygon[i + segments] =
                new Vector3((float)Math.Cos(angle) * radius, (float)Math.Sin(angle) * radius, height);
        }

        return new Polygon(polygon);
    }

    /// <summary>
    ///     Creates a polygon that is a cone with the specified radius, height, and number of segments.
    /// </summary>
    /// <param name="radius"></param>
    /// <param name="height"></param>
    /// <param name="segments"></param>
    /// <returns></returns>
    public static Polygon CreateCone(float radius, float height, int segments)
    {
        var polygon = new Vector3[segments + 1];
        var step = (float)(Math.PI * 2 / segments);
        for (var i = 0; i < segments; i++)
        {
            var angle = step * i;
            polygon[i] = new Vector3((float)Math.Cos(angle) * radius, (float)Math.Sin(angle) * radius, 0);
        }

        polygon[segments] = new Vector3(0, 0, height);
        return new Polygon(polygon);
    }

    /// <summary>
    ///     Creates a polygon that is a pyramid with the specified width and height.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public static Polygon CreatePyramid(float width, float height)
    {
        var polygon = new Vector3[5];
        polygon[0] = new Vector3(0, 0, 0);
        polygon[1] = new Vector3(width, 0, 0);
        polygon[2] = new Vector3(width, height, 0);
        polygon[3] = new Vector3(0, height, 0);
        polygon[4] = new Vector3(width / 2, height / 2, height);
        return new Polygon(polygon);
    }
}