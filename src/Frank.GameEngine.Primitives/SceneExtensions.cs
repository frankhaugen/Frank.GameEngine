using System.Numerics;

namespace Frank.GameEngine.Primitives;

public static class SceneExtensions
{
    public static IEnumerable<Shape> GetTransformedShapes(this Scene scene) 
        => scene.GameObjects
            .Select(gameObject => new { gameObject, shape = gameObject.Shape })
            .Select(x => new { x, transform = x.gameObject.Transform })
            .Select(y => y.x.shape.Transform(y.transform));

}