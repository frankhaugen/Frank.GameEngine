using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Physics;

public interface ICollisionHandler
{
    /// <summary>
    ///     Detects collisions between game objects in a scene.
    /// </summary>
    /// <param name="scene"></param>
    /// <returns></returns>
    void HandleCollisions(Scene scene);
}