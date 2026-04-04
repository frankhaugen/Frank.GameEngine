using System.Numerics;

namespace Frank.GameEngine.Primitives;

/// <summary>
///     2D transform: position of the sprite pivot, rotation, and non-uniform scale.
/// </summary>
public sealed class Transform2D
{
    /// <summary>World-space pivot (see <see cref="Sprite2D.Origin" />).</summary>
    public Vector2 Position { get; set; }

    /// <summary>Clockwise rotation in degrees (Raylib / MonoGame sprite conventions).</summary>
    public float RotationDegrees { get; set; }

    public Vector2 Scale { get; set; } = Vector2.One;
}
