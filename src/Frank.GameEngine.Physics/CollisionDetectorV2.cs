using Frank.GameEngine.Primitives;
using System.Numerics;

namespace Frank.GameEngine.Physics;

public class CollisionHandler : ICollisionHandler
{
    /// <summary>
    /// Detects collisions between game objects in a scene. This is a brute force method that checks every game object against every other game object.
    /// </summary>
    /// <param name="scene"></param>
    public void HandleCollisions(Scene scene)
    {
        var collisions = scene.GetCollisions();
        
        foreach (var collision in collisions)
        {
            if (!collision.A.IsActive || !collision.B.IsActive) continue;
            if (!collision.A.Rigidbody.IsColliding || !collision.B.Rigidbody.IsColliding) continue;

            if (collision.A.Rigidbody.IsBouncy && collision.B.Rigidbody.IsBouncy)
            {
                collision.A.Rigidbody.Velocity = Vector3.Reflect(collision.A.Rigidbody.Velocity, collision.Normal);
                collision.B.Rigidbody.Velocity = Vector3.Reflect(collision.B.Rigidbody.Velocity, collision.Normal);
            }
            else if (collision.A.Rigidbody.IsBouncy)
            {
                collision.A.Rigidbody.Velocity = Vector3.Reflect(collision.A.Rigidbody.Velocity, collision.Normal);
            }
            else if (collision.B.Rigidbody.IsBouncy)
            {
                collision.B.Rigidbody.Velocity = Vector3.Reflect(collision.B.Rigidbody.Velocity, collision.Normal);
            }
            else
            {
                collision.A.Rigidbody.Velocity = Vector3.Zero;
                collision.B.Rigidbody.Velocity = Vector3.Zero;
            }
        }
    }
}