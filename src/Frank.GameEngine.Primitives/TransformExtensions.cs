using System.Numerics;

namespace Frank.GameEngine.Primitives;

public static class TransformExtensions
{
    /// <summary>
    ///     Moves the transform in the specified direction.
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="direction"></param>
    public static void Translate(this Transform transform, Vector3 direction)
    {
        transform.Position += direction;
    }

    /// <summary>
    ///     Moves the transform to the specified position. This will override the current position.
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="position"></param>
    public static void MoveTo(this Transform transform, Vector3 position)
    {
        transform.Position = position;
    }

    /// <summary>
    ///     Rotates the transform by the specified radians.
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="radians"></param>
    public static void Rotate(this Transform transform, Vector3 radians)
    {
        transform.Rotation *= Quaternion.CreateFromYawPitchRoll(radians.X, radians.Y, radians.Z);
    }

    /// <summary>
    ///     Rotates the transform to the specified radians.
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="radians"></param>
    public static void RotateTo(this Transform transform, Vector3 radians)
    {
        transform.Rotation = Quaternion.CreateFromYawPitchRoll(radians.X, radians.Y, radians.Z);
    }

    /// <summary>
    ///     Scales the transform by the specified amount. If the amount is less than 1, the transform will shrink. If the
    ///     amount is greater than 1, the transform will grow.
    ///     It will not override the scale, it will multiply it.
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="scale"></param>
    public static void ScaleBy(this Transform transform, float scale)
    {
        transform.Scale *= scale;
    }

    /// <summary>
    ///     Scales the transform to the specified amount. This will override the current scale.
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="scale"></param>
    public static void ScaleTo(this Transform transform, float scale)
    {
        transform.Scale = scale;
    }
}