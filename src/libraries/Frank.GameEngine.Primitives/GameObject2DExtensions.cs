using System.Numerics;

namespace Frank.GameEngine.Primitives;

public static class GameObject2DExtensions
{
    /// <summary>
    ///     Unscaled local corners (before <see cref="Transform2D.Scale" />) relative to pivot, counter-clockwise from top-left in local +Y down space.
    /// </summary>
    public static void GetLocalCorners(Sprite2D sprite, Span<Vector2> corners)
    {
        var w = sprite.Size.X;
        var h = sprite.Size.Y;
        var ox = sprite.Origin.X * w;
        var oy = sprite.Origin.Y * h;
        corners[0] = new Vector2(-ox, -oy);
        corners[1] = new Vector2(w - ox, -oy);
        corners[2] = new Vector2(w - ox, h - oy);
        corners[3] = new Vector2(-ox, h - oy);
    }

    /// <summary>Axis-aligned bounding box in world space after transform (conservative for rotated sprites).</summary>
    public static (Vector2 Min, Vector2 Max) GetAxisAlignedBoundingBox(this GameObject2D go)
    {
        Span<Vector2> local = stackalloc Vector2[4];
        GetLocalCorners(go.Sprite, local);
        var rad = go.Transform.RotationDegrees * (MathF.PI / 180f);
        var cos = MathF.Cos(rad);
        var sin = MathF.Sin(rad);
        var s = go.Transform.Scale;
        var min = new Vector2(float.MaxValue, float.MaxValue);
        var max = new Vector2(float.MinValue, float.MinValue);
        for (var i = 0; i < 4; i++)
        {
            var lx = local[i].X * s.X;
            var ly = local[i].Y * s.Y;
            var wx = lx * cos - ly * sin + go.Transform.Position.X;
            var wy = lx * sin + ly * cos + go.Transform.Position.Y;
            min = Vector2.Min(min, new Vector2(wx, wy));
            max = Vector2.Max(max, new Vector2(wx, wy));
        }

        return (min, max);
    }
}
