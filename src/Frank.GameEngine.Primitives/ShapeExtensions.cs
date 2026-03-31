using System.Numerics;

namespace Frank.GameEngine.Primitives;

public static class ShapeExtensions
{
    /// <summary>
    ///     Gets the transformed shape.
    /// </summary>
    /// <param name="shape"></param>
    /// <param name="transform"></param>
    /// <returns></returns>
    public static Shape GetTransformedShape(this Shape shape, Transform transform) => shape.Transform(transform);

    /// <summary>
    ///     Gets the collision between two shapes.
    /// </summary>
    /// <param name="shape"></param>
    /// <param name="otherShape"></param>
    /// <returns></returns>
    public static bool Intersect(this Shape shape, Shape otherShape)
    {
        return shape.Polygon.Intersect(otherShape.Polygon);
    }

    /// <summary>
    ///     Gets the intersection points between two shapes.
    /// </summary>
    /// <param name="shape"></param>
    /// <param name="otherShape"></param>
    /// <returns></returns>
    public static IEnumerable<Vector3> GetIntersectionPoints(this Shape shape, Shape otherShape)
    {
        return shape.Polygon.GetIntersectionPoints(otherShape.Polygon);
    }

    /// <summary>
    ///     Gets the transformed shape.
    /// </summary>
    /// <param name="shape"></param>
    /// <param name="transform"></param>
    /// <returns></returns>
    /// <summary>
    /// Applies scale (around the origin), rotation, then translation in world space — typical for model vertices defined in local space.
    /// </summary>
    public static Shape Transform(this Shape shape, Transform transform)
    {
        var result = shape.GetCopy();
        if (Math.Abs(transform.Scale - 1f) > 0.001f)
            result = result.Scale(transform.Scale);
        if (transform.Rotation != Quaternion.Identity)
            result = result.Rotate(transform.Rotation);
        result = result.Translate(transform.Position);
        return result;
    }

    /// <summary>
    ///     Gets a copy of the shape with the same polygon and color.
    /// </summary>
    /// <param name="shape"></param>
    /// <returns></returns>
    public static Shape GetCopy(this Shape shape)
    {
        return new Shape
        {
            Polygon = shape.Polygon.GetCopy(),
            Color = shape.Color
        };
    }

    /// <summary>
    ///     Moves the shape the specified amount in the specified direction.
    /// </summary>
    /// <param name="shape"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public static Shape Translate(this Shape shape, Vector3 position)
    {
        return new Shape
        {
            Polygon = shape.Polygon.Translate(position),
            Color = shape.Color
        };
    }

    /// <summary>
    ///     Rotates the shape the specified amount.
    /// </summary>
    /// <param name="shape"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public static Shape Rotate(this Shape shape, Quaternion rotation)
    {
        return new Shape
        {
            Polygon = shape.Polygon.Rotate(rotation),
            Color = shape.Color
        };
    }

    /// <summary>
    ///     Scales the shape the specified amount.
    /// </summary>
    /// <param name="shape"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static Shape Scale(this Shape shape, float scale)
    {
        return new Shape
        {
            Polygon = shape.Polygon.Scale(scale),
            Color = shape.Color
        };
    }
}