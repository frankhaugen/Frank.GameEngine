using System.Numerics;

namespace Frank.GameEngine.Physics;

/// <summary>
///     A force is something that can be applied to a game object to change its velocity.
/// </summary>
public static class ForceHelper
{
    private const double DegToRad = Math.PI / 180.0;

    /// <summary>
    ///     Gets the initial velocity of an object given a force, mass, and direction.
    /// </summary>
    /// <param name="force"></param>
    /// <param name="mass"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static Vector3 GetInitialVelocity(double force, float mass, Vector3 direction)
    {
        var initialSpeed = (float)(force / mass);
        direction = Vector3.Normalize(direction); // Normalize direction vector
        return direction * initialSpeed;
    }

    /// <summary>
    ///     Calculates the force (velocity change) to apply to something given a direction and magnitude.
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="magnitude"></param>
    /// <param name="deltaTime"></param>
    /// <returns></returns>
    public static Vector3 CalculateForce(Vector3 direction, float magnitude, TimeSpan deltaTime)
    {
        return direction * magnitude * deltaTime.Seconds;
    }

    /// <summary>
    ///     Calculates the force (velocity change) to apply to something given a direction and magnitude.
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="force"></param>
    /// <returns></returns>
    public static Vector3 ApplyForce(Vector3 vector, Vector3 force)
    {
        return vector + force;
    }

    /// <summary>
    ///     Calculates the force (velocity change) to apply to something given a direction and magnitude.
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="force"></param>
    /// <param name="mass"></param>
    /// <returns></returns>
    public static Vector3 ApplyForce(Vector3 vector, Vector3 force, float mass)
    {
        return vector + force / mass;
    }

    /// <summary>
    ///     Calculates the force (velocity change) to apply to something given a direction and magnitude.
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="force"></param>
    /// <param name="mass"></param>
    /// <param name="deltaTime"></param>
    /// <returns></returns>
    public static Vector3 ApplyForce(Vector3 vector, Vector3 force, float mass, TimeSpan deltaTime)
    {
        return vector + force / mass * deltaTime.Seconds;
    }
}