using System.Numerics;
using FluentAssertions;
using Frank.GameEngine.Physics;
using Frank.GameEngine.Primitives;
using Moq;

namespace Frank.GameEngine.Tests.Physics;

public class PhysicsEngineIntegrationTests
{
    private sealed class ConstantForce(Vector3 deltaVPerSecond) : IForce
    {
        public Vector3? Calculate(GameObject gameObject, TimeSpan deltaTime) =>
            deltaVPerSecond * (float)deltaTime.TotalSeconds;
    }

    [Fact]
    public void Update_SumsMultipleForcesIntoVelocity()
    {
        var scene = new Scene("s", new Camera());
        var body = new GameObject { Rigidbody = new Rigidbody { Velocity = Vector3.Zero } };
        scene.GameObjects.Add(body);

        var physics = new PhysicsEngine(new NullCollisionHandler());
        physics.Forces.Add(new ConstantForce(new Vector3(1f, 0f, 0f)));
        physics.Forces.Add(new ConstantForce(new Vector3(0f, 2f, 0f)));

        physics.Update(scene, TimeSpan.FromSeconds(1));

        body.Rigidbody.Velocity.Should().Be(new Vector3(1f, 2f, 0f));
        body.Transform.Position.Should().Be(new Vector3(1f, 2f, 0f));
    }

    [Fact]
    public void Update_CallsCollisionHandlerOncePerUpdate()
    {
        var scene = new Scene("s", new Camera());
        scene.GameObjects.Add(new GameObject());

        var handler = new Mock<ICollisionHandler>();
        var physics = new PhysicsEngine(handler.Object);

        physics.Update(scene, TimeSpan.FromMilliseconds(16));
        physics.Update(scene, TimeSpan.FromMilliseconds(16));

        handler.Verify(h => h.HandleCollisions(scene), Times.Exactly(2));
    }

    [Fact]
    public void Update_AppliesTranslationFromVelocity()
    {
        var scene = new Scene("s", new Camera());
        var body = new GameObject
        {
            Rigidbody = new Rigidbody { Velocity = new Vector3(0f, 3f, 0f) },
            Transform = new Transform { Position = new Vector3(1f, 1f, 1f) }
        };
        scene.GameObjects.Add(body);

        var physics = new PhysicsEngine(new NullCollisionHandler());

        physics.Update(scene, TimeSpan.FromSeconds(0.5f));

        body.Transform.Position.Should().Be(new Vector3(1f, 2.5f, 1f));
    }
}
