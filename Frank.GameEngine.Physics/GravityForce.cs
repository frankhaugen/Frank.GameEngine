using Frank.GameEngine.Primitives;
using System.Numerics;

namespace Frank.GameEngine.Physics;

public class GravityForce : IForce
{
    public Vector3? Calculate(GameObject gameObject, TimeSpan deltaTime)
    {
        var gravity = new Vector3(0, Constants.TerrestrialConstants.EarthGravity, 0) * deltaTime.Seconds;
        return gravity;
    }
}