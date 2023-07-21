using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Physics;

/// <summary>
/// A collision handler that does nothing, not even detect collisions.
/// This is useful for testing, or for when you don't want to handle collisions.
/// </summary>
public class NullCollisionHandler : ICollisionHandler
{
    public void HandleCollisions(Scene scene)
    {
        // do nothing
    }
}