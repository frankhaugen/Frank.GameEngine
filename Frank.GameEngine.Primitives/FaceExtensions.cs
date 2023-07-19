using System.Numerics;

namespace Frank.GameEngine.Primitives;

public static class FaceExtensions
{
    public static Vector3 GetNormal(this Face face)
    {
        var v1 = face.B - face.A;
        var v2 = face.C - face.A;
        var normal = Vector3.Cross(v1, v2);
        return Vector3.Normalize(normal);
    }
}