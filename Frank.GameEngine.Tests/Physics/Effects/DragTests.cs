using Frank.GameEngine.Core._2D.Calculators;
using Frank.GameEngine.Core._2D.Comparers;
using Frank.GameEngine.Core._2D.Extensions;
using Frank.GameEngine.Core._2D.GameObjects;
using Frank.GameEngine.Core._2D.Physics;
using Frank.GameEngine.Core._2D.Physics.Effects;
using Frank.GameEngine.Core._2D.Shapes;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Tests.Physics.Effects;

public class DragTests
{
    
    [Fact]
    public void Drag_Calculate_AppliesCorrectAcceleration()
    {
        // Arrange
        var environment = new EnvironmentalFactors
        {
            Gravity = 0,
            Medium = new Fluid(FluidName.Air),
            Wind = Vector2.Zero
        };
        var drag = new Drag(environment);
        var gameObject = new GameObject
        {
            Mass = 1,
            Velocity = new Vector2(10, 0),
            Direction = new Vector2(1, 0),
            Polygon = PolygonFactory.GetCircle(32, 25),
        };
        var elapsedTime = TimeSpan.FromSeconds(1);

        // Act
        var acceleration = drag.Calculate(gameObject, elapsedTime);

        // Assert
        var expectedAcceleration = -gameObject.Direction * AerodynamicsCalculator.CalculateCoefficientOfDrag(gameObject.Polygon, environment.Medium, gameObject.Velocity) * 0.5f * environment.Medium.Density * gameObject.Velocity.GetMagnitude() * gameObject.Velocity.GetMagnitude() / gameObject.Mass;
        Assert.Equal(expectedAcceleration, acceleration, new Vector2Comparer(0.0001f));
    }

    
    
    [Fact]
    public void Calculate()
    {
        var drag = new Drag(new EnvironmentalFactors(FluidName.Air));
        var obj = new GameObject
        {
            Velocity = Vector2.Zero,
            Direction = Vector2.UnitX,
            Polygon = PolygonFactory.GetCircle(32, 25)
        };
        var time = new TimeSpan(0, 0, 0, 1, 0);
        drag.Calculate(obj, time);
        Assert.Equal(new Vector2(0, 0), obj.Velocity);
    }
    
}