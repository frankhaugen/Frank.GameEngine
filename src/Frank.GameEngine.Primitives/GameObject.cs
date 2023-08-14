using System.Drawing;

namespace Frank.GameEngine.Primitives;

/// <summary>
///     Represents a game object. A game object is an object that can be placed in a scene.
/// </summary>
public class GameObject
{
    /// <summary>
    ///     The unique identifier of the game object. This is generated when the game object is created.
    /// </summary>
    public Guid Id { get; } = Guid.NewGuid();

    /// <summary>
    ///     The name of the game object. This is used to identify the game object in a friendly way.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     The tag of the game object. This is used to identify the game object in a friendly way.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    ///     Transform of the game object. This is used to position the game object in the scene.
    ///     A Transform is a combination of a position, rotation and scale.
    /// </summary>
    public Transform Transform { get; set; } = new();

    /// <summary>
    ///     The shape of the game object. This is used to determine the collision of the game object.
    ///     A shape is <see cref="Polygon" /> and a <see cref="Color" />.
    /// </summary>
    public Shape Shape { get; set; } = new();

    /// <summary>
    ///     The rigidbody of the game object. This is used to determine the physics of the game object.
    ///     A rigidbody is a combination of a mass, velocity and acceleration. It also contains switches to enable/disable
    ///     gravity and collisions etc.
    /// </summary>
    public Rigidbody Rigidbody { get; set; } = new();

    /// <summary>
    ///     Returns a string representation of the game object.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"{Name} - {Shape}";
    }
}