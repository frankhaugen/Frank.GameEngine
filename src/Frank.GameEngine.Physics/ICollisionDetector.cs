using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Physics;

public interface ICollisionDetector
{
    /// <summary>
    /// Detects collisions between game objects in a scene.
    /// </summary>
    /// <param name="scene"></param>
    /// <returns></returns>
    IEnumerable<(GameObject, GameObject)> DetectCollisions(Scene scene);
}