using System.Numerics;

namespace Frank.GameEngine.Primitives;

public static class ShapeExtensions
{
    /// <summary>
    /// Gets the transformed shape.
    /// </summary>
    /// <param name="shape"></param>
    /// <param name="transform"></param>
    /// <returns></returns>
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
    
    /// <summary>
    /// Gets the collision between two shapes.
    /// </summary>
    /// <param name="shape"></param>
    /// <param name="otherShape"></param>
    /// <returns></returns>
    public static bool Intersect(this Shape shape, Shape otherShape) => shape.Polygon.Intersect(otherShape.Polygon);
    
    /// <summary>
    /// Gets the intersection points between two shapes.
    /// </summary>
    /// <param name="shape"></param>
    /// <param name="otherShape"></param>
    /// <returns></returns>
    public static IEnumerable<Vector3> GetIntersectionPoints(this Shape shape, Shape otherShape) => shape.Polygon.GetIntersectionPoints(otherShape.Polygon);

    /// <summary>
    /// Gets the transformed shape.
    /// </summary>
    /// <param name="shape"></param>
    /// <param name="transform"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Gets a copy of the shape with the same polygon and color.
    /// </summary>
    /// <param name="shape"></param>
    /// <returns></returns>
    public static Shape GetCopy(this Shape shape)
    {
        return new Shape()
        {
            Polygon = shape.Polygon.GetCopy(),
            Color = shape.Color
        };
    }
    
    /// <summary>
    /// Moves the shape the specified amount in the specified direction.
    /// </summary>
    /// <param name="shape"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public static Shape Translate(this Shape shape, Vector3 position)
    {
        return new Shape()
        {
            Polygon = shape.Polygon.Translate(position),
            Color = shape.Color
        };
    }

    /// <summary>
    /// Rotates the shape the specified amount.
    /// </summary>
    /// <param name="shape"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public static Shape Rotate(this Shape shape, Quaternion rotation)
    {
        return new Shape()
        {
            Polygon = shape.Polygon.Rotate(rotation),
            Color = shape.Color
        };
    }

    /// <summary>
    /// Scales the shape the specified amount. 
    /// </summary>
    /// <param name="shape"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static Shape Scale(this Shape shape, float scale)
    {
        return new Shape()
        {
            Polygon = shape.Polygon.Scale(scale),
            Color = shape.Color
        };
    }
}