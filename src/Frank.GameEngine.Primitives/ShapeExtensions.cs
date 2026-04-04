using System.Numerics;

namespace Frank.GameEngine.Primitives;

public static class ShapeExtensions
{
    public static Shape GetTransformedShape(this Shape shape, Transform transform) => shape.Transform(transform);

    /// <summary>
    ///     Axis-aligned bounds of <see cref="Polygon" /> and/or <see cref="Shape.TriangleMesh" /> (union when both).
    /// </summary>
    public static (Vector3 Min, Vector3 Max) GetAxisAlignedBoundingBox(this Shape shape)
    {
        var hasMesh = shape.TriangleMesh != null;
        var hasPoly = shape.Polygon.Length > 0;
        if (!hasMesh && !hasPoly)
            return (Vector3.Zero, Vector3.Zero);
        if (hasMesh && !hasPoly)
            return shape.TriangleMesh!.GetAxisAlignedBoundingBox();
        if (!hasMesh)
            return shape.Polygon.GetAxisAlignedBoundingBox();

        var (mMin, mMax) = shape.TriangleMesh!.GetAxisAlignedBoundingBox();
        var (pMin, pMax) = shape.Polygon.GetAxisAlignedBoundingBox();
        return (Vector3.Min(mMin, pMin), Vector3.Max(mMax, pMax));
    }

    public static bool BoundingBoxesOverlap(this Shape a, Shape b)
    {
        var (aMin, aMax) = a.GetAxisAlignedBoundingBox();
        var (bMin, bMax) = b.GetAxisAlignedBoundingBox();
        return aMin.X <= bMax.X && aMax.X >= bMin.X
               && aMin.Y <= bMax.Y && aMax.Y >= bMin.Y
               && aMin.Z <= bMax.Z && aMax.Z >= bMin.Z;
    }

    public static bool Intersect(this Shape shape, Shape otherShape)
    {
        if (shape.TriangleMesh != null || otherShape.TriangleMesh != null)
            return shape.BoundingBoxesOverlap(otherShape);

        return shape.Polygon.Intersect(otherShape.Polygon);
    }

    public static IEnumerable<Vector3> GetIntersectionPoints(this Shape shape, Shape otherShape)
    {
        if (shape.TriangleMesh != null || otherShape.TriangleMesh != null)
        {
            if (!shape.BoundingBoxesOverlap(otherShape))
                return [];

            var (aMin, aMax) = shape.GetAxisAlignedBoundingBox();
            var (bMin, bMax) = otherShape.GetAxisAlignedBoundingBox();
            var p = (aMin + aMax + bMin + bMax) * 0.25f;
            return [p];
        }

        return shape.Polygon.GetIntersectionPoints(otherShape.Polygon);
    }

    /// <summary>
    ///     Applies scale (around the origin), rotation, then translation — typical for model vertices in local space.
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

    public static Shape GetCopy(this Shape shape)
    {
        return new Shape
        {
            Polygon = shape.Polygon.GetCopy(),
            TriangleMesh = shape.TriangleMesh?.GetCopy(),
            Color = shape.Color
        };
    }

    public static Shape Translate(this Shape shape, Vector3 position)
    {
        return new Shape
        {
            Polygon = shape.Polygon.Translate(position),
            TriangleMesh = shape.TriangleMesh?.Translate(position),
            Color = shape.Color
        };
    }

    public static Shape Rotate(this Shape shape, Quaternion rotation)
    {
        return new Shape
        {
            Polygon = shape.Polygon.Rotate(rotation),
            TriangleMesh = shape.TriangleMesh?.Rotate(rotation),
            Color = shape.Color
        };
    }

    public static Shape Scale(this Shape shape, float scale)
    {
        return new Shape
        {
            Polygon = shape.Polygon.Scale(scale),
            TriangleMesh = shape.TriangleMesh?.Scale(scale),
            Color = shape.Color
        };
    }
}
