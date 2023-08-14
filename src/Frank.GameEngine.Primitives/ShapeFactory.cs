using System.Drawing;

namespace Frank.GameEngine.Primitives;

/// <summary>
///     A factory for creating shapes of known types.
/// </summary>
public static class ShapeFactory
{
    /// <summary>
    ///     Creates a sphere.
    /// </summary>
    /// <param name="color"></param>
    /// <param name="radius"></param>
    /// <param name="resolution"></param>
    /// <returns></returns>
    public static Shape CreateSpere(Color color, float radius = 10f, int resolution = 16)
    {
        var shape = new Shape
        {
            Color = color,
            Polygon = PolygonFactory.CreateSphere(radius, resolution)
        };
        return shape;
    }

    /// <summary>
    ///     Creates a cube.
    /// </summary>
    /// <param name="color"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static Shape CreateCube(Color color, float size = 10f)
    {
        var shape = new Shape
        {
            Color = color,
            Polygon = PolygonFactory.CreateCube(size)
        };
        return shape;
    }

    /// <summary>
    ///     Creates a cylinder.
    /// </summary>
    /// <param name="color"></param>
    /// <param name="radius"></param>
    /// <param name="height"></param>
    /// <param name="resolution"></param>
    /// <returns></returns>
    public static Shape CreateCylinder(Color color, float radius = 10f, float height = 10f, int resolution = 16)
    {
        var shape = new Shape
        {
            Color = color,
            Polygon = PolygonFactory.CreateCylinder(radius, height, resolution)
        };
        return shape;
    }

    /// <summary>
    ///     Creates a cone.
    /// </summary>
    /// <param name="color"></param>
    /// <param name="radius"></param>
    /// <param name="height"></param>
    /// <param name="resolution"></param>
    /// <returns></returns>
    public static Shape CreateCone(Color color, float radius = 10f, float height = 10f, int resolution = 16)
    {
        var shape = new Shape
        {
            Color = color,
            Polygon = PolygonFactory.CreateCone(radius, height, resolution)
        };
        return shape;
    }

    /// <summary>
    ///     Creates a pyramid.
    /// </summary>
    /// <param name="color"></param>
    /// <param name="radius"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public static Shape CreatePyramid(Color color, float radius = 10f, float height = 10f)
    {
        var shape = new Shape
        {
            Color = color,
            Polygon = PolygonFactory.CreatePyramid(radius, height)
        };
        return shape;
    }
}