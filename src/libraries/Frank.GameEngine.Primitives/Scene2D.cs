namespace Frank.GameEngine.Primitives;

/// <summary>
///     2D scene: orthographic camera and drawable objects (HUD, tile games, or underlay layers with 3D).
/// </summary>
public sealed class Scene2D
{
    public Scene2D(string name, Camera2D camera)
    {
        Name = name;
        Id = Guid.NewGuid();
        Camera = camera;
    }

    public string Name { get; }

    public Guid Id { get; }

    public Camera2D Camera { get; }

    public Rgba32 BackgroundColor { get; set; } = Rgba32.Black;

    public List<GameObject2D> GameObjects { get; } = new();
}
