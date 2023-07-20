using System.Numerics;

namespace Frank.GameEngine.Primitives;

/// <summary>
/// Extensions for Vector3 clas <see href="https://docs.microsoft.com/en-us/dotnet/api/system.numerics.vector3?view=net-7.0">Vector3</see>.
/// </summary>
public static class Vector3Extensions
{
    /// <summary>
    /// Gets the magnitude of the vector. The magnitude is the absolute value of the vector. It is the distance from the origin.
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    public static Vector3 GetMagnitude(this Vector3 vector) => new(MathF.Abs(vector.X), MathF.Abs(vector.Y), MathF.Abs(vector.Z));
    
    /// <summary>
    /// Gets the velocity of the vector. The velocity is the square root of the sum of the squares of the components.
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    public static float GetVelocity(this Vector3 vector) => MathF.Sqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z);
    
    /// <summary>
    /// Gets the normal of the vector. The normal is the vector divided by the velocity.
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    public static Vector3 GetNormal(this Vector3 vector) => Vector3.Normalize(vector);
    
    /// <summary>
    /// Gets the distance between the vector and the other vector.
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static float GetDistance(this Vector3 vector, Vector3 other) => Vector3.Distance(vector, other);
    
    /// <summary>
    /// Gets the direction from the vector to the other vector.
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Vector3 GetDirection(this Vector3 vector, Vector3 other) => Vector3.Normalize(other - vector);
    
    /// <summary>
    /// Gets the midpoint between the vector and the other vector.
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Vector3 GetMidpoint(this Vector3 vector, Vector3 other) => new((vector.X + other.X) / 2, (vector.Y + other.Y) / 2, (vector.Z + other.Z) / 2);
}