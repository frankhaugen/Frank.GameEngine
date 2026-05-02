using System.Numerics;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Core.RandomPile.Effects;

public class Lift : PhysicalEffect
{
    public Lift(EnvironmentalFactors environment) : base(environment)
    {
    }

    public override Vector3 Calculate(GameObject gameObject, TimeSpan elapsedTime)
    {
        // var liftCoefficient = LiftCalculator.CalculateLiftCoefficient(gameObject.Polygon, _environment.Medium, gameObject.Velocity);
        // var velocityMagnitude = gameObject.Velocity.GetMagnitude();
        // var referenceArea = gameObject.Polygon.GetArea();
        // var liftForce = 0.5f * _environment.Medium.Density * velocityMagnitude * velocityMagnitude * liftCoefficient * referenceArea;
        // var liftAcceleration = liftForce / gameObject.Mass;
        // return gameObject.Direction.GetNormal() * liftAcceleration * (float)elapsedTime.TotalSeconds;

        return default;
    }
}