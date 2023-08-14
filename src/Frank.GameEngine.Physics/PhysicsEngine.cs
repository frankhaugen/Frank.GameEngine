using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Physics;

public class PhysicsEngine
{
    private readonly ICollisionHandler _collisionHandler;

    public PhysicsEngine(ICollisionHandler collisionHandler)
    {
        _collisionHandler = collisionHandler;
    }

    public List<IForce> Forces { get; } = new();

    public void Update(Scene scene, TimeSpan deltaTime)
    {
        foreach (var gameObject in scene.GameObjects)
        {
            if (Forces.Any())
            {
                var force = Forces.Select(x => x.Calculate(gameObject, deltaTime)).Where(x => x.HasValue)
                    .Select(x => x!.Value).Aggregate((x, y) => x + y);
                gameObject.Rigidbody.Velocity += force;
            }

            gameObject.Transform.Translate(gameObject.Rigidbody.Velocity * (float)deltaTime.TotalSeconds);

            // gameObject.Rigidbody.Velocity = Vector3.Zero;
        }

        _collisionHandler.HandleCollisions(scene);
    }
}