using System.Numerics;
using FluentAssertions;
using Frank.GameEngine.Physics;

namespace Frank.GameEngine.Tests.Physics;

public class ForceHelperTests
{
    [Test]
    public void GetInitialVelocity_ScalesDirectionByForceOverMass()
    {
        var direction = new Vector3(3f, 0f, 4f); // length 5
        var v = ForceHelper.GetInitialVelocity(10.0, 2f, direction);

        v.Length().Should().BeApproximately(5f, 0.001f);
    }

    [Test]
    [Arguments(1f, 2f, 2f)]
    [Arguments(0f, 5f, 0f)]
    public void CalculateForce_MultipliesBySecondsComponent(float magnitude, float seconds, float expectedLength)
    {
        var dir = Vector3.UnitY;
        var delta = TimeSpan.FromSeconds(seconds);

        var f = ForceHelper.CalculateForce(dir, magnitude, delta);

        f.Y.Should().BeApproximately(expectedLength, 0.0001f);
    }

    [Test]
    public void ApplyForce_AddsVectors()
    {
        var r = ForceHelper.ApplyForce(new Vector3(1, 2, 3), new Vector3(4, 5, 6));
        r.Should().Be(new Vector3(5, 7, 9));
    }

    [Test]
    public void ApplyForce_WithMass_DividesForce()
    {
        var r = ForceHelper.ApplyForce(Vector3.Zero, new Vector3(0f, 10f, 0f), 2f);
        r.Should().Be(new Vector3(0f, 5f, 0f));
    }

    [Test]
    public void ApplyForce_WithMassAndDelta_AppliesImpulse()
    {
        var r = ForceHelper.ApplyForce(Vector3.Zero, new Vector3(0f, 10f, 0f), 2f, TimeSpan.FromSeconds(2));
        r.Should().Be(new Vector3(0f, 10f, 0f));
    }
}
