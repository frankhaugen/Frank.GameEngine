using System.Numerics;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Physics;

/// <summary>
///     A force that applies gravity to a game object. Gravity is a constant force that is always applied to a game object.
/// </summary>
public class GravityForce : IForce
{
    /// <summary>
    ///     Calculates the force (velocity change) to apply to something
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="deltaTime"></param>
    /// <returns></returns>
    public Vector3? Calculate(GameObject gameObject, TimeSpan deltaTime)
    {
        var gravity = new Vector3(0, Constants.TerrestrialConstants.EarthGravity, 0) * deltaTime.Seconds;
        return gravity;
    }
}