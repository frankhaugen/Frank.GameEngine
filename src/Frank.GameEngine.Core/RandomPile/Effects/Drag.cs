using System.Numerics;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Core.RandomPile.Effects;

public class Drag : PhysicalEffect
{
    public Drag(EnvironmentalFactors environment) : base(environment)
    {
    }

    public override Vector3 Calculate(GameObject gameObject, TimeSpan elapsedTime)
    {
        // var dragCoefficient = AerodynamicsCalculator.CalculateCoefficientOfDrag(gameObject.Polygon, _environment.Medium, gameObject.Velocity);
        // var velocityMagnitude = gameObject.Velocity.GetMagnitude();
        // var referenceArea = gameObject.Polygon.GetArea();
        // var dragForce = 0.5f * _environment.Medium.Density * velocityMagnitude * velocityMagnitude * dragCoefficient * referenceArea;
        // var dragAcceleration = dragForce / gameObject.Mass;
        // return -gameObject.Direction * dragAcceleration * (float)elapsedTime.TotalSeconds;

        return default;
    }
}