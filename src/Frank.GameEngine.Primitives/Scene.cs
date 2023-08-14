using System.Drawing;

namespace Frank.GameEngine.Primitives;

/// <summary>
///     A scene is a collection of game objects. A scene is rendered by a camera.
/// </summary>
public class Scene
{
    public Scene(string name, Camera camera)
    {
        Name = name;
        Id = Guid.NewGuid();
        Camera = camera;
    }

    /// <summary>
    ///     A friendly name for the scene. This is used to identify the scene in a friendly way.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     The unique identifier of the scene. This is generated when the scene is created.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    ///     The background color of the scene. This is used to clear the screen before rendering the scene.
    /// </summary>
    public Color BackgroundColor { get; set; } = Color.Black;

    /// <summary>
    ///     The game objects in the scene. These are rendered by the camera.
    /// </summary>
    public List<GameObject> GameObjects { get; } = new();

    /// <summary>
    ///     The camera that renders the scene. This is used to render the game objects in the scene.
    /// </summary>
    public Camera Camera { get; }

    /// <summary>
    ///     The size of the scene. This is used to determine the size of the rendered scene.
    ///     Default is 1000x1000 pixels, starting at 0,0.
    /// </summary>
    public Rectangle Size { get; set; } = new(0, 0, 1000, 1000);
}