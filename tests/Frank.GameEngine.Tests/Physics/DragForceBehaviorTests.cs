using System.Numerics;
using FluentAssertions;
using Frank.GameEngine.Physics.Forces;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Tests.Physics;

public class DragForceBehaviorTests
{
    [Fact]
    public void Calculate_ReturnsZeroVector_WhenVelocityIsZero()
    {
        var drag = new DragForce(0.5f);
        var body = new GameObject { Rigidbody = new Rigidbody { Velocity = Vector3.Zero } };

        var f = drag.Calculate(body, TimeSpan.FromSeconds(1));

        f.Should().NotBeNull();
        f!.Value.Should().Be(Vector3.Zero);
    }

    [Fact]
    public void Calculate_PointsOppositeToVelocity()
    {
        var drag = new DragForce(0.25f);
        var body = new GameObject { Rigidbody = new Rigidbody { Velocity = new Vector3(10f, 0f, 0f) } };

        var f = drag.Calculate(body, TimeSpan.FromSeconds(1));

        f.Should().NotBeNull();
        Vector3.Dot(f!.Value, body.Rigidbody.Velocity).Should().BeLessThan(0f);
    }
}
