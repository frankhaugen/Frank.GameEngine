using System.Drawing;

namespace Frank.GameEngine.Primitives;

public class Scene
{
    public Scene(string name, Camera camera)
    {
        Name = name;
        Id = Guid.NewGuid();
        Camera = camera;
    }

    public string Name { get; }

    public Guid Id { get; }

    public Color BackgroundColor { get; set; }

    public List<GameObject> GameObjects { get; } = new();

    public Rectangle SceneSize { get; set; }

    public Camera Camera { get; set; }
}