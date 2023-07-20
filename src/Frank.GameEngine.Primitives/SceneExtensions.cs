using System.Numerics;

namespace Frank.GameEngine.Primitives;

public static class SceneExtensions
{
    /// <summary>
    /// Gets the shapes of the game objects after they have been transformed by the game object's transform.
    /// </summary>
    /// <param name="scene"></param>
    /// <returns></returns>
    public static IEnumerable<Shape> GetTransformedShapes(this Scene scene) 
        => scene.GameObjects
            .Select(gameObject => new { gameObject, shape = gameObject.Shape })
            .Select(x => new { x, transform = x.gameObject.Transform })
            .Select(y => y.x.shape.Transform(y.transform));

}