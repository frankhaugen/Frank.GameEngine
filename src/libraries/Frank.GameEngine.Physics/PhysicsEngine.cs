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
            var forceContributions = Forces
                .Select(f => f.Calculate(gameObject, deltaTime))
                .Where(f => f.HasValue)
                .Select(f => f!.Value)
                .ToList();

            if (forceContributions.Count > 0)
                gameObject.Rigidbody.Velocity += forceContributions.Aggregate((x, y) => x + y);

            gameObject.Transform.Translate(gameObject.Rigidbody.Velocity * (float)deltaTime.TotalSeconds);

            // gameObject.Rigidbody.Velocity = Vector3.Zero;
        }

        _collisionHandler.HandleCollisions(scene);
    }
}