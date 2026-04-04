using System.Numerics;

namespace Frank.GameEngine.Primitives;

/// <summary>
///     Orthographic 2D camera: world point <see cref="Target" /> is pinned to screen <see cref="Offset" /> (typically half the viewport).
/// </summary>
public sealed class Camera2D
{
    /// <summary>World-space point that appears at <see cref="Offset" /> on screen.</summary>
    public Vector2 Target { get; set; }

    /// <summary>Screen-space anchor (often viewport width/height × 0.5).</summary>
    public Vector2 Offset { get; set; } = new(400f, 300f);

    public float RotationDegrees { get; set; }

    /// <summary>World units per screen pixel at zoom 1 (larger = more world visible).</summary>
    public float Zoom { get; set; } = 1f;

    public int ViewportWidth { get; set; } = 800;

    public int ViewportHeight { get; set; } = 600;
}
