using System.Numerics;

namespace Frank.GameEngine.Primitives;

public static class TransformExtensions
{
    public static void Translate(this Transform transform, Vector3 direction)
    {
        transform.Position += direction;
    }

    public static void Rotate(this Transform transform, Vector3 radians)
    {
        transform.Rotation *= Quaternion.CreateFromYawPitchRoll(radians.X, radians.Y, radians.Z);
    }

    public static void ScaleBy(this Transform transform, float scale)
    {
        transform.Scale *= scale;
    }
}