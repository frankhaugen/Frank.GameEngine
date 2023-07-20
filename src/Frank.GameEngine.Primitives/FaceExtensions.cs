using System.Numerics;

namespace Frank.GameEngine.Primitives;

public static class FaceExtensions
{
    /// <summary>
    /// Gets the normal of the face. A normal is a vector that is perpendicular to a surface. The normal of a face is the normal of the plane that the face is on.
    /// </summary>
    /// <param name="face"></param>
    /// <returns></returns>
    public static Vector3 GetNormal(this Face face)
    {
        var v1 = face.B - face.A;
        var v2 = face.C - face.A;
        var normal = Vector3.Cross(v1, v2);
        return Vector3.Normalize(normal);
    }
}