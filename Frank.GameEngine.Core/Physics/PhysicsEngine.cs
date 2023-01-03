using Frank.GameEngine.Core.GameObjects;
using Frank.GameEngine.Core.Physics.Effects;

namespace Frank.GameEngine.Core.Physics;

public class PhysicsEngine : IPhysicsEngine
{
	private readonly EnvironmentalFactors _environment;
	private readonly List<IPhysicalEffect> _physicalEffects = new();

	public PhysicsEngine(EnvironmentalFactors environment)
	{
		_environment = environment;
		_physicalEffects.Add(new Gravity(_environment));
		//_physicalEffects.Add(new Drag(_environment));
		//_physicalEffects.Add(new Lift(_environment));
	}

	public void Update(IGameObject gameObject, TimeSpan elapsed)
	{
		if (!gameObject.PhysicsEnebled) return;
		foreach (var physicalEffect in _physicalEffects)
		{
			gameObject.Velocity += physicalEffect.Calculate(gameObject, elapsed);
		}

		gameObject.Position += gameObject.Velocity * (float)elapsed.TotalSeconds;
	}
}

//public void UpdateNew(IGameObject gameObject, TimeSpan elapsed)
//{
//    var acceleration = CalculateAcceleration(gameObject);
//    var velocity = CalculateVelocity(gameObject.Velocity, acceleration, elapsed);
//    var position = CalculatePosition(gameObject.Position, velocity, elapsed);

//    velocity.EnsureNoNaNs();
//    position.EnsureNoNaNs();

//    gameObject.Velocity = velocity;
//    gameObject.Position = position;
//}

//public void Update(IGameObject gameObject, TimeSpan elapsed)
//{
//    var gravityAcceleration = new Vector2(0, _environment.Gravity);

//    var velocity = gameObject.Velocity;
//    var density = _environment.Medium.Density;
//    var surfaceArea = gameObject.Polygon.Area();
//    var dragCoefficient = AerodynamicsCalculator.CalculateCoefficientOfDrag(gameObject.Polygon, _environment.Medium, velocity);
//    var airResistance = 0.5f * density * velocity.LengthSquared() * surfaceArea * dragCoefficient;
//    var airResistanceVector = new Vector2(-velocity.X * airResistance, -velocity.Y * airResistance);

//    var force = airResistanceVector + gravityAcceleration * gameObject.Mass;

//    var acceleration = force / gameObject.Mass;

//    if (!float.IsNaN(acceleration.X) && !float.IsNaN(acceleration.Y))
//    {
//        gameObject.Velocity += acceleration * (float)elapsed.TotalSeconds;
//    }

//    if (!float.IsNaN(gameObject.Velocity.X) && !float.IsNaN(gameObject.Velocity.Y))
//    {
//        gameObject.Position += gameObject.Velocity * (float)elapsed.TotalSeconds;
//    }

//    var optimalDirection = gameObject.Polygon.OptimalFlowDirection();
//    var something = DirectionsCalculator.Vector2ToHeadingAndSpeed(optimalDirection);
//    gameObject.Polygon = gameObject.Polygon.RotateToDirection(something.heading);
//}

//public Vector2 CalculateAcceleration(IGameObject gameObject)
//{
//    var accelerationDueToGravity = new Vector2(0, _environment.Gravity);
//    var accelerationDueToMedium = CalculateAccelerationDueToMedium(gameObject);
//    var accelleration = accelerationDueToGravity + accelerationDueToMedium;

//    accelleration.EnsureNoNaNs();

//    return accelleration;
//}

//public Vector2 CalculateAccelerationDueToMedium(IGameObject gameObject)
//{
//    var area = gameObject.Polygon.Area();
//    var drag = CalculateDrag(gameObject);
//    var forceDueToDrag = CalculateForceDueToDrag(drag, area, gameObject.Velocity);
//    var accelleration = forceDueToDrag / gameObject.Mass;

//    accelleration.EnsureNoNaNs();

//    return accelleration;
//}

//private float CalculateDrag(IGameObject gameObject)
//    => 0.5f * AerodynamicsCalculator.CalculateCoefficientOfDrag(gameObject.Polygon, _environment.Medium, gameObject.Velocity) * gameObject.Polygon.Area() * gameObject.Velocity.LengthSquared();

//private Vector2 CalculateForceDueToDrag(float drag, float area, Vector2 velocity)
//{
//    var speed = velocity.Length();
//    var forceDueToDrag = -0.5f * drag * _environment.Medium.Density * area * speed * speed * velocity;

//    forceDueToDrag.EnsureNoNaNs();

//    return forceDueToDrag;
//}

//private Vector2 CalculateVelocity(Vector2 velocity, Vector2 acceleration, TimeSpan elapsed)
//{
//    var seconds = (float)elapsed.TotalSeconds;
//    var newVelocity = velocity + acceleration * seconds;

//    newVelocity.EnsureNoNaNs();

//    return newVelocity;
//}

//private Vector2 CalculatePosition(Vector2 position, Vector2 velocity, TimeSpan elapsed)
//{
//    var seconds = (float)elapsed.TotalSeconds;
//    var newPosition = position + velocity * seconds;

//    newPosition.EnsureNoNaNs();

//    return newPosition;
//}
//}