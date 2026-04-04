using System.Numerics;

namespace Frank.GameEngine.Primitives;

public static class Camera2DExtensions
{
    /// <summary>
    ///     World → screen (pixel) matrix matching Raylib <c>Camera2D</c> semantics (Y down, rotation clockwise in degrees).
    /// </summary>
    public static Matrix3x2 GetWorldToScreenMatrix(this Camera2D camera)
    {
        var rad = camera.RotationDegrees * (MathF.PI / 180f);
        var zoom = Math.Max(camera.Zoom, 1e-4f);
        var translateToOrigin = Matrix3x2.CreateTranslation(-camera.Target);
        var rotate = Matrix3x2.CreateRotation(-rad);
        var scale = Matrix3x2.CreateScale(zoom);
        var translateToScreen = Matrix3x2.CreateTranslation(camera.Offset);
        return translateToOrigin * rotate * scale * translateToScreen;
    }

    /// <summary>Maps a world point to screen pixels using <see cref="GetWorldToScreenMatrix" />.</summary>
    public static Vector2 WorldToScreen(this Camera2D camera, Vector2 world)
    {
        return Vector2.Transform(world, camera.GetWorldToScreenMatrix());
    }
}
