using System.Numerics;
using FluentAssertions;
using Frank.GameEngine.Physics;
using Frank.GameEngine.Primitives;
using Xunit.Abstractions;

namespace Frank.GameEngine.Tests.Physics.Forces;

public class DragForceTests
{
    private readonly ITestOutputHelper _output;

    public DragForceTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [InlineData(1, 1, 1, 2)]
    public void Calculate_DragForce_ReturnsCorrectResult(float dragCoefficient, float gameObjectVelocityX,
        float gameObjectVelocityY, float gameObjectVelocityZ)
    {
        // Arrange
        var dragForce = new DragForce(dragCoefficient);
        var gameObject = new GameObject
        {
            Rigidbody = new Rigidbody
            {
                Velocity = new Vector3(gameObjectVelocityX, gameObjectVelocityY, gameObjectVelocityZ)
            }
        };
        var expectedDragMagnitude = dragCoefficient * gameObject.Rigidbody.Velocity.LengthSquared();

        // Act
        var result = dragForce.Calculate(gameObject, TimeSpan.FromSeconds(1));

        // Outputting the information
        _output.WriteLine($"Expected: {expectedDragMagnitude}");
        _output.WriteLine($"Actual: {result?.Length()}");
        _output.WriteLine($"Velocity: {result}");

        // Assert
        result?.Should().NotBeNull();
        result?.Length().Should()
            .BeApproximately((float)(expectedDragMagnitude * TimeSpan.FromSeconds(1).TotalSeconds),
                0.0001f); // Tolerating small differences due to floating-point precision
        Vector3.Dot(result.Value, gameObject.Rigidbody.Velocity).Should()
            .BeLessThan(0); // Drag force should always be opposite to direction of motion
    }
}