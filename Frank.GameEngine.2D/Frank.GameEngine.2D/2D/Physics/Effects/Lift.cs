using Frank.GameEngine.Core._2D.Calculators;
using Frank.GameEngine.Core._2D.Extensions;
using Frank.GameEngine.Core._2D.GameObjects;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core._2D.Physics.Effects;

public class Lift : PhysicalEffect
{
	public Lift(EnvironmentalFactors environment) : base(environment)
	{
	}

	public override Vector2 Calculate(IGameObject gameObject, TimeSpan elapsedTime)
	{
		var liftCoefficient = LiftCalculator.CalculateLiftCoefficient(gameObject.Polygon, _environment.Medium, gameObject.Velocity);
		var velocityMagnitude = gameObject.Velocity.GetMagnitude();
		var referenceArea = gameObject.Polygon.GetArea();
		var liftForce = 0.5f * _environment.Medium.Density * velocityMagnitude * velocityMagnitude * liftCoefficient * referenceArea;
		var liftAcceleration = liftForce / gameObject.Mass;
		return gameObject.Direction.GetNormal() * liftAcceleration * (float)elapsedTime.TotalSeconds;
	}
}