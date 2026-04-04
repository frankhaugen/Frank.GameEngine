using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Rendering;

/// <summary>
///     Orthographic 2D presentation (sprites, tile maps, HUD). Pair with <see cref="IRenderer" /> for 3D worlds.
/// </summary>
public interface IRenderer2D
{
    void Render(Scene2D scene);
}
