using System.Numerics;

namespace Frank.GameEngine.Primitives;

public static class Vector3Extensions
{
    public static Vector3 GetMagnitude(this Vector3 vector) => new(MathF.Abs(vector.X), MathF.Abs(vector.Y), MathF.Abs(vector.Z));
    
    public static float GetVelocity(this Vector3 vector) => MathF.Sqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z);
    
    public static Vector3 GetNormal(this Vector3 vector) => Vector3.Normalize(vector);
    
    public static float GetDistance(this Vector3 vector, Vector3 other) => Vector3.Distance(vector, other);
    
    public static Vector3 GetDirection(this Vector3 vector, Vector3 other) => Vector3.Normalize(other - vector);
    
    public static Vector3 GetMidpoint(this Vector3 vector, Vector3 other) => new((vector.X + other.X) / 2, (vector.Y + other.Y) / 2, (vector.Z + other.Z) / 2);
}