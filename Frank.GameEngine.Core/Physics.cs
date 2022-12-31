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
        var accelleration = forceDueToDrag / gameObject.Mass;

        accelleration.EnsureNoNaNs();

        return accelleration;
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

        forceDueToDrag.EnsureNoNaNs();
        return forceDueToDrag;
    }


    private Vector2 CalculateVelocity(Vector2 velocity, Vector2 acceleration, TimeSpan elapsed)
    {
        var seconds = (float)elapsed.TotalSeconds;
        var newVelocity = velocity + acceleration * seconds;

        newVelocity.EnsureNoNaNs();

        return newVelocity;
    }

    private Vector2 CalculatePosition(Vector2 position, Vector2 velocity, TimeSpan elapsed)
    {
        var seconds = (float)elapsed.TotalSeconds;
        var newPosition = position + velocity * seconds;

        newPosition.EnsureNoNaNs();

        return newPosition;
    }
}