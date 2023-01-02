using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core;

public class Gravity : PhysicalEffect
{
    public Gravity(EnvironmentalFactors environment) : base(environment)
    {
    }

    public override Vector2 Calculate(IGameObject gameObject, TimeSpan elapsedTime)
    {
        var accelerationDueToGravity = _environment.Gravity;
        var gravitationalForce = gameObject.Mass * accelerationDueToGravity;
        var gravitationalAcceleration = gravitationalForce / gameObject.Mass;
        return Vector2.UnitY * gravitationalAcceleration * (float)elapsedTime.TotalSeconds;
    }
}