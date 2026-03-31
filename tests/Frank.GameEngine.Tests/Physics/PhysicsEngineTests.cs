using System.Numerics;
using FluentAssertions;
using Frank.GameEngine.Physics;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Tests.Physics;

public class PhysicsEngineTests
{
    [Fact]
    public void Update_WhenAllForcesReturnNull_DoesNotThrow_AndStillIntegratesVelocity()
    {
        var scene = new Scene("s", new Camera());
        var body = new GameObject { Rigidbody = new Rigidbody { Velocity = new Vector3(1, 0, 0) } };
        scene.GameObjects.Add(body);

        var physics = new PhysicsEngine(new NullCollisionHandler());
        physics.Forces.Add(new NullForce());
        physics.Forces.Add(new NullForce());

        var act = () => physics.Update(scene, TimeSpan.FromSeconds(1));
        act.Should().NotThrow();

        body.Transform.Position.X.Should().BeApproximately(1f, 0.001f);
    }

    private sealed class NullForce : IForce
    {
        public Vector3? Calculate(GameObject gameObject, TimeSpan deltaTime) => null;
    }
}
