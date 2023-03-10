using Frank.GameEngine.Core._2D.Calculators;
using Frank.GameEngine.Core._2D.Extensions;
using Frank.GameEngine.Core._2D.GameObjects;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core._2D.Physics.Effects;

public class Drag : PhysicalEffect
{
	public Drag(EnvironmentalFactors environment) : base(environment)
	{
	}

	public override Vector2 Calculate(IGameObject gameObject, TimeSpan elapsedTime)
	{
		var dragCoefficient = AerodynamicsCalculator.CalculateCoefficientOfDrag(gameObject.Polygon, _environment.Medium, gameObject.Velocity);
		var velocityMagnitude = gameObject.Velocity.GetMagnitude();
		var referenceArea = gameObject.Polygon.GetArea();
		var dragForce = 0.5f * _environment.Medium.Density * velocityMagnitude * velocityMagnitude * dragCoefficient * referenceArea;
		var dragAcceleration = dragForce / gameObject.Mass;
		return -gameObject.Direction * dragAcceleration * (float)elapsedTime.TotalSeconds;
	}
}