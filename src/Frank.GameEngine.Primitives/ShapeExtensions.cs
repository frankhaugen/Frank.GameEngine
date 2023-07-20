using System.Numerics;

namespace Frank.GameEngine.Primitives;

public static class ShapeExtensions
{
    public static Shape GetTransformedShape(this Shape shape, Transform transform)
    {
        var polygon = shape.Polygon;
        var transformedPolygon = polygon.Translate(transform.Position);
        var transformedShape = new Shape
        {
            Polygon = transformedPolygon,
            Color = shape.Color
        };
        return transformedShape;
    }
    
    public static Shape Transform(this Shape shape, Transform transform)
    {
        var transformedShape = shape.GetCopy();
        if (transform.Position != shape.Polygon.Position)
            transformedShape = transformedShape.Translate(transform.Position);
        if (transform.Rotation != Quaternion.Zero)
            transformedShape = transformedShape.Rotate(transform.Rotation);
        if (Math.Abs(transform.Scale - 1f) > 0.001f)
            transformedShape = transformedShape.Scale(transform.Scale);
        return transformedShape;
    }

    public static Shape GetCopy(this Shape shape)
    {
        return new Shape()
        {
            Polygon = shape.Polygon.GetCopy(),
            Color = shape.Color
        };
    }
    
    public static Shape Translate(this Shape shape, Vector3 position)
    {
        return new Shape()
        {
            Polygon = shape.Polygon.Translate(position),
            Color = shape.Color
        };
    }

    public static Shape Rotate(this Shape shape, Quaternion rotation)
    {
        return new Shape()
        {
            Polygon = shape.Polygon.Rotate(rotation),
            Color = shape.Color
        };
    }

    public static Shape Scale(this Shape shape, float scale)
    {
        return new Shape()
        {
            Polygon = shape.Polygon.Scale(scale),
            Color = shape.Color
        };
    }
}