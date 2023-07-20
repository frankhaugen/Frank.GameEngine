namespace Frank.GameEngine.Primitives;

public static class GameObjectExtensions
{
    /// <summary>
    /// Gets the shape of the game object after it has been transformed by the game object's transform.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public static Shape GetTransformedShape(this GameObject gameObject)
    {
        var shape = gameObject.Shape;
        var transform = gameObject.Transform;
        var transformedShape = shape.GetTransformedShape(transform);
        return transformedShape;
    }
}