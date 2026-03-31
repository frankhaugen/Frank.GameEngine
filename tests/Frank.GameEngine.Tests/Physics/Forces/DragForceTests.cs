using System.Numerics;
using FluentAssertions;
using Frank.GameEngine.Physics;
using Frank.GameEngine.Physics.Forces;
using Frank.GameEngine.Primitives;
using TUnit.Core;

namespace Frank.GameEngine.Tests.Physics.Forces;

public class DragForceTests
{
    [Test]
    [Arguments(1f, 1f, 1f, 2f)]
    public void Calculate_DragForce_ReturnsCorrectResult(
        float dragCoefficient,
        float gameObjectVelocityX,
        float gameObjectVelocityY,
        float gameObjectVelocityZ)
    {
        var dragForce = new DragForce(dragCoefficient);
        var gameObject = new GameObject
        {
            Rigidbody = new Rigidbody
            {
                Velocity = new Vector3(gameObjectVelocityX, gameObjectVelocityY, gameObjectVelocityZ)
            }
        };
        var expectedDragMagnitude = dragCoefficient * gameObject.Rigidbody.Velocity.LengthSquared();

        var result = dragForce.Calculate(gameObject, TimeSpan.FromSeconds(1));

        var context = TestContext.Current!;
        context.Output.WriteLine($"Expected: {expectedDragMagnitude}");
        context.Output.WriteLine($"Actual: {result?.Length()}");
        context.Output.WriteLine($"Velocity: {result}");

        result.Should().NotBeNull();
        var drag = result!.Value;
        drag.Length().Should()
            .BeApproximately((float)(expectedDragMagnitude * TimeSpan.FromSeconds(1).TotalSeconds),
                0.0001f);
        Vector3.Dot(drag, gameObject.Rigidbody.Velocity).Should()
            .BeLessThan(0);
    }
}
