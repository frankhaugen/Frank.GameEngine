using System.Numerics;

namespace Frank.GameEngine.Primitives;

/// <summary>
///     Solid-color axis-aligned quad in local space before <see cref="Transform2D" /> (textures can be layered on later).
/// </summary>
public sealed class Sprite2D
{
    /// <summary>Logical size in world units before scale.</summary>
    public Vector2 Size { get; set; } = new(32f, 32f);

    /// <summary>Normalized pivot in sprite space: (0,0) top-left, (0.5,0.5) center.</summary>
    public Vector2 Origin { get; set; } = new(0.5f, 0.5f);

    public Rgba32 Tint { get; set; } = Rgba32.White;
}
