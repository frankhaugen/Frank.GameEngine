using Frank.GameEngine.Primitives;
using System.Numerics;

namespace Frank.GameEngine.Physics;

public class PhysicsEngine
{
    public List<IForce> Forces { get; } = new();

    public PhysicsEngine()
    {
    }

    public void Update(Scene scene, TimeSpan deltaTime)
    {
        foreach (var gameObject in scene.GameObjects)
        {
            if (Forces.Any())
            {
                var force = Forces.Select(x => x.Calculate(gameObject, deltaTime)).Where(x => x.HasValue).Select(x => x!.Value).Aggregate((x, y) => x + y);
                gameObject.Rigidbody.Velocity += force;
            }
            
            gameObject.Transform.Translate(gameObject.Rigidbody.Velocity * (float)deltaTime.TotalSeconds);
            
            gameObject.Rigidbody.Velocity = Vector3.Zero;
        }
    }
}