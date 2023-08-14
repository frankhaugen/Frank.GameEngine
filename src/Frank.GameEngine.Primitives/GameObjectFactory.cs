using System.Drawing;

namespace Frank.GameEngine.Primitives;

/// <summary>
///     Creates game objects.
/// </summary>
public static class GameObjectFactory
{
    /// <summary>
    ///     Creates a game object with the specified transform, shape, and name.
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="shape"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static GameObject CreateGameObject(Transform transform, Shape shape, string? name = null)
    {
        var gameObject = new GameObject
        {
            Name = name,
            Transform = transform,
            Shape = shape
        };
        return gameObject;
    }

    /// <summary>
    ///     Creates a game object with all default values.
    /// </summary>
    /// <returns></returns>
    public static GameObject CreateGameObject()
    {
        var gameObject = new GameObject
        {
            Transform = TransformFactory.CreateTransform(),
            Shape = ShapeFactory.CreateSpere(Color.White)
        };
        return gameObject;
    }
}