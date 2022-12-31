using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core;

public class Physics : IPhysics
{
    private readonly EnvironmentalFactors _environment;

    public Physics(EnvironmentalFactors environment)
    {
        _environment = environment;
    }

    public void Update(IGameObject gameObject, TimeSpan elapsed)
    {
        var acceleration = CalculateAcceleration(gameObject);
        var velocity = CalculateVelocity(gameObject.Velocity, acceleration, elapsed);
        var position = CalculatePosition(gameObject.Position, velocity, elapsed);

        gameObject.Velocity = velocity;
        gameObject.Position = position;
    }

    private Vector2 CalculateAcceleration(IGameObject gameObject)
    {
        var accelerationDueToGravity = new Vector2(0, _environment.Gravity);
        var accelerationDueToMedium = CalculateAccelerationDueToMedium(gameObject);
        return accelerationDueToGravity + accelerationDueToMedium;
    }

    private Vector2 CalculateAccelerationDueToMedium(IGameObject gameObject)
    {
        var area = gameObject.Polygon.Area();
        var drag = CalculateDrag(gameObject);
        var forceDueToDrag = CalculateForceDueToDrag(drag, area, gameObject.Velocity);
        return forceDueToDrag / gameObject.Mass;
    }

    private float CalculateDrag(IGameObject gameObject)
    {
        var dragCoefficient = AerodynamicsCalculator.CalculateCoefficientOfDrag(gameObject.Polygon, _environment.Medium, gameObject.Velocity);
        var area = gameObject.Polygon.Area();
        var velocitySquared = gameObject.Velocity.LengthSquared();
        return 0.5f * dragCoefficient * area * velocitySquared;
    }

    private Vector2 CalculateForceDueToDrag(float drag, float area, Vector2 velocity)
    {
        var speed = velocity.Length();
        var forceDueToDrag = -0.5f * drag * _environment.Medium.Density * area * speed * speed * velocity;
        return forceDueToDrag;
    }

    private Vector2 CalculateVelocity(Vector2 velocity, Vector2 acceleration, TimeSpan elapsed)
    {
        var seconds = (float)elapsed.TotalSeconds;
        var newVelocity = velocity + acceleration * seconds;
        return newVelocity;
    }

    private Vector2 CalculatePosition(Vector2 position, Vector2 velocity, TimeSpan elapsed)
    {
        var seconds = (float)elapsed.TotalSeconds;
        var newPosition = position + velocity * seconds;
        return newPosition;
    }

    public void UpdateOld(IGameObject gameObject, TimeSpan elapsed)
    {
        // Calculate the acceleration due to gravity
        var gravityAcceleration = new Vector2(0, _environment.Gravity);

        // Calculate the air resistance force
        var velocity = gameObject.Velocity;
        var density = _environment.Medium.Density;
        var surfaceArea = gameObject.Polygon.Area();
        //var aerodynamics = Aerodynamics.Calculate(gameObject.Polygon, _environment.Medium, velocity);
        var dragCoefficient =
            AerodynamicsCalculator.CalculateCoefficientOfDrag(gameObject.Polygon, _environment.Medium, velocity);
        var airResistance = 0.5f * density * velocity.LengthSquared() * surfaceArea * dragCoefficient;
        var airResistanceVector = new Vector2(-velocity.X * airResistance, -velocity.Y * airResistance);

        // Calculate the total force acting on the object
        var force = airResistanceVector + gravityAcceleration * gameObject.Mass;

        // Calculate the acceleration of the object
        var acceleration = force / gameObject.Mass;

        // Update the velocity of the object based on the acceleration
        if (!float.IsNaN(acceleration.X) && !float.IsNaN(acceleration.Y))
        {
            gameObject.Velocity += acceleration * (float)elapsed.TotalSeconds;
        }

        // Update the position of the object based on the velocity
        if (!float.IsNaN(gameObject.Velocity.X) && !float.IsNaN(gameObject.Velocity.Y))
        {
            gameObject.Position += gameObject.Velocity * (float)elapsed.TotalSeconds;
        }

        var optimalDirection = gameObject.Polygon.OptimalFlowDirection();
        var something = DirectionsCalculator.Vector2ToHeadingAndSpeed(optimalDirection);
        gameObject.Polygon = gameObject.Polygon.RotateToDirection(something.heading);
    }
}